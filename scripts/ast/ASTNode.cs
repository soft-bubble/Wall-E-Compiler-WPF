using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Wall_E_Compiler;

namespace Wall_E_Compiler
{
    public abstract class ASTNode
    {
        public ASType Type { get; set; }
        public int Line { get; protected set; }

        public ASTNode (ASType type, int line)
        {
            this.Type = type;
            this.Line = line;
        }

        public abstract bool IsValid(Global global);
        public bool MatchType(ASType type) => Type == type;
    }

    
}
