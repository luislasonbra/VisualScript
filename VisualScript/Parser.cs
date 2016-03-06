using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;
using VisualScript.Commads;

namespace VisualScript
{
	public class Parser
	{
		public enum TypeTokens
		{
			None,
			If,
			For,
			While,
			Switch,
			Enum,
			Class,
			Function,
			TryCatchFinally
		}

		private Lexer lexer;

		public Parser(string text) : this(new Lexer(text)) { }
		public Parser(Lexer lexer) { this.lexer = lexer; }

		/// <summary>
		/// Parse Command
		/// </summary>
		/// <returns></returns>
		public Stmt ParseCommand(TypeTokens stateToken = TypeTokens.None)
		{
			Token token = this.lexer.NextToken();
			if (token == null) return null;

			if (token.TokenType == TokenType.Name)
			{
				// mal definido // reparar
				if (stateToken != TypeTokens.If && stateToken != TypeTokens.For &&
					stateToken != TypeTokens.Function && stateToken != TypeTokens.While &&
					stateToken != TypeTokens.Switch)
				{
					if (token.Value == "if") return this.ParseIfCommand();
					if (token.Value == "for") return this.ParseForCommand();
					if (token.Value == "enum") return this.ParseEnumCommand();
					if (token.Value == "while") return this.ParseWhileCommand();
					if (token.Value == "using") return this.ParseUsingCommand();

					if (token.Value == "class") return this.ParseClassCommand();
					if (token.Value == "switch") return this.ParseSwitchCommand();

					if (token.Value == "foreach") return this.ParseForEachCommand();
					if (token.Value == "function") return this.ParseFunctionCommand();

					// eliminar luego
					if (token.Value == "try") return this.ParseTryCommand();
				}
				if (stateToken == TypeTokens.If || stateToken == TypeTokens.For ||
					stateToken == TypeTokens.Function || stateToken == TypeTokens.While ||
					stateToken == TypeTokens.Switch || stateToken == TypeTokens.TryCatchFinally)
				{
					if (token.Value == "if") return this.ParseIfCommand();
					if (token.Value == "for") return this.ParseForCommand();
					if (token.Value == "while") return this.ParseWhileCommand();
					if (token.Value == "switch") return this.ParseSwitchCommand();
					if (token.Value == "try") return this.ParseTryCommand();

					// /////////////////////////////////////////////////////////////////////////
					if (token.Value == "break") return this.ParseBreakCommand();
					if (token.Value == "return") return this.ParseReturnCommand();
					if (token.Value == "continue") return this.ParseContinueCommand();
					// /////////////////////////////////////////////////////////////////////////

					if (token.Value == "foreach") return this.ParseForEachCommand();
					if (token.Value == "enum" || token.Value == "function" || token.Value == "class")
					{
						string msg = "Unexpected Token Exception: {0}\nLine: {1}\nCol: {2}";
						throw new Exception(string.Format(
							msg,
							token.Value,
							token.Line,
							(token.Col + this.lexer.PeekToken().Col) - token.Value.Length
						));
					}
				}
			}

			if (token.TokenType == TokenType.Separator && token.Value == "{") return this.ParseCompositeCommand(stateToken);
			this.lexer.PushToken(token);
			Stmt command = this.ParseSimpleCommand();

			////////////////////////////////////////////////////////////////////
			if (command == null)
			{
				string msg = "Unexpected Token Exception: {0}\nLine: {1}";
				throw new Exception(string.Format(msg, token.Value, token.Line));
			}
			////////////////////////////////////////////////////////////////////

			if (!this.TryParse(TokenType.Separator, ";"))
			{
				Token token2 = this.lexer.PeekLastToken();
				if (token2 != null)
				{
					string msg = "Se esperaba ;\nLine: {0}\nCol: {1}";
					throw new Exception(string.Format(msg, token2.Line, token2.Col));
				}
			}
			/////////////////////////////////////////////////////////////////////////
			this.Parse(TokenType.Separator, ";");

			//
			return command;
		}

		/// <summary>
		/// Parse Simple Command
		/// </summary>
		/// <returns></returns>
		private Stmt ParseSimpleCommand()
		{
			if (this.TryParse(TokenType.Name, "var"))
			{
				this.lexer.NextToken();
				//return this.ParseVarCommand();

				//
				return this.ParseVarCommands();
			}

			Expr expression = this.ParseExpression();
			if (expression == null) return null;

			//if (this.TryParse(TokenType.Operator, "="))
			//{
			//    this.lexer.NextToken();
			//    Stmt command = new SetCommand(expression, this.ParseExpression());

			//    //
			//    return command;
			//}

			//
			return new ExpressionCommand(expression);
		}

