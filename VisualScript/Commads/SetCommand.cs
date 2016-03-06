using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

namespace VisualScript.Commads
{
	public class SetCommand : Stmt
	{
		private Expr leftValue;
		private Expr expression;

		public SetCommand(Expr leftValue, Expr expression)
		{
			this.leftValue = leftValue;
			this.expression = expression;
		}

		public void execute(IBindingEnvironment environment)
		{
			object valueLeft = this.leftValue.evaluate(environment);
			if (!(this.leftValue is VariableExpression) || valueLeft is ICallableClass)
			{
				string msg = "La parte izquierda de una asignación debe ser una variable";
				throw new Exception(string.Format(msg));
			}
			if (this.leftValue is VariableExpression)
			{
				string name = ((VariableExpression)this.leftValue).VariableName;
				if (!environment.ContainsName(name))
				{
					string msg = "El nombre '{0}' no existe en el contexto actual";
					throw new Exception(string.Format(msg, name));
				}
			}

			object value = this.expression.evaluate(environment);
			ExpressionUtilities.SetValue(this.leftValue, value, environment);
		}

		public Expr LeftValue { get { return this.leftValue; } }
		public Expr Expression { get { return this.expression; } }
	}
}
