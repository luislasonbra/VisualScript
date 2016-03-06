using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class CompositeExpression : Expr
	{
		private List<Expr> exprs;

		public CompositeExpression(List<Expr> expressions)
		{
			this.exprs = expressions;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			for (int i = 0; i < this.exprs.Count; i++)
			{
				this.exprs[i].evaluate(environment);
			}

			//
			return null;
		}

		public List<Expr> Expressions { get { return this.exprs; } }
	}
}
