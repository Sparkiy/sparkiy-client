using Windows.ApplicationModel;
using Microsoft.Practices.ServiceLocation;
using SparkiyClient.UILogic.ViewModels;

namespace SparkiyClient.Services
{
	public class ViewModelProviderService
	{
		public IMainPageViewModel MainPage => DesignMode.DesignModeEnabled ? new MainPageViewModelDesignTime() : ServiceLocator.Current?.GetInstance<IMainPageViewModel>();

		public IProjectPageViewModel ProjectPage => DesignMode.DesignModeEnabled ? new ProjectPageViewModelDesignTime() : ServiceLocator.Current?.GetInstance<IProjectPageViewModel>();

		public IPlayPageViewModel PlayPage => ServiceLocator.Current?.GetInstance<IPlayPageViewModel>();

#if WINDOWS_APP
		public ICreateProjectPageViewModel CreateProjectPage => ServiceLocator.Current?.GetInstance<ICreateProjectPageViewModel>();

		public IEditPageViewModel EditPage => DesignMode.DesignModeEnabled ? new EditPageViewModelDesignTime() : ServiceLocator.Current?.GetInstance<IEditPageViewModel>();

		public IDebugPageViewModel DebugPage => ServiceLocator.Current?.GetInstance<IDebugPageViewModel>();
#endif
	}
}