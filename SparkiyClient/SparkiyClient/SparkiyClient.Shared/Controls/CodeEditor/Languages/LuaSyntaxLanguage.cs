using System.Collections.Generic;
using Windows.UI;
using SparkiyClient.Controls.CodeEditor.Lexer;

namespace SparkiyClient.Controls.CodeEditor.Languages
{
	public class LuaSyntaxLanguage : SyntaxLanguage
	{
		public static readonly Color CommentColor = Color.FromArgb(255, 0, 142, 0);
		public static readonly Color StringColor = Color.FromArgb(255, 223, 0, 2);
		public static readonly Color NumberColor = Color.FromArgb(255, 58, 0, 220);
		public static readonly Color BuiltinsColor = Color.FromArgb(255, 69, 0, 132);
		public static readonly Color KeywordsColor = Color.FromArgb(255, 200, 0, 164);

		public LuaSyntaxLanguage() : base("Lua")
		{
			this.Grammer = new LuaGrammer();

			HighlightColors = new Dictionary<TokenType, Color>
			{
				{ TokenType.Comment, CommentColor },
				{ TokenType.String, StringColor },
				{ TokenType.Number, NumberColor },
                { TokenType.Builtins, BuiltinsColor },
				{ TokenType.Keyword, KeywordsColor },
			};

			IndentationProvider = new LuaIndentationProvider(); 
		}
	}
}