using SparkiyEngine.Bindings.Component.Common;

namespace SparkiyEngine.Bindings.Component.Engine
{
	public sealed class EngineMessage
	{
		public string Message { get; set; }

		public IBindingsBase Source { get; set; }

		public BindingTypes SourceType { get; set; }

		public bool IsError { get; set; }


		public override string ToString()
		{
			return this.Message;
		}
	}
}