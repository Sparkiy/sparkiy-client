using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.System.UserProfile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Nito.AsyncEx;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;
using SparkiyClient.UILogic.ViewModels;
using INavigationService = SparkiyClient.UILogic.Services.INavigationService;

namespace SparkiyClient.UILogic.Windows.ViewModels
{
	public interface ICreateProjectPageViewModel
	{
		Project Project { get; set; }

		RelayCommand CreateProjectCommand { get; }
	}

	public class CreateProjectPageViewModel : ExtendedViewModel, ICreateProjectPageViewModel
	{
		private readonly INavigationService navigationService;
		private readonly IProjectService projectService;


		public CreateProjectPageViewModel(IProjectService projectService, INavigationService navigationService)
		{
			this.navigationService = navigationService;
			this.projectService = projectService;

			this.CreateProjectCommand = new RelayCommand(this.CreateProjectCommandExecuteAsync);

			this.Project = new Project()
			{
				Description = "Amazing new project. Touch Play button and try it out yourself. It's great!",
				Author = "Unknown",
				Files = new ObservableCollection<CodeFile>()
				{
					new Script()
					{
						Name = "Entry",
						Code = "function Created()\r\n\r\nend\r\n\r\nfunction Started()\r\n\r\nend\r\n\r\nfunction Draw()\r\n\r\nend\r\n\r\nfunction Touched(type, x, y)\r\n\r\nend\r\n\r\nfunction Stopped()\r\n\r\nend\r\n"
					}
				}
			};
		}


		public override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
		}

		private async void CreateProjectCommandExecuteAsync()
		{
            await this.projectService.CreateProjectAsync(this.Project);
			this.navigationService.NavigateTo("ProjectPage", this.Project, false);
		}

		public Project Project
		{
			get { return this.GetProperty<Project>(); }
			set { this.SetProperty(value); }
		}

		public RelayCommand CreateProjectCommand { get; }
	}

	public sealed class CreateProjectPageViewModelDesignTime : CreateProjectPageViewModel
	{
		public CreateProjectPageViewModelDesignTime() : base(null, null)
		{
		}
	}
}
