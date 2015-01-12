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
        public DebugPage()
        {
            this.InitializeComponent();
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

			throw new InvalidOperationException("O' no you di'in'!");
		}


	    public new IDebugPageViewModel ViewModel => this.DataContext as IDebugPageViewModel;
    }
}
