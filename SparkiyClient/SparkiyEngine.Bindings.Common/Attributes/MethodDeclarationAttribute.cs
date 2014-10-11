﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkiyEngine.Bindings.Common.Attributes
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class MethodDeclarationAttribute : Attribute
	{
		public MethodDeclarationAttribute(SupportedLanguages languages, string name, MethodTypes type, BindingsVersion supportedVersion = new BindingsVersion())
		{
			this.Languages = languages;
			this.Name = name;
			this.Type = type;
			this.SupportedVersion = supportedVersion;
		}


		public SupportedLanguages Languages { get; set; }

		public string Name { get; set; }
	
		public MethodTypes Type { get; set; }

		public BindingsVersion SupportedVersion { get; set; }
	}
}
