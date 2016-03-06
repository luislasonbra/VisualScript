using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	public class VarCommand : Stmt
	{
		private string name;
		private Expr expression;

		public VarCommand(string name, Expr expression)
		{
			this.name = name;
			this.expression = expression;
		}

		public void execute(IBindingEnvironment environment)
		{
			object value = (this.expression != null) ? this.expression.evaluate(environment) : null;

			if (environment.ContainsName(this.name))
			{
				string msg = "Ya se ha definido una variable local denominada '{0}' en este ámbito";
				throw new Exception(string.Format(msg, this.name));
			}

			environment.SetValue(this.name, value);
		}

		public string Name { get { return this.name; } }
		public Expr Expression { get { return this.expression; } }
	}
}
