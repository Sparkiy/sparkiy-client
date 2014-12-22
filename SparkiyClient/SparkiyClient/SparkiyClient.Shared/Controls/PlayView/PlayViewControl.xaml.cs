using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MetroLog;
using Microsoft.Practices.ServiceLocation;
using SparkiyClient.Services;
using SparkiyClient.UILogic.Models;
using SparkiyEngine.Bindings.Component.Engine;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SparkiyClient.Controls.PlayView
{
	public interface IProjectPlayStateManagment
	{
		Task AssignProjectAsync(Project project);

		void PlayProject();

		void PauseProject();

		void RestartProject();

		void TakeScreenshot();


		bool IsInitialized { get; }

		bool IsPlaying { get; }

		bool IsPause { get; }
	}

	public interface IProjectPlayEngineManagement
	{
		IEngineBindings Engine { get; }
	}

    public sealed partial class PlayViewControl : UserControl, IProjectPlayStateManagment, IProjectPlayEngineManagement
    {
	    private static ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<PlayViewControl>();

	    private Project project;
	    private IEngineBindings engine;

		// State tracking
	    private bool isPaused;
	    private bool isInitialized;


	    public PlayViewControl()
        {
            this.InitializeComponent();
        }


	    public async Task AssignProjectAsync(Project project)
	    {
			// Stop currently running project
		    if (this.IsInitialized)
			    this.StopProject();

			// Assign new project
		    this.project = project;

			// TODO Load Scripts and Classes from project
			// TODO Load assets from project

			// TODO Combine Classes with scripts

		    this.engine = ServiceLocator.Current.GetInstance<EngineProviderService>().GetLuaDxEngine(this.SwapChainPanel);
			// TODO Add scripts to the engine
			
			throw new NotImplementedException();

			this.isInitialized = true;
		}

	    private void StopProject()
	    {
			this.isInitialized = false;

			this.Engine.Pause();
			this.Engine.Reset();
		    throw new NotImplementedException();
	    }

	    public void PlayProject()
	    {
			if (this.IsPlaying)
				return;

			this.Engine.Play();

		    throw new NotImplementedException();

		    this.isPaused = false;
	    }

	    public void PauseProject()
	    {
		    if (this.IsPause)
			    return;

			this.Engine.Pause();

			throw new NotImplementedException();

			this.isPaused = true;
		}

	    public void RestartProject()
	    {
			this.StopProject();
			// TODO Restart project (Re-add scripts)
			this.Engine.Play();
		    throw new NotImplementedException();
	    }

	    public void TakeScreenshot()
	    {
			throw new NotImplementedException();
	    }

	    public bool IsInitialized => this.isInitialized;

	    public bool IsPlaying => !this.isPaused;

	    public bool IsPause => this.isPaused;

	    public IEngineBindings Engine => this.engine;
    }
}
