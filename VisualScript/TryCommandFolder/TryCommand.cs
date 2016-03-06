using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.TryCommandFolder
{
	public class TryCommand : Stmt
	{
		private Stmt tryBody;
		private Stmt catchBody;
		private Stmt finallyBody;

		public TryCommand(Stmt tryBody, Stmt catchBody, Stmt finallyBody)
		{
			this.tryBody = tryBody;
			this.catchBody = catchBody;
			this.finallyBody = finallyBody;
		}

		public Stmt TryBody { get { return tryBody; } }
		public Stmt CatchBody { get { return catchBody; } }
		public Stmt FinallyBody { get { return finallyBody; } }

		public void execute(IBindingEnvironment environment)
		{
			LocalBindingEnvironment local = new LocalBindingEnvironment(environment);

			if (tryBody != null) tryBody.execute(local);
			if (Machine.CurrentErrorTryStatus.Returned)
			{
				if (catchBody != null)
					catchBody.execute(local);
			}
			else
			{
				if (finallyBody != null)
					finallyBody.execute(local);
			}

			if (finallyBody == null && catchBody == null) throw new Exception("Expected catch or finally");
		}
	}
}
