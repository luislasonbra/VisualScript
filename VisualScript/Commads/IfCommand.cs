using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	public class IfCommand : Stmt
	{
		private Expr condition;
		private Stmt thenCommand;
		private Stmt elseCommand;

		public IfCommand(Expr condition, Stmt thenCommand) : this(condition, thenCommand, null) { }

		public IfCommand(Expr condition, Stmt thenCommand, Stmt elseCommand)
		{
			this.condition = condition;
			this.thenCommand = thenCommand;
			this.elseCommand = elseCommand;
		}

		public void execute(IBindingEnvironment environment)
		{
			object result = this.condition.evaluate(environment);

			if (Predicates.IsTrue(result)) this.thenCommand.execute(environment);
			else if (this.elseCommand != null)
				this.elseCommand.execute(environment);
		}

		public Expr Condition { get { return this.condition; } }
		public Stmt ThenCommand { get { return this.thenCommand; } }
		public Stmt ElseCommand { get { return this.elseCommand; } }
	}
}
