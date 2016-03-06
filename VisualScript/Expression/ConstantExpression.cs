using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class ConstantExpression : Expr
	{
		private object value;

		public ConstantExpression(object value)
		{
			this.value = value;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			if (ObjectUtilities.IsBoolean(this.value))
			{
				bool v = (bool)this.value;
				if (v == true) return "true";

				//
				return "false";
			}

			//
			return this.value;
		}
	}
}
