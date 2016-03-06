using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript
{
	public class TestArray
	{
		private List<object> arguments;

		public TestArray(List<object> arguments) { this.arguments = arguments; }

		public void Add(object item) { arguments.Add(item); }

		public override string ToString() { return "TestArray"; }

		public int Count { get { return arguments.Count; } }
		public List<object> Arguments { get { return arguments; } }
		public object this[int index] { get { return arguments[index]; } set { arguments[index] = value; } }
	}
}
