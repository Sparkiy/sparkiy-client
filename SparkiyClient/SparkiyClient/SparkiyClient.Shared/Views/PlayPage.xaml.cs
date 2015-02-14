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
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;
using SparkiyClient.UILogic.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SparkiyClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayPage : PageBase
    {
        public PlayPage()
        {
            this.InitializeComponent();
        }

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			// Assign play engine
			this.ViewModel.AssignProjectPlayEngineManager(this.PlayView);

			// Assign play state manager
			this.ViewModel.AssignProjectPlayStateManager(this.PlayView);
			this.PlaybackControlsControl.AssignPlayStateManager(this.PlayView);

			base.OnNavigatedTo(e);
		}

		public new IPlayPageViewModel ViewModel { get { return this.DataContext as IPlayPageViewModel; } }
	}
}
