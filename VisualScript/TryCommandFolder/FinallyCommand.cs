using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.TryCommandFolder
{
	public class FinallyCommand : Stmt
	{
		private Stmt body;

		public FinallyCommand(Stmt body)
		{
			this.body = body;
		}

		public void execute(IBindingEnvironment environment)
		{
			LocalBindingEnvironment local = new LocalBindingEnvironment(environment);
			body.execute(local);
		}

		public Stmt Body { get { return this.body; } }
	}
}
