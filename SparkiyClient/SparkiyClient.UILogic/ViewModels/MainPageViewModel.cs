using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Views;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Services;

namespace SparkiyClient.UILogic.ViewModels
{
    [ComVisible(false)]
    public class MainPageViewModel : ExtendedViewModel
	{
		private INavigationService navigationService;
		private readonly IAlertMessageService alertMessageService;

		public MainPageViewModel(INavigationService navigationService, IAlertMessageService alertMessageService)
		{
			this.navigationService = navigationService;
			this.alertMessageService = alertMessageService;
        }


		public bool LoadingData
		{
			get { return this.GetProperty<bool>(); }
			private set { this.SetProperty(value); }
		}
    }
}