		/// <summary>
		/// Multtiple Command Execute
		/// </summary>
		/// <returns></returns>
		private Stmt ParseCompositeCommand(TypeTokens stateToken)
		{
			List<Stmt> commands = new List<Stmt>();
			while (!this.TryParse(TokenType.Separator, "}")) commands.Add(this.ParseCommand(stateToken));
			this.lexer.NextToken();

			//
			return new CompositeCommand(commands);
		}

		// Parse Var Command ////////////////////////////////////////////////////////////////////////////
		#region Parse Var Command

		/// <summary>
		/// Var Commands
		/// </summary>
		/// <example>
		/// <code>
		/// var a, b, c, d, e, f;
		/// var a = 10, b = 20, c = 30, d = 40, e = 60, f = 80;
		/// </code>
		/// </example>
		/// <returns></returns>
		public Stmt ParseVarCommands()
		{
			List<Stmt> commans = new List<Stmt>();
			commans.Add(this.ParseVarCommand());

			while (this.TryParse(TokenType.Separator, ","))
			{
				this.lexer.NextToken();
				commans.Add(this.ParseVarCommand());
			}

			//
			return new VarCompositeCommand(commans);
		}

		/// <summary>
		/// Var Command
		/// </summary>
		/// <example>
		/// <code>
		/// var a;
		/// var a = 10;
		/// </code>
		/// </example>
		/// <returns></returns>
		public Stmt ParseVarCommand()
		{
			string name = this.ParseName();
			Expr expression = null;

			if (this.TryParse(TokenType.Operator, "="))
			{
				this.lexer.NextToken();
				expression = this.ParseExpression();
			}
			if (expression == null) expression = new NullExpression();

			//
			return new VarCommand(name, expression);
		}
		
		#endregion

		/// <summary>
		/// If Command
		/// </summary>
		/// <returns></returns>
		private Stmt ParseIfCommand()
		{
			this.Parse(TokenType.Separator, "(");
			Expr condition = this.ParseExpression();
			this.Parse(TokenType.Separator, ")");
			Stmt thencmd = this.ParseCommand(TypeTokens.If);

			if (!this.TryParse(TokenType.Name, "else")) return new IfCommand(condition, thencmd);

			this.lexer.NextToken();
			Stmt elsecmd = this.ParseCommand(TypeTokens.If);

			//
			return new IfCommand(condition, thencmd, elsecmd);
		}

		/// <summary>
		/// For Command
		/// </summary>
		/// <returns></returns>
		private Stmt ParseForCommand()
		{
			this.Parse(TokenType.Separator, "(");
			Stmt initial = this.ParseSimpleCommand();
			this.Parse(TokenType.Separator, ";");

			Expr condition = this.ParseExpression();
			this.Parse(TokenType.Separator, ";");

			Stmt endcmd = this.ParseSimpleCommand();
			this.Parse(TokenType.Separator, ")");

			Stmt command = this.ParseCommand(TypeTokens.For);

			//
			return new ForCommand(initial, condition, endcmd, command);
		}

		/// <summary>
		/// ForEach Command
		/// </summary>
		/// <returns></returns>
		private Stmt ParseForEachCommand()
		{
			this.Parse(TokenType.Separator, "(");
			string name = this.ParseName();
			bool localvar = false;

			if (name == "var")
			{
				localvar = true;
				name = this.ParseName();
			}

			this.Parse(TokenType.Name, "in");
			Expr values = this.ParseExpression();
			this.Parse(TokenType.Separator, ")");
			Stmt command = this.ParseCommand(TypeTokens.For);

			//
			return new ForEachCommand(name, values, command, localvar);
		}

		/// <summary>
		/// While Command
		/// </summary>
		/// <returns></returns>
		private Stmt ParseWhileCommand()
		{
			this.Parse(TokenType.Separator, "(");
			Expr condition = this.ParseExpression();
			this.Parse(TokenType.Separator, ")");
			Stmt command = this.ParseCommand(TypeTokens.While);

			//
			return new WhileCommand(condition, command);
		}

		/// <summary>
		/// Using Command
		/// </summary>
		/// <returns></returns>
		private Stmt ParseUsingCommand()
		{
			// example using com.handstudios.IO;
			string name = ParseQualifiedName();
			this.Parse(TokenType.Separator, ";");

			//
			return new UsingCommand(name);
		}

		// Parse Loops Command ///////////////////////////////////////////////////////////////////////////////
		#region Loops Command

