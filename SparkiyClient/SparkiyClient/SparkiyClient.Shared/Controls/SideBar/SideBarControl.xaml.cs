using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace SparkiyClient.Controls.SideBar
{
    public sealed partial class SideBarControl : UserControl
    {
		private double toggleButtonOpacity;

		public SideBarControl()
        {
            this.InitializeComponent();


			// Opacity to 1 on pointer hover
			this.SideBarToggleButton.PointerEntered += (s, e) =>
			{
				this.toggleButtonOpacity = this.SideBarToggleButton.Opacity;
				this.SideBarToggleButton.Opacity = 1;
			};
			this.SideBarToggleButton.PointerExited += (s, e) =>
				this.SideBarToggleButton.Opacity = this.toggleButtonOpacity;

			// Enable toggle buttons when animation ends
			this.SideBarLeftHideStoryboard.Completed += SideBarToggleStoryboardCompleted;
			this.SideBarLeftShowStoryboard.Completed += SideBarToggleStoryboardCompleted;
			this.SideBarRightHideStoryboard.Completed += SideBarToggleStoryboardCompleted;
			this.SideBarRightShowStoryboard.Completed += SideBarToggleStoryboardCompleted;
		}

	    private void SideBarToggleStoryboardCompleted(object sender, object e)
	    {
			this.SideBarToggleButton.IsEnabled = true;
		}

	    private void SideBarToggleButtonOnClick(object sender, RoutedEventArgs e)
	    {
			if (this.IsOpen)
				this.GetHideStoryboard().Begin();
			else this.GetShowStoryboard().Begin();

			this.IsOpen = !this.IsOpen;
			this.SideBarToggleButton.IsEnabled = false;
		}

	    private Storyboard GetHideStoryboard()
	    {
		    if (this.IsLeft)
			    return this.SideBarLeftHideStoryboard;
		    return this.SideBarRightHideStoryboard;
	    }

		private Storyboard GetShowStoryboard()
		{
			if (this.IsLeft)
				return this.SideBarLeftShowStoryboard;
			return this.SideBarRightShowStoryboard;
		}


		public bool IsLeft
		{
			get { return (bool)GetValue(IsLeftProperty); }
			set { SetValue(IsLeftProperty, value); }
		}
		public static readonly DependencyProperty IsLeftProperty =
			DependencyProperty.Register("IsLeft", typeof(bool), typeof(SideBarControl), new PropertyMetadata(true));
		

		public bool IsOpen
		{
			get { return (bool)GetValue(IsOpenProperty); }
			set { SetValue(IsOpenProperty, value); }
		}
		public static readonly DependencyProperty IsOpenProperty =
			DependencyProperty.Register("IsOpen", typeof(bool), typeof(SideBarControl), new PropertyMetadata(true));


		public new object Content
		{
			get { return (object)GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}
		public new static readonly DependencyProperty ContentProperty =
			DependencyProperty.Register("Content", typeof(object), typeof(SideBarControl), new PropertyMetadata(null));
    }
}
