using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
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
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Engine;
using SparkiyEngine.Bindings.Component.Graphics;
using SparkiyEngine.Bindings.Component.Language;
using SparkiyEngine.Engine.Implementation;
using SparkiyEngine.Graphics.DirectX;
using SparkiyEngine_Language_LuaImplementation;
#if WINDOWS_APP
using Windows.UI.ApplicationSettings;
#endif
using Microsoft.Practices.ServiceLocation;
using SparkiyClient.UILogic.ViewModels;
using MetroLog;
using MetroLog.Targets;
using MetroLog.Layouts;
using MetroLog.Internal;

namespace SparkiyClient
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	public sealed partial class App : Microsoft.Practices.Prism.Mvvm.MvvmAppBase
	{
		private static readonly ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<App>();
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
			this.RequestedTheme = ApplicationTheme.Light;

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

			// Debug logging option
#if DEBUG
			LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Trace, LogLevel.Fatal, new DebugTarget());
#endif

			// Configure crach handling
			GlobalCrashHandler.Configure();
			log.Debug("Global crash handler configured.");
		}

		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used when the application is launched to open a specific file, to display
		/// search results, and so forth.
		/// </summary>
		/// <param name="e">Details about the launch request and process.</param>
		protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
		{
			// Navigate to the initial page
			log.Debug("Navigating to Playground page");
			this.NavigationService.Navigate("Playground", null);

			// Ensure the current window is active
			Window.Current.Activate();
			return Task.FromResult<object>(null);
		}

		protected override void OnRegisterKnownTypesForSerialization()
		{
			// Set up the list of known types for the SuspensionManager
			//SessionStateService.RegisterKnownType(typeof(SOME_MODEL));
		}

		protected override Task OnInitializeAsync(IActivatedEventArgs args)
		{
			try
			{
				// Allow the implementing class the opportunity to
				// register types. DO not allow exceptions to abort
				// the initialization process.
				try
				{
					this.OnContainerRegistration((UnityContainer)this.container);
				}
				catch (Exception ex)
				{
					this.OnUnhandledRegistrationException(ex);
				}

				// Set the ViewModel Locator service to resolve View to ViewModel Types
				SparkiyClient.Common.ViewModelLocator.SetDefaultViewTypeToViewModelTypeResolver(ResolveViewViewModelConnection);

				// Set the ViewModel Locator service to use the Unity Container
				SparkiyClient.Common.ViewModelLocator.SetDefaultViewModelFactory((viewModelType) =>
				{
					return ServiceLocator.Current.GetInstance(viewModelType);
				});
			}
			finally
			{
				this.OnApplicationInitialize(args);
			}

			return base.OnInitializeAsync(args);
		}

		/// <summary>
		/// Override this method with the initialization logic of your application. Here you can initialize 
		/// services, repositories, and so on.
		/// </summary>
		/// <param name="args">The <see cref="IActivatedEventArgs"/> instance containing the event data.</param>
		private void OnApplicationInitialize(IActivatedEventArgs args)
		{
			// TODO Implement Live Tiles
			// Documentation on working with tiles can be found at http://go.microsoft.com/fwlink/?LinkID=288821&clcid=0x409
			//tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
			//tileUpdater.StartPeriodicUpdate(new Uri(Constants.ServerAddress + "/api/TileNotification"), PeriodicUpdateRecurrence.HalfHour);
		}

		private static Type ResolveViewViewModelConnection(Type viewType)
		{
			var viewModelTypeName = string.Format(
								CultureInfo.InvariantCulture,
								"SparkiyClient.UILogic.ViewModels.{0}ViewModel, SparkiyClient.UILogic, Version=1.0.0.0",
								viewType.Name);
			var viewModelType = Type.GetType(viewModelTypeName);
			if (viewModelType == null)
			{
				viewModelTypeName = string.Format(
					CultureInfo.InvariantCulture,
					"SparkiyClient.UILogic.ViewModels.{0}ViewModel, SparkiyClient.UILogic.Windows, Version=1.0.0.0",
					viewType.Name);
				viewModelType = Type.GetType(viewModelTypeName);
			}

			log.Debug("View ({0}) resolved as ({1})", viewType.FullName, viewModelType.FullName);
			return viewModelType;
		}

