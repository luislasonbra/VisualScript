using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Commads;

namespace VisualScript.Expression
{
	public class FunctionExpression : Expr
	{
		private Stmt body;
		private string[] parameterNames;
		private bool hasvariableparameters;

		public FunctionExpression(string[] parameterNames, Stmt body, bool hasvariableparameters)
		{
			this.body = body;
			this.parameterNames = parameterNames;
			this.hasvariableparameters = hasvariableparameters;
		}

		public object evaluate(IBindingEnvironment environment)
		{
			return new Function(this.parameterNames, this.body, environment, this.hasvariableparameters);
		}

		public Stmt Body { get { return this.body; } }
		public string[] ParameterNames { get { return this.parameterNames; } }
		public bool HasVariableParameters { get { return this.hasvariableparameters; } }
	}
}
