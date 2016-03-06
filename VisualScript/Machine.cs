using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript
{
	public class Machine
	{
		private static Machine current;

		// ////////////////////////////////////////////////////////////////////////////
		private static LoopStatus currentLoopStatus;
		private static FunctionStatus currentFunctionStatus;
		private static TryCommandFolder.ErrorTryStatus currentErrorTryStatus;
		// ////////////////////////////////////////////////////////////////////////////

		private BindingEnvironment environment = new BindingEnvironment();

		public Machine() : this(true) { }
		public Machine(bool iscurrent) { if (iscurrent) this.SetCurrent(); }

		public static void SetCurrent(Machine machine) { current = machine; }

		public void SetCurrent() { current = this; }

		// ////////////////////////////////////////////////////////////////////////////
		public static LoopStatus CurrentLoopStatus
		{
			get
			{
				if (currentLoopStatus == null) currentLoopStatus = new LoopStatus();
				return currentLoopStatus;
			}
			set { currentLoopStatus = value; }
		}

		public static FunctionStatus CurrentFunctionStatus
		{
			get
			{
				if (currentFunctionStatus == null) currentFunctionStatus = new FunctionStatus();
				return currentFunctionStatus;
			}
			set { currentFunctionStatus = value; }
		}

		public static TryCommandFolder.ErrorTryStatus CurrentErrorTryStatus
		{
			get
			{
				if (currentErrorTryStatus == null) currentErrorTryStatus = new TryCommandFolder.ErrorTryStatus();
				return currentErrorTryStatus;
			}
			set { currentErrorTryStatus = value; }
		}
		// ////////////////////////////////////////////////////////////////////////////

		public static Machine Current { get { return current; } }
		public BindingEnvironment Environment { get { return this.environment; } }
	}
}
