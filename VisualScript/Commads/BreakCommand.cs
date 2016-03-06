using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualScript.Commads
{
	public class BreakCommand : Stmt
	{
		public BreakCommand() { }

		public void execute(IBindingEnvironment environment) { Machine.CurrentLoopStatus.isBreak = true; }
	}
}
