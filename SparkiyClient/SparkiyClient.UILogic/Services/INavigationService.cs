namespace SparkiyClient.UILogic.Services
{
	public interface INavigationService 
	{
		bool CanGoBack { get; }

		string CurrentPageKey { get; }

		void GoBack();

		void GoHome();

		void NavigateTo(string pageKey, bool addSelfToStack = true);

		void NavigateTo(string pageKey, object parameter, bool addToStack = true);

		void NavigateTo<T>(bool addToStack = true);

		void NavigateTo<T>(object parameter, bool addSelfToStack = true);
	}
}