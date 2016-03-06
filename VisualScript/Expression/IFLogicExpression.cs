using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class IFLogicExpression : Expr
	{
		private Expr left;
		private Expr right;
		private Expr condition;

		public IFLogicExpression(Expr condition, Expr left, Expr right)
		{
			this.left = left;
			this.right = right;
			this.condition = condition;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			object result = this.condition.evaluate(environment);
			//
			if (Predicates.IsTrue(result)) return left.evaluate(environment);
			else
				return right.evaluate(environment);
		}

		public Expr Condition { get { return this.condition; } }
		public Expr LeftExpression { get { return this.left; } }
		public Expr RightExpression { get { return this.right; } }
	}
}
