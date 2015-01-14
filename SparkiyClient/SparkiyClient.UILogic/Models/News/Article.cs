using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkiyClient.Common;

namespace SparkiyClient.UILogic.Models.News
{
	public class Article : ExtendedObservableObject
	{
		public object Summary
		{
			get { return this.GetProperty<object>(); }
			set { this.SetProperty(value); }
		}

		public Action Action
		{
			get { return this.GetProperty<Action>(); }
			set { this.SetProperty(value); }
		}

		public bool IsDismissed
		{
			get { return this.GetProperty<bool>(); }
			set { this.SetProperty(value); }
		}

		public bool CanDismiss
		{
			get { return this.GetProperty<bool>(); }
			set { this.SetProperty(value); }
		}
	}
}
