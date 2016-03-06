using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public abstract class BinaryExpression : Expr
	{
		private Expr leftExpression;
		private Expr rigthExpression;

		public BinaryExpression(Expr left, Expr right)
		{
			this.leftExpression = left;
			this.rigthExpression = right;
		}

		public abstract object Apply(object leftValue, object rightValue);

		public object evaluate(IBindingEnvironment environment)
		{
			object leftValue = this.leftExpression.evaluate(environment);
			object rightValue = this.rigthExpression.evaluate(environment);

			return this.Apply(leftValue, rightValue);
		}

		public Expr LeftExpression { get { return this.leftExpression; } }
		public Expr RightExpression { get { return this.rigthExpression; } }
	}
}
