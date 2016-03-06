using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	/*
		Define las functiones
	*/
	public class DefineFunctionCommand : Stmt
	{
		private Stmt body;
		private string name;
		private string[] parameterNames;
		private bool hasvariableparameters;

		public DefineFunctionCommand(string name, string[] parameterNames, Stmt body, bool hasvariableparameters)
		{
			this.name = name;
			this.body = body;
			this.parameterNames = parameterNames;
			this.hasvariableparameters = hasvariableparameters;
		}

		public void execute(IBindingEnvironment environment)
		{
			if (Machine.Current.Environment.ContainsName(this.name))
			{
				string msg = "Ya se ha definido una funsion denominada '{0}' en este ámbito";
				throw new Exception(string.Format(msg, this.name));
			}

			//
			Function func = new Function(this.parameterNames, this.body, environment, this.hasvariableparameters);
			Machine.Current.Environment.SetValue(this.name, func);
		}

		public Stmt Body { get { return this.body; } }
		public string FunctionName { get { return this.name; } }
		public string[] ParameterNames { get { return this.parameterNames; } }
		public bool HasVariableParameters { get { return this.hasvariableparameters; } }
	}
}
