using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SparkiyClient.Controls
{
    public sealed partial class TopNavigationControl : UserControl
    {
        public TopNavigationControl()
        {
            this.InitializeComponent();

			this.ItemsPrimary = new ObservableCollection<object>();
        }


		public string Header
		{
			get { return (string)GetValue(HeaderProperty); }
			set { SetValue(HeaderProperty, value); }
		}

		public static readonly DependencyProperty HeaderProperty =
			DependencyProperty.Register("Header", typeof(string), typeof(TopNavigationControl), new PropertyMetadata(String.Empty));


		public ICommand GoBackCommand
		{
			get { return (ICommand)GetValue(GoBackCommandProperty); }
			set { SetValue(GoBackCommandProperty, value); }
		}

		public static readonly DependencyProperty GoBackCommandProperty =
			DependencyProperty.Register("GoBackCommand", typeof(ICommand), typeof(TopNavigationControl), new PropertyMetadata(null));


		public ObservableCollection<object> ItemsPrimary
		{
			get { return (ObservableCollection<object>)GetValue(ItemsPrimaryProperty); }
			set { SetValue(ItemsPrimaryProperty, value); }
		}

		public static readonly DependencyProperty ItemsPrimaryProperty =
			DependencyProperty.Register("ItemsPrimary", typeof(ObservableCollection<object>), typeof(TopNavigationControl), new PropertyMetadata(null));
	}
}
