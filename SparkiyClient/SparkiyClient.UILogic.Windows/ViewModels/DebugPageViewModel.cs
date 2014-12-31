using System.Threading.Tasks;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;

namespace SparkiyClient.UILogic.Windows.ViewModels
{
	public interface IDebugPageViewModel
	{
		void AssignProjectPlayEngineManager(IProjectPlayEngineManagement projectPlayEngineManagement);

		void AssignProjectPlayStateManager(IProjectPlayStateManagment projectPlayStateManagment);

		Task AssignProjectAsync(Project project);
	}

	public class DebugPageViewModel : ExtendedViewModel, IDebugPageViewModel
	{
		private IProjectPlayEngineManagement projectPlayEngineManager;
		private IProjectPlayStateManagment projectPlayStateManager;
		private Project project;



		public void AssignProjectPlayEngineManager(IProjectPlayEngineManagement projectPlayEngineManager)
		{
			this.projectPlayEngineManager = projectPlayEngineManager;
		}

		public void AssignProjectPlayStateManager(IProjectPlayStateManagment projectPlayStateManager)
		{
			this.projectPlayStateManager = projectPlayStateManager;
		}

		public async Task AssignProjectAsync(Project project)
		{
			this.project = project;
			this.projectPlayEngineManager.AssignProject(this.project);

			this.projectPlayStateManager.PlayProject();
		}
	}

	public sealed class DebugPageViewModelDesignTime : DebugPageViewModel
	{
		
	}
}