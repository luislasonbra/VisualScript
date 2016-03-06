using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.TryCommandFolder
{
	public class CatchCommand : Stmt
	{
		private Stmt body;
		private Stmt varCommand;

		public CatchCommand(Stmt varCommand, Stmt body)
		{
			this.body = body;
			this.varCommand = varCommand;
		}

		public void execute(IBindingEnvironment environment)
		{
			LocalBindingEnvironment local = new LocalBindingEnvironment(environment);
			
			// set value in variable
			varCommand.execute(local);
			local.SetValue(((Commads.VarCommand)varCommand).Name, Machine.CurrentErrorTryStatus.ErrorMessage);

			body.execute(local);
		}

		public Stmt Body { get { return this.body; } }
		public Stmt VariableCommand { get { return this.varCommand; } }
	}
}
