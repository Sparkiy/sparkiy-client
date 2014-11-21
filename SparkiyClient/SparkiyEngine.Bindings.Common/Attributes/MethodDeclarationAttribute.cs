using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkiyEngine.Bindings.Common.Component;

namespace SparkiyEngine.Bindings.Common.Attributes
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class MethodDeclarationAttribute : Attribute
	{
		public MethodDeclarationAttribute(SupportedLanguages languages, string name, MethodTypes type)
		{
			this.Languages = languages;
			this.Name = name;
			this.Type = type;
		}


		public SupportedLanguages Languages { get; set; }

		public string Name { get; set; }
	
		public MethodTypes Type { get; set; }

		public BindingsVersion SupportedVersion { get; set; }
	}
}
