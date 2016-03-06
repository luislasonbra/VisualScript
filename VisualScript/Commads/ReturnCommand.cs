using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	public class ReturnCommand : Stmt
	{
		private Expr expression;

		public ReturnCommand() : this(null) { }

		public ReturnCommand(Expr expression)
		{
			this.expression = expression;
		}

		public void execute(IBindingEnvironment environment)
		{
			if (this.expression != null)
				Machine.CurrentFunctionStatus.ReturnValue = this.expression.evaluate(environment);

			//
			Machine.CurrentFunctionStatus.Returned = true;
		}

		public Expr Expression { get { return this.expression; } }
	}
}
