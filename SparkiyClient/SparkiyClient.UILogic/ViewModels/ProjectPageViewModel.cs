using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;
using INavigationService = SparkiyClient.UILogic.Services.INavigationService;

namespace SparkiyClient.UILogic.ViewModels
{
	[ComVisible(false)]
	public interface IProjectPageViewModel : IViewModelBase
	{
		Project Project { get; }

		bool IsEditMode { get; set; }

		RelayCommand EditCommand { get; }

		RelayCommand PlayCommand { get; }

		RelayCommand GoBackCommand { get; }
    }

    [ComVisible(false)]
    public class ProjectPageViewModel : ExtendedViewModel, IProjectPageViewModel
    {
	    private readonly IProjectService projectService;
        private readonly INavigationService navigationService;
        private readonly IAlertMessageService alertMessageService;


        public ProjectPageViewModel(
			IProjectService projectService,
            INavigationService navigationService, 
            IAlertMessageService alertMessageService)
        {
	        this.projectService = projectService;
            this.navigationService = navigationService;
            this.alertMessageService = alertMessageService;

			this.EditCommand = new RelayCommand(this.EditCommandExecuteAsync);
			this.PlayCommand = new RelayCommand(this.PlayCommandExecuteAsync);
			this.GoBackCommand = new RelayCommand(this.GoBackCommandExecuteAsync);
        }


	    public override void OnNavigatedTo(NavigationEventArgs e)
	    {
		    var project = e.Parameter as Project;
		    if (project == null)
			    throw new NullReferenceException("Couldn't retrieve project model passed to this page.");

		    this.Project = project;

		    base.OnNavigatedTo(e);
	    }

		private async void GoBackCommandExecuteAsync()
		{
			this.IsEditMode = false;
			await this.SaveChangesAsync();
			this.navigationService.GoBack();
		}

		private async void EditCommandExecuteAsync()
	    {
			this.IsEditMode = false;
			await this.SaveChangesAsync();
			this.navigationService.NavigateTo("EditPage", this.Project);
	    }

	    private async void PlayCommandExecuteAsync()
	    {
		    this.IsEditMode = false;
			await this.SaveChangesAsync();
			this.navigationService.NavigateTo("PlayPage", this.Project);
		}

	    private async Task SaveChangesAsync()
	    {
		    await this.projectService.SaveAsync();
	    }

	    public Project Project
	    {
		    get { return this.GetProperty<Project>(); }
		    protected set { this.SetProperty(value); }
	    }

	    public bool IsEditMode
	    {
		    get { return this.GetProperty<bool>(); }
		    set
		    {
			    if (this.IsEditMode.Equals(value))
				    return;

			    this.SetProperty(value);

#pragma warning disable 4014
				this.SaveChangesAsync();
#pragma warning restore 4014
		    }
	    }

		public RelayCommand EditCommand { get; private set; }

		public RelayCommand PlayCommand { get; private set; }

		public RelayCommand GoBackCommand { get; private set; }

	    public bool LoadingData
        {
            get { return this.GetProperty<bool>(); }
            private set { this.SetProperty(value); }
        }
    }

	[ComVisible(false)]
	public class ProjectPageViewModelDesignTime : ProjectPageViewModel
	{
		public ProjectPageViewModelDesignTime() : base(null, null, null)
		{
			this.Project = new Project()
			{
				Author = "Sparkiy",
				Name = "First game",
				Description = "Fusce posuere at turpis et laoreet. Praesent et pretium lectus, non fringilla orci. Donec feugiat, nisi et semper rutrum, turpis orci tempus odio, eu malesuada ipsum turpis et dolor. In viverra suscipit vestibulum. Sed pharetra elementum suscipit. Donec semper iaculis pharetra. Ut ultricies turpis urna, tristique porttitor risus imperdiet id. Nullam orci felis, congue in elementum vitae, rutrum quis erat. Aliquam tincidunt condimentum pellentesque. Nulla maximus, sapien nec condimentum luctus, ipsum nisl fringilla mauris, at pulvinar nisl enim tempus risus. Pellentesque hendrerit, leo convallis euismod egestas, magna nisi tempor tellus, eu varius ante felis sit amet elit. Aenean quis risus a magna accumsan congue. Nam ultricies neque enim, ut accumsan ex vulputate at. Vestibulum posuere mi sit amet purus iaculis, eget fringilla sem iaculis. Etiam sit amet nisi sem. Fusce ac urna molestie, consequat massa in, vulputate ante."
			};
		}
	}
}
