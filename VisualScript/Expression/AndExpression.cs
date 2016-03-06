using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class AndExpression : Expr
	{
		private Expr leftExpression;
		private Expr rigthExpression;

		public AndExpression(Expr left, Expr right)
		{
			this.leftExpression = left;
			this.rigthExpression = right;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			object leftValue = this.leftExpression.evaluate(environment);
			if (Predicates.IsFalse(leftValue)) return false;

			//
			return Predicates.IsTrue(this.rigthExpression.evaluate(environment));
		}

		public Expr LeftExpression { get { return this.leftExpression; } }
		public Expr RightExpression { get { return this.rigthExpression; } }
	}
}
