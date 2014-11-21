using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkiyEngine.Bindings.Common.Component;

namespace SparkiyEngine.Bindings.Language.Component
{
	public sealed class FunctionDoesNotExistError
	{
		public string Name { get; set; }

		public MethodDeclarationOverloadDetails MethodOverload { get; set; }


		public FunctionDoesNotExistError(string name, MethodDeclarationOverloadDetails overload)
		{
			this.Name = name;
			this.MethodOverload = overload;
		}
	}
}
