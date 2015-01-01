using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;

namespace SparkiyClient.UILogic.ViewModels
{
	public interface IPlayPageViewModel
	{
		void AssignProjectPlayEngineManager(IProjectPlayEngineManagement projectPlayEngineManagement);

		void AssignProjectPlayStateManager(IProjectPlayStateManagment projectPlayStateManagment);
	}

	public class PlayPageViewModel : ExtendedViewModel, IPlayPageViewModel
	{
		private readonly IProjectService projectService;
		private IProjectPlayStateManagment projectPlayStateManager;
		private IProjectPlayEngineManagement projectPlayEngineManager;
		private Project project;


		public PlayPageViewModel(IProjectService projectService)
		{
			this.projectService = projectService;
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
	}

	public sealed class PlayPageViewModelDesignTime : PlayPageViewModel
	{
		public PlayPageViewModelDesignTime() : base(null)
		{
		}
	}
}