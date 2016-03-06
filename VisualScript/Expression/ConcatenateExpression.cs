using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class ConcatenateExpression : BinaryExpression
	{
		public ConcatenateExpression(Expr left, Expr right) : base(left, right) { }

		public override object Apply(object leftValue, object rightValue)
		{
			if (leftValue == null) leftValue = string.Empty;
			if (rightValue == null) rightValue = string.Empty;

			//
			return MathObject.ConcatenateObject(leftValue, rightValue);
		}
	}
}
