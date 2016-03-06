using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	public class ForCommand : Stmt
	{
		private Stmt body;
		private Expr condition;
		private Stmt endCommand;
		private Stmt initialCommand;

		public ForCommand(Stmt initialCommand, Expr condition, Stmt endCommand, Stmt body)
		{
			this.body = body;
			this.condition = condition;
			this.endCommand = endCommand;
			this.initialCommand = initialCommand;
		}

		private object start = 0;
		public void execute(IBindingEnvironment environment)
		{
			IBindingEnvironment newenv = new LocalBindingEnvironment(environment);
			if (this.initialCommand != null) this.initialCommand.execute(newenv);
			while (this.condition == null || Predicates.IsTrue(this.condition.evaluate(newenv)))
			{
				if (this.body != null) this.body.execute(newenv);
				if (this.endCommand != null) this.endCommand.execute(newenv);
			}
			Machine.CurrentLoopStatus.isBreak = false;
		}

		public Stmt Body { get { return this.body; } }
		public Expr Condition { get { return this.condition; } }
		public Stmt EndCommand { get { return this.endCommand; } }
		public Stmt InitialCommand { get { return this.initialCommand; } }
	}
}
