using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript
{
	public interface IBindingEnvironment
	{
		object GetValue(string name);

		bool ContainsName(string name);

		void SetValue(string name, object value);
	}
}
