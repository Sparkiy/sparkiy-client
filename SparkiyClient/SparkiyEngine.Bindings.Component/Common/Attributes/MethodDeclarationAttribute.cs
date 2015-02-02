using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkiyEngine.Bindings.Component.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	internal class MethodDeclarationAttribute : Attribute
	{
		public MethodDeclarationAttribute(SupportedLanguages languages, string name, MethodTypes type)
		{
            this.Names = new string[1];

			this.Languages = languages;
			this.Names[0] = name;
			this.Type = type;
		}

        public MethodDeclarationAttribute(SupportedLanguages languages, string name, MethodTypes type, string[] returnParams)
            : this(languages, name, type)
        {
            this.ReturnParams = returnParams;
        }

        public MethodDeclarationAttribute(SupportedLanguages languages, string[] names, MethodTypes type)
        {
            this.Languages = languages;
            this.Names = names;
            this.Type = type;
        }

        public MethodDeclarationAttribute(SupportedLanguages languages, string[] names, MethodTypes type, string[] returnParams)
            : this(languages, names, type)
        {
            this.ReturnParams = returnParams;
        }


        public SupportedLanguages Languages { get; set; }

		public string[] Names { get; set; }
	
		public MethodTypes Type { get; set; }

		public BindingsVersion SupportedVersion { get; set; }

        public string[] ReturnParams { get; set; }
	}
}
