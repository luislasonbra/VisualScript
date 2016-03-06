using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class NullExpression : Expr
	{
		public object evaluate(IBindingEnvironment environment) { return null; }
	}
}
