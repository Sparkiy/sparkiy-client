﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Nito.AsyncEx;
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

		string SelectedScriptName { get; }

		string SelectedScriptCode { get; set; }

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


		public async override void OnNavigatedTo(NavigationEventArgs e)
		{
			var project = e.Parameter as Project;
			if (project == null)
				throw new InvalidDataException("Passed project object is not valid or null.");

			this.Project = project;

			// Load scripts and assets list
			await this.Project.LoadScriptsAsync(this.projectService);

			// Select first script
			this.SelectedScript = this.Project.Scripts.Result.FirstOrDefault();

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
				this.Project.Scripts.Result.Add(new Script() {Name = this.NewFileViewModel.Name});
				await this.projectService.SaveAsync();

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
			set
			{
				this.SetProperty(value);
				this.RaisePropertyChanged(() => this.SelectedScriptName);
				this.RaisePropertyChanged(() => this.SelectedScriptCode);
			}
		}

		public string SelectedScriptName => this.SelectedScript?.Name ?? String.Empty;

		public string SelectedScriptCode
		{
			get { return this.SelectedScript?.Code ?? String.Empty; }
			set { this.SelectedScript.Code = value; }
		}

		public RelayCommand AddNewFileCommand { get; }

		public INewFileViewModel NewFileViewModel
		{
			get { return this.GetProperty<INewFileViewModel>(); }
			protected set { this.SetProperty(value); }
		}
	}

	public class EditPageViewModelDesignTime : EditPageViewModel
	{
		public EditPageViewModelDesignTime() : base(null, null)
		{
			this.Project = new Project()
			{
				Author = "Aleksandar Toplek",
				Description = "Sample description",
				Name = "Sample project",
				Scripts = NotifyTaskCompletion.Create(Task.Run(() => new ObservableCollection<Script>()
				{
					new Script { Code="Sample code", Name = "main"},
					new Script { Code="Sample code", Name = "script1"},
					new Script { Code="Sample code", Name = "script2"}
				}))
			};
			this.SelectedScript = this.Project.Scripts.Result.FirstOrDefault();
		}
	}
}