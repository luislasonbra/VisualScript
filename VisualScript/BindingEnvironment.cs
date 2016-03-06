using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript
{
	public class BindingEnvironment : IBindingEnvironment
	{
		private IBindingEnvironment parent;
		private Dictionary<string, object> values = new Dictionary<string, object>();

		public BindingEnvironment() { }
		public BindingEnvironment(IBindingEnvironment parent) : this() { this.parent = parent; }

		public virtual bool ContainsName(string name)
		{
			return this.values.ContainsKey(name);
		}

		// fix 1
		public virtual object GetValue(string name)
		{
			if (!this.values.ContainsKey(name))
			{
				if (this.parent != null) return this.parent.GetValue(name);

				//if (token != null)
				//{
				//    string msg2 = "El nombre '{0}' no existe en el contexto actual\nLine: {1}\nCol: {2}";
				//    throw new Exception(string.Format(msg2, token.Value, token.Line, token.Col));
				//}

				//
				string msg = "El nombre '{0}' no existe en el contexto actual";
				throw new Exception(string.Format(msg, name));
			}

			//
			return this.values[name];
		}

		public virtual void SetValue(string name, object value)
		{
			//if (this.parent != null && !this.values.ContainsKey(name) && this.parent.ContainsName(name))
			//{
			//    this.parent.SetValue(name, value);
			//    return;
			//}

			if (this.parent != null && !this.values.ContainsKey(name) &&
				(this.parent is LocalBindingEnvironment) && this.parent.ContainsName(name))
			{
				this.parent.SetValue(name, value);
				return;
			}

			//
			this.values[name] = value;
		}

		protected IBindingEnvironment Parent { get { return this.parent; } }
		protected Dictionary<string, object> Values { get { return this.values; } }
	}
}
