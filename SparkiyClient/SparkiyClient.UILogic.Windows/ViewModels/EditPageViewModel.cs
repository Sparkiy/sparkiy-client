using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using MetroLog;
using Nito.AsyncEx;
using SparkiyClient.Common;
using SparkiyClient.Common.Controls;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;

namespace SparkiyClient.UILogic.Windows.ViewModels
{
	public interface INewFileViewModel
	{
		string Name { get; set; }

		int TypeIndex { get; set; }
	}

	public class NewFileViewModel : ExtendedViewModel, INewFileViewModel
	{
		public string Name
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		public int TypeIndex
		{
			get { return this.GetProperty<int>(); }
			set { this.SetProperty(value); }
		}
	}

	public interface IEditPageViewModel
	{
		void AssignEditor(ICodeEditor editor);

		Project Project { get; }

		CodeFile SelectedFile { get; set; }
		
		RelayCommand AddNewFileCommand { get; }

		INewFileViewModel NewFileViewModel { get; }

		RelayCommand AddNewAssetCommand { get; }

		RelayCommand NavigateToHomeCommand { get; }

		RelayCommand NavigateToProjectCommand { get; }

		RelayCommand DebugProjectCommand { get; }

		RelayCommand PlayProjectCommand { get; }
	}

	public class EditPageViewModel : ExtendedViewModel, IEditPageViewModel
	{
		private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<EditPageViewModel>();
		private readonly IProjectService projectService;
		private readonly INavigationService navigationService;

		private ICodeEditor editor;


		public EditPageViewModel(IProjectService projectService, INavigationService navigationService)
		{
			this.projectService = projectService;
			this.navigationService = navigationService;

			this.AddNewFileCommand = new RelayCommand(this.AddNewFileCommandExecuteAsync);
			this.AddNewAssetCommand = new RelayCommand(this.AddNewAssetCommandExecuteAsync);
			this.NavigateToHomeCommand = new RelayCommand(this.NavigateToHomeCommandExecuteAsync);
			this.NavigateToProjectCommand = new RelayCommand(this.NavigateToProjectCommandExecuteAsync);
			this.DebugProjectCommand = new RelayCommand(this.DebugProjectCommandExecuteAsync);
			this.PlayProjectCommand = new RelayCommand(this.PlayProjectCommandExecuteAsync);
		}


		public async override void OnNavigatedTo(NavigationEventArgs e)
		{
			var project = e.Parameter as Project;
			if (project == null)
				throw new InvalidDataException("Passed project object is not valid or null.");

			this.Project = project;

			// Load scripts and assets list
			await this.Project.LoadAsync(this.projectService);

			// Select first script
			this.SelectedFile = this.Project.Files.FirstOrDefault();

			base.OnNavigatedTo(e);
		}

		private async void NavigateToHomeCommandExecuteAsync()
		{
			await this.SaveChangesAsync();
			this.navigationService.NavigateTo("MainPage");
		}

		private async void NavigateToProjectCommandExecuteAsync()
		{
			await this.SaveChangesAsync();
			this.navigationService.NavigateTo("ProjectPage", this.Project);
		}

		private async void DebugProjectCommandExecuteAsync()
		{
			await this.SaveChangesAsync();
			this.navigationService.NavigateTo("DebugPage", this.Project);
		}

		private async void PlayProjectCommandExecuteAsync()
		{
			await this.SaveChangesAsync();
			this.navigationService.NavigateTo("PlayPage", this.Project);
		}


		private async void AddNewFileCommandExecuteAsync()
		{
			if (this.NewFileViewModel == null)
			{
				this.NewFileViewModel = new NewFileViewModel();
			}
			else
			{
				if (this.NewFileViewModel.TypeIndex == 1)
					this.Project.Files.Add(new Class {Name = this.NewFileViewModel.Name});
				else this.Project.Files.Add(new Script {Name = this.NewFileViewModel.Name});
				await this.projectService.SaveAsync();

				this.NewFileViewModel = null;
			}
		}

		private async void AddNewAssetCommandExecuteAsync()
		{
			// Create picker
			var picker = new FileOpenPicker()
			{
				SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
				ViewMode = PickerViewMode.Thumbnail
			};
			picker.FileTypeFilter.Add(".png");

			// Pick files
			var selectedFiles = await picker.PickMultipleFilesAsync();
			if (!selectedFiles.Any())
				return;

			// Import assets
			foreach (var selectedFile in selectedFiles)
				await this.projectService.ImportAsset(this.Project, selectedFile);

			var img = new ImageAsset();
			
		}

		public void AssignEditor(ICodeEditor editor)
		{
			this.editor = editor;
			this.editor.OnCodeChanged += (sender, args) =>
			{
				if (this.SelectedFile != null)
					this.SelectedFile.Code = this.editor.Code;
			};
		}

		private async Task SaveChangesAsync()
		{
			SelectedFile.Code = this.editor.Code;
			await this.projectService.SaveAsync();
		}


		public Project Project
		{
			get { return this.GetProperty<Project>(); }
			protected set { this.SetProperty(value); }
		}

		public CodeFile SelectedFile
		{
			get { return this.GetProperty<CodeFile>(); }
			set
			{
				if (this.SelectedFile == value)
					return;

				if (this.SelectedFile != null && this.editor?.Code != null)
				{
					Log.Debug("Assigning code from editor to file \"{0}\"", this.SelectedFile.Name);
					this.SelectedFile.Code = this.editor.Code ?? String.Empty;
				}

				this.SetProperty(value);

				if (this.SelectedFile != null && editor != null)
				{
					Log.Debug("Assigning code from file \"{0}\" to editor", this.SelectedFile.Name);
					this.editor.Code = this.SelectedFile.Code ?? String.Empty;
				}
			}
		}

		public RelayCommand AddNewFileCommand { get; }

		public INewFileViewModel NewFileViewModel
		{
			get { return this.GetProperty<INewFileViewModel>(); }
			protected set { this.SetProperty(value); }
		}

		public RelayCommand AddNewAssetCommand { get; }

		public RelayCommand NavigateToHomeCommand { get; }

		public RelayCommand NavigateToProjectCommand { get; }

		public RelayCommand DebugProjectCommand { get; }

		public RelayCommand PlayProjectCommand { get; }
	}

	public sealed class EditPageViewModelDesignTime : EditPageViewModel
	{
		public EditPageViewModelDesignTime() : base(null, null)
		{
			this.Project = new Project()
			{
				Author = "Aleksandar Toplek",
				Description = "Sample description",
				Name = "Sample project",
				Files = new ObservableCollection<CodeFile>()
				{
					new Script {Code = "Sample code", Name = "main"},
					new Script {Code = "Sample code", Name = "script1"},
					new Script {Code = "Sample code", Name = "script2"},
					new Class {Code = "Class sample", Name = "class1"}
				}
			};
			this.SelectedFile = this.Project.Files.FirstOrDefault();
		}
	}
}