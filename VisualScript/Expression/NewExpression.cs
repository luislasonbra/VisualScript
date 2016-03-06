using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class NewExpression : Expr
	{
		private string name;
		private List<Expr> arguments;

		public NewExpression(string name, List<Expr> arguments)
		{
			this.name = name;
			this.arguments = arguments;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			object value = environment.GetValue(this.name);

			if (value is DynamicObject)
			{
				string ms = "No se puede crear ninguna instancia de la clase estática '{0}'";
				throw new Exception(string.Format(ms, this.name));
			}

			object[] parameters = null;
			if (this.arguments != null && this.arguments.Count > 0)
			{
				List<object> values = new List<object>();
				foreach (Expr argument in this.arguments) values.Add(argument.evaluate(environment));
				//
				parameters = values.ToArray();
			}

			if (value is IClass) return ((IClass)value).NewInstance(parameters);

			//
			string msg = "No se puede crear ninguna instancia de '{0}'";
			throw new Exception(string.Format(msg, name));
		}

		public string TypeName { get { return this.name; } }
		public List<Expr> Arguments { get { return this.arguments; } }
	}
}