		/// <summary>
		/// Break Command
		/// </summary>
		/// <returns></returns>
		private Stmt ParseBreakCommand()
		{
			if (this.TryParse(TokenType.Separator, ";"))
			{
				this.lexer.NextToken();
				return new BreakCommand();
			}
			else
			{
				Token last = this.lexer.PeekLastToken();
				string msg = "Se esperaba ;\nLine: {0}";
				throw new Exception(string.Format(msg, last.Line));
			}
		}

		/// <summary>
		/// Return Command
		/// </summary>
		/// <returns></returns>
		private Stmt ParseReturnCommand()
		{
			if (this.TryParse(TokenType.Separator, ";"))
			{
				this.lexer.NextToken();
				return new ReturnCommand();
			}

			Expr expression = this.ParseExpression();
			this.Parse(TokenType.Separator, ";");

			//
			return new ReturnCommand(expression);
		}

		/// <summary>
		/// Continue Command
		/// </summary>
		/// <returns></returns>
		private Stmt ParseContinueCommand()
		{
			if (this.TryParse(TokenType.Separator, ";"))
			{
				this.lexer.NextToken();
				return new ContinueCommand();
			}
			else
			{
				Token last = this.lexer.PeekLastToken();
				string msg = "Se esperaba ;\nLine: {0}";
				throw new Exception(string.Format(msg, last.Line));
			}
		}

		#endregion

		// Parse Switch Command ///////////////////////////////////////////////////////////////////////////////
		#region Parse Switch Command

		/// <summary>
		/// Switch Command
		/// </summary>
		/// <returns></returns>
		private Stmt ParseSwitchCommand()
		{
			List<Stmt> body = new List<Stmt>();
			this.Parse(TokenType.Separator, "(");
			Expr condition = this.ParseExpression();
			this.Parse(TokenType.Separator, ")");

			this.Parse(TokenType.Separator, "{");

			///////////////////////////////////////////////////////////////////////
			while (this.TryParse(TokenType.Name, "case"))
			{
				this.lexer.NextToken();
				Expr condision = this.ParseExpression();
				this.Parse(TokenType.Separator, ":");

				Stmt command = this.CompositeCommandCase();
				body.Add(new CaseCommand(condision, command));
			}
			if (this.TryParse(TokenType.Name, "default"))
			{
				this.lexer.NextToken();
				this.Parse(TokenType.Separator, ":");

				Stmt command = this.CompositeCommandCase();
				body.Add(new DefaultCommand(command));
			}
			///////////////////////////////////////////////////////////////////////

			this.Parse(TokenType.Separator, "}");

			//
			return new SwitchCommand(condition, body);
		}

		private Stmt CompositeCommandCase()
		{
			List<Stmt> commands = new List<Stmt>();
			while (!this.TryParse(TokenType.Name, "return", "break"))
			{
				commands.Add(this.ParseCommand(TypeTokens.While));
			}
			commands.Add(this.ParseCommand(TypeTokens.While));

			//
			return new CompositeCommand(commands);
		}

		#endregion

		// Parse Try, Catch and Finally Command ///////////////////////////////////////////////////////////////////////////////
		#region Parse Try Command

		private Stmt CompositeTryCommand()
		{
			List<Stmt> commands = new List<Stmt>();
			while (!this.TryParse(TokenType.Separator, "}"))
			{
				commands.Add(this.ParseCommand(TypeTokens.TryCatchFinally));
			}
			this.lexer.NextToken();

			//
			return new TryCommandFolder.TryCommandComposite(commands);
		}

		private Stmt ParseTryCommand()
		{
			this.Parse(TokenType.Separator, "{");

			Stmt tryBody = null;
			Stmt catchCommand = null;
			Stmt finallyCommand = null;

			tryBody = CompositeTryCommand();
			if (this.TryParse(TokenType.Name, "catch"))
			{
				this.Parse(TokenType.Name, "catch");
				this.Parse(TokenType.Separator, "(");
				Stmt varCommand = new VarCommand(this.ParseName(), null);
				this.Parse(TokenType.Separator, ")");

				if (this.TryParse(TokenType.Separator, "{"))
				{
					this.Parse(TokenType.Separator, "{");
					Stmt catchBody = CompositeTryCommand();
					catchCommand = new TryCommandFolder.CatchCommand(varCommand, catchBody);
				}
			}
			if (this.TryParse(TokenType.Name, "finally"))
			{
				this.Parse(TokenType.Name, "finally");
				if (this.TryParse(TokenType.Separator, "{"))
				{
					this.Parse(TokenType.Separator, "{");
					Stmt finallyBody = CompositeTryCommand();
					finallyCommand = new TryCommandFolder.FinallyCommand(finallyBody);
				}
			}

			return new TryCommandFolder.TryCommand(tryBody, catchCommand, finallyCommand);
		}

