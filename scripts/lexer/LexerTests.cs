using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Wall_E_Compiler.scripts.lexer;

namespace Wall_E_Compiler.scripts.lexer
{
    public class LexerTests
    {
        private List<Token> Tokenize(string code)
        {
            var lexer = new Lexer(code);
            return lexer.Tokenize().ToList();
        }

        [Theory]
        [InlineData("+", TokenType.Plus)]
        [InlineData("-", TokenType.Minus)]
        [InlineData("%", TokenType.Module)]
        [InlineData("(", TokenType.LeftParenthesis)]

        public void ShouldRecognizeSingleCharTokens(string input, TokenType expectedType)
        {
            var tokens = Tokenize(input);

            Assert.Single(tokens);
            Assert.Equal(expectedType, tokens[0].Type);
            Assert.Equal(input, tokens[0].Lexeme);
        }

        [Theory]
        [InlineData("Spawn", TokenType.Spawn)]
        [InlineData("Color", TokenType.Color)]
        [InlineData("DrawLine", TokenType.DrawLine)]

        public void ShouldRecognizeKeywords(string input, TokenType expectedType)
        {
            var tokens = Tokenize(input);

            Assert.Equal(expectedType, tokens[0].Type);
        }

        [Fact]
        public void ShouldAcceptHyphensInIdentifiers()
        {
            var tokens = Tokenize("mi-variable");

            Assert.Single(tokens);
            Assert.Equal(TokenType.Identifier, tokens[0].Type);
            Assert.Equal("mi-variable", tokens[0].Lexeme);
        }

        [Fact]
        public void ShouldHandleNegativeNumbers()
        {
            var tokens = Tokenize("-42");

            Assert.Single(tokens);
            Assert.Equal(TokenType.Number, tokens[0].Type);
            Assert.Equal(-42, tokens[0].LiteralValue);
        }

        [Fact]
        public void ShouldTokenizeCommandCorrectly()
        {
            var tokens = Tokenize("Spawn(0, 0)");

            var expectedTypes = new[]
            {
            TokenType.Spawn,
            TokenType.LeftParenthesis,
            TokenType.Number,
            TokenType.Comma,
            TokenType.Number,
            TokenType.RightParenthesis
    };

            Assert.Equal(expectedTypes, tokens.Select(t => t.Type));
        }

        [Fact]
        public void ShouldTrackLineNumbers()
        {
            var code = "Spawn(0,\n 0)";
            var tokens = Tokenize(code);

            Assert.Equal(1, tokens[0].Line); // Spawn
            Assert.Equal(1, tokens[3].Line); // Comma
            Assert.Equal(2, tokens[4].Line); // Segundo 0
        }
    }
}
