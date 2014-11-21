using System;
using SparkiyEngine.Bindings.Common.Component;

namespace SparkiyEngine.Bindings.Language.Component
{
	public sealed class MethodRequestEventArguments
	{
		public MethodDeclarationDetails Declaration { get; set; }
	
		public MethodDeclarationOverloadDetails Overload { get; set; }

		public Object[] InputValues { get; set; }
	}
}