		#endregion

		// Parse Class Command ///////////////////////////////////////////////////////////////////////////////
		#region Parse Class Command

		private Stmt ParseClassCommand()
		{
			string name = this.ParseName();
			List<string> memberNames = new List<string>();
			List<Expr> memberExpressions = new List<Expr>();

			this.Parse(TokenType.Separator, "{");
			while (this.TryParse(TokenType.Name, "var", "function"))
			{
				Token token = this.lexer.NextToken();

				if (token.Value == "var") this.ParseMemberVariable(memberNames, memberExpressions);
				else if (token.Value == "function") this.ParseMemberMethod(memberNames, memberExpressions);
				else
					throw new Exception("Unexpected Token Exception: " + token.Value);
			}
			this.Parse(TokenType.Separator, "}");

			//
			return new DefineClassCommand(name, memberNames.ToArray(), memberExpressions);
		}

		private void ParseMemberVariable(List<string> memberNames, List<Expr> memberExpressions)
		{
			string name = this.ParseName();
			Expr expression = null;
			if (this.TryParse(TokenType.Operator, "="))
			{
				this.lexer.NextToken();
				expression = this.ParseExpression();
			}
			if (expression == null) expression = new NullExpression();

			this.Parse(TokenType.Separator, ";");
			memberNames.Add(name);
			memberExpressions.Add(expression);
		}

		private void ParseMemberMethod(IList<string> memberNames, List<Expr> memberExpressions)
		{
			string name = this.ParseName();
			bool hasvariableparameters = false;
			string[] parameterNames = this.ParseParameters(ref hasvariableparameters);
			Stmt body = this.ParseCommand(TypeTokens.Class);

			memberNames.Add(name);
			memberExpressions.Add(new FunctionExpression(parameterNames, body, hasvariableparameters));
		}

		private Expr ParseNewExpression()
		{
			this.lexer.NextToken();
			if (this.TryPeekName())
			{
				string typename = this.ParseQualifiedName();
				List<Expr> arguments = this.ParseArgumentList();
				Expr expression = new NewExpression(typename, arguments);

				if (!this.TryParse(TokenType.Separator, "{")) return expression;
			}

			throw new Exception("Invalid arguments");
		}

		#endregion

		// Parse Function Command ////////////////////////////////////////////////////////////////////////////
		#region Parse Function Command

		/// <summary>
		/// Function Command
		/// </summary>
		/// <returns></returns>
		private Stmt ParseFunctionCommand()
		{
			string name = this.ParseName();
			bool hasvariableparameters = false;
			string[] parameterNames = this.ParseParameters(ref hasvariableparameters);
			Stmt body = this.ParseCommand(TypeTokens.Function);

			//
			return new DefineFunctionCommand(name, parameterNames, body, hasvariableparameters);
		}

		/// <summary>
		/// Function Expression
		/// </summary>
		/// <returns></returns>
		private Expr ParseFunctionExpression()
		{
			bool hasvariableparameters = false;
			this.lexer.NextToken();
			string[] parameterNames = this.ParseParameters(ref hasvariableparameters);
			Stmt body = this.ParseCommand();

			//
			return new FunctionExpression(parameterNames, body, hasvariableparameters);
		}

		/// <summary>
		/// Argument Functions
		/// </summary>
		/// <returns></returns>
		private List<Expr> ParseArgumentList()
		{
			List<Expr> expressions = new List<Expr>();

			this.Parse(TokenType.Separator, "(");
			while (!this.TryParse(TokenType.Separator, ")"))
			{
				if (expressions.Count > 0) this.Parse(TokenType.Separator, ",");
				expressions.Add(this.ParseExpression());
			}
			this.Parse(TokenType.Separator, ")");

			//
			return expressions;
		}

		/// <summary>
		/// Parameters Names Functions
		/// </summary>
		/// <returns></returns>
		private string[] ParseParameters(ref bool hasvariableparameters)
		{
			List<string> names = new List<string>();

			this.Parse(TokenType.Separator, "(");
			while (!this.TryParse(TokenType.Separator, ")"))
			{
				if (names.Count > 0) this.Parse(TokenType.Separator, ",");
				names.Add(this.ParseName());

				if (this.TryParse(TokenType.Operator, "..."))
				{
					this.lexer.NextToken();
					this.Parse(TokenType.Separator, ")");
					hasvariableparameters = true;
					//
					return names.ToArray();
				}
			}

			this.lexer.NextToken();
			return names.ToArray();
		}

