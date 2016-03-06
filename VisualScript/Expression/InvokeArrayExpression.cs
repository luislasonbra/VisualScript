using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class InvokeArrayExpression : Expr
	{
		private Expr expression;
		private List<Expr> arguments;

		public InvokeArrayExpression(List<Expr> arguments) : this(null, arguments) { }

		public InvokeArrayExpression(Expr expression, List<Expr> arguments)
		{
			this.expression = expression;
			this.arguments = arguments;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			object obj = this.Expression.evaluate(environment);

			object[] parameters = null;
			if (this.arguments != null)
			{
				List<object> values = new List<object>();
				foreach (Expr argument in this.arguments)
					values.Add(argument.evaluate(environment));

				//
				parameters = values.ToArray();
			}

			// TODO if undefined, do nothing
			if (obj == null) return null;

			if (obj is string)
			{
				if (ObjectUtilities.IsInt(parameters[0])) return ((string)obj)[(int)parameters[0]];
			}
			else if (ObjectUtilities.IsNumber(obj))
				throw new Exception("Cannot apply indexing with [] to an expression of type 'numeric'");

			//
			return ObjectUtilities.GetIndexedValue(obj, parameters);
		}

		public Expr Expression { get { return this.expression; } }
		public List<Expr> Arguments { get { return this.arguments; } }
	}
}
