/*
 * Date:
 * dd/mm/yyyy
 * 06/03/2015
 * 
 * Lexer v2.0
 * Developed by luislasonbra
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VisualScript
{
	public class Lexer
	{
		private bool hasChar;
		private char lastChar;
		private Token lastToken;
		private ReaderString reader;

		private const char StringChar = '"';
		private const char QuotedStringChar = '\'';
		private const string Operators = "!~+-*/%&|^<>=.";
		private const string Separators = "()[]{},:?;";

		private static string[] otherOperators = new string[]
		{
			"++", "--", "<=", ">=",
			"==", "!=", "&&", "||",
			"*=", "/=", "%=", "+=", "-=",
			"..", "..."
		};

		public Lexer(string text)
		{
			if (text == null) throw new ArgumentNullException("text");
			this.reader = new ReaderString(text);
		}

		public List<Token> getTokens()
		{
			List<Token> tokens = new List<Token>();

			Token token;
			while ((token = NextToken()) != null) tokens.Add(token);
			
			//
			return tokens;
		}
		// =======================================================================

		private Token lastIndex;
		private Token lastDefineToken;
		public Token PeekLastToken()
		{
			if (lastDefineToken == null) lastDefineToken = lastIndex;
			Token token = this.lastDefineToken;
			token.Col += token.Value.Length; //  + 1

			//
			return token;
		}

		public Token PeekToken()
		{
			Token token = this.NextToken();
			this.PushToken(token);

			//
			return token;
		}

		public Token NextToken()
		{
			//if (this.lastIndex != null)
			//{
			//    this.lastDefineToken = this.lastIndex;
			//    this.lastIndex = null;
			//}
			if (this.lastToken != null)
			{
				Token t = this.lastToken;
				this.lastToken = null;

				//
				return t;
			}
			if (this.lastIndex != null)
			{
				this.lastDefineToken = this.lastIndex;
				//this.lastIndex = null;
			}

			char ch;
			try
			{
				ch = this.NextCharSkipBlanks();
			}
			catch (Exception) { return null; }

			if (char.IsDigit(ch))
			{
				Token t = this.NextInteger(ch);
				if (t != null) this.lastIndex = t;
				return t; // this.NextInteger(ch);
			}
			if (char.IsLetter(ch) || ch == '_')
			{
				Token t = this.NextName(ch);
				if (t != null) this.lastIndex = t;
				return t; // this.NextName(ch);
			}

			if (ch == StringChar)
			{
				Token t = this.NextString();
				if (t != null) this.lastIndex = t;
				return t; // this.NextString();
			}
			if (ch == QuotedStringChar)
			{
				Token t = this.NextQuotedString();
				if (t != null) this.lastIndex = t;
				return t; // this.NextQuotedString();
			}

			if (Separators.Contains(ch))
			{
				Token t = this.NextSeparator(ch);
				if (t != null) this.lastIndex = t;
				return t; // this.NextSeparator(ch);
			}
			if (Operators.Contains(ch))
			{
				Token t = this.NextOperator(ch);
				if (t != null) this.lastIndex = t;
				return t; // this.NextOperator(ch);
			}

			//
			throw new InvalidDataException(string.Format("Unknown input '{0}'", ch));
		}

		public void PushToken(Token token)
		{
			if (this.lastToken != null) throw new InvalidOperationException();

			//
			this.lastToken = token;
		}

		private Token NextOperator(char ch)
		{
			char ch2;
			try
			{
				ch2 = this.NextChar();
				string op = ch.ToString() + ch2.ToString();

				if (otherOperators.Contains(op))
				{
					try
					{
						char ch3 = this.NextChar();
						string op2 = op + ch3.ToString();

						if (otherOperators.Contains(op2))
						{
							return new Token()
							{
								TokenType = TokenType.Operator,
								Value = op2,
								Line = reader.Line,
								Col = reader.Col - op2.Length
							};
						}

						//
						this.PushChar(ch3);
					}
					catch (Exception) { }

					//
					return new Token()
					{
						TokenType = TokenType.Operator,
						Value = op,
						Line = reader.Line,
						Col = reader.Col - op.Length
					};
				}
				else
				{
					this.PushChar(ch2);
				}
			}
			catch (Exception) { }

			return new Token()
			{
				TokenType = TokenType.Operator,
				Value = ch.ToString(),
				Line = reader.Line,
				Col = reader.Col - ch.ToString().Length
			};
		}

		// fixe + 1
		private Token NextSeparator(char ch)
		{
			return new Token()
			{
				TokenType = TokenType.Separator,
				Value = ch.ToString(),
				Line = reader.Line,
				Col = reader.Col - ch.ToString().Length + 1
			};
		}

		private Token NextString()
		{
			StringBuilder sb = new StringBuilder();
			char lastChar = (char)0;
			char ch = this.NextChar();

			while (ch != StringChar || lastChar == '\\')
			{
				if (lastChar == '\\')
				{
					switch (ch)
					{
						case 't':
							sb.Length--;
							sb.Append('\t');
							break;
						case 'a':
							sb.Length--;
							sb.Append('\a');
							break;
						case 'b':
							sb.Length--;
							sb.Append('\b');
							break;
						case 'e':
							sb.Length--;
							sb.Append((char)27);
							break;
						case 'f':
							sb.Length--;
							sb.Append('\f');
							break;
						case 'n':
							sb.Length--;
							sb.Append('\n');
							break;
						case 'r':
							sb.Length--;
							sb.Append('\r');
							break;
						case 'v':
							sb.Length--;
							sb.Append('\v');
							break;
						case '\\':
							break;
						default:
							sb.Length--;
							sb.Append(ch);
							break;
					}

					//
					lastChar = (char)0;
				}
				else
				{
					sb.Append(ch);
					lastChar = ch;
				}

				//
				ch = this.NextChar();
			}

			Token token = new Token();
			token.Value = sb.ToString();
			token.TokenType = TokenType.String;
			token.Line = reader.Line;
			token.Col = reader.Col - sb.Length;

			//
			return token;
		}

		private Token NextQuotedString()
		{
			StringBuilder sb = new StringBuilder();

			char lastChar = (char)0;
			char ch = this.NextChar();

			while (ch != QuotedStringChar)
			{
				sb.Append(ch);
				lastChar = ch;

				//
				ch = this.NextChar();
			}

			Token token = new Token();
			token.Value = sb.ToString();
			token.TokenType = TokenType.String;
			token.Line = reader.Line;
			token.Col = reader.Col - sb.Length;

			//
			return token;
		}

		private Token NextInteger(char ch)
		{
			string integer = ch.ToString();

			try
			{
				ch = this.NextChar();

				while (char.IsDigit(ch))
				{
					integer += ch;
					ch = this.NextChar();
				}
				if (ch == '.') return this.NextReal(integer);

				//
				this.PushChar(ch);
			}
			catch (Exception) { }

			Token token = new Token();
			token.Value = integer;
			token.TokenType = TokenType.Integer;
			token.Line = reader.Line;
			token.Col = reader.Col - integer.Length;

			//
			return token;
		}

		private Token NextReal(string integerPart)
		{
			string real = integerPart + ".";
			char ch;
			try
			{
				ch = this.NextChar();

				while (char.IsDigit(ch))
				{
					real += ch;
					ch = this.NextChar();
				}

				//
				this.PushChar(ch);
			}
			catch (Exception) { }

			Token token = new Token();
			token.Value = real;
			token.TokenType = TokenType.Real;
			token.Line = reader.Line;
			token.Col = reader.Col - real.Length;

			//
			return token;
		}

		private Token NextName(char ch)
		{
			string name = ch.ToString();
			try
			{
				ch = this.NextChar();
				while (char.IsLetterOrDigit(ch) || ch == '_')
				{
					name += ch;
					ch = this.NextChar();
				}

				//
				this.PushChar(ch);
			}
			catch (Exception) { }

			Token token = new Token();
			token.Value = name;
			token.TokenType = TokenType.Name;
			token.Line = reader.Line;
			token.Col = reader.Col - name.Length;

			if (name == "null") token.TokenType = TokenType.Null;
			if (name == "true" || name == "false") token.TokenType = TokenType.Boolean;
			if (name == "@" || name == "@@") throw new Exception("Invalid Input Exception: " + name);

			//
			return token;
		}

		private char NextCharSkipBlanks()
		{
			char ch = this.NextChar();

			while (char.IsWhiteSpace(ch) || ch == '/' || ch == '\ufeff' || ch == '\ufffe')
			{
				if (ch == '/')
				{
					char ch2 = this.NextChar();

					if (ch2 == '/') this.SkipToEndOfLine();
					else if (ch2 == '*') this.SkipToEndOfComment();
					else
					{
						this.PushChar(ch2);
						return ch;
					}
				}

				//
				ch = this.NextChar();
			}

			//
			return ch;
		}

		// comentario de una sola linea, end line.
		private void SkipToEndOfLine()
		{
			char ch = this.NextChar();
			while (ch != '\r' && ch != '\n') ch = this.NextChar();

			//
			this.PushChar(ch);
		}

		// comentario de multiples lineas, end line.
		private void SkipToEndOfComment()
		{
			char ch = this.NextChar();
			while (true)
			{
				while (ch != '*') ch = this.NextChar();
				char ch2 = this.NextChar();
				if (ch2 == '/') return;

				ch = ch2;
			}
		}

		private void PushChar(char ch)
		{
			this.lastChar = ch;
			this.hasChar = true;
		}

		private char NextChar()
		{
			if (this.hasChar)
			{
				this.hasChar = false;
				return this.lastChar;
			}

			int ch = this.reader.Read();
			if (ch < 0) throw new Exception("End Of Input Exception");

			//
			return Convert.ToChar(ch);
		}
	}
}
