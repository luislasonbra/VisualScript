using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

namespace VisualScript.Native
{
	public class StringProperties
	{
		private object obj;
		private object[] parameters;

		public StringProperties(object obj, object[] parameters)
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
				if (parameters != null && parameters.Length != 0)
				{
					string msg = "No overload for method '{0}' takes {1} arguments";
					throw new Exception(string.Format(
						msg,
						name,
						parameters.Length
					));
				}

				//
				return "string";
			}
			if (name.Equals("length"))
			{
				if (parameters != null)
				{
					string msg = "No se puede utilizar como método el miembro 'string.{0}' no invocable.";
					throw new Exception(string.Format(
						msg,
						name
					));
				}

				//
				return obj.ToString().Length;
			}
			if (name.Equals("contains"))
			{
				if (parameters != null)
				{
					switch (parameters.Length - 1)
					{
						case 0:
							// Not implement
							break;
					}
				}
				else
				{
					string msg = "No se puede convertir el grupo de métodos '{0}' en tipo no delegado.\n";
					msg += "¿Intentó invocar el método?";
					throw new Exception(string.Format(msg, name));
				}
			}
			if (name.Equals("substring"))
			{
				if (parameters != null)
				{
					switch (parameters.Length - 1)
					{
						case 0:
							if (!isInt(parameters[0]))
							{
								string msg2 = "Invalid Parameters";
								throw new Exception(string.Format(
									msg2
								));
							}
							//
							string str = (string)obj;
							return str.Substring((int)parameters[0]);

						case 1:
							if (!isInt(parameters[0]) || !isInt(parameters[1]))
							{
								string msg2 = "Invalid Parameters";
								throw new Exception(string.Format(
									msg2
								));
							}
							//
							string value = (string)obj;
							int start = (int)parameters[0];
							int end = (int)parameters[1];
							//
							return value.Substring(start, end - start);

						default:
							string msg = "No overload for method '{0}' takes {1} arguments";
							throw new Exception(string.Format(
								msg,
								name,
								parameters.Length
							));
					}
				}
				else
				{
					string msg = "No se puede convertir el grupo de métodos '{0}' en tipo no delegado.\n";
					msg += "¿Intentó invocar el método?";
					throw new Exception(string.Format(msg, name));
				}
			}

			//
			throw new Exception(string.Format("Unknown member '{0}'", name));
		}

		private bool isInt(object param) { return ObjectUtilities.IsInt(param); }
	}
}
