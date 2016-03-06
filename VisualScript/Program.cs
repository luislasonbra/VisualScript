using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Commads;

namespace VisualScript
{
	class Program
	{
		static void Main(string[] args)
		{
			// Read File
			string source = System.IO.File.ReadAllText("main.vs");

			// Run it.
			VisualScript vScript = new VisualScript(source);
			vScript.DefineCommands();
			vScript.InvokeFunction("Main", args);
			vScript.ExecuteCommands();

			////////////////////////////////////////////////////////////////////////////
			Console.WriteLine();
			Console.Write("Presione una tecla para continuar...");
			Console.ReadKey();
		}
	}
}