		///// <summary>
		///// Return Command
		///// </summary>
		///// <returns></returns>
		//private Stmt ParseReturnCommand()
		//{
		//    if (this.TryParse(TokenType.Separator, ";"))
		//    {
		//        this.lexer.NextToken();
		//        return new ReturnCommand();
		//    }

		//    Expr expression = this.ParseExpression();
		//    this.Parse(TokenType.Separator, ";");

		//    return new ReturnCommand(expression);
		//}

		#endregion

		// Parse Array Command ////////////////////////////////////////////////////////////////////////////
		#region Parse Array Commnad

		/// <summary>
		/// Get Array Argument 
		/// </summary>
		/// <returns></returns>
		private Expr ParseArrayArgument()
		{
			this.Parse(TokenType.Separator, "[");
			Expr expressions = this.ParseExpression();
			this.Parse(TokenType.Separator, "]");

			//
			return expressions;
		}

		/// <summary>
		/// Get Array Argument 
		/// </summary>
		/// <returns></returns>
		private List<Expr> ParseArrayArgumentList()
		{
			List<Expr> expressions = new List<Expr>();

			this.Parse(TokenType.Separator, "{");
			while (!this.TryParse(TokenType.Separator, "}"))
			{
				if (expressions.Count > 0)
				{
					if (!this.TryParse(TokenType.Separator, ","))
					{
						Token token = this.lexer.NextToken();
						throw new Exception(
							"Error: " + token.Value +
							"\nLine: " + token.Line +
							"\nCol: " + (token.Col + 3)
						);
					}
					this.Parse(TokenType.Separator, ",");
				}
				if (this.TryParse(TokenType.Separator, ","))
				{
					Token token = this.lexer.PeekToken();
					string msg = "El término de la expresión '{0}' no es válido\nLine: {1}\nCol: {2}";
					throw new Exception(string.Format(
						msg,
						token.Value,
						token.Line,
						(token.Col + token.Value.Length + 2)));
				}
				if (this.TryParse(TokenType.Separator, "}"))
				{
					Token token = this.lexer.PeekLastToken();
					throw new Exception(
						"Error: " + token.Value +
						"\nLine: " + token.Line +
						"\nCol: " + (token.Col + token.Value.Length + 1)
					);
				}

				expressions.Add(this.ParseExpression());
			}
			this.Parse(TokenType.Separator, "}");

			//
			return expressions;
		}

		#endregion

		// Parse Enum Command ////////////////////////////////////////////////////////////////////////////
		#region Enum Command

		private Stmt ParseEnumCommand()
		{
			string name = this.ParseName();
			string[] methodesNames = this.ParseEnumMethode();

			//
			return new DefineEnumCommand(name, methodesNames);
		}

		private string[] ParseEnumMethode()
		{
			List<string> names = new List<string>();

			this.Parse(TokenType.Separator, "{");
			while (!this.TryParse(TokenType.Separator, "}"))
			{
				if (names.Count > 0)
				{
					if (!this.TryParse(TokenType.Separator, ","))
					{
						Token token = this.lexer.NextToken();
						throw new Exception(
							"Error: " + token.Value +
							"\nLine: " + token.Line +
							"\nCol: " + (token.Col + 3)
						);
					}
					this.Parse(TokenType.Separator, ",");
				}
				if (this.TryParse(TokenType.Separator, ","))
				{
					Token token = this.lexer.PeekToken();
					string msg = "El término de la expresión '{0}' no es válido\nLine: {1}\nCol: {2}";
					throw new Exception(string.Format(
						msg,
						token.Value,
						token.Line,
						(token.Col + token.Value.Length + 2)));
				}
				if (this.TryParse(TokenType.Separator, "}"))
				{
					Token token = this.lexer.PeekLastToken();
					throw new Exception(
						"Error: " + token.Value +
						"\nLine: " + token.Line +
						"\nCol: " + (token.Col + token.Value.Length + 1)
					);
				}

				names.Add(this.ParseName());
			}

			//
			this.lexer.NextToken();
			return names.ToArray();
		}

		#endregion

		// Parse Expression ///////////////////////////////////////////////////////////////////////////////
		#region Parse Expression

		public Expr ParseExpression()
		{
			if (this.TryParse(TokenType.Name, "new")) return this.ParseNewExpression();
			if (this.TryParse(TokenType.Separator, "{")) return new ArrayExpression(this.ParseArrayArgumentList());

			//
			return this.ParseBinaryLogicalExpressionLevelOne();
		}

