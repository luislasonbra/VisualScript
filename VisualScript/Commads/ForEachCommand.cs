using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	public class ForEachCommand : Stmt
	{
		private string name;
		private Stmt command;
		private bool localvar;
		private Expr expression;

		public ForEachCommand(string name, Expr expression, Stmt command) : this(name, expression, command, false) { }

		public ForEachCommand(string name, Expr expression, Stmt command, bool localvar)
		{
			this.name = name;
			this.expression = expression;
			this.command = command;
			this.localvar = localvar;
		}

		public void execute(IBindingEnvironment environment)
		{
			IBindingEnvironment newenv = environment;

			if (this.localvar)
			{
				newenv = new LocalBindingEnvironment(environment);
				newenv.SetValue(this.name, null);
			}
			else
			{
				if (!newenv.ContainsName(this.name))
				{
					string msg = "El nombre '{0}' no existe en el contexto actual";
					throw new Exception(string.Format(msg, name));
				}
			}

			object obj = this.expression.evaluate(newenv);

			if (obj is string)
			{
				foreach (char result in (string)obj)
				{
					newenv.SetValue(this.name, result);
					this.command.execute(newenv);
				}
				Machine.CurrentLoopStatus.isBreak = false;
			}

			if (obj is TestArray)
			{
				foreach (object result in ((TestArray)obj).Arguments)
				{
					newenv.SetValue(this.name, result);
					this.command.execute(newenv);
				}
				Machine.CurrentLoopStatus.isBreak = false;
			}
		}

		public string Name { get { return this.name; } }
		public Stmt Command { get { return this.command; } }
		public Expr Expression { get { return this.expression; } }
		public bool LocalVariable { get { return this.localvar; } }
	}
}
