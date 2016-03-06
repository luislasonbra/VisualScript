using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public interface ICallable
	{
		int Arity { get; }

		object Invoke(object[] arguments);

		IBindingEnvironment Environment { get; }

		object Invoke(IBindingEnvironment environment, object[] arguments);
	}
}
