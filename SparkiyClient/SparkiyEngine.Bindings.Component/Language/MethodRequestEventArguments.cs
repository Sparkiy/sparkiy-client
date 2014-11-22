using System;
using SparkiyEngine.Bindings.Component.Common;

namespace SparkiyEngine.Bindings.Component.Language
{
	public sealed class MethodRequestEventArguments
	{
		public MethodDeclarationDetails Declaration { get; set; }
	
		public MethodDeclarationOverloadDetails Overload { get; set; }

		public Object[] InputValues { get; set; }
	}
}