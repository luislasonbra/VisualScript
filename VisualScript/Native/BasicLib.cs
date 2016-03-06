using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Native
{
	public class BasicLib : ICallableClass, IReadOnly
	{
		public object Invoke(string name, object[] parameters, object obj)
		{
			if (name.Equals("_NAME"))
			{
				if (parameters != null) errorParameters(name);
				return "VisualScript";
			}
			if (name.Equals("_VERSION"))
			{
				if (parameters != null) errorParameters(name);
				return "0.1";
			}

			//
			throw new Exception(string.Format("Unknown member '{0}'", name));
		}

		private void errorParameters(string name)
		{
			string msg = "No se puede utilizar como método el miembro 'APP.{0}' no invocable.";
			throw new Exception(string.Format(msg, name));
		}

		private void errorParametersNull(string name, object[] parameters)
		{
			string msg = "No overload for method '{0}' takes {1} arguments";
			throw new Exception(string.Format(msg, name, parameters.Length));
		}

		public override string ToString() { return "[Object APP]"; }
	}
}
