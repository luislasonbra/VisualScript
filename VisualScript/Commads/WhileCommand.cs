using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	public class WhileCommand : Stmt
	{
		private Stmt command;
		private Expr condition;

		public WhileCommand(Expr condition, Stmt command)
		{
			this.command = command;
			this.condition = condition;
		}

		public void execute(IBindingEnvironment environment)
		{
			IBindingEnvironment newenv = new LocalBindingEnvironment(environment);
			while (Predicates.IsTrue(this.condition.evaluate(newenv)))
			{
				if (Machine.CurrentLoopStatus.isBreak) break;
				if (Machine.CurrentLoopStatus.isContinue)
				{
					Machine.CurrentLoopStatus.isContinue = false;
					//
					if (command is ExpressionCommand) Machine.CurrentLoopStatus.isContinue = true;
					//
					continue;
				}
				if (Machine.CurrentFunctionStatus.Returned) return;
				//
				this.command.execute(newenv);
			}
			Machine.CurrentLoopStatus.isBreak = false;
		}

		public Stmt Command { get { return this.command; } }
		public Expr Condition { get { return this.condition; } }
	}
}
