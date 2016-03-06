using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	public class CaseCommand : Stmt
	{
		private Stmt command;
		private Expr expression;

		public CaseCommand(Expr expression, Stmt command)
		{
			this.command = command;
			this.expression = expression;
		}

		public void execute(IBindingEnvironment environment)
		{
			command.execute(environment);
		}

		public Stmt Command { get { return this.command; } }
		public Expr Expression { get { return this.expression; } }
	}
}
