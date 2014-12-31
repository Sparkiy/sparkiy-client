using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Nito.AsyncEx;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;

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
				Files = NotifyTaskCompletion.Create(async () =>
				{
					return new ObservableCollection<CodeFile>()
					{
						new Script()
						{
							Name = "Entry",
							Code = "function Created()\n\nend\n\nfunction Started()\n\nend\n\nfunction Draw()\n\nend\n\nfunction Touched(type, x, y)\n\nend\n\nfunction Stopped()\n\nend\n"
						}
					};
				})
			};
		}


		private async void CreateProjectCommandExecuteAsync()
		{
            await this.projectService.CreateProjectAsync(this.Project);
			this.navigationService.NavigateTo("ProjectPage", this.Project);
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
