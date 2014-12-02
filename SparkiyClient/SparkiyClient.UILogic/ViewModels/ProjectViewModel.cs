using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Models;

namespace SparkiyClient.UILogic.ViewModels
{
	public class ProjectViewModel : ExtendedViewModel
	{
		public Project Model
		{
			get { return this.GetProperty<Project>(); }
			set { this.SetProperty(value); }
		}
	}
}
