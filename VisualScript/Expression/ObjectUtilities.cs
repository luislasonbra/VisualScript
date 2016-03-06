using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public class ObjectUtilities
	{
		// ===========================================================================================

		public static void SetValue(object obj, string name, object value)
		{
			if (obj is IObject)
			{
				// permite usar y declarar parametros en tiempo de ejecusion
				//if (((IObject)obj).GetValue(name) == null)
				//    throw new Exception(string.Format("'{0}' is not a method", name));

				//
				if (!((IObject)obj).ContainsName(name))
					throw new Exception(string.Format("'{0}' is not a method", name));

				((IObject)obj).SetValue(name, value);

				//
				return;
			}

			// gui
			if (obj is ICallableClass)
			{
				((ICallableClass)obj).Invoke(name, new object[] { value }, obj);
				return;
			}

			throw new Exception(string.Format("'{0}' is not a method", name));
		}

		public static object GetValue(object obj, string name)
		{
			if (obj is IObject)
			{
				if (!((IObject)obj).ContainsName(name))
					throw new Exception(string.Format("'{0}' is not a method", name));

				//
				return ((IObject)obj).GetValue(name);
			}

			//
			return null;
		}

		public static object GetValue(object obj, string name, object[] parameters)
		{
			if (obj is IObject)
			{
				if (!((IObject)obj).ContainsName(name))
				{
					if (obj is DynamicClassicObject)
					{
						DynamicClassicObject obj2 = ((DynamicClassicObject)obj);

						//
						string msg2 = "'{0}' no contiene una definición de '{1}'";
						throw new Exception(string.Format(msg2, obj2.GetClass().Name, name));
					}

					//
					throw new Exception(string.Format("'{0}' is not a method", name));
				}

				//
				if (parameters == null) return ((IObject)obj).GetValue(name);
				return ((IObject)obj).Invoke(name, parameters);
			}

			//
			string msg = "Se requiere una referencia de objeto para el campo o método no estáticos";
			throw new Exception(string.Format(msg, name));
		}

		// ===========================================================================================

		public static void SetIndexedValue(object obj, object[] indexes, object value)
		{
			if (obj is Array)
			{
				SetIndexedValue((Array)obj, indexes, value);
				return;
			}

			if (obj is TestArray)
			{
				SetIndexedValue((TestArray)obj, indexes, value);
				return;
			}

			//
			// TODO as in GetIndexedValue, consider Default member
			throw new InvalidOperationException(string.Format("Not indexed value of type {0}", obj.GetType().ToString()));
		}

		public static void SetIndexedValue(TestArray array, object[] indexes, object value)
		{
			switch (indexes.Length)
			{
				case 1:
					array[(int)indexes[0]] = value;
					return;
			}

			//
			throw new InvalidOperationException("Invalid number of subindices");
		}

		public static object GetIndexedValue(object obj, object[] indexes)
		{
			if (obj is TestArray) return GetIndexedValue((TestArray)obj, indexes);

			//
			return GetValue(obj, string.Empty, indexes);
		}

		private static object GetIndexedValue(TestArray array, object[] indexes)
		{
			switch (indexes.Length)
			{
				case 1:
					int index = (int)indexes[0];

					if (index >= array.Count) throw new Exception("Índice fuera de los límites de la matriz.");

					//
					return array[index];
			}

			//
			throw new InvalidOperationException("Invalid number of subindices");
		}

		#region Tools /////////////////////////////////////////////////////////////////////////////////

		public static string getType(object obj)
		{
			if (IsEnum(obj)) return "enum";
			if (IsNull(obj)) return "null";
			if (IsArray(obj)) return "array";
			if (IsString(obj)) return "string";
			if (IsNumber(obj)) return "numeric"; // number
			if (IsBoolean(obj)) return "boolean";
			if (IsObject(obj)) return "object";
			if (IsClass(obj)) return "class";

			//
			return "null";
		}

		public static bool IsClass(object obj)
		{
			if (obj is DynamicClass) return true;
			return false;
		}

		public static bool IsNumber(object obj)
		{
			return obj is int ||
				obj is short ||
				obj is long ||
				obj is decimal ||
				obj is double ||
				obj is float ||
				obj is byte;
		}

		public static bool IsObject(object obj)
		{
			if (obj is IObject) return true;
			return false;
		}

		public static bool IsNull(object obj)
		{
			if (obj == null) return true;
			if (obj is NullExpression) return true;
			return false;
		}

		public static bool IsInt(object obj)
		{
			if (obj is int) return true;
			return false;
		}

		public static bool IsReal(object obj)
		{
			if (obj is double) return true;
			return false;
		}

		public static bool IsArray(object obj)
		{
			//if (obj is ArrayCommand) return true;
			if (obj is TestArray) return true;
			return false;
		}

		public static bool IsEnum(object obj)
		{
			if (obj is EnumExpression) return true;
			return false;
		}

		public static bool IsString(object obj)
		{
			if (obj is string) return true;
			return false;
		}

		public static bool IsBoolean(object obj)
		{
			if (obj is bool) return true;
			return false;
		}

		#endregion
	}
}
