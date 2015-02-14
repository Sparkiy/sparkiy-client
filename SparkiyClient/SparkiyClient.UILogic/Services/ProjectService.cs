using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using MetroLog;
using SparkiyClient.Common.Helpers;
using SparkiyClient.UILogic.Models;

namespace SparkiyClient.UILogic.Services
{
	public class ProjectService : IProjectService
	{
		private ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<ProjectService>();

		// Project 
		private const string ProjectFileExtension = ".sparkiyproj.xml";
		private const string ProjectScreenshotsPath = "Screenshots";
		private const string ProjectAssetsPath = "Assets";
		private const string ProjectFilesPath = "Files";
		private const string ProjectFilesScriptExtension = ".script.lua";
		private const string ProjectFilesClassExtension = ".class.lua";

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

		public async Task ImportAsset(Project project, StorageFile file) 
		{
			Log.Debug("Importing asset from: {0}", file.Path);

			var projectFolder = await this.storageService.WorkspaceFolder.GetFolderAsync(project.Name);
			var assetsFolder = await projectFolder.GetFolderAsync(ProjectAssetsPath);

			var localCopy = await file.CopyAsync(assetsFolder, file.Name, NameCollisionOption.ReplaceExisting);

			var asset = this.ResolveAsset(localCopy);
			if (asset != null)
				project.Assets.Add(asset);

			Log.Debug("Asset \"{0}\" imported.", asset != null ? asset.Name : "<INVALID TYPE>");
		}

		private Asset ResolveAsset(StorageFile file)
		{
			if (file.ContentType == "image/png")
				return new ImageAsset()
				{
					Name = file.DisplayName,
					Path = file.Path
				};

			Log.Debug("Couldn't resolve asset type. Returning null.");
			return null;
		}

		public async Task<IEnumerable<Project>> GetAvailableProjectsAsync()
		{
			Log.Debug("Retrieving projects...");

			// Retrieve projects
			var projectFiles = await this.GetProjectsFilesAsync();

			// Load projects files
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

			Log.Debug("{0} projects found:{1}", 
				loadedProjects.Count,
				loadedProjects.Aggregate(String.Empty, (s, p) => s + "\n\t - " + p.Name ));

			return loadedProjects;
		}

		public async Task SaveAsync()
		{
			Log.Debug("Saving projects...");

			// Retrieve all dirty projects
			var dirtyProjects = this.projects.Where(p => p.IsDirty || (p.Files != null ? p.Files.Any(s => s.IsDirty) : false));
			foreach (var dirtyProject in dirtyProjects)
			{
				// Create project folder if it doesnt exist or retrieve one from project files path
				var projectFolder = await this.storageService.WorkspaceFolder.CreateFolderAsync(dirtyProject.Name, CreationCollisionOption.OpenIfExists);
				
				// Ensure folders
				await projectFolder.EnsureFolderExistsAsync(ProjectScreenshotsPath);
				await projectFolder.EnsureFolderExistsAsync(ProjectAssetsPath);
				await projectFolder.EnsureFolderExistsAsync(ProjectFilesPath);

				// Save project file if it changed
				if (dirtyProject.IsDirty)
					await SaveProjectFileSafeAsync(projectFolder, dirtyProject);

				// Save all dirty files
				var scriptsFolder = await projectFolder.GetFolderAsync(ProjectFilesPath);
				if (dirtyProject.Files != null)
				{
					var dirtyFiles = dirtyProject.Files.Where(s => s.IsDirty);
					if (dirtyFiles != null)
					{
						foreach (var script in dirtyFiles)
						{
							await SaveFileSafeAsync(
								scriptsFolder,
								script.Name,
								script is Script ? ProjectFilesScriptExtension : ProjectFilesClassExtension,
								script.Code ?? String.Empty);
							script.MarkAsClean();
						}
					}
				}

				dirtyProject.MarkAsClean();

				Log.Debug("Saved project \"{0}\"", dirtyProject.Name);
			}
		}

