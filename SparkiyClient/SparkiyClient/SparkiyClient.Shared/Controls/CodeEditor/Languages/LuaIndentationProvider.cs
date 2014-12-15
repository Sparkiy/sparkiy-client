namespace SparkiyClient.Controls.CodeEditor.Languages
{
	public class LuaIndentationProvider : IndentationProvider
	{
		public override int GuessIndentLevel(string text, int index)
		{
			return this.GetIndentLevel(text);
		}
	}
}