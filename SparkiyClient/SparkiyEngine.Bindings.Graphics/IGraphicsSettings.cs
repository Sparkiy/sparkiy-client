namespace SparkiyEngine.Bindings.Graphics
{
	public interface IGraphicsSettings
	{
		void AssignPanel(object panel);

		IGraphicsBindings GraphicsBindings { get; }
	}
}