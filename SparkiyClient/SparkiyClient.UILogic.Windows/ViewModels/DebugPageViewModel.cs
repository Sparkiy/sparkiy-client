using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
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

		IProjectPlayStateManagment ProjectPlayStateManagment { get; }
	}

	public class DebugPageViewModel : ExtendedViewModel, IDebugPageViewModel
	{
		private IProjectPlayEngineManagement projectPlayEngineManager;
		private IProjectPlayStateManagment projectPlayStateManager;
		private Project project;

		private DispatcherTimer messagesCheckTimer;


		public DebugPageViewModel()
		{
			this.messagesCheckTimer = new DispatcherTimer();
			this.messagesCheckTimer.Interval = TimeSpan.FromMilliseconds(100);
			this.messagesCheckTimer.Tick += MessagesCheckTimerOnTick;
			this.messagesCheckTimer.Start();
		}

		private int counter = 0;
		private void MessagesCheckTimerOnTick(object sender, object o)
		{
			if (this.projectPlayEngineManager?.Engine == null)
				return;

			foreach (var engineMessage in this.projectPlayEngineManager.Engine.GetMessages())
				this.OutputMessages.Add(engineMessage);
			this.projectPlayEngineManager.Engine.ClearMessages();
		}

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

		public ObservableCollection<EngineMessage> OutputMessages { get; } = new ObservableCollection<EngineMessage>();

		public IProjectPlayStateManagment ProjectPlayStateManagment => this.projectPlayStateManager;
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