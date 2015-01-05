using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using MetroLog;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;
using INavigationService = SparkiyClient.UILogic.Services.INavigationService;

namespace SparkiyClient.UILogic.ViewModels
{
	[ComVisible(false)]
	public interface IMainPageViewModel : IViewModelBase
	{
		bool RequiresWorkspaceInitialization { get; }

		ObservableCollection<Project> Projects { get; } 

		RelayCommand InitializeWorkspaceCommand { get; }

		RelayCommand<Project> ProjectSelectedCommand { get; } 

		RelayCommand NewProjectCommand { get; }
    }

    [ComVisible(false)]
    public class MainPageViewModel : ExtendedViewModel, IMainPageViewModel
    {
	    private static ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<MainPageViewModel>();

		private readonly INavigationService navigationService;
		private readonly IAlertMessageService alertMessageService;
	    private readonly IStorageService storageService;
	    private readonly IProjectService projectService;


	    public MainPageViewModel(INavigationService navigationService, IAlertMessageService alertMessageService, IStorageService storageService, IProjectService projectService)
	    {
		    this.navigationService = navigationService;
		    this.alertMessageService = alertMessageService;
		    this.storageService = storageService;
		    this.projectService = projectService;

		    this.RequiresWorkspaceInitialization = this.storageService.RequiresHardStorageInitialization();

			this.InitializeWorkspaceCommand = new RelayCommand(this.InitializeWorkspaceCommandExecuteAsync);
			this.ProjectSelectedCommand = new RelayCommand<Project>(this.ProjectSelectedCommandExecute);
			this.NewProjectCommand = new RelayCommand(this.NewProjectCommandExecute);
	    }


	    public override async void OnNavigatedTo(NavigationEventArgs e)
	    {
		    base.OnNavigatedTo(e);

		    if (this.LoadingData)
		    {
			    await this.LoadProjectsAsync();
			    this.LoadingData = false;
		    }
	    }

	    private async Task LoadProjectsAsync()
	    {
			Log.Debug("Loading project...");
		    if (this.storageService.RequiresHardStorageInitialization())
		    {
			    Log.Warn("Can't load projects. Storage isn't initialized.");
			    return;
		    }

		    // Load available projects
		    foreach (var project in await this.projectService.GetAvailableProjectsAsync())
			    this.Projects.Add(project);
	    }

	    private void NewProjectCommandExecute()
	    {
			this.navigationService.NavigateTo("CreateProjectPage");
		}

	    protected void ProjectSelectedCommandExecute(Project project)
	    {
		    this.navigationService.NavigateTo("ProjectPage", project);
	    }

	    protected async void InitializeWorkspaceCommandExecuteAsync()
	    {
		    await this.storageService.InitializeStorageAsync();
		    this.RequiresWorkspaceInitialization = this.storageService.RequiresHardStorageInitialization();
		    await this.LoadProjectsAsync();
	    }

	    public bool LoadingData
		{
			get { return this.GetProperty<bool>(defaultValue: true); }
			protected set { this.SetProperty(value); }
		}

		public bool RequiresWorkspaceInitialization
		{
			get { return this.GetProperty<bool>(defaultValue: true); }
			protected set { this.SetProperty(value); }
		}

		public RelayCommand InitializeWorkspaceCommand { get; }

		public RelayCommand<Project> ProjectSelectedCommand { get; }

		public ObservableCollection<Project> Projects { get; } = new ObservableCollection<Project>();

		public RelayCommand NewProjectCommand { get; }
	}

	[ComVisible(false)]
	public class MainPageViewModelDesignTime : MainPageViewModel
	{
		public MainPageViewModelDesignTime() : base(null, null, new StorageService(), new ProjectService(new StorageService()))
		{
			this.Projects.Add(new Project() { Name = "Project One" });
			this.Projects.Add(new Project() { Name = "Project Two" });
			this.Projects.Add(new Project() { Name = "Project Three" });
			this.Projects.Add(new Project() { Name = "Project Four" });
			this.Projects.Add(new Project() { Name = "Project Five" });
		}
	}
}
