using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.Devices.Enumeration;
using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using SparkiyClient.Common;
using SparkiyClient.Common.Controls;
using SparkiyClient.UILogic.Services;
using SparkiyEngine.Bindings.Engine;
using SparkiyEngine.Bindings.Graphics;
using SparkiyEngine.Bindings.Language;
using SparkiyEngine.Core;
using SparkiyEngine.Engine.Implementation;
using SparkiyEngine.Graphics.DirectX;

namespace SparkiyClient.UILogic.ViewModels
{
	public interface IPlaygroundViewModel
	{
		void AssignCodeEditor(ICodeEditor editor);
		void AssignGraphicsPanel(object panel);
	}

	public class PlaygroundPageViewModel : ExtendedViewModel, IPlaygroundViewModel
	{
		private readonly INavigationService navigationService;
		private readonly IAlertMessageService alertMessageService;
		private readonly IResourceLoader resourceLoader;

		private const int AutoRerunTimeout = 1000;

		// Code editor
		private ICodeEditor editor;
		private Timer editorTimer;

		private SparkiyBootstrap bootstrap;
		private IGraphicsSettings graphicsSettings;


		public PlaygroundPageViewModel(
			INavigationService navigationService, 
			IAlertMessageService alertMessageService,
			IResourceLoader resourceLoader,
			IEngineBindings engineBindings, 
			ILanguageBindings languageBindings,
			IGraphicsSettings graphicsSettings,
			IGraphicsBindings graphicsBindings)
		{
			this.navigationService = navigationService;
			this.alertMessageService = alertMessageService;
			this.resourceLoader = resourceLoader;
			this.graphicsSettings = graphicsSettings;

			// Bootstrap the engine
			this.bootstrap = new SparkiyBootstrap();
			this.bootstrap.InitializeLua(languageBindings, graphicsBindings, engineBindings);
			this.bootstrap.Bindings.Engine.OnMessageCreated += this.EngineOnOnMessageCreated;
		}


		public async override void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
		{
			string errorMessage = string.Empty;
			//ICollection<SOME_MODEL> rootCategories = null;
			try
			{
				// TODO Retrieve models here
			}
			catch (Exception ex)
			{
				errorMessage = string.Format(CultureInfo.CurrentCulture,
					this.resourceLoader.GetString("GeneralServiceErrorMessage"),
					Environment.NewLine, ex.Message);
			}
			finally
			{
				this.LoadingData = false;
			}

			if (!String.IsNullOrWhiteSpace(errorMessage))
			{
				await this.alertMessageService.ShowAsync(errorMessage, this.resourceLoader.GetString("ErrorServiceUnreachable"));
				return;
			}


			// Initialize editor auto-rerun timer
			this.editorTimer = new Timer(this.EditorCallback, null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(-1));

			// TODO Fill view model with models
		}


		public void AssignCodeEditor(ICodeEditor editor)
		{
			this.editor = editor;

			this.editor.OnCodeChanged += (sender, eventArgs) => this.RetriggerTimer();
		}

		public void AssignGraphicsPanel(object panel)
		{
			this.graphicsSettings.AssignPanel(panel);
		}

		private void EditorCallback(object state)
		{
			if (!this.IsAutoRerunEnabled) return;

			System.Diagnostics.Debug.WriteLine("Script restart triggered by editor");
			CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.RunScript);
		}

		private void RetriggerTimer()
		{
			this.editorTimer.Change(TimeSpan.FromMilliseconds(AutoRerunTimeout), TimeSpan.FromMilliseconds(-1));
		}

		private void RunScript()
		{
			this.SetupPlayground();

			// Run script
			this.LoadScript("playground", this.GetPlaygroundCode());
			this.RunScript("playground");
		}

		private string GetPlaygroundCode()
		{
			return this.editor.Code;
		}

		private void LoadScript(string name, string code)
		{
			this.bootstrap.Bindings.Language.LoadScript(name, code);
		}

		private void RunScript(string name)
		{
			this.bootstrap.Bindings.Language.StartScript(name);
		}

		private void SetupPlayground()
		{
			this.bootstrap.Bindings.Engine.Reset();
			this.bootstrap.Bindings.Graphics.Reset();
			this.bootstrap.Bindings.Language.Reset();
		}

		private void EngineOnOnMessageCreated(object sender)
		{
			this.bootstrap.Bindings.Engine.GetMessages().ForEach(msg => System.Diagnostics.Debug.WriteLine(msg.Message));
			this.bootstrap.Bindings.Engine.ClearMessages();
		}


		public bool IsAutoRerunEnabled
		{
			get { return this.GetProperty<bool>(); }
			set { this.SetProperty(value); }
		}

		public bool LoadingData
		{
			get { return this.GetProperty<bool>(); }
			private set { this.SetProperty(value); }
		}
	}
}