using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript
{
	public class LocalBindingEnvironment : BindingEnvironment
	{
		public LocalBindingEnvironment(IBindingEnvironment parent) : base(parent)
		{
			if (parent == null) throw new ArgumentNullException("parent");
		}

		public override void SetValue(string name, object value)
		{
			if (this.Values.ContainsKey(name))
			{
				this.Values[name] = value;
				return;
			}
			
			if (this.Parent != null)
			{
				if (this.Parent.ContainsName(name))
				{
					this.Parent.SetValue(name, value);
					return;
				}
			}

			//
			base.SetValue(name, value);
		}

		public override bool ContainsName(string name)
		{
			if (this.Values.ContainsKey(name)) return true;

			//
			return this.Parent.ContainsName(name);
		}
	}
}
