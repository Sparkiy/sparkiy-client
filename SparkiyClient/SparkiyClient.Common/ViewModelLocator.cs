using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkiyClient.Common
{
	public static class ViewModelLocator
	{
		private static Func<Type, Type> autoWireFunction;
		private static Func<Type, object> containerResolveFunction;

		public static void SetDefaultViewTypeToViewModelTypeResolver(Func<Type, Type> resolve)
		{
			ViewModelLocator.autoWireFunction = resolve;
		}

		public static void SetDefaultViewModelFactory(Func<Type, object> resolve)
		{
			ViewModelLocator.containerResolveFunction = resolve;
		}

		public static object GetViewModel(Type view)
		{
			return ViewModelLocator.containerResolveFunction.Invoke(ViewModelLocator.autoWireFunction.Invoke(view));
		}
	}
}