		private Expr ParseBinaryLogicalExpressionLevelOne()
		{
			Expr expression = this.ParseBinaryLogicalExpressionLevelTwo();
			if (expression == null) return null;

			while (this.TryParse(TokenType.Operator, "||"))
			{
				Token oper = this.lexer.NextToken();
				Expr right = this.ParseBinaryLogicalExpressionLevelTwo();

				//
				expression = new OrExpression(expression, right);
			}

			//
			return expression;
		}

		private Expr ParseBinaryLogicalExpressionLevelTwo()
		{
			Expr expression = this.ParseBinaryExpressionZerothLevel();
			if (expression == null) return null;

			while (this.TryParse(TokenType.Operator, "&&"))
			{
				Token oper = this.lexer.NextToken();
				Expr right = this.ParseBinaryExpressionZerothLevel();

				//
				expression = new AndExpression(expression, right);
			}

			//
			return expression;
		}

		// fix 1
		private Expr ParseBinaryExpressionZerothLevel()
		{
			Token mToken = this.lexer.PeekToken();
			Token mTokenLast = this.lexer.PeekLastToken();
			Expr expression = this.ParseBinaryExpressionFirstLevel();
			if (expression == null) return null;

			while (this.TryParse(TokenType.Operator, "<", ">", "==", ">=", "<=", "!="))
			{
				Token oper = this.lexer.NextToken();
				Expr right = this.ParseBinaryExpressionFirstLevel();

				ComparisonOperator op = 0;

				if (oper.Value == "<") op = ComparisonOperator.Less;
				if (oper.Value == ">") op = ComparisonOperator.Greater;
				if (oper.Value == "<=") op = ComparisonOperator.LessEqual;
				if (oper.Value == ">=") op = ComparisonOperator.GreaterEqual;
				if (oper.Value == "==") op = ComparisonOperator.Equal;
				if (oper.Value == "!=") op = ComparisonOperator.NotEqual;

				//
				expression = new CompareExpression(op, expression, right);
			}

			// if logico
			if (this.TryParse(TokenType.Separator, "?"))
			{
				Token oper = this.lexer.NextToken();
				Expr left = this.ParseExpression();

				if (!this.TryParse(TokenType.Separator, ":"))
				{
					Token token = this.lexer.PeekToken();
					if (token != null)
					{
						string msg = "Se esperaba :\nLine: {0}\nCol: {1}";
						throw new Exception(string.Format(msg, token.Line, token.Col));
					}
				}

				this.lexer.NextToken();
				Expr right = this.ParseExpression();

				//
				return new IFLogicExpression(expression, left, right);
			}
			
			////////////////////////////////////////////////////////////
			// Equal '=' Expression
			////////////////////////////////////////////////////////////
			if (this.TryParse(TokenType.Operator, "="))
			{
				int col = mTokenLast.TokenType == TokenType.Separator ? 1 : 0;
				Token newToken = new Token()
				{
					Value = mToken.Value,
					Line = mToken.Line,
					Col = mToken.Col - col
				};
				Token oper = this.lexer.NextToken();
				Expr right = this.ParseExpression();

				////////////////////////////////////////////////////////////
				if (!this.TryParse(TokenType.Separator, ";"))
				{
					Token token = this.lexer.PeekLastToken();
					if (mToken != null)
					{
						string msg = "Se esperaba una expresion\nLine: {0}\nCol: {1}";
						throw new Exception(string.Format(msg, mToken.Line, oper.Col));
					}
				}
				////////////////////////////////////////////////////////////

				//
				return new SetExpression(expression, right, newToken);
			}
			////////////////////////////////////////////////////////////

			//
			return expression;
		}

		private Expr ParseBinaryExpressionFirstLevel()
		{
			Expr expression = this.ParseBinaryExpressionSecondLevel();
			if (expression == null) return null;

			while (this.TryParse(TokenType.Operator, "+", "-"))
			{
				Token oper = this.lexer.NextToken();
				Expr right = this.ParseBinaryExpressionSecondLevel();
				ArithmeticOperator op = oper.Value == "+" ? ArithmeticOperator.Add : ArithmeticOperator.Subtract;

				//
				expression = new ArithmeticBinaryExpression(op, expression, right);
			}

			//
			return expression;
		}

