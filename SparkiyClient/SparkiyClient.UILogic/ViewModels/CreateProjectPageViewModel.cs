using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;

namespace SparkiyClient.UILogic.ViewModels
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
		}


		private async void CreateProjectCommandExecuteAsync()
		{
            await this.projectService.CreateProjectAsync(this.Project);
			this.navigationService.NavigateTo("ProjectPage", this.Project);
		}

		public Project Project
		{
			get { return this.GetProperty<Project>(defaultValue: new Project()); }
			set { this.SetProperty(value); }
		}

		public RelayCommand CreateProjectCommand { get; }
	}
}
