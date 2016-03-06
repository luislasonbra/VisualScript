using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace VisualScript.Expression
{
	public static class ExpressionUtilities
	{
		public static void SetValue(Expr expression, object value, IBindingEnvironment environment)
		{
			if (expression is VariableExpression)
			{
				SetValue((VariableExpression)expression, value, environment);
				return;
			}

			// ========================================
			if (expression is DotExpression)
			{
				SetValue((DotExpression)expression, value, environment);
				return;
			}

			//if (expression is ArrayExpression)
			//{
			//    SetValue((ArrayExpression)expression, value, environment);
			//    return;
			//}
			// ========================================

			throw new InvalidOperationException("Invalid left value");
		}

		private static void SetValue(VariableExpression expression, object value, IBindingEnvironment environment)
		{
			environment.SetValue(expression.VariableName, value);
		}

		// ========================================
		private static void SetValue(DotExpression expression, object value, IBindingEnvironment environment)
		{
			if (expression.Arguments != null) throw new InvalidOperationException("Invalid left value");

			object obj = ResolveToObject(expression.Expression, environment);

			ObjectUtilities.SetValue(obj, expression.Name, value);
		}

		//private static void SetValue(ArrayExpression expression, object value, IBindingEnvironment environment)
		//{
		//    object indexes = expression.Arguments[0].evaluate(environment);
		//    object obj = expression.Expression.evaluate(environment);
		//    //
		//    ObjectUtilities.SetIndexedValue((ArrayCommand)obj, new object[] { indexes }, value);
		//}
		// ========================================

		public static object ResolveToObject(Expr expression, IBindingEnvironment environment)
		{
			if (expression is VariableExpression) return ResolveToObject((VariableExpression)expression, environment);

			if (expression is DotExpression) return ResolveToObject((DotExpression)expression, environment);

			return expression.evaluate(environment);
		}

		private static object ResolveToObject(VariableExpression expression, IBindingEnvironment environment)
		{
			string name = expression.VariableName;
			object obj = environment.GetValue(name);

			if (obj == null)
			{
				string ms = "The name '{0}' does not exist in the current context";
				throw new Exception(string.Format(ms, name));
			}

			return obj;
		}

		// ========================================
		private static object ResolveToObject(DotExpression expression, IBindingEnvironment environment)
		{
			object obj = ResolveToObject(expression.Expression, environment);

			//if (obj is DynamicObject)
			//{
			//    DynamicObject dynobj = (DynamicObject)obj;
			//    obj = dynobj.GetValue(expression.Name);

			//    if (obj == null)
			//    {
			//        obj = new DynamicObject();
			//        dynobj.SetValue(expression.Name, obj);
			//    }

			//    return obj;
			//}

			return ObjectUtilities.GetValue(obj, expression.Name);
		}
		// ========================================

		public static object ResolveToList(Expr expression, IBindingEnvironment environment)
		{
			if (expression is VariableExpression) return ResolveToList((VariableExpression)expression, environment);

			//
			return expression.evaluate(environment);
		}

		private static object ResolveToList(VariableExpression expression, IBindingEnvironment environment)
		{
			string name = expression.VariableName;
			object obj = environment.GetValue(name);

			if (obj == null)
			{
				obj = new ArrayList();

				// TODO Review if Local or not
				environment.SetValue(name, obj);
			}

			return obj;
		}
	}
}
