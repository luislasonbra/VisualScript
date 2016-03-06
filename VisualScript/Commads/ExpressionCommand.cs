using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	public class ExpressionCommand : Stmt
	{
		private Expr expression;

		public ExpressionCommand(Expr expression)
		{
			this.expression = expression;
		}

		public void execute(IBindingEnvironment environment)
		{
			this.expression.evaluate(environment);
		}

		public Expr Expression { get { return this.expression; } }
	}
}
