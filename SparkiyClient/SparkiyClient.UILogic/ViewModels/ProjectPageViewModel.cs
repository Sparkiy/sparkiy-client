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
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;

namespace SparkiyClient.UILogic.ViewModels
{
	[ComVisible(false)]
	public interface IProjectPageViewModel : IViewModelBase
	{
		Project Project { get; }

		bool IsEditMode { get; set; }
	}

    [ComVisible(false)]
    public class ProjectPageViewModel : ExtendedViewModel, IProjectPageViewModel
    {
        private INavigationService navigationService;
        private readonly IAlertMessageService alertMessageService;


        public ProjectPageViewModel(
            INavigationService navigationService, 
            IAlertMessageService alertMessageService)
        {
            this.navigationService = navigationService;
            this.alertMessageService = alertMessageService;
        }


	    public override void OnNavigatedTo(NavigationEventArgs e)
	    {
		    var project = e.Content as Project;
		    if (project == null)
			    throw new NullReferenceException("Couldn't retrieve project model passed to this page.");

		    this.Project = project;

		    base.OnNavigatedTo(e);
	    }

	    public Project Project
	    {
		    get { return this.GetProperty<Project>(); }
		    protected set { this.SetProperty(value); }
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
