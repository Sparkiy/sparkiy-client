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
using SparkiyClient.UILogic.Models;

namespace SparkiyClient.UILogic.Services
{
	public interface IStorageService
	{
		Task InitializeStorageAsync();

		bool RequiresStorageInitialization();

		StorageFolder WorkspaceFolder { get; }
	}

	public interface IProjectService
	{
		Task<IEnumerable<Project>> GetAvailableProjectsAsync();

		void Save();

		Task<IEnumerable<Script>> GetScriptsAsync(Project project);
	}

	public class StorageService : IStorageService
	{
		private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<StorageService>();
		private const string WorkspaceFolderTokenKey = "WorkspaceFolderToken";
		private bool isInitialized;

		private StorageFolder workspaceFolder;


		public async Task InitializeStorageAsync()
		{
			Log.Debug("Initializing storage...");

			// Get workspace folder
			var workspaceFolderToken = ApplicationData.Current.LocalSettings.Values[WorkspaceFolderTokenKey];
			if (workspaceFolderToken == null)
			{
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

			this.isInitialized = true;
			Log.Debug("Storage successfully initialized.");
		}

		public bool RequiresStorageInitialization()
		{
			return ApplicationData.Current.LocalSettings.Values[WorkspaceFolderTokenKey] == null;
		}

		public StorageFolder WorkspaceFolder => this.workspaceFolder;

		/// <summary>
		/// Returns true if a folder contains another folder with the given name
		/// </summary>
		/// <param name="folder"></param>
		/// <param name="name"></param>
		/// <returns>True if the folder contains the folder with given name. False - otherwise</returns>
		public static async Task<bool> ContainsFolderAsync(StorageFolder folder, string name)
		{
			return (await folder.GetFoldersAsync()).Any(l => l.Name == name);
		}

		/// <summary>
		/// Ensures that a folder with given name exists in given folder
		/// </summary>
		/// <param name="folder"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static async Task EnsureFolderExistsAsync(StorageFolder folder, string name)
		{
			if (await ContainsFolderAsync(folder, name))
			{
				return;
			}

			await folder.CreateFolderAsync(name);
		}
	}

	public class ProjectService : IProjectService
	{
		// Project 
		private const string ProjectFileExtension = ".sparkiyproj";
		private const string ProjectScreenshotsPath = "Screenshots";
		private const string ProjectAssetsPath = "Assets";
		private const string ProjectFilesScriptsPath = "Scripts";

		private readonly IStorageService storageService;

		private readonly List<Project> projects = new List<Project>(); 


		public ProjectService(IStorageService storageService)
		{
			this.storageService = storageService;
		}


		public async Task CreateProjectAsync(Project project)
		{
			this.projects.Add(project);
        }

		public async Task<IEnumerable<Project>> GetAvailableProjectsAsync()
		{
			// Get project folders
			var folders = await this.storageService.WorkspaceFolder.GetFoldersAsync();
			
			// Get project files
			var projectFiles = new List<StorageFile>();
			foreach (var projectFolder in folders)
			{
				// Retrieve project files
				var projectFileQueryOptions = new QueryOptions(CommonFileQuery.OrderByName, new List<string> {ProjectFileExtension});
				var projectFileResult = projectFolder.CreateFileQueryWithOptions(projectFileQueryOptions);
				var projectFileResultsFiles = await projectFileResult.GetFilesAsync();
				projectFiles.AddRange(projectFileResultsFiles);
            }

			// Load project files
			var loadedProjects = new List<Project>();
			var serializer = new DataContractSerializer(typeof (Project));
			foreach (var projectFile in projectFiles)
				using (var projectFileStream = await projectFile.OpenStreamForReadAsync())
					loadedProjects.Add(serializer.ReadObject(projectFileStream) as Project);

			return loadedProjects;
		}

		public void Save()
		{
			foreach (var dirtyProject in this.projects.Where(p => p.IsDirty))
			{
				// Ensure folders

				dirtyProject.MarkAsClean();
			}
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
