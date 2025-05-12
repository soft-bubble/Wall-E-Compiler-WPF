using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wall_E_Compiler
{
    public static class ConsoleTester
    {

        public static void DebugLexerOutput(string code)
        {
            Console.WriteLine("\n=== DEBUG LEXER OUTPUT ===");
            Console.WriteLine("Input code:\n" + code + "\n");

            var lexer = new Lexer();
            var tokens = lexer.Tokenize(code);
            int counter = 0;

            Console.WriteLine("Tokens found:");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("| #  | Type            | Lexeme        | Literal  |");
            Console.WriteLine("--------------------------------------------------");

            foreach (var token in tokens)
            {
                string literalStr = token.LiteralValue != null ? token.LiteralValue.ToString() : "null";
                Console.WriteLine($"| {counter++,3} | {token.Type,-15} | {token.Lexeme,-13} | {literalStr,-8} |");

                if (token.Type == TokenType.EndOfFile)
                    break;
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Total tokens: {counter} (including EOF)");
            Console.WriteLine("=== END DEBUG ===");
        }
    }

}
