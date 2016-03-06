using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualScript.Commads
{
	public class ContinueCommand : Stmt
	{
		public ContinueCommand() { }

		public void execute(IBindingEnvironment environment) { Machine.CurrentLoopStatus.isContinue = true; }
	}
}
