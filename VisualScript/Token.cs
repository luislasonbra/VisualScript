using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript
{
	public class Token
	{
		public int Line { get; set; }

		public int Col { get; set; }

		public string Value { get; set; }

		public TokenType TokenType { get; set; }
	}
}
