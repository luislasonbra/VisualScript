using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

namespace VisualScript.Commads
{
	public class DefineEnumCommand : Stmt
	{
		private string name;
		private string[] methodesNames;

		public DefineEnumCommand(string name, string[] methodesNames)
		{
			this.name = name;
			this.methodesNames = methodesNames;
		}

		public void execute(IBindingEnvironment environment)
		{
			environment.SetValue(this.name, new EnumExpression(this.name));
			for (int i = 0; i < methodesNames.Length; i++)
			{
				string newName = name + "." + methodesNames[i];

				//
				environment.SetValue(newName, methodesNames[i]);
			}
		}

		public string EnumName { get { return this.name; } }
		public string[] MethodesNames { get { return this.methodesNames; } }
	}
}
