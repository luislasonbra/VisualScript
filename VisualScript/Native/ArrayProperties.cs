using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

namespace VisualScript.Native
{
	public class ArrayProperties
	{
		private object obj;
		private object[] parameters;

		public ArrayProperties(object obj, object[] parameters)
		{
			this.obj = obj;
			this.parameters = parameters;
		}

		public object getProperties(string name)
		{
			if (name.Equals("toString"))
			{
				if (parameters == null)
				{
					string msg = "No se puede convertir el grupo de métodos '{0}' en tipo no delegado.\n";
					msg += "¿Intentó invocar el método?";
					throw new Exception(string.Format(msg, name));
				}

				return "array";
			}
			if (name.Equals("length"))
			{
				if (parameters != null)
				{
					string msg = "No se puede utilizar como método el miembro 'array.{0}' no invocable.";
					throw new Exception(string.Format(msg, name));
				}

				//
				return ((TestArray)obj).Count;
			}

			//
			throw new Exception(string.Format("Unknown member '{0}'", name));
		}

		private bool isInt(object param) { return ObjectUtilities.IsInt(param); }
	}
}
