using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Services;

namespace SparkiyClient.UILogic.ViewModels
{
    [ComVisible(false)]
    public class ProjectPageViewModel : ExtendedViewModel
    {
        private INavigationService navigationService;
        private readonly IAlertMessageService alertMessageService;
        private readonly IResourceLoader resourceLoader;
        private bool loadingData;

        public ProjectPageViewModel(
            INavigationService navigationService, 
            IAlertMessageService alertMessageService,
            IResourceLoader resourceLoader)
        {
            this.navigationService = navigationService;
            this.alertMessageService = alertMessageService;
            this.resourceLoader = resourceLoader;
        }


        public override async void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode,
            Dictionary<string, object> viewModelState)
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
                await
                    this.alertMessageService.ShowAsync(errorMessage,
                        this.resourceLoader.GetString("ErrorServiceUnreachable"));
                return;
            }

            // TODO Fill view model with models
        }


        public bool LoadingData
        {
            get { return this.loadingData; }
            private set { this.SetProperty(ref this.loadingData, value); }
        }
    }
}
