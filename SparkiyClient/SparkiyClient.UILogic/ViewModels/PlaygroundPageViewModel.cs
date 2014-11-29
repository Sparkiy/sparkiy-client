using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
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
using SparkiyEngine.Bindings.Component.Engine;
using SparkiyEngine.Bindings.Component.Graphics;
using SparkiyEngine.Bindings.Component.Language;
using SparkiyEngine.Engine;
using SparkiyEngine.Graphics.DirectX;

namespace SparkiyClient.UILogic.ViewModels
{
	public interface IPlaygroundViewModel
	{
		void AssignCodeEditor(ICodeEditor editor);
	    void AssignEngine(IEngineBindings engineBindings);
	    void AssignMessagesPopup(IMessagesPopup messagesPopup);
	}

    [ComVisible(false)]
    public class PlaygroundPageViewModel : ExtendedViewModel, IPlaygroundViewModel, IDisposable
	{
		private readonly INavigationService navigationService;
		private readonly IAlertMessageService alertMessageService;
		private readonly IResourceLoader resourceLoader;

		private const int AutoRerunTimeout = 2000;

		// Code editor
		private ICodeEditor editor;
		private Timer editorTimer;

        // Mesages
        private IMessagesPopup messagesPopup;

		private IEngineBindings engine;


		public PlaygroundPageViewModel(
			INavigationService navigationService, 
			IAlertMessageService alertMessageService,
			IResourceLoader resourceLoader)
		{
			this.navigationService = navigationService;
			this.alertMessageService = alertMessageService;
			this.resourceLoader = resourceLoader;
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


			// TODO Fill view model with models
		}


		public void AssignCodeEditor(ICodeEditor editor)
		{
			this.editor = editor;

			this.editor.OnCodeChanged += (sender, eventArgs) => this.RetriggerTimer();
        }

	    public void AssignEngine(IEngineBindings engineBindings)
	    {
	        this.engine = engineBindings;
	        this.engine.OnMessageCreated += this.EngineOnOnMessageCreated;
	        this.engine.Initialize();
	    }

        public void AssignMessagesPopup(IMessagesPopup messagesPopup)
        {
            this.messagesPopup = messagesPopup;
        }

		private async void EditorCallback(object state)
		{
			if (!this.IsAutoRerunEnabled) return;

			await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await this.RunScriptAsync());
		}

		private void RetriggerTimer()
		{
		    this.editorTimer?.Dispose();
		    this.editorTimer = new Timer(this.EditorCallback, null, TimeSpan.FromMilliseconds(AutoRerunTimeout), TimeSpan.FromMilliseconds(-1));
        }

		private async Task RunScriptAsync()
		{
            this.engine.Pause();
            this.engine.Reset();
		    await this.messagesPopup.ClearAsync();
            this.engine.AddScript("playground", this.GetPlaygroundCode());
            this.engine.Play();
		}

		private string GetPlaygroundCode()
		{
			return this.editor.Code;
		}

		private void EngineOnOnMessageCreated(object sender)
		{
            this.engine.GetMessages().ForEach(async msg =>
            {
                if (msg.Message.StartsWith("Error"))
                    await this.messagesPopup.AddErrorMessageAsync(msg.Message.Trim());
                else await this.messagesPopup.AddTemporaryMessageAsync(msg.Message.Trim());
            });
            this.engine.ClearMessages();
		}


		public bool IsAutoRerunEnabled
		{
			get { return this.GetProperty<bool>(defaultValue: true); }
			set { this.SetProperty(value); }
		}

		public bool LoadingData
		{
			get { return this.GetProperty<bool>(); }
			private set { this.SetProperty(value); }
		}

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.editorTimer?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources. 
        // ~PlaygroundPageViewModel() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}