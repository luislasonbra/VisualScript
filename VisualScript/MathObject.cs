/*
 * Date:
 * dd/mm/yyyy
 * 06/03/2015
 * 
 * MathObject v2.0
 * Developed by luislasonbra
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

namespace VisualScript
{
	public static class MathObject
	{
		/// <summary>
		/// Reprecenta (+) de dos objetos.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object AddObject(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right)) return ((int)left) + ((int)right);
				//
				else if (isReal(left) && isReal(right)) return ((double)left) + ((double)right);
				//
				else if (isInt(left) && isReal(right)) return (double)(((int)left) + ((double)right));
				//
				else // if (isReal(left) && isInt(right))
					return (double)((double)left) + ((int)right);
			}
			else
			{
				return ConcatenateObject(left, right);
			}
		}

		/// <summary>
		/// Reprecenta (-) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object SubtractObject(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right)) return ((int)left) - ((int)right);
				//
				else if (isReal(left) && isReal(right)) return ((double)left) - ((double)right);
				//
				else if (isInt(left) && isReal(right)) return ((int)left) - ((double)right);
				//
				else // if (isReal(left) && isInt(right))
					return ((double)left) - ((int)left);
			}

			return null;
		}

		/// <summary>
		/// Reprecenta (*) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object MultiplyObject(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right)) return ((int)left) * ((int)right);
				//
				else if (isReal(left) && isReal(right)) return ((double)left) * ((double)right);
				//
				else if (isInt(left) && isReal(right)) return ((int)left) * ((double)right);
				//
				else // if (isReal(left) && isInt(right))
					return ((double)left) * ((int)right);
			}

			return null;
		}

		/// <summary>
		/// Reprecenta (/) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object DivideObject(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right)) return ((int)left) / ((int)right);
				//
				else if (isReal(left) && isReal(right)) return ((double)left) / ((double)right);
				//
				else if (isInt(left) && isReal(right)) return ((int)left) / ((double)right);
				//
				else // if (isReal(left) && isInt(right))
					return ((double)left) / ((int)right);
			}

			return null;
		}

		/// <summary>
		/// Reprecenta (%) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object ModObject(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right)) return ((int)left) % ((int)right);
				//
				else if (isReal(left) && isReal(right)) return ((double)left) % ((double)right);
				//
				else if (isInt(left) && isReal(right)) return ((int)left) % ((double)right);
				//
				else // if (isReal(left) && isInt(right))
					return ((double)left) % ((int)right);
			}

			return null;
		}

		/// <summary>
		/// Reprecenta (^) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object PowObject(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right)) return ((int)left) ^ ((int)right);
				else
				{
					string ms = "El operador '^' no se puede aplicar a operandos del tipo '{0}' y '{1}'";
					object obj = isInt(left) ? "int" : isReal(left) ? "double" : isString(left) ? "string" : "null";
					object obj2 = isInt(right) ? "int" : isReal(right) ? "double" : isString(right) ? "string" : "null";

					//
					throw new Exception(string.Format(ms, obj, obj2));
				}
			}

			return null;
		}

		/// <summary>
		/// Reprecenta (+=) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object IncrementEqualObject(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right))
				{
					int a = ((int)left);
					int b = ((int)right);
					int r = a + b;
					//
					return r;
				}
				//
				else if (isReal(left) && isReal(right))
				{
					double a = ((double)left);
					double b = ((double)right);
					double r = a + b;
					//
					return r;
				}
				//
				else if (isInt(left) && isReal(right))
				{
					int a = ((int)left);
					double b = ((double)right);
					var r = a + b;
					//
					return r;
				}
				//
				else // if (isReal(left) && isInt(right))
				{
					double a = ((double)left);
					int b = ((int)right);
					double r = a + b;
					//
					return r;
				}
			}

			return null;
		}

		/// <summary>
		/// Reprecenta (-=) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object DecrementEqualObject(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right))
				{
					int a = ((int)left);
					int b = ((int)right);
					int r = a - b;
					//
					return r;
				}
				//
				else if (isReal(left) && isReal(right))
				{
					double a = ((double)left);
					double b = ((double)right);
					double r = a - b;
					//
					return r;
				}
				//
				else if (isInt(left) && isReal(right))
				{
					int a = ((int)left);
					double b = ((double)right);
					var r = a - b;
					//
					return r;
				}
				//
				else // if (isReal(left) && isInt(right))
				{
					double a = ((double)left);
					int b = ((int)right);
					double r = a - b;
					//
					return r;
				}
			}

			return null;
		}

		/// <summary>
		/// Reprecenta (/=) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object DivideEqualObject(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right))
				{
					int a = ((int)left);
					int b = ((int)right);
					int r = a / b;
					//
					return r;
				}
				//
				else if (isReal(left) && isReal(right))
				{
					double a = ((double)left);
					double b = ((double)right);
					double r = a / b;
					//
					return r;
				}
				//
				else if (isInt(left) && isReal(right))
				{
					int a = ((int)left);
					double b = ((double)right);
					var r = a / b;
					//
					return r;
				}
				//
				else // if (isReal(left) && isInt(right))
				{
					double a = ((double)left);
					int b = ((int)right);
					double r = a / b;
					//
					return r;
				}
			}

			return null;
		}

		/// <summary>
		/// Reprecenta (*=) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object MultipplyEqualObject(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right))
				{
					int a = ((int)left);
					int b = ((int)right);
					int r = a * b;
					//
					return r;
				}
				//
				else if (isReal(left) && isReal(right))
				{
					double a = ((double)left);
					double b = ((double)right);
					double r = a * b;
					//
					return r;
				}
				//
				else if (isInt(left) && isReal(right))
				{
					int a = ((int)left);
					double b = ((double)right);
					var r = a * b;
					//
					return r;
				}
				//
				else // if (isReal(left) && isInt(right))
				{
					double a = ((double)left);
					int b = ((int)right);
					double r = a * b;
					//
					return r;
				}
			}

			return null;
		}

		/// <summary>
		/// Reprecenta (%=) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object ModuleEqualObject(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right))
				{
					int a = ((int)left);
					int b = ((int)right);
					int r = a % b;
					//
					return r;
				}
				//
				else if (isReal(left) && isReal(right))
				{
					double a = ((double)left);
					double b = ((double)right);
					double r = a % b;
					//
					return r;
				}
				//
				else if (isInt(left) && isReal(right))
				{
					int a = ((int)left);
					double b = ((double)right);
					var r = a % b;
					//
					return r;
				}
				//
				else // if (isReal(left) && isInt(right))
				{
					double a = ((double)left);
					int b = ((int)right);
					double r = a % b;
					//
					return r;
				}
			}

			return null;
		}

		/// <summary>
		/// Reprecenta (&) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object ConcatenateObject(object left, object right)
		{
			if (left == null) left = string.Empty;
			if (right == null) right = string.Empty;
			//
			return left.ToString() + right.ToString();
		}

		// Result (false|true)=========================================================

		/// <summary>
		/// Reprecenta (==) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object CompareObjectEqual(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right)) return (((int)left) == ((int)right)) == true ? "true" : "false";
				//
				else if (isReal(left) && isReal(right)) return (((double)left) == ((double)right)) ? "true" : "false";
				//
				else if (isInt(left) && isReal(right)) return (((int)left) == ((double)right)) ? "true" : "false";
				//
				else // if (isReal(left) && isInt(right))
					return (((double)left) == ((int)right)) ? "true" : "false";
			}
			else
			{
				//if (left == null && isNumeric(right))
				//{
				//    string ms = "El operador '==' no se puede aplicar a operandos del tipo '{0}' y '{1}'";
				//    object obj = isInt(right) ? "int" : isReal(right) ? "double" : "Numeric";

				//    //
				//    throw new Exception(string.Format(ms, "null", obj));
				//}

				if (left == null && isNumeric(right))
				{
					string ms = "El operador '==' no se puede aplicar a operandos del tipo '{0}' y '{1}'";
					object obj = isInt(left) ? "int" : isReal(left) ? "double" : isString(left) ? "string" : "null";
					object obj2 = isInt(right) ? "int" : isReal(right) ? "double" : isString(right) ? "string" : "null";

					//
					throw new Exception(string.Format(ms, obj, obj2));
				}

				if (right == null) return (left == right) ? "true" : "false";
				if (isNumeric(left) || isNumeric(right))
				{
					object obj = isNumeric(left) ? right : left;
					string ms = "La conversión de la cadena \"{0}\" en el tipo 'Numeric' no es válida.";
					//
					throw new Exception(string.Format(ms, obj));
				}
				//
				if (left == null) left = string.Empty;
				if (right == null) right = string.Empty;
				//
				return (left.ToString() == right.ToString()) ? "true" : "false";
			}
		}

		/// <summary>
		/// Reprecenta (!=) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object CompareObjectNotEqual(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right)) return (((int)left) != ((int)right)) ? "true" : "false";
				//
				else if (isReal(left) && isReal(right)) return (((double)left) != ((double)right)) ? "true" : "false";
				//
				else if (isInt(left) && isReal(right)) return (((int)left) != ((double)right)) ? "true" : "false";
				//
				else // if (isReal(left) && isInt(right))
					return (((double)left) != ((int)right)) ? "true" : "false";
			}
			else
			{
				if (right == null) return (left == right) ? "true" : "false";
				if (isNumeric(left) || isNumeric(right))
				{
					object obj = isNumeric(left) ? right : left;
					string ms = "La conversión de la cadena \"{0}\" en el tipo 'numeric' no es válida.";
					//
					throw new Exception(string.Format(ms, obj));
				}
				//
				if (left == null) left = string.Empty;
				if (right == null) right = string.Empty;
				//
				return (left.ToString() != right.ToString()) ? "true" : "false";
			}
		}

		/// <summary>
		/// Reprecenta (<) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object CompareObjectLess(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right)) return (((int)left) < ((int)right)) ? "true" : "false";
				//
				else if (isReal(left) && isReal(right)) return (((double)left) < ((double)right)) ? "true" : "false";
				//
				else if (isInt(left) && isReal(right)) return (((int)left) < ((double)right)) ? "true" : "false";
				//
				else // if (isReal(left) && isInt(right))
					return (((double)left) < ((int)right)) ? "true" : "false";
			}
			else
			{
				if ((left == null || isNumeric(left) || !isNumeric(left)) &&
					(right == null || isNumeric(right) || !isNumeric(right)))
				{
					string ms = "El operador '<' no se puede aplicar a operandos del tipo '{0}' y '{1}'";
					object obj = isInt(left) ? "int" : isReal(left) ? "double" : isString(left) ? "string" : "null";
					object obj2 = isInt(right) ? "int" : isReal(right) ? "double" : isString(right) ? "string" : "null";
					
					//
					throw new Exception(string.Format(ms, obj, obj2));
				}
				if (isNumeric(left) || isNumeric(right))
				{
					object obj = isNumeric(left) ? right : left;
					string ms = "La conversión de la cadena \"{0}\" en el tipo 'Numeric' no es válida.";
					//
					throw new Exception(string.Format(ms, obj));
				}
				//
				string msg = "Operator '{0}' cannot be applied to operands of type 'string' and 'string'";
				throw new Exception(string.Format(msg, "<"));
			}
		}

		/// <summary>
		/// Reprecenta (<=) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object CompareObjectLessEqual(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right)) return (((int)left) <= ((int)right)) ? "true" : "false";
				//
				else if (isReal(left) && isReal(right)) return (((double)left) <= ((double)right)) ? "true" : "false";
				//
				else if (isInt(left) && isReal(right)) return (((int)left) <= ((double)right)) ? "true" : "false";
				//
				else // if (isReal(left) && isInt(right))
					return (((double)left) <= ((int)right)) ? "true" : "false";
			}
			else
			{
				if ((left == null || isNumeric(left) || !isNumeric(left)) &&
					(right == null || isNumeric(right) || !isNumeric(right)))
				{
					string ms = "El operador '<=' no se puede aplicar a operandos del tipo '{0}' y '{1}'";
					object obj = isInt(left) ? "int" : isReal(left) ? "double" : isString(left) ? "string" : "null";
					object obj2 = isInt(right) ? "int" : isReal(right) ? "double" : isString(right) ? "string" : "null";

					//
					throw new Exception(string.Format(ms, obj, obj2));
				}

				if (isNumeric(left) || isNumeric(right))
				{
					object obj = isNumeric(left) ? right : left;
					string ms = "La conversión de la cadena \"{0}\" en el tipo 'Numeric' no es válida.";
					//
					throw new Exception(string.Format(ms, obj));
				}
				//
				string msg = "Operator '{0}' cannot be applied to operands of type 'string' and 'string'";
				throw new Exception(string.Format(msg, "<="));
			}
		}

		/// <summary>
		/// Reprecenta (>) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object CompareObjectGreater(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right)) return (((int)left) > ((int)right)) ? "true" : "false";
				//
				else if (isReal(left) && isReal(right)) return (((double)left) > ((double)right)) ? "true" : "false";
				//
				else if (isInt(left) && isReal(right)) return (((int)left) > ((double)right)) ? "true" : "false";
				//
				else // if (isReal(left) && isInt(right))
					return (((double)left) > ((int)right)) ? "true" : "false";
			}
			else
			{
				if ((left == null || isNumeric(left) || !isNumeric(left)) &&
					(right == null || isNumeric(right) || !isNumeric(right)))
				{
					string ms = "El operador '>' no se puede aplicar a operandos del tipo '{0}' y '{1}'";
					object obj = isInt(left) ? "int" : isReal(left) ? "double" : isString(left) ? "string" : "null";
					object obj2 = isInt(right) ? "int" : isReal(right) ? "double" : isString(right) ? "string" : "null";

					//
					throw new Exception(string.Format(ms, obj, obj2));
				}

				if (isNumeric(left) || isNumeric(right))
				{
					object obj = isNumeric(left) ? right : left;
					string ms = "La conversión de la cadena \"{0}\" en el tipo 'Numeric' no es válida.";
					//
					throw new Exception(string.Format(ms, obj));
				}
				//
				string msg = "Operator '{0}' cannot be applied to operands of type 'string' and 'string'";
				throw new Exception(string.Format(msg, ">"));
			}
		}

		/// <summary>
		/// Reprecenta (>=) de dos objetos
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static object CompareObjectGreaterEqual(object left, object right)
		{
			if (isNumeric(left) && isNumeric(right))
			{
				if (isInt(left) && isInt(right)) return (((int)left) >= ((int)right)) ? "true" : "false";
				//
				else if (isReal(left) && isReal(right)) return (((double)left) >= ((double)right)) ? "true" : "false";
				//
				else if (isInt(left) && isReal(right)) return (((int)left) >= ((double)right)) ? "true" : "false";
				//
				else // if (isReal(left) && isInt(right))
					return (((double)left) >= ((int)right)) ? "true" : "false";
			}
			else
			{
				if ((left == null || isNumeric(left) || !isNumeric(left)) &&
					(right == null || isNumeric(right) || !isNumeric(right)))
				{
					string ms = "El operador '>=' no se puede aplicar a operandos del tipo '{0}' y '{1}'";
					object obj = isInt(left) ? "int" : isReal(left) ? "double" : isString(left) ? "string" : "null";
					object obj2 = isInt(right) ? "int" : isReal(right) ? "double" : isString(right) ? "string" : "null";

					//
					throw new Exception(string.Format(ms, obj, obj2));
				}

				if (isNumeric(left) || isNumeric(right))
				{
					object obj = isNumeric(left) ? right : left;
					string ms = "La conversión de la cadena \"{0}\" en el tipo 'Numeric' no es válida.";
					//
					throw new Exception(string.Format(ms, obj));
				}
				//
				string msg = "Operator '{0}' cannot be applied to operands of type 'string' and 'string'";
				throw new Exception(string.Format(msg, ">="));
			}
		}

		/// <summary>
		/// Convierte el simbolo del objeto.
		/// </summary>
		/// <param name="Operand"></param>
		/// <returns></returns>
		public static object NegateObject(object Operand)
		{
			if (isNumeric(Operand))
			{
				if (isInt(Operand)) return newOperand(Operand, true);
				else if (isReal(Operand)) return newOperand(Operand, false);
			}

			string msg = "La conversión de la cadena \"{0}\" en el tipo 'Double' no es válida.";
			throw new Exception(string.Format(msg, Operand));
		}

		/// <summary>
		/// Reprecenta (+)
		/// </summary>
		/// <param name="Operand"></param>
		/// <returns></returns>
		public static object PlusObject(object Operand)
		{
			if (isNumeric(Operand)) return Operand;

			string msg = "La conversión de la cadena \"{0}\" en el tipo 'Double' no es válida.";
			throw new Exception(string.Format(msg, Operand));
		}

		// ===================================================================

		private static bool isNumeric(object obj)
		{
			return obj is int ||
				obj is short ||
				obj is long ||
				obj is decimal ||
				obj is double ||
				obj is float ||
				obj is byte;
		}

		private static bool isInt(object obj)
		{
			if (obj is int) return true;
			return false;
		}

		private static bool isReal(object obj)
		{
			if (obj is double || obj is float || obj is decimal) return true;
			return false;
		}

		private static bool isString(object obj)
		{
			if (obj is string) return true;
			return false;
		}

		private static bool isNegate(object Operand)
		{
			string value = Operand.ToString();
			if (value.Contains("-")) return true;

			return false;
		}

		private static object newOperand(object Operand, bool isInt)
		{
			string value = Operand.ToString();
			if (isInt)
			{
				if (value.Substring(0, 1).Equals("-")) value = value.Remove(0, 1);
				else
					value = "-" + value;
				//
				return (int)Convert.ToInt32(value);
			}
			else
			{
				if (value.Substring(0, 1).Equals("-")) value = value.Remove(0, 1);
				else
					value = "-" + value;
				//
				return (double)Convert.ToDouble(value);
			}
		}
	}
}