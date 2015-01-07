using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;
using SparkiyEngine.Bindings.Component.Engine;
using INavigationService = SparkiyClient.UILogic.Services.INavigationService;

namespace SparkiyClient.UILogic.Windows.ViewModels
{
	public interface IDebugPageViewModel
	{
		void AssignProjectPlayEngineManager(IProjectPlayEngineManagement projectPlayEngineManagement);

		void AssignProjectPlayStateManager(IProjectPlayStateManagment projectPlayStateManagment);

		Task AssignProjectAsync(Project project);

		RelayCommand NavigateToEditorCommand { get; }

		RelayCommand NavigateToHomeCommand { get; }

		Project Project { get; }

		ObservableCollection<EngineMessage> OutputMessages { get; }
	}

	public class DebugPageViewModel : ExtendedViewModel, IDebugPageViewModel
	{
		private readonly IProjectService projectService;
		private readonly INavigationService navigationService;
		private IProjectPlayEngineManagement projectPlayEngineManager;
		private IProjectPlayStateManagment projectPlayStateManager;
		private Project project;

		private DispatcherTimer messagesCheckTimer;


		public DebugPageViewModel(IProjectService projectService, INavigationService navigationService)
		{
			this.projectService = projectService;
			this.navigationService = navigationService;

			this.messagesCheckTimer = new DispatcherTimer();
			this.messagesCheckTimer.Interval = TimeSpan.FromMilliseconds(100);
			this.messagesCheckTimer.Tick += MessagesCheckTimerOnTick;
			this.messagesCheckTimer.Start();

			this.NavigateToHomeCommand = new RelayCommand(this.NavigateToHomeCommandExecute);
			this.NavigateToEditorCommand = new RelayCommand(this.NavigateToEditorCommandExecute);
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

		private void NavigateToEditorCommandExecute()
		{
			this.navigationService.GoBack();
		}

		private void NavigateToHomeCommandExecute()
		{
			this.navigationService.GoHome();
		}

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

			// Assign project to the engine
			this.projectPlayEngineManager.AssignProject(this.project);

			// Run the project
			this.projectPlayStateManager.PlayProject();
		}


		public RelayCommand NavigateToEditorCommand { get; }

		public RelayCommand NavigateToHomeCommand { get; }


		public Project Project => this.project;

		public ObservableCollection<EngineMessage> OutputMessages { get; } = new ObservableCollection<EngineMessage>();
	}

	public sealed class DebugPageViewModelDesignTime : DebugPageViewModel
	{
		public DebugPageViewModelDesignTime() : base(null, null)
		{
			this.OutputMessages.Add(new EngineMessage() { Message = "Test1" });
			this.OutputMessages.Add(new EngineMessage() { Message = "Test2" });
			this.OutputMessages.Add(new EngineMessage() { Message = "Test3" });
		}
	}
}