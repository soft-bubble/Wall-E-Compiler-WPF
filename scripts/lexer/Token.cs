using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wall_E_Compiler.scripts.lexer
{
    public class Token
    {
        public TokenType Type { get; }
        public string Lexeme { get; }
        public int Line { get; }
        public object LiteralValue { get; } 

        public Token(TokenType type, string lexeme, int line, object literalValue = null)
        {
            Type = type;
            Lexeme = lexeme;
            Line = line;
            LiteralValue = literalValue;
        }

        public override string ToString() => $"{Type} '{Lexeme}' at {Line}";
    }

    public enum TokenType
    {
        // 1. Intrucciones 
        Spawn, Color, Size, DrawLine, DrawCircle, DrawRectangle, Fill, 

        // 2. Funciones
        GetActualX, GetActualY, GetCanvasSize, GetColorCount, IsBrushColor, IsBrushSize, IsCanvasColor,

        // 3. Operadores 
        Plus, Minus, Multiply, Divide, Power, Module, Equal, Less, LessOrEqual, Greater, GreaterOrEqual, Or, And,

        // 4. Herramientas de orden
        Comma, LeftParenthesis, RightParenthesis, LeftBracket, RightBracker, Arrow,

        // Expresiones arítmeticas: Literales 
        Number, ColorLit, String, Identifier,

        //Ciclo
        GoTo,

        //Entrada y cierre
        NewLine, EndOfFile
    }
}
