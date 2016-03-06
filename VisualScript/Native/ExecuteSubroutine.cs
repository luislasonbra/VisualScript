using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

namespace VisualScript.Native
{
	public class ExecuteSubroutine : ICallable
	{
		public object Invoke(IBindingEnvironment environment, object[] arguments)
		{
			if (arguments == null || arguments.Length != 1)
				throw new InvalidOperationException("Invalid number of parameters");

			string text = (string)arguments[0];
			Parser parser = new Parser(text);
			Stmt command;
			while ((command = parser.ParseCommand()) != null) command.execute(environment);

			//
			return null;
		}

		public object Invoke(object[] arguments)
		{
			return this.Invoke(Machine.Current.Environment, arguments);
		}

		public int Arity { get { return 1; } }
		public IBindingEnvironment Environment { get { return null; } }
	}
}
