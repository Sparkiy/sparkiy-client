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
		public IMainPageViewModel MainPage { get { return DesignMode.DesignModeEnabled ? new MainPageViewModelDesignTime() : ServiceLocator.Current == null ? null : ServiceLocator.Current.GetInstance<IMainPageViewModel>();}}

		public IProjectPageViewModel ProjectPage { get { return DesignMode.DesignModeEnabled ? new ProjectPageViewModelDesignTime() : ServiceLocator.Current == null ? null : ServiceLocator.Current.GetInstance<IProjectPageViewModel>();}}

		public IPlayPageViewModel PlayPage { get { return DesignMode.DesignModeEnabled ? new PlayPageViewModelDesignTime() : ServiceLocator.Current == null ? null : ServiceLocator.Current.GetInstance<IPlayPageViewModel>();}}

#if WINDOWS_APP
		public ICreateProjectPageViewModel CreateProjectPage { get { return DesignMode.DesignModeEnabled ? new CreateProjectPageViewModelDesignTime() : ServiceLocator.Current == null ? null : ServiceLocator.Current.GetInstance<ICreateProjectPageViewModel>();}}

		public IEditPageViewModel EditPage { get { return DesignMode.DesignModeEnabled ? new EditPageViewModelDesignTime() : ServiceLocator.Current == null ? null : ServiceLocator.Current.GetInstance<IEditPageViewModel>();}}

		public IDebugPageViewModel DebugPage { get { return DesignMode.DesignModeEnabled ? new DebugPageViewModelDesignTime() : ServiceLocator.Current == null ? null : ServiceLocator.Current.GetInstance<IDebugPageViewModel>(); }}
#endif
	}
}