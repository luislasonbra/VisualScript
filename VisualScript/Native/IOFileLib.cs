using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Native
{
	public class IOFileLib : ICallableClass, IReadOnly
	{
		public object Invoke(string name, object[] parameters, object obj)
		{
			if (name == "write")
			{
				if (parameters == null)
				{
					string msg = "No se puede convertir el grupo de métodos '{0}' en tipo no delegado.\n";
					msg += "¿Intentó invocar el método?";
					throw new Exception(string.Format(msg, name));
				}

				if (parameters != null && parameters.Length != 1) Console.WriteLine();
				foreach (object argument in parameters)
				{
					if (argument != null) Console.WriteLine(argument.ToString());
				}

				//
				return null;
			}
			else if (name == "print")
			{
				if (parameters == null)
				{
					string msg2 = "No se puede convertir el grupo de métodos '{0}' en tipo no delegado.\n";
					msg2 += "¿Intentó invocar el método?";
					throw new Exception(string.Format(msg2, name));
				}

				if (parameters != null && parameters.Length != 1)
				{
					Console.WriteLine();
					return null;
				}

				//
				string msg = "No overload for method '{0}' takes {1} arguments";
				throw new Exception(string.Format(msg, name, parameters.Length));
			}

			//
			throw new Exception(string.Format("Unknown member '{0}'", name));
		}

		public override string ToString()
		{
			return "[Object IO]";
		}

		public string Name { get { return ""; } }
		public object[] Parameters { get { return null; } }
	}
}
