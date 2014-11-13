using SparkiyEngine.Bindings.Common.Component;

namespace SparkiyEngine.Bindings.Engine
{
	public sealed class EngineMessage
	{
		public string Message { get; set; }

		public IBindingsBase Source { get; set; }

		public BindingTypes SourceType { get; set; }


		public override string ToString()
		{
			return this.Message;
		}
	}
}