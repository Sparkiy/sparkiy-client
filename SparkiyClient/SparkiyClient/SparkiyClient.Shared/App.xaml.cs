using Microsoft.Practices.Unity;
using SparkiyClient.Services;
using SparkiyClient.UILogic.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Engine;
using SparkiyEngine.Bindings.Component.Graphics;
using SparkiyEngine.Bindings.Component.Language;
using SparkiyEngine.Engine;
using SparkiyEngine.Graphics.DirectX;
using SparkiyEngine_Language_LuaImplementation;
#if WINDOWS_APP
using Windows.UI.ApplicationSettings;
#endif
using SparkiyClient.UILogic.ViewModels;
using MetroLog;
using MetroLog.Targets;
using MetroLog.Layouts;
using MetroLog.Internal;
using Microsoft.Practices.ServiceLocation;
using SparkiyClient.Common;
using SparkiyClient.Views;

namespace SparkiyClient
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	public sealed partial class App : Application, IDisposable
	{
		private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<App>();
		private readonly IUnityContainer container = null;

		//Bootstrap: App singleton service declarations
		//private TileUpdater tileUpdater;

		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			this.InitializeComponent();
			this.Suspending += OnSuspending;

			// Create container and register itself
			this.container = new UnityContainer();
			this.container.RegisterInstance(this.container);

			// Set up the global locator service
			ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(this.container));

			// Allow the implementation class the opportunity to register
			// types early in the process. Do not allow exceptions to abort
			// the object creation.
			try
			{
				this.OnEarlyContainerRegistration(this.container);
			}
			catch (Exception ex)
			{
				this.OnUnhandledRegistrationException(ex);
			}

			// Configure crash handling
			GlobalCrashHandler.Configure();
			Log.Debug("Global crash handler configured.");
		}

		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used such as when the application is launched to open a specific file.
		/// </summary>
		/// <param name="e">Details about the launch request and process.</param>
		protected async override void OnLaunched(LaunchActivatedEventArgs e)
		{
			Frame rootFrame = Window.Current.Content as Frame;

			// Do not repeat app initialization when the Window already has content,
			// just ensure that the window is active
			if (rootFrame == null)
			{
				// Create a Frame to act as the navigation context and navigate to the first page
				rootFrame = new Frame();
				rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];
				rootFrame.NavigationFailed += OnNavigationFailed;

				if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
				{
					//TODO: Load state from previously suspended application
				}

				// Place the frame in the current Window
				Window.Current.Content = rootFrame;
			}

			// Initialize navigation helper
			DispatcherHelper.Initialize();

			// Initialize container
			try
			{
				this.OnContainerRegistration(this.container as UnityContainer);
			}
			catch (Exception ex)
			{
				this.OnUnhandledRegistrationException(ex);
			}

			// Initialize navigation
			var navigationService = this.Container.Resolve<INavigationService>() as NavigationService;
			if (navigationService == null)
				throw new InvalidOperationException("Couldn't configure navigation service. Navigation service not registered properly.");
			navigationService.Configure(typeof(MainPage).Name, typeof(MainPage));
			navigationService.Configure(typeof(ProjectPage).Name, typeof(ProjectPage));
			navigationService.Configure(typeof(PlayPage).Name, typeof(PlayPage));
#if !WINDOWS_PHONE_APP
			// Register Windows specific views
			navigationService.Configure(typeof(CreateProjectPage).Name, typeof(CreateProjectPage));
			navigationService.Configure(typeof(EditPage).Name, typeof(EditPage));
			navigationService.Configure(typeof(DebugPage).Name, typeof(DebugPage));
