using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class ArrayExpression : Expr
	{
		private List<Expr> arguments;

		public ArrayExpression(List<Expr> arguments)
		{
			this.arguments = arguments;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			List<object> values = new List<object>();
			foreach (Expr argument in this.arguments)
				values.Add(argument.evaluate(environment));

			//
			return new TestArray(values);
		}

		public List<Expr> Arguments { get { return this.arguments; } }
	}
}
