using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	public class DefaultCommand : Stmt
	{
		private Stmt command;

		public DefaultCommand(Stmt command) { this.command = command; }

		public void execute(IBindingEnvironment environment)
		{
			command.execute(environment);
		}

		public Stmt Command { get { return this.command; } }
	}
}
