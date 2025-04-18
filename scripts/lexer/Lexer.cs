using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;

namespace Wall_E_Compiler.scripts.lexer
{
    public class Lexer
    {
        private Regex identifier = new Regex(@"^[a-zA-Z_][a-zA-Z0-9_-]*");
        private Regex number = new Regex(@"^-?\d+");
        private Regex symbol = new Regex(@"^(<-|<=|>=|==|\*\*|&&|\|\||->|[+\-*/%<>,()\[\]])");
        private Regex spaces = new Regex(@"^[\s\t]+");
        private Regex invalid = new Regex(@"^[=;:{}\\]");

        private readonly string[] colors = { "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Black", "White", "Transparent" };

        public List<Token> Tokenize(string input)
        {
            List<Token> tokens = new List<Token>();
            string[] lines = input.Split('\n');
            int currentLine = 0;

            foreach (string line in lines)
            {
                int position = 0;
                bool inColorString = false;
                string colorValue = "";
                (int line, int column) quotePosition = (0, 0);

                while (position < line.Length)
                {
                    char currentChar = line[position];

                    if (currentChar == '"')
                    {
                        if (inColorString)
                        {
                            if (!colors.Contains(colorValue))
                            {
                                throw new Exception($"Invalid color '{colorValue}' at line {currentLine + 1}. Valid colors: {string.Join(", ", colors)}");
                            }

                            tokens.Add(new Token(TokenType.ColorLit, $"\"{colorValue}\"", currentLine + 1, colorValue));
                            colorValue = "";
                        }
                        else
                        {
                            quotePosition = (currentLine + 1, position + 1);
                        }
                        inColorString = !inColorString;
                        position++;
                        continue;
                    }

                    if (inColorString)
                    {
                        colorValue += currentChar;
                        position++;
                        continue;
                    }

                    var spaceMatch = spaces.Match(line.Substring(position));
                    if (spaceMatch.Success)
                    {
                        position += spaceMatch.Length;
                        continue;
                    }

                    var symMatch = symbol.Match(line.Substring(position));
                    if (symMatch.Success)
                    {
                        string value = symMatch.Value;

                        if (value == "-" && position + 1 < line.Length && char.IsDigit(line[position + 1]))
                        {
                            //analiza luego
                        }
                        else if (Token.Types.TryGetValue(value, out TokenType type))
                        {
                            tokens.Add(new Token(type, value, currentLine + 1));
                            position += value.Length;
                            continue;
                        }
                    }
                    
                    var idMatch = identifier.Match(line.Substring(position));
                    if (idMatch.Success)
                    {
                        string value = idMatch.Value;

                        if (Token.Types.TryGetValue(value, out TokenType keywordType))
                        {
                            tokens.Add(new Token(keywordType, value, currentLine + 1));
                        }
                        else
                        {
                            bool isLabel = position == 0 || spaces.IsMatch(line.Substring(0, position));

                            tokens.Add(new Token(
                                isLabel ? TokenType.Label : TokenType.Identifier,
                                value,
                                currentLine + 1
                            ));
                        }
                        position += idMatch.Length;
                        continue;
                    }

                    var numMatch = number.Match(line.Substring(position));
                    if (numMatch.Success)
                    {
                        string value = numMatch.Value;
                        try
                        {
                            int numValue = int.Parse(value);
                            tokens.Add(new Token(TokenType.Number, value, currentLine + 1, numValue));
                            position += numMatch.Length;
                            continue;
                        }
                        catch (Exception)
                        {
                            throw new Exception($"Invalid number '{value}' at line {currentLine + 1}");
                        }
                    }

                    throw new Exception($"Invalid character '{currentChar}' at line {currentLine + 1}, column {position + 1}");
                }

                if (inColorString)
                {
                    throw new Exception($"Missing closing quote for color at line {quotePosition.line}, column {quotePosition.column}");
                }

                if (currentLine < lines.Length - 1)
                {
                    tokens.Add(new Token(TokenType.NewLine, "\\n", currentLine + 1));
                }

                currentLine++;
            }

            tokens.Add(new Token(TokenType.EndOfFile, "", currentLine));
            return tokens;
        }
    }
}