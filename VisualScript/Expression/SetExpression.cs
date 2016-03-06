using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class SetExpression : Expr
	{
		private Token token;
		private Expr leftValue;
		private Expr expression;

		public SetExpression(Expr leftValue, Expr expression, Token token)
		{
			this.token = token;
			this.leftValue = leftValue;
			this.expression = expression;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			if (this.leftValue is VariableExpression)
			{
				string name = ((VariableExpression)this.leftValue).VariableName;
				if (!environment.ContainsName(name))
				{
					string msg = "El nombre '{0}' no existe en el contexto actual";
					throw new Exception(string.Format(msg, name));
				}

				//
				object obj = this.leftValue.evaluate(environment);
				if (obj is ICallable || obj is IReadOnly)
				{
					string msg = "La parte izquierda de una asignación debe ser una variable\nLine: {0}";
					throw new Exception(string.Format(msg, token.Line));
				}
			}

			object value = this.expression.evaluate(environment);
			ExpressionUtilities.SetValue(this.leftValue, value, environment);

			//
			return value;
		}

		public Token Token { get { return this.token; } }
		public Expr LeftValue { get { return this.leftValue; } }
		public Expr Expression { get { return this.expression; } }
	}
}
