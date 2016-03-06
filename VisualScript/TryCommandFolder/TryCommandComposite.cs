using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.TryCommandFolder
{
	public class TryCommandComposite : Stmt
	{
		private List<Stmt> commands;

		public TryCommandComposite(List<Stmt> commands)
		{
			this.commands = commands;
		}

		public void execute(IBindingEnvironment environment)
		{
			LocalBindingEnvironment local = new LocalBindingEnvironment(environment);
			for (int i = 0; i < this.commands.Count; i++)
			{
				Stmt command = this.commands[i];

				try
				{
					command.execute(local);
				}
				catch (Exception ex)
				{
					Machine.CurrentErrorTryStatus.Returned = true;
					Machine.CurrentErrorTryStatus.ErrorMessage = ex.Message;
					break;
				}
			}
		}

		public List<Stmt> Commands { get { return this.commands; } }
		public int CommandCount { get { return this.commands.Count; } }
	}
}
