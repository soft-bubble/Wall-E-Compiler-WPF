using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Wall_E_Compiler.scripts.lexer
{
    public class Lexer
    {
        private string sourceCode;
        private int position;
        private int line;

        public Lexer(string sourceCode)
        {
            this.sourceCode = sourceCode;
            position = 0;
            line = 1;
        }

        public IEnumerable<Token> Tokenize()
        {
            while (position < sourceCode.Length)
            {
                char currentChar = sourceCode[position];    

                if(char.IsWhiteSpace(currentChar))
                {
                    if (currentChar == '\n') line++;
                    position++;
                    break;
                }
            }
        }

        private Token ClassifyToken()
        {
            char currentChar = sourceCode[position];

            switch (currentChar)
            {
                case '(': position++; return new Token(TokenType.LeftParenthesis, "(", line);
                case ')': position++; return new Token(TokenType.RightParenthesis, ")", line);
                case '[': position++; return new Token(TokenType.LeftBracket, "[", line);
                case ']': position++; return new Token(TokenType.RightBracker, "]", line);
                case ',': position++; return new Token(TokenType.Comma, ",", line);
                case '+': position++; return new Token(TokenType.Plus, "+", line);
                case '-':
                    if (position == 0 || char.IsWhiteSpace(sourceCode[position - 1]))
                    {
                        position++;
                        return new Token(TokenType.Minus, "-", line);
                    }
                    break;
                case '*':
                    position++;
                    if(Peek()  == '*') { position++; return new Token(TokenType.Power, "**", line); }
                    return new Token(TokenType.Multiply, "*", line);
                case '/': position++; return new Token(TokenType.Divide, "/", line);
                case '%': position++; return new Token(TokenType.Module, "%", line);
                case '=':
                    if(Peek() == '=') { position += 2; return new Token(TokenType.Equal, "==", line); }
                    return ThrowSyntaxError("Operador '=' no soportado.");
                case '<':
                    if (Peek() == '-') { position += 2; return new Token(TokenType.Arrow, "<-", line); }
                    else if (Peek() == '=') { position += 2; return new Token(TokenType.LessOrEqual, "<=", line); }
                    position++;
                    return new Token(TokenType.Less, "<", line);
                case '>':
                    if (Peek() == '=') { position += 2; return new Token(TokenType.GreaterOrEqual, ">=", line); }
                    position++;
                    return new Token(TokenType.Greater, ">", line);
                case '|':
                    if(Peek() == '|') { position += 2; return new Token(TokenType.Or, "||", line); }
                    return ThrowSyntaxError("Operador '|' no soportado.");
                case '&':
                    if(Peek() == '&') { position += 2; return new Token(TokenType.And, "&&", line); }
                    return ThrowSyntaxError("Operador '&' no soportado.");
            }

            if (char.IsDigit(currentChar))
            {
                return ReadNumber();
            }


        }

        private char Peek()
        {
            if (position >= sourceCode.Length - 1)
                return '\0';
            return sourceCode[position + 1];
        }

        private Token ReadNumber()
        {
            int start = position;
            while (position < sourceCode.Length && char.IsDigit(sourceCode[position]))
            {
                position++;
            }

            string lexeme = sourceCode.Substring(start, position - start);
            int value = int.Parse(lexeme);
            return new Token(TokenType.Number, lexeme, line, value);
        }

        private Token ReadIdentifierOrKeyword()
        {
            int start = position;
            while (position < sourceCode.Length && (char.IsLetterOrDigit(sourceCode[position]) || sourceCode[position] == '-'))
            {
                position++;
            }


        }

        private Token ThrowSyntaxError(string message)
        {
             throw new SyntaxErrorException(line, position, message);
        }
    }
}
