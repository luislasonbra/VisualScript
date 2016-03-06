using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript.Commads
{
	public class UsingCommand : Stmt
	{
		private string path;
		private string extencionFile = ".vs";
		private string pathExe = AppDomain.CurrentDomain.BaseDirectory;

		public UsingCommand(string path)
		{
			this.path = pathExe + convertPathFile(path);
		}

		public string convertPathFile(string path)
		{
			string pathFile = "";
			string[] split = path.Split(new char[] { '.' });

			for (int i = 0; i < split.Length; i++)
			{
				if (i < split.Length - 1) pathFile += split[i] + "\\";
				else
					pathFile += split[i];
			}
			pathFile += extencionFile;

			//
			return pathFile;
		}

		public void execute(IBindingEnvironment environment)
		{
			//if (path == null || path.Length != 1)
			//    throw new InvalidOperationException("Invalid number of parameters");

			Parser parser = new Parser(System.IO.File.ReadAllText(path));

			Stmt command;
			while ((command = parser.ParseCommand()) != null)
			{
				if (command is DefineClassCommand || command is DefineFunctionCommand || command is DefineEnumCommand)
					command.execute(environment);
			}
			parser = new Parser(System.IO.File.ReadAllText(path));

			//
			while ((command = parser.ParseCommand()) != null) command.execute(environment);
		}
	}
}
