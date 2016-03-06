using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class ArithmeticBinaryExpression : BinaryExpression
	{
		private ArithmeticOperator operation;

		public ArithmeticBinaryExpression(ArithmeticOperator operation, Expr left, Expr right) : base(left, right)
		{
			this.operation = operation;
		}

		public override object Apply(object leftValue, object rightValue)
		{
			if (leftValue == null)
			{
				if (!(leftValue is string) && !(rightValue is string))
				{
					VariableExpression left = null;
					if (this.LeftExpression is VariableExpression) left = ((VariableExpression)this.LeftExpression);

					string msg = "Uso de la variable local no asignada '{0}'";
					if (left != null) throw new Exception(string.Format(msg, left.VariableName));

					//
					throw new Exception("No se puede realizar la operacion, en un valor null");
				}
			}
			else if (rightValue == null)
			{
				if (!(leftValue is string) && !(rightValue is string))
				{
					VariableExpression right = null;
					if (this.RightExpression is VariableExpression) right = ((VariableExpression)this.RightExpression);

					string msg = "Uso de la variable local no asignada '{0}'";
					if (right != null) throw new Exception(string.Format(msg, right.VariableName));

					//
					throw new Exception("No se puede realizar la operacion, en un valor null");
				}
			}

			switch (operation)
			{
				case ArithmeticOperator.Add: return MathObject.AddObject(leftValue, rightValue);
				//
				case ArithmeticOperator.Subtract: return MathObject.SubtractObject(leftValue, rightValue);
				//
				case ArithmeticOperator.Multiply: return MathObject.MultiplyObject(leftValue, rightValue);
				//
				case ArithmeticOperator.Divide: return MathObject.DivideObject(leftValue, rightValue);
				//
				case ArithmeticOperator.Modulo: return MathObject.ModObject(leftValue, rightValue);
				//
				case ArithmeticOperator.Pow: return MathObject.PowObject(leftValue, rightValue);
				//
				default: throw new ArgumentException("Invalid operator");
			}
		}

		public ArithmeticOperator Operation { get { return this.operation; } }
	}
}
