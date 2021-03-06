﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

namespace VisualScript
{
	public class DynamicObject : IObject
	{
		private Dictionary<string, object> values = new Dictionary<string, object>();

		public virtual void SetValue(string name, object value)
		{
			this.values[name] = value;
		}

		public virtual object GetValue(string name)
		{
			if (this.values.ContainsKey(name)) return this.values[name];

			//
			return null;
		}

		public virtual List<string> GetNames()
		{
			List<string> names = new List<string>();
			foreach (string n in this.values.Keys) names.Add(n);

			//
			return names;
		}

		public virtual bool ContainsName(string name)
		{
			return this.values.ContainsKey(name);
		}

		public virtual object Invoke(string name, object[] parameters)
		{
			object value = this.GetValue(name);
			if (value == null) throw new InvalidOperationException(string.Format("Unknown member '{0}'", name));

			if (!(value is ICallable))
			{
				if (parameters == null) return value;
				//
				throw new InvalidOperationException(string.Format("'{0}' is not a method", name));
			}

			ICallable method = (ICallable)value;
			IBindingEnvironment objenv = new ObjectEnvironment(this, method.Environment);

			//
			return method.Invoke(objenv, parameters);
		}

		public virtual object Invoke(ICallable method, object[] parameters)
		{
			IBindingEnvironment objenv = new ObjectEnvironment(this, method.Environment);

			//
			return method.Invoke(objenv, parameters);
		}
	}
}
