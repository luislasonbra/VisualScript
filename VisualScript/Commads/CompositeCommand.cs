using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	public class CompositeCommand : Stmt
	{
		private List<Stmt> commands;

		public CompositeCommand(List<Stmt> commands)
		{
			this.commands = commands;
		}

		public void execute(IBindingEnvironment environment)
		{
			LocalBindingEnvironment local = new LocalBindingEnvironment(environment);
			for (int i = 0; i < this.commands.Count; i++)
			{
				Stmt command = this.commands[i];
				if (Machine.CurrentLoopStatus.isBreak) break;
				if (Machine.CurrentLoopStatus.isContinue)
				{
					Machine.CurrentLoopStatus.isContinue = false;
					if (command is ExpressionCommand) Machine.CurrentLoopStatus.isContinue = true;
					continue;
				}
				if (Machine.CurrentFunctionStatus.Returned) return;
				command.execute(local);
			}
		}

		public List<Stmt> Commands { get { return this.commands; } }
		public int CommandCount { get { return this.commands.Count; } }
	}
}
