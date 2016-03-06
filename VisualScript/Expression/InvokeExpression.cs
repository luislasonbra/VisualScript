using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace VisualScript.Expression
{
	/*
		Invoca La exprecion indicada y le agrega el numero de parametros.
	*/
	public class InvokeExpression : Expr
	{
		private string name;
		private List<Expr> arguments;

		public InvokeExpression(string name, List<Expr> arguments)
		{
			this.name = name;
			this.arguments = arguments;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			ICallable callable = (ICallable)environment.GetValue(this.name);
			if (callable == null) callable = (ICallable)Machine.Current.Environment.GetValue(this.name);

			List<object> parameters = new List<object>();
			foreach (Expr expression in this.arguments)
			{
				object parameter = expression.evaluate(environment);

				//
				parameters.Add(parameter);
			}
			if (callable is ILocalCallable) return callable.Invoke(environment, parameters.ToArray());
			//if (callable == null)
			//{
			//    string ms = "The name '{0}' does not exist in the current context";
			//    throw new Exception(string.Format(ms, this.name));
			//}

			//
			return callable.Invoke(parameters.ToArray());
		}

		public string Name { get { return this.name; } }
		public List<Expr> Arguments { get { return this.arguments; } }
	}
}
