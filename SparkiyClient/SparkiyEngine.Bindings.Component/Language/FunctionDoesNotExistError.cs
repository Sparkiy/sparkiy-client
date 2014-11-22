using SparkiyEngine.Bindings.Component.Common;

namespace SparkiyEngine.Bindings.Component.Language
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