		private Expr ParseBinaryExpressionSecondLevel()
		{
			Expr expression = this.ParseUnaryExpression();
			if (expression == null) return null;

			while (this.TryParse(TokenType.Operator, "*", "/", "%", "^"))
			{
				Token oper = this.lexer.NextToken();
				Expr right = this.ParseUnaryExpression();
				ArithmeticOperator op;

				if (oper.Value == "*") op = ArithmeticOperator.Multiply;
				else if (oper.Value == "/") op = ArithmeticOperator.Divide;
				else if (oper.Value == "%") op = ArithmeticOperator.Modulo;
				else if (oper.Value == "^") op = ArithmeticOperator.Pow;
				else
				{
					string msg = "Invalid operator '{0}'\nLine: {1}\nCol: {2}";
					throw new Exception(string.Format(msg, oper.Value, oper.Line, oper.Col));
				}

				//
				expression = new ArithmeticBinaryExpression(op, expression, right);
			}

			//
			return expression;
		}

		// fix 1/ (+/-)
		private Expr ParseUnaryExpression()
		{
			if (this.TryParse(TokenType.Name, "typeof"))
			{
				this.lexer.NextToken();
				Expr expression = this.ParseUnaryExpression();

				//
				return new TypeofExpression(expression);
			}
			
			/////////////////////////////////////////////////////////////////////////////

			if (this.TryParse(TokenType.Operator, "!", "+", "-"))
			{
				Token oper = this.lexer.NextToken();
				Expr unaryExpression = this.ParseUnaryExpression();
				if (oper.Value == "!") return new NotExpression(unaryExpression);

				//
				ArithmeticOperator op = oper.Value == "+" ? ArithmeticOperator.Plus : ArithmeticOperator.Minus;
				return new ArithmeticUnaryExpression(op, unaryExpression);
			}

			Expr termexpr = this.ParseTermExpression();

			if (this.TryParse(TokenType.Operator, "++", "--", "+=", "-=", "/=", "*=", "%="))
			{
				Expr right = null;
				Token oper = this.lexer.NextToken();
				IncrementOperator op = IncrementOperator.none;

				switch (oper.Value)
				{
					case "++":
						op = IncrementOperator.Increment;
						break;
					case "--":
						op = IncrementOperator.Decrement;
						break;
					case "+=":
						right = this.ParseUnaryExpression();
						op = IncrementOperator.IncrementEqual;
						break;
					case "-=":
						right = this.ParseUnaryExpression();
						op = IncrementOperator.DecrementEqual;
						break;
					case "/=":
						right = this.ParseUnaryExpression();
						op = IncrementOperator.DivideEqual;
						break;
					case "*=":
						right = this.ParseUnaryExpression();
						op = IncrementOperator.MultiplyEqual;
						break;
					case "%=":
						right = this.ParseUnaryExpression();
						op = IncrementOperator.ModuloEqual;
						break;
				}

				//
				return new ArithmeticAvancedExpression(op, termexpr, right);
			}

			//
			return termexpr;
		}

		private Expr ParseTermExpression()
		{
			Expr expression = this.ParseSimpleTermExpression();

			//
			while (this.TryParse(TokenType.Operator, ".") || this.TryParse(TokenType.Separator, "["))
			{
				if (this.TryParse(TokenType.Operator, "."))
				{
					Token t = this.lexer.PeekLastToken();
					this.lexer.NextToken();
					string name = this.ParseName();
					List<Expr> arguments = null;

					if (this.TryParse(TokenType.Separator, "(")) arguments = this.ParseArgumentList();

					//
					expression = new DotExpression(expression, t.Value, name, arguments);
				}
				else
				{
					List<Expr> temp = new List<Expr>();
					temp.Add(this.ParseArrayArgument());
					//
					// Retornamos el valor del indice del array.
					expression = new InvokeArrayExpression(expression, temp);
				}
			}

			//
			return expression;
		}

