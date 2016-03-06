using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class EnumExpression : Expr
	{
		private string name;

		public EnumExpression(string name)
		{
			this.name = name;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			return environment.GetValue(name);
		}

		public string Name { get { return this.name; } }
	}
}
