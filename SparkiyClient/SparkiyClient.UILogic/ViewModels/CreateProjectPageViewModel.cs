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
		string ProjectName { get; set; }

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
			var project = new Project() {Name = this.ProjectName};
            await this.projectService.CreateProjectAsync(project);
			this.navigationService.NavigateTo("ProjectPage", project);
		}

		public string ProjectName
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		public RelayCommand CreateProjectCommand { get; }
	}
}
