using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Expression
{
	public enum IncrementOperator
	{
		none,
		Increment,			// ++
		Decrement,			// --
		IncrementEqual,		// +=
		DecrementEqual,		// -=
		DivideEqual,		// /=
		MultiplyEqual,		// *=
		ModuloEqual			// %=
	}
}
