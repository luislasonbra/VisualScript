using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript
{
	public interface Stmt { void execute(IBindingEnvironment environment);}

	public interface Expr { object evaluate(IBindingEnvironment environment);}
}
