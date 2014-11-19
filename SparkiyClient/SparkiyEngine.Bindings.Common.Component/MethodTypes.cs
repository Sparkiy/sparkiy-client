using System;

namespace SparkiyEngine.Bindings.Common.Component
{
	[Flags]
	public enum MethodTypes
	{
		Call = 0x00,
		Get = 0x01,
		Set = 0x02,
		Function = 0x04
	}
}