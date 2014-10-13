namespace SparkiyEngine.Bindings.Common.Component
{
	public sealed class MethodDeclarationOverloadDetails
	{
		public MethodTypes Type { get; set; }

		public DataTypes[] Input { get; set; }

		public DataTypes[] Return { get; set; }
	}
}