using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SparkiyClient.Common;
using SparkiyClient.Controls.PlayView;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;
using SparkiyClient.UILogic.ViewModels;
using SparkiyClient.UILogic.Windows.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SparkiyClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DebugPage : PageBase
    {
        public DebugPage()
        {
            this.InitializeComponent();
        }


	    protected override async void OnNavigatedTo(NavigationEventArgs e)
	    {
			base.OnNavigatedTo(e);

			var project = e.Parameter as Project;
			if (project == null)
				throw new NullReferenceException("Passed data is not in expected format.");

			this.ViewModel.AssignProjectPlayEngineManager(this.PlayView as IProjectPlayEngineManagement);
			this.ViewModel.AssignProjectPlayStateManager(this.PlayView as IProjectPlayStateManagment);

			await this.ViewModel.AssignProjectAsync(project);
	    }


	    public new IDebugPageViewModel ViewModel => this.DataContext as IDebugPageViewModel;
    }
}
