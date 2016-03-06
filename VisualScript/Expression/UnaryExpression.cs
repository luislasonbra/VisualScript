using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public abstract class UnaryExpression : Expr
	{
		private Expr expression;

		public UnaryExpression(Expr expression)
		{
			this.expression = expression;
		}

		public abstract object Apply(object value);

		#region IExpression Members

		public virtual object evaluate(IBindingEnvironment environment)
		{
			return this.Apply(this.expression.evaluate(environment));
		}

		#endregion

		public Expr Expression { get { return this.expression; } }
	}
}
