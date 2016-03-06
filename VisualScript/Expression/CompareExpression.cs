using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class CompareExpression : BinaryExpression
	{
		private ComparisonOperator operation;

		public CompareExpression(ComparisonOperator operation, Expr left, Expr right) : base(left, right)
		{
			this.operation = operation;
		}

		public override object Apply(object leftValue, object rightValue)
		{
			switch (operation)
			{
				case ComparisonOperator.Equal: return MathObject.CompareObjectEqual(leftValue, rightValue);
				case ComparisonOperator.NotEqual: return MathObject.CompareObjectNotEqual(leftValue, rightValue);
				case ComparisonOperator.Less: return MathObject.CompareObjectLess(leftValue, rightValue);
				case ComparisonOperator.LessEqual: return MathObject.CompareObjectLessEqual(leftValue, rightValue);
				case ComparisonOperator.Greater: return MathObject.CompareObjectGreater(leftValue, rightValue);
				case ComparisonOperator.GreaterEqual: return MathObject.CompareObjectGreaterEqual(leftValue, rightValue);
				default: throw new ArgumentException("Invalid operator");
			}
		}

		public ComparisonOperator Operation { get { return this.operation; } }
	}
}
