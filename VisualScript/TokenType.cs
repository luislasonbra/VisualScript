using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript
{
	public enum TokenType
	{
		Name, // letras
		Integer, // numeros enteros
		Real, // numeros reales
		String, // "cadena" = [""]
		Separator, // ()[]{},:;
		Operator, // [+ - * / < >]
		Boolean, // Boolean
		EOF, // error
		Null // null
	}
}
