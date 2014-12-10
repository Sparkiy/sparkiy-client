using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SparkiyClient.UILogic.ViewModels;

namespace SparkiyClient.Views
{
	/// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : PageBase
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="MainPage"/> class.
		/// </summary>
		public MainPage()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			
			base.OnNavigatedFrom(e);
		}

		private void ProjectOnItemClick(object sender, ItemClickEventArgs e)
		{
			this.ViewModel.ProjectSelectedCommand.Execute(e.ClickedItem);
		}

		/// <summary>
		/// The view model
		/// </summary>
		public IMainPageViewModel ViewModel => this.DataContext as IMainPageViewModel;
    }
}
