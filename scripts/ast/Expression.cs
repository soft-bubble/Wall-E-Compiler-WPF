using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Wall_E_Compiler.scripts.lexer;

namespace Wall_E_Compiler.scripts.ast
{
    public abstract class Expression : ASTNode
    {
        public Expression(ASType type, int line) : base(type, line) { }
        public abstract void Evaluate();
    }
}
