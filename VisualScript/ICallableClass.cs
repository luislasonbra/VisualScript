using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript
{
	public interface ICallableClass
	{
		object Invoke(string name, object[] parameters, object obj);
	}
}
