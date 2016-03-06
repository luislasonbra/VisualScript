using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class ArithmeticUnaryExpression : UnaryExpression
	{
		private ArithmeticOperator operation;

		public ArithmeticUnaryExpression(ArithmeticOperator operation, Expr expression) : base(expression)
		{
			this.operation = operation;
		}

		public ArithmeticOperator Operation { get { return this.operation; } }

		public override object Apply(object value)
		{
			if (value == null) return value;

			switch (operation)
			{
				case ArithmeticOperator.Minus: return MathObject.NegateObject(value);
				case ArithmeticOperator.Plus: return MathObject.PlusObject(value);
				default: throw new ArgumentException("Invalid operator");
			}
		}
	}
}
