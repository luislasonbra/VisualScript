using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Native
{
	public class StringLib : ICallableClass, IReadOnly
	{
		public object Invoke(string name, object[] parameters, object obj)
		{
			if (name == "length")
			{
				return parameters[0].ToString().Length;
			}

			//
			return null;
		}

		public override string ToString()
		{
			return "[Object String]";
		}
	}
}