		private Expr ParseSimpleTermExpression()
		{
			Token token = this.lexer.NextToken();
			if (token == null) return null;

			switch (token.TokenType)
			{
				case TokenType.Separator:
					if (token.Value == "(")
					{
						Expr expr = this.ParseExpression();
						this.Parse(TokenType.Separator, ")");

						//
						return expr;
					}
					break;
				case TokenType.Boolean: return new ConstantExpression((bool)Convert.ToBoolean(token.Value));
				case TokenType.Integer: return new ConstantExpression((int)int.Parse(token.Value));
				case TokenType.Real: return new ConstantExpression((double)double.Parse(token.Value.Replace(".", ",")));
				case TokenType.String:
					List<string> parts = StringUtilities.SplitText(token.Value);
					if (parts.Count == 1) return new ConstantExpression(token.Value);
					Expr strexpr = new ConstantExpression(parts[0]);

					for (int k = 1; k < parts.Count; k++)
					{
						if ((k % 2) == 0) strexpr = new ConcatenateExpression(strexpr, new ConstantExpression(parts[k]));
						else
						{
							Parser parser = new Parser(parts[k]);
							strexpr = new ConcatenateExpression(strexpr, parser.ParseExpression());
						}
					}

					return strexpr;
				case TokenType.Null: return new NullExpression();
				case TokenType.Name:
					if (this.TryParse(TokenType.Separator, "("))
					{
						List<Expr> arguments = this.ParseArgumentList();
						//
						return new InvokeExpression(token.Value, arguments);
					}

					//
					return new VariableExpression(token.Value);
			}

			////////////////////////////////////////////////////////////////////
			string msg = "Unexpected Token Exception: {0}\nLine: {1}\nCol: {2}";
			throw new Exception(string.Format(msg, token.Value, token.Line, token.Col));
		}

		#endregion

		// Parse Command Tools ////////////////////////////////////////////////////////////////////////////
		#region Parse Command Tools

		private bool TryPeekName()
		{
			Token token = this.lexer.PeekToken();
			if (token == null) return false;

			//
			return token.TokenType == TokenType.Name;
		}

		private object ParseValue()
		{
			Token token = this.lexer.NextToken();
			if (token == null) throw new Exception("Unexpected End Of Input Exception");

			if (token.TokenType == TokenType.String) return token.Value;
			if (token.TokenType == TokenType.Integer) return int.Parse(token.Value);

			string msg = "Unexpected Token Exception: {0}\nLine: {1}";
			throw new Exception(string.Format(msg, token.Value, token.Line));
		}

		private bool TryParse(TokenType type, params string[] values)
		{
			Token token = this.lexer.PeekToken();
			if (token == null) return false;

			if (type == TokenType.Name)
			{
				foreach (string value in values)
					if (IsName(token, value)) return true;

				//
				return false;
			}
			if (token.TokenType == type)
			{
				foreach (string value in values)
					if (token.Value == value) return true;
			}

			//
			return false;
		}

		private void Parse(TokenType type, string value)
		{
			Token token = this.lexer.NextToken();
			if (token == null) throw new Exception("Unexpected End Of Input Exception");

			if (type == TokenType.Name)
			{
				if (IsName(token, value)) return;
				else
				{
					string msg = "Unexpected Token Exception: {0}\nLine: {1}";
					throw new Exception(string.Format(msg, token.Value, token.Line));
				}
			}

			if (token.TokenType != type || token.Value != value)
			{
				string msg = "Unexpected Token Exception: {0}\nLine: {1}";
				throw new Exception(string.Format(msg, token.Value, token.Line));
			}
		}

		/* ORDER RESERVED */
		private string[] nameReservet = new string[] {
			"for", "if", "class",
			"function", "break", "var",
			"return", "switch", "case", "default"
		};

		private string ParseName()
		{
			Token token = this.lexer.NextToken();
			//if (nameReservet.Contains(token.Value))
			//{
			//    string msg2 = "Se esperaba un identificador; '{0}' es una palabra clave";
			//    throw new Exception(string.Format(msg2, token.Value));
			//}
			if (token == null) throw new Exception(string.Format("Unexpected end of input"));
			if (token.TokenType == TokenType.Name) return token.Value;

			string msg = "Unexpected Token Exception: {0}\nLine: {1}";
			throw new Exception(string.Format(msg, token.Value, token.Line));
		}

		// fix 1
		private Token ParseTokenName()
		{
			Token token = this.lexer.NextToken();
			if (token == null) throw new Exception(string.Format("Unexpected end of input"));
			if (token.TokenType == TokenType.Name) return token;

			string msg = "Unexpected Token Exception: {0}\nLine: {1}";
			throw new Exception(string.Format(msg, token.Value, token.Line));
		}

		private string ParseQualifiedName()
		{
			string name = this.ParseName();
			while (this.TryParse(TokenType.Operator, "."))
			{
				this.lexer.NextToken();
				name += "." + this.ParseName();
			}

			//
			return name;
		}

		private static bool IsName(Token token, string value)
		{
			return IsToken(token, value, TokenType.Name);
		}

		private static bool IsToken(Token token, string value, TokenType type)
		{
			if (token == null) return false;
			if (token.TokenType != type) return false;
			if (type == TokenType.Name) return token.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase);

			//
			return token.Value.Equals(value);
		}

		#endregion
	}
}
