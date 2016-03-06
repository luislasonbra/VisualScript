using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

namespace VisualScript.Commads
{
	public class DefineClassCommand : Stmt
	{
		private string name;
		private string[] memberNames;
		private List<Expr> memberExpressions;

		public DefineClassCommand(string name, string[] memberNames, List<Expr> memberExpressions)
		{
			this.name = name;
			this.memberNames = memberNames;
			this.memberExpressions = memberExpressions;
		}

		public void execute(IBindingEnvironment environment)
		{
			DynamicClass dynclass = new DynamicClass(this.name);
			int k = 0;

			if (this.memberExpressions != null)
			{
				foreach (Expr expression in this.memberExpressions)
				{
					string name = this.memberNames[k++];
					object value = expression != null ? expression.evaluate(environment) : null;

					//
					dynclass.SetMember(name, value);
				}
			}

			//
			Machine.Current.Environment.SetValue(this.name, dynclass);
		}

		public string ClassName { get { return this.name; } }
		public string[] MemberNames { get { return this.MemberNames; } }
		public List<Expr> MemberExpressions { get { return this.memberExpressions; } }
	}
}
