using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualScript.Commads;
using VisualScript.Native;
using VisualScript.Expression;

namespace VisualScript
{
	public class VisualScript
	{
		private string source;
		private Machine machine;

		public VisualScript(string source)
		{
			this.source = source;
			this.machine = new Machine();

			//
			this.DefineNativeFunctions();
		}

		/// <summary>
		/// Define las funsiones nativas del lenguaje
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void DefineNativeFunctions()
		{
			// default
			this.AddNative("APP", new BasicLib());
			//this.AddNative("eval", new EvaluateFunction());
			//this.AddNative("execute", new ExecuteSubroutine());

			// Primitive Functions
			this.AddNative("print", new PrintSubroutine());
			this.AddNative("println", new PrintLineSubroutine());

			//
			this.AddNative("IO", new IOFileLib());
			this.AddNative("Math", new MathLib());
			this.AddNative("string", new StringLib());
		}

		/// <summary>
		/// Define las funsiones nativas del lenguaje
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void AddNative(string name, object value)
		{
			machine.Environment.SetValue(name, value);
		}

		/// <summary>
		/// Invoca la funsion indicada.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="arguments"></param>
		public void InvokeFunction(string name, object[] arguments)
		{
			if (machine.Environment.ContainsName(name))
			{
				object obj = machine.Environment.GetValue(name);
				if (obj is ICallable) ((ICallable)obj).Invoke(arguments);
			}
		}

		/// <summary>
		/// Define las funsiones
		/// </summary>
		public void DefineCommands()
		{
			Lexer lexer = new Lexer(source);
			Parser parser = new Parser(lexer);

			for (Stmt cmd = parser.ParseCommand(); cmd != null; cmd = parser.ParseCommand())
			{
				if (cmd is DefineClassCommand || cmd is DefineFunctionCommand || cmd is DefineEnumCommand)
					cmd.execute(machine.Environment);
			}
		}

		/// <summary>
		/// Ejecuta las funsiones definidos
		/// </summary>
		public void ExecuteCommands()
		{
			Lexer lexer = new Lexer(source);
			Parser parser = new Parser(lexer);

			for (Stmt cmd = parser.ParseCommand(); cmd != null; cmd = parser.ParseCommand())
			{
				if (!(cmd is DefineClassCommand) && !(cmd is DefineFunctionCommand) && !(cmd is DefineEnumCommand))
					cmd.execute(machine.Environment);
			}
		}
	}
}
