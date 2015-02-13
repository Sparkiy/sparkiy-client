using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using AdDuplex.Controls;
using AdRotator;
using AdRotator.Model;
using MetroLog;
using SOMAW81;
using SparkiyClient.Common;
using SparkiyClient.Common.Extensions;
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
        private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<DebugPage>();
        private SomaAdViewer somaAdViewer;
        private AdDuplex.Controls.AdControl adduplexAdControl;

        public DebugPage()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            // Load AdRotator
            AdRotatorControl.Log += message => Log.Debug(message);
            var adRotatorControl = new AdRotator.AdRotatorControl
            {
                Margin = new Thickness(0, 5, 0, 10),
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                LocalSettingsLocation = "AdSettings.xml",
                AutoStartAds = true,
                AdRefreshInterval = 20,
            };
            adRotatorControl.PlatformAdProviderComponents.Add(AdType.AdDuplex, typeof(AdDuplex.Controls.AdControl));
            adRotatorControl.PlatformAdProviderComponents.Add(AdType.Smaato, typeof(SOMAW81.AdViewer));
            Grid.SetRow(adRotatorControl, 1);
            (this.SideBarControl.Content as Grid).Children.Add(adRotatorControl);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
	    {
			// Assign play engine
			this.ViewModel.AssignProjectPlayEngineManager(this.PlayView);

			// Assign play state manager
			this.ViewModel.AssignProjectPlayStateManager(this.PlayView);
			this.PlaybackControlsControl.AssignPlayStateManager(this.PlayView);

			// Watch output messages changes
			this.ViewModel.OutputMessages.CollectionChanged += OutputMessagesOnCollectionChanged;

			base.OnNavigatedTo(e);
	    }

	    private void OutputMessagesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
	    {
			// Scroll to bottom
			var scrollViewer = this.SideBarMessagesListView.GetFirstDescendantOfType<ScrollViewer>();
			if (scrollViewer != null)
			{
				scrollViewer.ChangeView(null, scrollViewer.ScrollableHeight, null);
		    }
		}


	    public new IDebugPageViewModel ViewModel => this.DataContext as IDebugPageViewModel;
    }
}
