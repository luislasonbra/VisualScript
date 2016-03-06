using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class VariableExpression : Expr
	{
		private string name;

		public VariableExpression(string name)
		{
			this.name = name;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			return environment.GetValue(this.name);
		}

		public string VariableName { get { return this.name; } }
	}
}
