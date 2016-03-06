using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

// my
namespace VisualScript
{
	public class DynamicClassicObject : DynamicObject, IClassicObject
	{
		private IClass objclass;
		private string classname;

		public DynamicClassicObject(IClass objclass)
		{
			this.objclass = objclass;
			this.classname = objclass.Name;
		}

		public IClass GetClass()
		{
			return this.objclass;
		}

		public override object Invoke(string name, object[] parameters)
		{
			object value = this.GetValue(name);
			if (value == null || !(value is ICallable)) value = this.objclass.GetMember(name);
			//
			if (value == null)
			{
				return base.Invoke(name, parameters);
			}

			if (!(value is ICallable))
			{
				if (parameters == null) return value;
				throw new InvalidOperationException(string.Format("'{0}' is not a method", name));
			}

			ICallable method = (ICallable)value;
			IBindingEnvironment objenv = new ObjectEnvironment(this, method.Environment);

			//
			return method.Invoke(objenv, parameters);
		}
	}
}
