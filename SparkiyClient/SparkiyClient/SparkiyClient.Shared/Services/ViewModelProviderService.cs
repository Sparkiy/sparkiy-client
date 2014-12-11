using Windows.ApplicationModel;
using Microsoft.Practices.ServiceLocation;
using SparkiyClient.UILogic.ViewModels;

namespace SparkiyClient.Services
{
	public class ViewModelProviderService
	{
		public IMainPageViewModel MainPage => DesignMode.DesignModeEnabled ? new MainPageViewModelDesignTime() : ServiceLocator.Current.GetInstance<IMainPageViewModel>();

		public ICreateProjectPageViewModel CreateProjectPage => ServiceLocator.Current?.GetInstance<ICreateProjectPageViewModel>();

		public IProjectPageViewModel ProjectPage => ServiceLocator.Current?.GetInstance<IProjectPageViewModel>();
	}
}