		private async Task SaveProjectFileSafeAsync(StorageFolder projectFolder, Project project)
		{
			await this.storageService.SaveFileSafeAsync(
				projectFolder,
				project.Name + ProjectFileExtension,
				async file =>
				{
					var serializer = new DataContractSerializer(typeof(Project));
					using (var fileStream = await file.OpenStreamForWriteAsync())
						serializer.WriteObject(fileStream, project);
				});
		}

		private async Task SaveFileSafeAsync(StorageFolder scriptFolder, string name, string extension, string code)
		{
			await this.storageService.SaveFileSafeAsync(
				scriptFolder,
				name + extension,
				async file => await FileIO.WriteTextAsync(file, code));
		}

		private async Task<Dictionary<string, StorageFile>> GetProjectsFilesAsync()
		{
			// Get project folders
			var folders = await this.storageService.WorkspaceFolder.GetFoldersAsync();

			// Get project files
			var projectFiles = new Dictionary<string, StorageFile>();
			foreach (var projectFolder in folders)
			{
				// Retrieve project files
				var projectFileQueryOptions = new QueryOptions(CommonFileQuery.OrderByName, new List<string> { ".xml" });
				var projectFileResult = projectFolder.CreateFileQueryWithOptions(projectFileQueryOptions);
				var projectFileResultsFiles = await projectFileResult.GetFilesAsync();
				var projectFileResultsFilesExtensionFilter = projectFileResultsFiles.Where(f => f.Name.EndsWith(ProjectFileExtension));
				var projectFileKVPs = projectFileResultsFilesExtensionFilter.Select(f => new KeyValuePair<string, StorageFile>(f.DisplayName, f));
				projectFiles.AddRange(projectFileKVPs);
			}

			return projectFiles;
		}

		public async Task<IEnumerable<CodeFile>> GetFilesAsync(Project project)
		{
			var projectFolder = await this.storageService.WorkspaceFolder.GetFolderAsync(project.Name);
            await projectFolder.EnsureFolderExistsAsync(ProjectFilesPath);
            var filesFolder = await projectFolder.GetFolderAsync(ProjectFilesPath);

			var scripts = (await this.QueryFiles<Script>(filesFolder, ProjectFilesScriptExtension)).ToList();
			var classes = (await this.QueryFiles<Class>(filesFolder, ProjectFilesClassExtension)).ToList();

			scripts.AsParallel().ForAll(s => s.MarkAsClean());
			classes.AsParallel().ForAll(c => c.MarkAsClean());

			var codeFiles = Enumerable.Union<CodeFile>(scripts, classes);

			return new ObservableCollection<CodeFile>(codeFiles);
		}

		public async Task<IEnumerable<Asset>> GetAssetsAsync(Project project)
		{
            var projectFolder = await this.storageService.WorkspaceFolder.GetFolderAsync(project.Name);
            await projectFolder.EnsureFolderExistsAsync(ProjectAssetsPath);
            var assetsFolder = await projectFolder.GetFolderAsync(ProjectAssetsPath);

			var assetFiles = await assetsFolder.GetFilesAsync();

			var assets = assetFiles.Select(this.ResolveAsset);

			return new ObservableCollection<Asset>(assets);
		}

		private async Task<IEnumerable<T>> QueryFiles<T>(StorageFolder folder, string extension)
			where T : CodeFile, new()
		{
			var queryOptions = new QueryOptions(CommonFileQuery.OrderByName, new List<string>() {".lua"});
			var queryResult = folder.CreateFileQueryWithOptions(queryOptions);
			var files = await queryResult.GetFilesAsync();
			var filesExtensionFilter = files.Where(f => f.Name.EndsWith(extension));
			var codeFiles = filesExtensionFilter.Select(f =>
				new T
				{
					Name = f.Name.Replace(extension, String.Empty),
					Path = f.Path
				});

			return codeFiles;
		}
	}
}