using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class ArithmeticAvancedExpression : UnaryExpression
	{
		private Expr left = null;
		private Expr right = null;
		private IncrementOperator oper;

		public ArithmeticAvancedExpression(IncrementOperator oper, Expr left, Expr right) : base(left)
		{
			this.oper = oper;
			this.left = left;
			this.right = right;
		}

		public override object Apply(object value) { throw new NotImplementedException(); }

		public object Apply(object value, IBindingEnvironment environment)
		{
			if (value == null) value = 0;
			object newvalue = null;
			object b = (right != null) ? right.evaluate(environment) : null;

			switch (this.oper)
			{
				// ++
				case IncrementOperator.Increment:
					newvalue = MathObject.AddObject(value, 1);
					break;
				// --
				case IncrementOperator.Decrement:
					newvalue = MathObject.SubtractObject(value, 1);
					break;
				// +=
				case IncrementOperator.IncrementEqual:
					newvalue = MathObject.AddObject(value, b);
					break;
				// -=
				case IncrementOperator.DecrementEqual:
					newvalue = MathObject.SubtractObject(value, b);
					break;
				// /=
				case IncrementOperator.DivideEqual:
					newvalue = MathObject.DivideObject(value, b);
					break;
				// *=
				case IncrementOperator.MultiplyEqual:
					newvalue = MathObject.MultiplyObject(value, b);
					break;
				// %=
				case IncrementOperator.ModuloEqual:
					newvalue = MathObject.ModObject(value, b);
					break;
			}
			ExpressionUtilities.SetValue(this.Expression, newvalue, environment);

			//
			return newvalue;
		}

		public override object evaluate(IBindingEnvironment environment)
		{
			return this.Apply(this.Expression.evaluate(environment), environment);
		}

		public IncrementOperator Operator { get { return this.oper; } }
	}
}
