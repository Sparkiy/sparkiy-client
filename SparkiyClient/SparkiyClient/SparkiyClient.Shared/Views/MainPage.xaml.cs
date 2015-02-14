using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models.News;
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

		private void ProjectOnItemClick(object sender, ItemClickEventArgs e)
		{
			this.ViewModel.ProjectSelectedCommand.Execute(e.ClickedItem);
		}

		/// <summary>
		/// The view model
		/// </summary>
		public new IMainPageViewModel ViewModel
		{
			get { return this.DataContext as IMainPageViewModel; }
		}
    }
}
