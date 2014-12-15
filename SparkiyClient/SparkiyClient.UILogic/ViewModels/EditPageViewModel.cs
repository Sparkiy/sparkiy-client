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
using SparkiyClient.Common.Controls;
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
		void AssignEditor(ICodeEditor editor);

		Project Project { get; }

		CodeFile SelectedFile { get; set; }
		
		RelayCommand AddNewFileCommand { get; }

		INewFileViewModel NewFileViewModel { get; }
	}

	public class EditPageViewModel : ExtendedViewModel, IEditPageViewModel
	{
		private readonly IProjectService projectService;
		private readonly INavigationService navigationService;

		private ICodeEditor editor;


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
			await this.Project.LoadFilesAsync(this.projectService);

			// Select first script
			this.SelectedFile = this.Project.Files.Result.FirstOrDefault();

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
				if (this.NewFileViewModel.TypeIndex == 1)
					this.Project.Files.Result.Add(new Class {Name = this.NewFileViewModel.Name});
				else this.Project.Files.Result.Add(new Script {Name = this.NewFileViewModel.Name});
				await this.projectService.SaveAsync();

				this.NewFileViewModel = null;
			}
		}

		public void AssignEditor(ICodeEditor editor)
		{
			this.editor = editor;
			this.editor.OnCodeChanged += (sender, args) => this.SelectedFile.Code = this.editor.Code;
		}


		public Project Project
		{
			get { return this.GetProperty<Project>(); }
			protected set { this.SetProperty(value); }
		}

		public CodeFile SelectedFile
		{
			get { return this.GetProperty<CodeFile>(); }
			set
			{
				this.SetProperty(value);

				if (editor != null)
					this.editor.Code = this.SelectedFile?.Code;
			}
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
				Files = NotifyTaskCompletion.Create(Task.Run(() => new ObservableCollection<CodeFile>()
				{
					new Script {Code = "Sample code", Name = "main"},
					new Script {Code = "Sample code", Name = "script1"},
					new Script {Code = "Sample code", Name = "script2"},
					new Class {Code = "Class sample", Name = "class1"}
				}))
			};
			this.SelectedFile = this.Project.Files.Result.FirstOrDefault();
		}
	}
}