#if WINDOWS_APP
		protected override IList<SettingsCommand> GetSettingsCommands()
		{
			var settingsCommands = new List<SettingsCommand>();

			//var accountService = _container.Resolve<IAccountService>();
			//var resourceLoader = _container.Resolve<IResourceLoader>();
			//var eventAggregator = _container.Resolve<IEventAggregator>();

			//if (accountService.SignedInUser == null)
			//{
			//	settingsCommands.Add(new SettingsCommand(Guid.NewGuid().ToString(), resourceLoader.GetString("LoginText"), (c) => new SignInFlyout(eventAggregator).Show()));
			//}
			//else
			//{
			//	settingsCommands.Add(new SettingsCommand(Guid.NewGuid().ToString(), resourceLoader.GetString("LogoutText"), (c) => new SignOutFlyout().Show()));
			//	settingsCommands.Add(new SettingsCommand(Guid.NewGuid().ToString(), resourceLoader.GetString("AddShippingAddressTitle"), (c) => NavigationService.Navigate("ShippingAddress", null)));
			//	settingsCommands.Add(new SettingsCommand(Guid.NewGuid().ToString(), resourceLoader.GetString("AddBillingAddressTitle"), (c) => NavigationService.Navigate("BillingAddress", null)));
			//	settingsCommands.Add(new SettingsCommand(Guid.NewGuid().ToString(), resourceLoader.GetString("AddPaymentMethodTitle"), (c) => NavigationService.Navigate("PaymentMethod", null)));
			//	settingsCommands.Add(new SettingsCommand(Guid.NewGuid().ToString(), resourceLoader.GetString("ChangeDefaults"), (c) => new ChangeDefaultsFlyout().Show()));
			//}
			//settingsCommands.Add(new SettingsCommand(Guid.NewGuid().ToString(), resourceLoader.GetString("PrivacyPolicy"), async (c) => await Launcher.LaunchUriAsync(new Uri(resourceLoader.GetString("PrivacyPolicyUrl")))));
			//settingsCommands.Add(new SettingsCommand(Guid.NewGuid().ToString(), resourceLoader.GetString("Help"), async (c) => await Launcher.LaunchUriAsync(new Uri(resourceLoader.GetString("HelpUrl")))));

			return settingsCommands;
		}
#endif

		/// <summary>
		/// Implements and seals the Resolves method to be handled by the Unity Container.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>A concrete instance of the specified type.</returns>
		protected sealed override object Resolve(Type type)
		{
			// 
			// Use the container to resolve types (e.g. ViewModels and Flyouts)
			// so their dependencies get injected
			// 
			return ServiceLocator.Current.GetInstance<IUnityContainer>().Resolve(type);
		}

		/// <summary>
		/// Override this method with code to initialize your container. This method will contain calls
		/// to the Unity container's RegisterType and RegisterInstance methods for example.
		/// </summary>
		/// <param name="container">The instance of the unity container that should be used for registering types.</param>
		private void OnContainerRegistration(IUnityContainer container)
		{
			log.Debug("Filling Container...");

			// Instantiate ResourceLoader
			var resourceLoader = new Microsoft.Practices.Prism.StoreApps.ResourceLoaderAdapter(new ResourceLoader());

			// Register instances
			this.container.RegisterInstance<IUnityContainer>(this.container);
			this.container.RegisterInstance<INavigationService>(this.NavigationService);
			this.container.RegisterInstance<ISessionStateService>(this.SessionStateService);
			this.container.RegisterInstance<IResourceLoader>(resourceLoader);

			// Register services
			this.container.RegisterType<IAlertMessageService, AlertMessageService>(new ContainerControlledLifetimeManager());

			// Register engine implementations
			this.container.RegisterInstance<IGraphicsSettings>(new Renderer());

			//// Register engine bindings
			this.container.RegisterInstance<ILanguageBindings>((new LuaImplementation()).GetLanguageBindings());
			this.container.RegisterInstance<IGraphicsBindings>(this.container.Resolve<IGraphicsSettings>().GraphicsBindings);
			this.container.RegisterInstance<IEngineBindings>(new Sparkiy(
				SupportedLanguages.Lua,
				this.container.Resolve<ILanguageBindings>(),
				this.container.Resolve<IGraphicsBindings>()));

			// Register ViewModels as Singeltons
			this.container.RegisterType<MainPageViewModel, MainPageViewModel>(new ContainerControlledLifetimeManager());
			this.container.RegisterType<PlaygroundPageViewModel, PlaygroundPageViewModel>(new ContainerControlledLifetimeManager());
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
		}

		/// <summary>
		/// Get an reference to the current Application instance
		/// as an MvvmAppBase object.
		/// </summary>
		public static new MvvmAppBase Current
		{
			get { return (MvvmAppBase)Application.Current; }
		}

		/// <summary>
		/// Get the IoC Unity Container 
		/// </summary>
		public IUnityContainer Container
		{
			get
			{
				return this.container;
			}
		}
	}
}