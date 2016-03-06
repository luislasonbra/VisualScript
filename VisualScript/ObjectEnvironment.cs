using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript
{
	public class ObjectEnvironment : IBindingEnvironment
	{
		private DynamicObject obj;
		private IBindingEnvironment parent;
		public const string ThisName = "this"; // permite usar funciones de la misma clase.

		public ObjectEnvironment(DynamicObject obj) : this(obj, null) { }

		public ObjectEnvironment(DynamicObject obj, IBindingEnvironment parent)
		{
			this.obj = obj;
			this.parent = parent;
		}

		public object GetValue(string name)
		{
			if (name == ThisName) return this.obj;
			object result = this.obj.GetValue(name);
			if (result == null && this.parent != null) return this.parent.GetValue(name);

			//
			return result;
		}

		public void SetValue(string name, object value) { this.obj.SetValue(name, value); }

		public bool ContainsName(string name) { return this.obj.GetNames().Contains(name); }

		public object Object { get { return this.obj; } }
	}
}
