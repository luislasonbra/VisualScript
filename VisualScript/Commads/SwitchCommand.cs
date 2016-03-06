using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	public class SwitchCommand : Stmt
	{
		private Expr condition;
		private List<Stmt> body;

		public SwitchCommand(Expr condition, List<Stmt> body)
		{
			this.body = body;
			this.condition = condition;
		}

		public void execute(IBindingEnvironment environment)
		{
			LocalBindingEnvironment local = new LocalBindingEnvironment(environment);
			bool isConditionTrue = false;
			object obj = condition.evaluate(local); // condition
			for (int i = 0; i < this.body.Count; i++)
			{
				Stmt command = this.body[i];
				if (command is CaseCommand) // case commands
				{
					object obj2 = ((CaseCommand)command).Expression.evaluate(local);
					if (Predicates.IsTrue(MathObject.CompareObjectEqual(obj, obj2)))
					{
						isConditionTrue = true;
						command.execute(local);
					}
				}
				// default commands
				else if (command is DefaultCommand) command.execute(local);

				//
				if (isConditionTrue == true) break;
			}
			Machine.CurrentLoopStatus.isBreak = false;
		}

		public List<Stmt> Body { get { return this.body; } }
		public Expr Condition { get { return this.condition; } }
	}
}
