using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript
{
	public interface IClass
	{
		string Name { get; }

		object GetMember(string name);

		List<string> GetMemberNames();

		object NewInstance(object[] parameters);

		void SetMember(string name, object value);
	}
}
