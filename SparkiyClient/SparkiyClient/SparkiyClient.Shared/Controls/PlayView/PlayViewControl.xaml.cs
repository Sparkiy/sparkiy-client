using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
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
using SharpDX;
using SharpDX.Toolkit.Graphics;
using SparkiyClient.Services;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;
using SparkiyEngine.Bindings.Component.Engine;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SparkiyClient.Controls.PlayView
{
	public sealed partial class PlayViewControl : UserControl, IProjectPlayStateManagment, IProjectPlayEngineManagement
	{
		private static ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<PlayViewControl>();

		private Project project;
		private IEngineBindings engine;

		// State tracking
		private bool isPaused = true;
		private bool isInitialized = false;

		public event ProjectPlayStateEventHandler OnStateChanged;


		public PlayViewControl()
		{
			this.InitializeComponent();
		}


		public void AssignProject(Project project)
		{
			Log.Debug("Assigned project \"{0}\" to the PlayView", project.Name);

			// Stop currently running project
			if (this.IsInitialized)
				this.StopProject();

			// Assign new project
			this.project = project;

			this.RebuildEngine();
		}

		public void StopProject()
	    {
			this.isInitialized = false;

		    this.Engine.Stop();

			if (this.OnStateChanged != null)
				this.OnStateChanged.Invoke(this);
		}

		public void PlayProject()
	    {
			if (!this.IsInitialized || this.IsPlaying)
				return;

			this.Engine.Play();

		    this.isPaused = false;

			if (this.OnStateChanged != null)
				this.OnStateChanged.Invoke(this);
		}

		public void PauseProject()
	    {
		    if (!this.IsInitialized || this.IsPause)
			    return;

			this.Engine.Pause();

			this.isPaused = true;

			if (this.OnStateChanged != null)
				this.OnStateChanged.Invoke(this);
		}

		public void RestartProject()
	    {
			this.StopProject();
			this.RebuildEngine();
			this.Engine.Play();

			if (this.OnStateChanged != null)
				this.OnStateChanged.Invoke(this);
		}

		private void RebuildEngine()
		{
			// Load assets from project
			var imageAssets = new List<ImageAsset>();
			foreach (var asset in this.project.Assets)
			{
				if (asset is ImageAsset)
				{
					var imageAsset = asset as ImageAsset;
					imageAssets.Add(imageAsset);
					Log.Debug("Added Asset:Image \"{0}\"", imageAsset.Name);
				}
				else
				{
					throw new NotSupportedException("Provided asset type is not supported by this player.");
				}
			}

			// Combine all classes into one string
			var classesCombined = this.project.Files
				.OfType<Class>()
				.Aggregate(
					String.Empty,
					(combined, current) => combined + current.Code);

			// Instantiate new engine
			if (this.engine != null)
			{
				Utilities.Dispose(ref this.engine);
			}

			this.engine = ServiceLocator.Current.GetInstance<EngineProviderService>().GetLuaDxEngine(this.SwapChainPanel);
			this.engine.Initialize();

			// Add assets to the engine
			foreach (var imageAsset in imageAssets)
			{
				this.engine.AddImageAsset(imageAsset.Name, imageAsset.Data);
			}

			// Add scripts to the engine
			bool allScriptsValid = true;
			foreach (var script in this.project.Files.OfType<Script>())
				allScriptsValid &= this.engine.AddScript(script.Name, classesCombined + script.Code);
			if (!allScriptsValid)
				this.isInitialized = false;
			else this.isInitialized = true;
		}

		public void TakeScreenshot()
		{
			throw new NotImplementedException();
		}

		public bool IsInitialized { get { return this.isInitialized; } }

		public bool IsPlaying { get { return !this.isPaused; } }

		public bool IsPause { get { return this.isPaused; } }

		public IEngineBindings Engine { get { return this.engine; } }
	}
}
