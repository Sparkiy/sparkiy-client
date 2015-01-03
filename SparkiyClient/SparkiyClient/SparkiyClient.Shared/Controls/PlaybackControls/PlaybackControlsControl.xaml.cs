using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SparkiyClient.UILogic.Services;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SparkiyClient.Controls.PlaybackControls
{
    public sealed partial class PlaybackControlsControl : UserControl
    {
	    private IProjectPlayStateManagment projectPlayStateManager;


        public PlaybackControlsControl()
        {
            this.InitializeComponent();

			this.Loaded += OnLoaded;
        }

	    private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
	    {
		    if (this.StopCommand == null)
				this.StopButton.Visibility = Visibility.Collapsed;
	    }


	    public void AssignPlayStateManager(IProjectPlayStateManagment manager)
	    {
		    this.projectPlayStateManager = manager;
		    this.projectPlayStateManager.OnStateChanged += sender => this.UpdatePlayState();
		    this.UpdatePlayState();
	    }

	    private void UpdatePlayState()
	    {
		    if (this.projectPlayStateManager.IsPlaying)
		    {
			    this.PlayButton.Visibility = Visibility.Collapsed;
				this.PauseButton.Visibility = Visibility.Visible;
			}
		    else
		    {
				this.PlayButton.Visibility = Visibility.Visible;
				this.PauseButton.Visibility = Visibility.Collapsed;
			}
	    }

	    private void ButtonPlayOnClick(object sender, RoutedEventArgs e)
	    {
		    this.projectPlayStateManager.PlayProject();
			this.UpdatePlayState();
		}

	    private void ButtonPauseOnClick(object sender, RoutedEventArgs e)
	    {
		    this.projectPlayStateManager.PauseProject();
			this.UpdatePlayState();
		}

	    private void ButtonRestartOnClick(object sender, RoutedEventArgs e)
	    {
		    this.projectPlayStateManager.RestartProject();
			this.UpdatePlayState();
		}

	    private void ButtonScreenshotOnClick(object sender, RoutedEventArgs e)
	    {
		    this.projectPlayStateManager.TakeScreenshot();
	    }

	    private void ButtonStopOnClick(object sender, RoutedEventArgs e)
	    {
			if (this.StopCommand == null)
				throw new InvalidOperationException("Can't invoke command if not set.");

		    if (this.StopCommand.CanExecute(null))
				this.StopCommand.Execute(null);
	    }


		public ICommand StopCommand
		{
			get { return (ICommand)GetValue(StopCommandProperty); }
			set { SetValue(StopCommandProperty, value); }
		}

		public static readonly DependencyProperty StopCommandProperty =
			DependencyProperty.Register("StopCommand", typeof(ICommand), typeof(PlaybackControlsControl), new PropertyMetadata(null));
	}
}
