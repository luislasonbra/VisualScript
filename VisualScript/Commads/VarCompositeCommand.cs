using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	public class VarCompositeCommand : Stmt
	{
		private List<Stmt> commands;

		public VarCompositeCommand(List<Stmt> commands)
		{
			this.commands = commands;
		}

		public void execute(IBindingEnvironment environment)
		{
			for (int i = 0; i < this.commands.Count; i++)
			{
				this.commands[i].execute(environment);
			}
		}

		public List<Stmt> Commands { get { return this.commands; } }
		public int CommandCount { get { return this.commands.Count; } }
	}
}
