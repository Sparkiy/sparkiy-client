using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models;
using SparkiyClient.UILogic.Services;

namespace SparkiyClient.UILogic.ViewModels
{
	public interface INewFileViewModel
	{
		string Name { get; set; }

		int TypeIndex { get; set; }
	}

	public class NewFileViewModel : ExtendedViewModel, INewFileViewModel
	{
		public string Name
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		public int TypeIndex
		{
			get { return this.GetProperty<int>(); }
			set { this.SetProperty(value); }
		}
	}

	public interface IEditPageViewModel
	{
		Project Project { get; }

		Script SelectedScript { get; set; }

		RelayCommand AddNewFileCommand { get; }

		INewFileViewModel NewFileViewModel { get; }
	}

	public class EditPageViewModel : ExtendedViewModel, IEditPageViewModel
	{
		private readonly IProjectService projectService;
		private readonly INavigationService navigationService;


		public EditPageViewModel(IProjectService projectService, INavigationService navigationService)
		{
			this.projectService = projectService;
			this.navigationService = navigationService;

			this.AddNewFileCommand = new RelayCommand(this.AddNewFileCommandExecuteAsync);
		}


		public override void OnNavigatedTo(NavigationEventArgs e)
		{
			var project = e.Parameter as Project;
			if (project == null)
				throw new InvalidDataException("Passed project object is not valid or null.");

			this.Project = project;

			base.OnNavigatedTo(e);
		}

		private async void AddNewFileCommandExecuteAsync()
		{
			if (this.NewFileViewModel == null)
			{
				this.NewFileViewModel = new NewFileViewModel();
			}
			else
			{
				this.NewFileViewModel = null;
			}
		}


		public Project Project
		{
			get { return this.GetProperty<Project>(); }
			protected set { this.SetProperty(value); }
		}

		public Script SelectedScript
		{
			get { return this.GetProperty<Script>(); }
			set { this.SetProperty(value); }
		}

		public RelayCommand AddNewFileCommand { get; }

		public INewFileViewModel NewFileViewModel
		{
			get { return this.GetProperty<INewFileViewModel>(); }
			protected set { this.SetProperty(value); }
		}
	}
}