using System.Collections.Generic;
using System.Text.RegularExpressions;
using SparkiyClient.Controls.CodeEditor.Lexer;

namespace SparkiyClient.Controls.CodeEditor.Languages
{
	public class LuaGrammer : IGrammer
	{
		public LuaGrammer()
		{
			// Grammer from: https://github.com/alandoherty/lua-lexer/blob/master/LuaLexer/Lua/LexerRules.cs

			Rules = new GrammerRule[]
			{
				// Comment
				new GrammerRule(TokenType.Comment, new Regex(@"^--(?:\[(=*)\[[\s\S]*?(?:\]\1\]|$)|[^\r\n]*)[-]*")),

				// Whitespace
				new GrammerRule(TokenType.WhiteSpace, new Regex(@"^[\t\n\r \xA0]+")),

				// Word Operator
				//new GrammerRule(TokenType.Operator, new Regex("^(and|or|not|is)\\b")), 

				// Operator
				new GrammerRule(TokenType.Operator, new Regex(@"^(?:\+|\-|\*|\/|\%|\^|\#|\=\=|\~\=|\<\=|\>\=|\<|\>|\=|\(|\)|\{|\}|\;|\:|\.|\.\.|\.\.\.|\!\=|\!)")),

				// Delimiter
				new GrammerRule(TokenType.Number, new Regex(@"^[+-]?(?:0x[\da-f]+|(?:(?:\.\d+|\d+(?:\.\d*)?)(?:e[+\-]?\d+)?))")),

				// Identifier
				new GrammerRule(TokenType.Identifier, new Regex(@"^[a-z_]\w*")),

				// String Marker
				new GrammerRule(TokenType.String, new Regex(@"^(?:\""(?:[^\""\\]|\\[\s\S])*(?:\""|$)|\'(?:[^\'\\]|\\[\s\S])*(?:\'|$))", RegexOptions.IgnoreCase | RegexOptions.Singleline)),
				new GrammerRule(TokenType.String, new Regex(@"^\[(=*)\[[\s\S]*?(?:\]\1\]|$)", RegexOptions.IgnoreCase | RegexOptions.Multiline)),
			};

			Keywords = new string[]
			{
				"and", "break", "do", "else", "elseif", "end",
				"false", "for", "function", "if", "in", "local",
				"nil", "not", "or", "repeat", "return", "then",
				"true", "until", "while"
			};

			Builtins = new string[]
			{
				"math"
			};
		}


		public IEnumerable<GrammerRule> Rules { get; private set; }

		public IEnumerable<string> Builtins { get; private set; }

		public IEnumerable<string> Keywords { get; private set; }
	}
}