#endif

			// Initialize storage if it doesn't require hard initialization
			var storageService = this.Container.Resolve<IStorageService>();
			if (!storageService.RequiresHardStorageInitialization())
				await storageService.InitializeStorageAsync();

			// Navigate to home page if navigation stack isn't restored
			if (rootFrame.Content == null)
				rootFrame.Navigate(typeof(MainPage), e.Arguments);
			
			// Ensure the current window is active
			Window.Current.Activate();
		}

		/// <summary>
		/// Invoked when Navigation to a certain page fails
		/// </summary>
		/// <param name="sender">The Frame which failed navigation</param>
		/// <param name="e">Details about the navigation failure</param>
		void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		/// <summary>
		/// Invoked when application execution is being suspended.  Application state is saved
		/// without knowing whether the application will be terminated or resumed with the contents
		/// of memory still intact.
		/// </summary>
		/// <param name="sender">The source of the suspend request.</param>
		/// <param name="e">Details about the suspend request.</param>
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();

			var saveTask = Task.Run(() => this.Container.Resolve<IProjectService>().SaveAsync());
			saveTask.Wait();

			deferral.Complete();
		}

		/// <summary>
		/// Override this method with code to initialize your container. This method will contain calls
		/// to the Unity container's RegisterType and RegisterInstance methods for example.
		/// </summary>
		/// <param name="container">The instance of the unity container that should be used for registering types.</param>
		private void OnContainerRegistration(IUnityContainer container)
		{
			Log.Debug("Filling Container...");

			// Register instances
			this.container.RegisterInstance<IUnityContainer>(this.container);
			
			// Register services
			this.container.RegisterType<IAlertMessageService, AlertMessageService>(new ContainerControlledLifetimeManager());
			this.container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
			this.container.RegisterType<EngineProviderService, EngineProviderService>(new ContainerControlledLifetimeManager());
			this.container.RegisterType<IStorageService, StorageService>(new ContainerControlledLifetimeManager());
			this.container.RegisterType<IProjectService, ProjectService>(new ContainerControlledLifetimeManager());

			// Register ViewModels as Singeltons
			this.container.RegisterType<IMainPageViewModel, MainPageViewModel>(new ContainerControlledLifetimeManager());
			
			this.container.RegisterType<IProjectPageViewModel, ProjectPageViewModel>(new ContainerControlledLifetimeManager());
			this.container.RegisterType<IPlayPageViewModel, PlayPageViewModel>(new ContainerControlledLifetimeManager());
#if WINDOWS_APP
			this.container.RegisterType<ICreateProjectPageViewModel, CreateProjectPageViewModel>(new ContainerControlledLifetimeManager());
			this.container.RegisterType<IEditPageViewModel, EditPageViewModel>(new ContainerControlledLifetimeManager());
			this.container.RegisterType<IDebugPageViewModel, DebugPageViewModel>(new ContainerControlledLifetimeManager());
#endif
		}

	    public static IEngineBindings InstantiateEngine(object panel)
	    {
            var engine = new Sparkiy();
            var language = LuaImplementation.Instantiate(engine);
            var graphics = new Renderer(engine);

            engine.AssignBindings(language.GetLanguageBindings(), graphics);
            engine.AssignPanel(panel);

	        return engine;
	    } 

		/// <summary>
		/// Override this method to register types in the container prior to any other code
		/// being run. This is especially useful when types need to be registered for application
		/// session state to be restored. Certain types may not be available or should not be registered
		/// in this method. For example, registering the Pub/Sub 
		/// </summary>
		/// <param name="container">The instance of the unity container that should be used for registering types.</param>
		private void OnEarlyContainerRegistration(IUnityContainer container)
		{
		}

		/// <summary>
		/// Override this method to catch any unhandled exceptions that occur during the registration process.
		/// </summary>
		/// <param name="ex">The exception that was thrown.</param>
		private void OnUnhandledRegistrationException(Exception ex)
		{
			throw new Exception("Failed to load container: " + ex.Message);
		}

		/// <summary>
		/// Get an reference to the current Application instance
		/// as an MvvmAppBase object.
		/// </summary>
		public static new App Current => (App)Application.Current;

		/// <summary>
		/// Get the IoC Unity Container 
		/// </summary>
		public IUnityContainer Container => this.container;

#region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.container?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources. 
        // ~App() {
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