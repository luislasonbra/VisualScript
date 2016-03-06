using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

namespace VisualScript.Native
{
	public class PrintLineSubroutine : ICallable
	{
		public object Invoke(IBindingEnvironment environment, object[] arguments)
		{
			return this.Invoke(arguments);
		}

		public object Invoke(object[] arguments)
		{
			if (arguments == null)
			{
				string msg = "No se puede convertir el grupo de métodos '{0}' en tipo no delegado.\n";
				msg += "¿Intentó invocar el método?";
				throw new Exception(string.Format(msg, "println"));
			}

			if (arguments != null && arguments.Length != 1) Console.WriteLine();
			foreach (object argument in arguments) if (argument != null) Console.WriteLine(argument.ToString());

			//
			return null;
		}

		public int Arity { get { return -1; } }
		public IBindingEnvironment Environment { get { return null; } }
	}
}
