using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

namespace VisualScript.Commads
{
	public class Function : ICallable
	{
		private Stmt body;
		private int arity;
		private string[] parameterNames;
		private bool hasvariableparameters;

		private IBindingEnvironment environment;

		public Function(string[] parameterNames, Stmt body) : this(parameterNames, body, null, false) { }

		public Function(string[] parameterNames, Stmt body, IBindingEnvironment environment, bool hasvariableparameters)
		{
			this.body = body;
			this.parameterNames = parameterNames;
			this.hasvariableparameters = hasvariableparameters;

			if (parameterNames == null) this.arity = 0;
			else
				this.arity = parameterNames.Length;

			//
			this.environment = environment;
		}

		public object Invoke(object[] arguments) { return this.Invoke(this.environment, arguments); }

		public object Invoke(IBindingEnvironment environment, object[] arguments)
		{
			int argcount = 0;
			if (arguments != null) argcount = arguments.Length;

			if (this.arity != argcount)
			{
				if (!this.hasvariableparameters || this.arity - 1 > argcount)
					throw new InvalidOperationException("Invalid number of arguments");
			}

			LocalBindingEnvironment newenv = new LocalBindingEnvironment(environment);
			if (this.hasvariableparameters)
			{
				List<object> list = new List<object>();
				if (arguments != null)
					for (int i = 0; i < arguments.Length; i++) list.Add(arguments[i]);
				//else
				//    list = null;

				//
				newenv.SetValue(this.parameterNames[this.arity - 1], new TestArray(list));
			}
			else
			{
				for (int k = 0; k < argcount; k++)
					newenv.SetValue(this.parameterNames[k], arguments[k]);
			}

			FunctionStatus fstatus = Machine.CurrentFunctionStatus;
			Machine.CurrentFunctionStatus = new FunctionStatus();

			try
			{
				this.body.execute(newenv);
				return Machine.CurrentFunctionStatus.ReturnValue;
			}
			finally
			{
				Machine.CurrentFunctionStatus = fstatus;
			}
		}

		public Stmt Body { get { return this.body; } }
		public string[] ParameterNames { get { return this.parameterNames; } }
		public IBindingEnvironment Environment { get { return this.environment; } }
		public bool HasVariableParameters { get { return this.hasvariableparameters; } }
		public int Arity { get { return this.parameterNames == null ? 0 : this.parameterNames.Length; } }
	}
}