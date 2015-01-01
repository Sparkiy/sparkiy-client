using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;
using SparkiyEngine.Bindings.Component.Engine;

namespace SparkiyClient.UILogic.Windows.ViewModels
{
	public interface IDebugPageViewModel
	{
		void AssignProjectPlayEngineManager(IProjectPlayEngineManagement projectPlayEngineManagement);

		void AssignProjectPlayStateManager(IProjectPlayStateManagment projectPlayStateManagment);

		Task AssignProjectAsync(Project project);

		ObservableCollection<EngineMessage> OutputMessages { get; }
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

			// Attach to on message created event and rigger first message read 
			this.projectPlayEngineManager.Engine.OnMessageCreated += EngineOnOnMessageCreated;
			this.EngineOnOnMessageCreated();

			this.projectPlayStateManager.PlayProject();
		}

		private void EngineOnOnMessageCreated(object sender = null)
		{
			foreach (var engineMessage in this.projectPlayEngineManager.Engine.GetMessages())
				this.OutputMessages.Add(engineMessage);
			this.projectPlayEngineManager.Engine.ClearMessages();
		}


		public ObservableCollection<EngineMessage> OutputMessages { get; } = new ObservableCollection<EngineMessage>();
	}

	public sealed class DebugPageViewModelDesignTime : DebugPageViewModel
	{
		public DebugPageViewModelDesignTime()
		{
			this.OutputMessages.Add(new EngineMessage() { Message = "Test1" });
			this.OutputMessages.Add(new EngineMessage() { Message = "Test2" });
			this.OutputMessages.Add(new EngineMessage() { Message = "Test3" });
		}
	}
}