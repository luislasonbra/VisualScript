using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class NotExpression : UnaryExpression
	{
		public NotExpression(Expr expression) : base(expression) { }

		public override object Apply(object value) { return !Predicates.IsTrue(value); }
	}
}
