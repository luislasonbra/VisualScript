using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Native;

namespace VisualScript.Expression
{
	public class DotExpression : Expr
	{
		private string name;
		private Expr expression;
		private string className;
		private List<Expr> arguments;

		public DotExpression(Expr expression, string name) : this(expression, name, null) { }

		public DotExpression(Expr expression, string name, List<Expr> arguments)
		{
			this.name = name;
			this.className = null;
			this.arguments = arguments;
			this.expression = expression;
		}

		public DotExpression(Expr expression, string className, string name, List<Expr> arguments)
		{
			this.name = name;
			this.className = className;
			this.arguments = arguments;
			this.expression = expression;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			object obj = this.expression.evaluate(environment);

			object[] parameters = null;
			if (this.arguments != null)
			{
				List<object> values = new List<object>();
				foreach (Expr argument in this.arguments)
				{
					values.Add(argument.evaluate(environment));
				}

				//
				parameters = values.ToArray();
			}

			//////////////////////////////////////////////////////////////////////////////
			// Enum type
			//////////////////////////////////////////////////////////////////////////////
			if (obj is EnumExpression)
			{
				string eName = ((EnumExpression)obj).Name;
				string newName = eName + "." + name;

				if (parameters != null)
				{
					string msg = "No se puede utilizar como método el miembro '{0}' no invocable.";
					throw new Exception(string.Format(msg, newName));
				}

				//
				return new EnumExpression(newName).evaluate(environment);
			}

			//////////////////////////////////////////////////////////////////////
			//// ICallableClass Methodes
			//////////////////////////////////////////////////////////////////////
			if (obj is ICallableClass) return ((ICallableClass)obj).Invoke(this.name, parameters, obj);
			//////////////////////////////////////////////////////////////////////

			//////////////////////////////////////////////////////////////////////
			// Native Methods
			//////////////////////////////////////////////////////////////////////
			if (obj is string) return new StringProperties(obj, parameters).getProperties(this.name);
			if (obj is TestArray) return new ArrayProperties(obj, parameters).getProperties(this.name);

			//throw new Exception("Error: " + name);
			//
			return ObjectUtilities.GetValue(obj, this.name, parameters);
		}

		public string Name { get { return this.name; } }
		public string ClassName { get { return this.className; } }
		public Expr Expression { get { return this.expression; } }
		public List<Expr> Arguments { get { return this.arguments; } }
	}
}
