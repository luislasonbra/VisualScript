using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public interface IObject
	{
		List<string> GetNames();

		object GetValue(string name);

		bool ContainsName(string name);

		void SetValue(string name, object value);

		object Invoke(string name, object[] parameters);

		object Invoke(ICallable method, object[] parameters);
	}
}
