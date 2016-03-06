using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Expression;

namespace VisualScript
{
	public interface IClassicObject : IObject
	{
		IClass GetClass();
	}
}
