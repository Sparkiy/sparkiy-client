using Windows.ApplicationModel;
using Microsoft.Practices.ServiceLocation;
using SparkiyClient.UILogic.ViewModels;
#if WINDOWS_APP
using SparkiyClient.UILogic.Windows.ViewModels;
#endif

namespace SparkiyClient.Services
{
	public class ViewModelProviderService
	{
		public IMainPageViewModel MainPage => DesignMode.DesignModeEnabled ? new MainPageViewModelDesignTime() : ServiceLocator.Current?.GetInstance<IMainPageViewModel>();

		public IProjectPageViewModel ProjectPage => DesignMode.DesignModeEnabled ? new ProjectPageViewModelDesignTime() : ServiceLocator.Current?.GetInstance<IProjectPageViewModel>();

		public IPlayPageViewModel PlayPage => DesignMode.DesignModeEnabled ? new PlayPageViewModelDesignTime() : ServiceLocator.Current?.GetInstance<IPlayPageViewModel>();

#if WINDOWS_APP
		public ICreateProjectPageViewModel CreateProjectPage => DesignMode.DesignModeEnabled ? new CreateProjectPageViewModelDesignTime() : ServiceLocator.Current?.GetInstance<ICreateProjectPageViewModel>();

		public IEditPageViewModel EditPage => DesignMode.DesignModeEnabled ? new EditPageViewModelDesignTime() : ServiceLocator.Current?.GetInstance<IEditPageViewModel>();

		public IDebugPageViewModel DebugPage => DesignMode.DesignModeEnabled ? new DebugPageViewModelDesignTime() : ServiceLocator.Current?.GetInstance<IDebugPageViewModel>();
#endif
	}
}