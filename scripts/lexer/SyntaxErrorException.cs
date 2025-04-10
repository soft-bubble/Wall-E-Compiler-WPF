using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Wall_E_Compiler.scripts.lexer
{
    public class SyntaxErrorException : Exception
    {
        public int Line { get; }
        public int Position { get; }
        public string ErrorDetails { get; }

        public SyntaxErrorException(int line, int position, string message) :
            base($"Error de sintaxis en línea {line}, posición {position} : {message}")
        {
            Line = line;
            Position = position;
            ErrorDetails = message;
        }
       
    }
}
