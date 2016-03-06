using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

namespace VisualScript
{
	public class Predicates
	{
		public static bool IsFalse(object obj)
		{
			if (obj == null) return true;
			if (obj is NullExpression) return true;
			if (obj is bool) return !(bool)obj;
			if (obj.ToString().Equals("true") || obj.ToString().Equals("false")) return !(bool)Convert.ToBoolean(obj);
			if (obj is int) return (int)obj == 0;
			if (obj is string) return string.IsNullOrEmpty((string)obj);
			if (obj is long) return (long)obj == 0;
			if (obj is short) return (short)obj == 0;
			if (obj is double) return (double)obj == 0;
			if (obj is float) return (float)obj == 0;

			//
			return false;
		}

		public static bool IsTrue(object obj)
		{
			return !IsFalse(obj);
		}
	}
}
