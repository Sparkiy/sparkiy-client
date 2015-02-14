using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;
using INavigationService = SparkiyClient.UILogic.Services.INavigationService;

namespace SparkiyClient.UILogic.ViewModels
{
	public interface IPlayPageViewModel
	{
		void AssignProjectPlayEngineManager(IProjectPlayEngineManagement projectPlayEngineManagement);

		void AssignProjectPlayStateManager(IProjectPlayStateManagment projectPlayStateManagment);

		RelayCommand StopCommand { get; }
	}

	public class PlayPageViewModel : ExtendedViewModel, IPlayPageViewModel
	{
		private readonly IProjectService projectService;
		private readonly INavigationService navigationService;
		private IProjectPlayStateManagment projectPlayStateManager;
		private IProjectPlayEngineManagement projectPlayEngineManager;
		private Project project;


		public PlayPageViewModel(IProjectService projectService, INavigationService navigationService)
		{
			this.projectService = projectService;
			this.navigationService = navigationService;

			this.StopCommand = new RelayCommand(this.StopCommandExecute);
		}


		private void StopCommandExecute()
		{
			this.projectPlayStateManager.StopProject();
			this.navigationService.GoBack();
		}

		public async override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			// Retrieve passed project
			var project = e.Parameter as Project;
			if (project == null)
				throw new NullReferenceException("Passed data is not in expected format.");

			// Load project
			await project.LoadAsync(this.projectService);

			// Assign the project to the engine
			await this.AssignProjectAsync(project);
		}

		public void AssignProjectPlayStateManager(IProjectPlayStateManagment projectPlayStateManager)
		{
			this.projectPlayStateManager = projectPlayStateManager;
		}

		public void AssignProjectPlayEngineManager(IProjectPlayEngineManagement projectPlayEngineManager)
		{
			this.projectPlayEngineManager = projectPlayEngineManager;
		}

		private async Task AssignProjectAsync(Project project)
		{
			this.project = project;

			// Assign project to the engine
			this.projectPlayEngineManager.AssignProject(this.project);

			// Run the project
			this.projectPlayStateManager.PlayProject();
		}

		public RelayCommand StopCommand { get; private set; }
	}

	public sealed class PlayPageViewModelDesignTime : PlayPageViewModel
	{
		public PlayPageViewModelDesignTime() : base(null, null)
		{
		}
	}
}