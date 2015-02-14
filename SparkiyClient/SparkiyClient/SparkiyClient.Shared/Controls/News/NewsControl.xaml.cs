using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SparkiyClient.UILogic.Models.News;


namespace SparkiyClient.Controls.News
{
    public sealed partial class NewsControl : UserControl
    {
        public NewsControl()
        {
			this.Articles = new ObservableCollection<Article>();

            this.InitializeComponent();
        }


		private void ArticleDismissOnClick(object sender, RoutedEventArgs e)
		{
			var sourceElement = e.OriginalSource as FrameworkElement;
			if (sourceElement == null)
				throw new NullReferenceException("Could not retrieve Article from source");

			var article = sourceElement.DataContext as Article;
			if (article == null)
				throw new NullReferenceException("Could not retrieve Article from source");

			article.IsDismissed = true;
		}

		private void ArticleOnClick(object sender, RoutedEventArgs e)
		{
			var sourceElement = e.OriginalSource as FrameworkElement;
			if (sourceElement == null)
				throw new NullReferenceException("Could not retrieve Article from source");

			var article = sourceElement.DataContext as Article;
			if (article == null)
				throw new NullReferenceException("Could not retrieve Article from source");

			article.Action.Invoke();
		}

		public ObservableCollection<Article> Articles { get; private set; }
    }
}
