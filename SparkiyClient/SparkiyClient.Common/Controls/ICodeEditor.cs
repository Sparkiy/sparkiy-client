using System;

namespace SparkiyClient.Common.Controls
{
	public interface ICodeEditor
	{
		event EventHandler OnCodeChanged;
		
		string Code { get; set; }
	}
}