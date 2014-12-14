using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Search;
using MetroLog;
using SparkiyClient.Common.Helpers;
using SparkiyClient.UILogic.Models;

namespace SparkiyClient.UILogic.Services
{
	public interface IStorageService
	{
		/// <summary>
		/// Initializes the storage.
		/// This will trigger hard initialization if access token is not available.
		/// </summary>
		Task InitializeStorageAsync();

		/// <summary>
		/// Requireses the hard storage initialization.
		/// Hard initialization means that user needs to interact with the component to initialize.
		/// </summary>
		/// <returns>Returns <c>true</c> if storage requires hard initialization; false otherwise.</returns>
		bool RequiresHardStorageInitialization();

		/// <summary>
		/// Gets the workspace folder.
		/// </summary>
		/// <value>
		/// The workspace folder.
		/// </value>
		StorageFolder WorkspaceFolder { get; }
	}

	public interface IProjectService
	{
		Task<IEnumerable<Project>> GetAvailableProjectsAsync();

		Task SaveAsync();

		Task<IEnumerable<Script>> GetScriptsAsync(Project project);

		Task CreateProjectAsync(Project project);
	}

	public class StorageService : IStorageService
	{
		private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<StorageService>();
		private const string WorkspaceFolderTokenKey = "WorkspaceFolderToken";

		private StorageFolder workspaceFolder;


		/// <summary>
		/// Initializes the storage.
		/// This will trigger hard initialization if access token is not available.
		/// </summary>
		public async Task InitializeStorageAsync()
		{
			Log.Debug("Initializing storage...");

			// Get workspace folder
			var workspaceFolderToken = ApplicationData.Current.LocalSettings.Values[WorkspaceFolderTokenKey];
			if (workspaceFolderToken == null)
			{
				Log.Debug("Storage initialization requires user interaction.");
				var picker = new FolderPicker
				{
					SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
					ViewMode = PickerViewMode.List
				};
				picker.FileTypeFilter.Add("*");
				var workspaceFolderSelected = await picker.PickSingleFolderAsync();
				if (workspaceFolderSelected == null)
				{
					Log.Warn("User canceled workspace folder selection.");
					return;
				}

				workspaceFolderToken = StorageApplicationPermissions.FutureAccessList.Add(workspaceFolderSelected);
				ApplicationData.Current.LocalSettings.Values[WorkspaceFolderTokenKey] = workspaceFolderToken;
			}
			this.workspaceFolder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(workspaceFolderToken.ToString());

			Log.Debug("Storage successfully initialized.");
		}

		/// <summary>
		/// Requireses the hard storage initialization.
		/// Hard initialization means that user needs to interact with the component to initialize.
		/// </summary>
		/// <returns>
		/// Returns <c>true</c> if storage requires hard initialization; false otherwise.
		/// </returns>
		public bool RequiresHardStorageInitialization()
		{
			return ApplicationData.Current.LocalSettings.Values[WorkspaceFolderTokenKey] == null;
		}

		/// <summary>
		/// The workspace folder
		/// </summary>
		public StorageFolder WorkspaceFolder => this.workspaceFolder;
	}

	public class ProjectService : IProjectService
	{
		// Project 
		private const string ProjectFileExtension = ".sparkiyproj";
		private const string ProjectScreenshotsPath = "Screenshots";
		private const string ProjectAssetsPath = "Assets";
		private const string ProjectFilesScriptsPath = "Scripts";
		private const string ProjectFilesScriptExtension = ".lua";
		private const string TempExtensions = ".temp";

		private readonly IStorageService storageService;

		private readonly List<Project> projects = new List<Project>(); 


		public ProjectService(IStorageService storageService)
		{
			this.storageService = storageService;
		}


		public async Task CreateProjectAsync(Project project)
		{
			this.projects.Add(project);
			await this.SaveAsync();
		}

		public async Task<IEnumerable<Project>> GetAvailableProjectsAsync()
		{
			// Retrieve projects
			var projectFiles = await this.GetProjectsAsync();

			// Load project files
			var loadedProjects = new List<Project>();
			var serializer = new DataContractSerializer(typeof (Project));
			foreach (var projectFile in projectFiles.Values)
				using (var projectFileStream = await projectFile.OpenStreamForReadAsync())
					loadedProjects.Add(serializer.ReadObject(projectFileStream) as Project);

			// Mark project as clean
			foreach (var loadedProject in loadedProjects)
				loadedProject.MarkAsClean();

			// Cache the projects
			this.projects.Clear();
			this.projects.AddRange(loadedProjects);

			return loadedProjects;
		}

