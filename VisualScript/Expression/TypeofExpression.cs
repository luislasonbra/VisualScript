using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class TypeofExpression : Expr
	{
		private Expr expression;

		public TypeofExpression(Expr expression)
		{
			this.expression = expression;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			object value = this.expression.evaluate(environment);

			//
			return ObjectUtilities.getType(value);
		}
	}
}
