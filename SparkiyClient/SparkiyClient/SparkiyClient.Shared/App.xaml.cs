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
#if WINDOWS_APP
using Windows.UI.ApplicationSettings;
#endif

namespace SparkiyClient
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	public sealed partial class App : Microsoft.Practices.Prism.Mvvm.MvvmAppBase
	{
		// Create the singleton container that will be used for type resolution in the app
		private readonly IUnityContainer container = new UnityContainer();

		//Bootstrap: App singleton service declarations
		private TileUpdater tileUpdater;

		public IEventAggregator EventAggregator { get; set; }

		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			this.InitializeComponent();
			this.RequestedTheme = ApplicationTheme.Light;
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
			// Initialize Event Aggregator
			this.EventAggregator = new EventAggregator();

			// Register PRISM service instances
			this.container.RegisterInstance<INavigationService>(NavigationService);
			this.container.RegisterInstance<ISessionStateService>(SessionStateService);
			this.container.RegisterInstance<IEventAggregator>(EventAggregator);
			this.container.RegisterInstance<IResourceLoader>(
				new Microsoft.Practices.Prism.StoreApps.ResourceLoaderAdapter(
					new ResourceLoader()));

			// Register global services
			this.container.RegisterType<IAlertMessageService, AlertMessageService>(new ContainerControlledLifetimeManager());

			// Set auto-wire between view and view models
			ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
				{
					var viewModelTypeName = string.Format(
						CultureInfo.InvariantCulture,
						"SparkiyClient.UILogic.ViewModels.{0}ViewModel, SparkiyClient.UILogic, Version=1.0.0.0, Culture=neutral", 
						viewType.Name);
					var viewModelType = Type.GetType(viewModelTypeName);
					if (viewModelType == null)
					{
						viewModelTypeName = string.Format(
							CultureInfo.InvariantCulture,
							"SparkiyClient.UILogic.ViewModels.{0}ViewModel, SparkiyClient.UILogic.Windows, Version=1.0.0.0, Culture=neutral", 
							viewType.Name);
						viewModelType = Type.GetType(viewModelTypeName);
					}

					return viewModelType;
				});

			// TODO Implement Live Tiles
			// Documentation on working with tiles can be found at http://go.microsoft.com/fwlink/?LinkID=288821&clcid=0x409
			//tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
			//tileUpdater.StartPeriodicUpdate(new Uri(Constants.ServerAddress + "/api/TileNotification"), PeriodicUpdateRecurrence.HalfHour);

			return base.OnInitializeAsync(args);
		}

		protected override object Resolve(Type type)
		{
			// Use the container to resolve types (e.g. ViewModels and Flyouts)
			// so their dependencies get injected
			return this.container.Resolve(type);
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
	}
}