namespace Wall_E_Compiler.scripts.lexer
{
    public class Lexer
    {
        private readonly string sourceCode;
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
                    continue;
                }

                Token token = ClassifyToken();
                if (token != null)
                {
                    yield return token;
                }
                else
                    ThrowSyntaxError($"Carácter inesperado '{currentChar}' en la línea {line}.");

                yield return new Token(TokenType.EndOfFile, "", line);
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

            if(char.IsLetter(currentChar))
            {
                return ReadIdentifierOrKeyword();
            }

            if( currentChar  == '"' )
            {
                return ReadString();
            }

            return null;
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

            string lexeme = sourceCode.Substring(start, position - start);

            TokenType? keywordType = GetKeywordType(lexeme);
            if (keywordType.HasValue) 
                return new Token(keywordType.Value, lexeme, line);

            return new Token(TokenType.Identifier, lexeme, line);
        }

        private TokenType? GetKeywordType(string lexeme)
        {
            return lexeme switch
            {
                "Spawn" => TokenType.Spawn,
                "Color" => TokenType.Color,
                "Size" => TokenType.Size,
                "DrawLine" => TokenType.DrawLine,
                "DrawCircle" => TokenType.DrawCircle,
                "DrawRectangle" => TokenType.DrawRectangle,
                "Fill" => TokenType.Fill,
                "GetActualX" => TokenType.GetActualX,
                "GetActualY" => TokenType.GetActualY,
                "GetCanvasSize" => TokenType.GetCanvasSize,
                "GetColorCount" => TokenType.GetColorCount,
                "IsBrushColor" => TokenType.IsBrushColor,
                "IsBrushSize" => TokenType.IsBrushSize,
                "IsCanvasColor" => TokenType.IsCanvasColor,
                "GoTo" => TokenType.GoTo,
                _ => null
            };
        }
        
        private Token ReadString()
        {
            position++;
            int start = position;

            while (position < sourceCode.Length && sourceCode[position] == '"')
            {
                position++;
            }

            if(position >= sourceCode.Length)
            {
                ThrowSyntaxError("String no terminado.");
            }

            string lexeme = sourceCode.Substring(start, position - start);
            position++;

            if(Enum.TryParse<Color>(lexeme, ignoreCase: true, out Color color))
            {
                return new Token(TokenType.ColorLit, lexeme, line, color);
            }

            return new Token(TokenType.String, lexeme, line, lexeme);

        }

        public enum Color
        {
            Red,
            Blue,
            Green,
            Yellow,
            Orange,
            Purple,
            Black,
            White,
            Transparent
        }

        private Token ThrowSyntaxError(string message)
        {
             throw new SyntaxErrorException(line, position, message);
        }
    }
}
