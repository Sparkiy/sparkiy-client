using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Practices.Prism.Mvvm;

namespace SparkiyClient.Common
{
	public class ExtendedViewModel : ViewModel
	{
		private Dictionary<string, object> propertyValues = new Dictionary<string, object>(); 


		public void SetProperty<T>(T value = default(T), [CallerMemberName] string propertyName = "")
		{
			this.propertyValues[propertyName] = value;
		}

		public T GetProperty<T>([CallerMemberName] string propertyName = "")
		{
			if (!this.propertyValues.ContainsKey(propertyName))
				return default(T);
			return (T)this.propertyValues[propertyName];
		}
	}
}
