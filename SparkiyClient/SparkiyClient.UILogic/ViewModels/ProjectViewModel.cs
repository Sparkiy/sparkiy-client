using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models;

namespace SparkiyClient.UILogic.ViewModels
{
	[ComVisible(false)]
	public interface IProjectViewModel : IViewModelBase
	{
	}


	[ComVisible(false)]
	public class ProjectViewModel : ExtendedViewModel, IProjectViewModel
	{
		public Project Model
		{
			get { return this.GetProperty<Project>(); }
			set { this.SetProperty(value); }
		}

		public bool LoadingData
		{
			get { return this.GetProperty<bool>(); }
			private set { this.SetProperty(value); }
		}
	}
}
