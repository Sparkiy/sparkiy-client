﻿using Microsoft.Practices.Prism.StoreApps;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SparkiyClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : VisualStateAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
			this.SizeChanged += this.PageSizeChanged;


		}

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

		protected override void SaveState(System.Collections.Generic.Dictionary<string, object> pageState)
		{
			if (pageState == null) return;

			base.SaveState(pageState);
		}

		protected override void LoadState(object navigationParameter, System.Collections.Generic.Dictionary<string, object> pageState)
		{
			if (pageState == null) return;

			base.LoadState(navigationParameter, pageState);
		}

		private void PageSizeChanged(object sender, SizeChangedEventArgs e)
		{
			
		}
	}
}
