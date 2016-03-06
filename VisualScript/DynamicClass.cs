using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;
using VisualScript.Commads;

namespace VisualScript
{
	// solo crea la nueva clase y carga el constructor de esta. (una sola vez)
	// declara la nueva clase dynamica (NewExpression).
	// Crea una nueva clase dynamica (DynamicClass).
	public class DynamicClass : IClass
	{
		private string name;
		private Dictionary<string, object> members = new Dictionary<string, object>();

		public DynamicClass(string name) { this.name = name; }

		public virtual void SetMember(string name, object value)
		{
			if (members.ContainsKey(name))
			{
				if (value is Function)
				{
					string ms = "'{0}': los nombres de los miembros no pueden coincidir con sus tipos envolventes";
					throw new Exception(string.Format(ms, this.name));
				}
				else
				{
					string ms = "Ya se ha definido una variable local denominada '{0}' en este ámbito";
					throw new Exception(string.Format(ms, name));
				}
			}

			//
			this.members[name] = value;
		}

		public virtual object GetMember(string name)
		{
			if (this.members.ContainsKey(name)) return this.members[name];

			//
			return null;
		}

		public virtual object NewInstance(object[] parameters)
		{
			IObject dynobj = new DynamicClassicObject(this);
			this.NewInstance(dynobj, parameters);

			//
			return dynobj;
		}

		// fix 1
		/// <summary>
		/// llama al constructor de la clase.
		/// </summary>
		/// <param name="dynobj"></param>
		/// <param name="parameters"></param>
		public virtual void NewInstance(IObject dynobj, object[] parameters)
		{
			object constr = this.GetMember(this.name);
			foreach (string name in this.members.Keys)
			{
				object member = this.members[name];
				//if (!(member is ICallable)) dynobj.SetValue(name, member);
				dynobj.SetValue(name, member);
			}

			if (constr == null)
			{
				if (parameters != null && parameters.Length != 0)
				{
					string msg = "No constructor in '{0}' for this arguments";
					throw new InvalidOperationException(string.Format(msg, this.name));
				}

				return;
			}
			dynobj.Invoke(this.name, parameters);

			//
			return;
		}

		public virtual List<string> GetMemberNames()
		{
			List<string> memberNames = new List<string>();
			foreach (string n in this.members.Keys) memberNames.Add(n);

			//
			return memberNames; // this.members.Keys;
		}

		public string Name { get { return this.name; } }
	}
}
