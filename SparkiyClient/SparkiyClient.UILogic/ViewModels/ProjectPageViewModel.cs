using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Views;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Services;

namespace SparkiyClient.UILogic.ViewModels
{
    [ComVisible(false)]
    public class ProjectPageViewModel : ExtendedViewModel
    {
        private INavigationService navigationService;
        private readonly IAlertMessageService alertMessageService;
        private bool loadingData;

        public ProjectPageViewModel(
            INavigationService navigationService, 
            IAlertMessageService alertMessageService)
        {
            this.navigationService = navigationService;
            this.alertMessageService = alertMessageService;
        }


	    public bool IsEditMode
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
