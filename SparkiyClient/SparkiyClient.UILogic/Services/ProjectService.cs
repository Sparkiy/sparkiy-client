using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using SparkiyClient.Common.Helpers;
using SparkiyClient.UILogic.Models;

namespace SparkiyClient.UILogic.Services
{
	public class ProjectService : IProjectService
	{
		// Project 
		private const string ProjectFileExtension = ".sparkiyproj";
		private const string ProjectScreenshotsPath = "Screenshots";
		private const string ProjectAssetsPath = "Assets";
		private const string ProjectFilesPath = "Files";
		private const string ProjectFilesScriptExtension = ".luascript";
		private const string ProjectFilesClassExtension = ".luaclass";

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
			// Retrieve all dirty projects
			var dirtyProjects = this.projects.Where(p => p.IsDirty || (p.Files?.Result?.Any(s => s.IsDirty) ?? false));
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
				var dirtyFiles = dirtyProject.Files?.Result?.Where(s => s.IsDirty);
				if (dirtyFiles != null)
					foreach (var script in dirtyFiles)
					{
						await SaveFileSafeAsync(
							scriptsFolder, 
							script.Name, 
							script is Script ? ProjectFilesScriptExtension : ProjectFilesClassExtension, 
							script.Code ?? String.Empty);
						script.MarkAsClean();
					}

				dirtyProject.MarkAsClean();
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

		public async Task<IEnumerable<CodeFile>> GetFilesAsync(Project project)
		{
			var projectFolder = await this.storageService.WorkspaceFolder.GetFolderAsync(project.Name);
			var filesFolder = await projectFolder.GetFolderAsync(ProjectFilesPath);

			var scripts = (await this.QueryFiles<Script>(filesFolder, ProjectFilesScriptExtension)).ToList();
			var classes = (await this.QueryFiles<Class>(filesFolder, ProjectFilesClassExtension)).ToList();

			scripts.AsParallel().ForAll(s => s.MarkAsClean());
			classes.AsParallel().ForAll(c => c.MarkAsClean());

			var codeFiles = Enumerable.Union<CodeFile>(scripts, classes);

			return new ObservableCollection<CodeFile>(codeFiles);
		}

		private async Task<IEnumerable<T>> QueryFiles<T>(StorageFolder folder, string extension)
			where T : CodeFile, new()
		{
			var queryOptions = new QueryOptions(CommonFileQuery.OrderByName, new List<string>() {extension});
			var queryResult = folder.CreateFileQueryWithOptions(queryOptions);
			var files = await queryResult.GetFilesAsync();
			var codeFiles = files.Select(f =>
				new T
				{
					Name = f.DisplayName,
					Path = f.Path
				});

			return codeFiles;
		}
	}
}