		public async Task SaveAsync()
		{
			// Retrieve projects
			var projectFiles = await this.GetProjectsAsync();

			// Retrieve all dirty projects
			var dirtyProjects = this.projects.Where(p => p.IsDirty || (p.Scripts?.Result?.Any(s => s.IsDirty) ?? false));
            foreach (var dirtyProject in dirtyProjects)
			{
				// Create project folder if it doesnt exist or retrieve one from project files path
				var projectFolder = await this.storageService.WorkspaceFolder.CreateFolderAsync(dirtyProject.Name, CreationCollisionOption.OpenIfExists);
				
				// Ensure folders
				await projectFolder.EnsureFolderExistsAsync(ProjectScreenshotsPath);
				await projectFolder.EnsureFolderExistsAsync(ProjectAssetsPath);
				await projectFolder.EnsureFolderExistsAsync(ProjectFilesScriptsPath);

				// Save project file if it changed
				if (dirtyProject.IsDirty)
					await SaveProjectFileSafeAsync(projectFolder, dirtyProject);

				// Save all dirty scripts
				var scriptsFolder = await projectFolder.GetFolderAsync(ProjectFilesScriptsPath);
				var dirtyScripts = dirtyProject.Scripts?.Result?.Where(s => s.IsDirty);
				if (dirtyScripts != null)
					foreach (var script in dirtyScripts)
					{
						await SaveScriptSafeAsync(scriptsFolder, script.Name, script.Code.Result);
						script.MarkAsClean();
					}

				dirtyProject.MarkAsClean();
			}
		}

		private static async Task SaveProjectFileSafeAsync(StorageFolder projectFolder, Project project)
		{
			var projectFileName = project.Name + ProjectFileExtension;
			var tempProjectFileName = projectFileName + TempExtensions;

			// Project file serializer
			var serializer = new DataContractSerializer(typeof(Project));

			// Create temp project file and write serialized data
			var temporaryProjectFile = await projectFolder.CreateFileAsync(tempProjectFileName, CreationCollisionOption.ReplaceExisting);
			using (var projectFileTempStream = await temporaryProjectFile.OpenStreamForWriteAsync())
				serializer.WriteObject(projectFileTempStream, project);

			// Rename temp file
			await temporaryProjectFile.RenameAsync(projectFileName, NameCollisionOption.ReplaceExisting);
		}

		private static async Task SaveScriptSafeAsync(StorageFolder scriptFolder, string name, string code)
		{
			var scriptFileName = name + ProjectFilesScriptExtension;
            var tempScriptFileName = scriptFileName + TempExtensions;

			// Write to temp file
			var tempScriptFile = await scriptFolder.CreateFileAsync(tempScriptFileName, CreationCollisionOption.ReplaceExisting);
			await FileIO.WriteTextAsync(tempScriptFile, code);

			// Rename temp file
			await tempScriptFile.RenameAsync(scriptFileName, NameCollisionOption.ReplaceExisting);
		}

		private async Task<Dictionary<string, StorageFile>> GetProjectsAsync()
		{
			// Get project folders
			var folders = await this.storageService.WorkspaceFolder.GetFoldersAsync();

			// Get project files
			var projectFiles = new Dictionary<string, StorageFile>();
            foreach (var projectFolder in folders)
			{
				// Retrieve project files
				var projectFileQueryOptions = new QueryOptions(CommonFileQuery.OrderByName, new List<string> { ProjectFileExtension });
				var projectFileResult = projectFolder.CreateFileQueryWithOptions(projectFileQueryOptions);
				var projectFileResultsFiles = await projectFileResult.GetFilesAsync();
				var projectFileKVPs = projectFileResultsFiles.Select(f => new KeyValuePair<string, StorageFile>(f.DisplayName, f));
				projectFiles.AddRange(projectFileKVPs);
			}

			return projectFiles;
		}

		public async Task<IEnumerable<Script>> GetScriptsAsync(Project project)
		{
			var projectFolder = await this.storageService.WorkspaceFolder.GetFolderAsync(project.Name);
			var scriptsFolder = await projectFolder.GetFolderAsync(ProjectFilesScriptsPath);

			// Retrieve all scripts
			var scriptQueryOptions = new QueryOptions(CommonFileQuery.OrderByName, new List<string>() {".lua"});
			var scriptQueryResult = scriptsFolder.CreateFileQueryWithOptions(scriptQueryOptions);
			var scriptFiles = await scriptQueryResult.GetFilesAsync();
			var scripts = scriptFiles.Select(sf => new Script() {Name = sf.DisplayName, Path = sf.Path});

			return scripts;
		}
	}
}
