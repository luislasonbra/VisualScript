using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

namespace VisualScript.Native
{
	public class MathLib : ICallableClass
	{
		public object Invoke(string name, object[] parameters, object obj)
		{
			// Variables/Math Object Properties //////////////////////////////////////////////////////////
			if (name.Equals("E"))
			{
				if (parameters != null) errorParameters(name);
				return Math.E;
			}
			if (name.Equals("PI"))
			{
				if (parameters != null) errorParameters(name);
				return Math.PI;
			}
			if (name.Equals("LN2"))
			{
				if (parameters != null) errorParameters(name);
				return Math.Log(2);
			}
			if (name.Equals("LN10"))
			{
				if (parameters != null) errorParameters(name);
				return Math.Log(10);
			}
			if (name.Equals("LN2E"))
			{
				if (parameters != null) errorParameters(name);
				return Math.Log(Math.E, 2);
			}
			if (name.Equals("LN10E"))
			{
				if (parameters != null) errorParameters(name);
				return Math.Log(Math.E, 10);
			}
			if (name.Equals("SQRT1_2"))
			{
				if (parameters != null) errorParameters(name);
				return Math.Sqrt(1) / Math.Sqrt(2);
			}
			if (name.Equals("SQRT2"))
			{
				if (parameters != null) errorParameters(name);
				return Math.Sqrt(2);
			}

			// Functiones/Math Object Methods //////////////////////////////////////////////////////////
			if (name.Equals("abs"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					if (ObjectUtilities.IsInt(parameters[0])) return Math.Abs((int)parameters[0]);
					else
						return Math.Abs((double)parameters[0]);
				}
			}
			if (name.Equals("acos"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					if (ObjectUtilities.IsInt(parameters[0])) return Math.Acos((int)parameters[0]);
					else
						return Math.Acos((double)parameters[0]);
				}
			}
			if (name.Equals("asin"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					if (ObjectUtilities.IsInt(parameters[0])) return Math.Asin((int)parameters[0]);
					else
						return Math.Asin((double)parameters[0]);
				}
			}
			if (name.Equals("atan"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					if (ObjectUtilities.IsInt(parameters[0])) return Math.Atan((int)parameters[0]);
					else
						return Math.Atan((double)parameters[0]);
				}
			}
			if (name.Equals("atan2"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 2) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]) && ObjectUtilities.IsNumber(parameters[1]))
				{
					double val = Convert.ToDouble(parameters[0]);
					double val2 = Convert.ToDouble(parameters[1]);

					//
					return Math.Atan2(val, val2);
				}
			}
			if (name.Equals("ceil"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					return (int)Math.Ceiling(((int)parameters[0]) * 1.0);
				}
			}
			if (name.Equals("cos"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					if (ObjectUtilities.IsInt(parameters[0])) return Math.Cos((int)parameters[0]);
					else
						return Math.Cos((double)parameters[0]);
				}
			}
			if (name.Equals("exp"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					if (ObjectUtilities.IsInt(parameters[0])) return Math.Exp((int)parameters[0]);
					else
						return Math.Exp((double)parameters[0]);
				}
			}
			if (name.Equals("floor"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					double val = Convert.ToDouble(parameters[0]);

					//
					return (int)Math.Floor(val);
				}
			}
			if (name.Equals("log"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					if (ObjectUtilities.IsInt(parameters[0])) return Math.Log((int)parameters[0], Math.E);
					else
						return Math.Log((double)parameters[0], Math.E);
				}
			}
			if (name.Equals("max"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length <= 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					double dValue = Convert.ToDouble(parameters[0]);
					for (int i = 0; i < parameters.Length; i++)
					{
						double newValue = Convert.ToDouble(parameters[i]);
						if (newValue > dValue) dValue = newValue;
					}

					//
					return dValue;
				}
			}
			if (name.Equals("min"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length <= 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					double dValue = Convert.ToDouble(parameters[0]);
					for (int i = 0; i < parameters.Length; i++)
					{
						double newValue = Convert.ToDouble(parameters[i]);
						if (newValue < dValue) dValue = newValue;
					}

					//
					return dValue;
				}
			}
			if (name.Equals("pow"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 2) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]) && ObjectUtilities.IsNumber(parameters[1]))
				{
					double val = (double)Convert.ToDouble(parameters[0]);
					double val2 = (double)Convert.ToDouble(parameters[1]);

					//
					return Math.Pow(val, val2);
				}
			}
			if (name.Equals("random"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 2) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]) && ObjectUtilities.IsNumber(parameters[1]))
				{
					int val = (int)(parameters[0]);
					int val2 = (int)(parameters[1]);

					//
					return new Random().Next(val, val2);
				}
			}
			if (name.Equals("round"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					double val = Convert.ToDouble(parameters[0]);

					//
					return (int)Math.Round(val);
				}
			}
			if (name.Equals("sin"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					if (ObjectUtilities.IsInt(parameters[0])) return Math.Sin((int)parameters[0]);
					else
						return Math.Sin((double)parameters[0]);
				}
			}
			if (name.Equals("sqrt"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					if (ObjectUtilities.IsInt(parameters[0])) return Math.Sqrt((int)parameters[0]);
					else
						return Math.Sqrt((double)parameters[0]);
				}
			}
			if (name.Equals("tan"))
			{
				if (parameters == null) errorParameters(name);
				if (parameters != null && parameters.Length != 1) errorParametersNull(name, parameters);
				if (ObjectUtilities.IsNumber(parameters[0]))
				{
					if (ObjectUtilities.IsInt(parameters[0])) return Math.Tan((int)parameters[0]);
					else
						return Math.Tan((double)parameters[0]);
				}
			}

			//
			throw new Exception(string.Format("Unknown member '{0}'", name));
		}

		private void errorParameters(string name)
		{
			string msg = "No se puede utilizar como método el miembro 'Math.{0}' no invocable.";
			throw new Exception(string.Format(msg, name));
		}

		private void errorParametersNull(string name, object[] parameters)
		{
			string msg = "No overload for method '{0}' takes {1} arguments";
			throw new Exception(string.Format(msg, name, parameters.Length));
		}

		public override string ToString() { return "[Object Math]"; }
	}
}
