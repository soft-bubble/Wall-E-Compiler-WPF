﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wall_E_Compiler
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

        public static readonly Dictionary<string, TokenType> Types = new Dictionary<string, TokenType>
        {
            {"Spawn", TokenType.Spawn},
            {"Color", TokenType.Color},
            {"Size", TokenType.Size},
            {"DrawLine", TokenType.DrawLine},
            {"DrawCircle", TokenType.DrawCircle},
            {"DrawRectangle", TokenType.DrawRectangle},
            {"Fill", TokenType.Fill},
            {"GoTo", TokenType.GoTo},
            {"GetActualX", TokenType.GetActualX},
            {"GetActualY", TokenType.GetActualY},
            {"GetCanvasSize", TokenType.GetCanvasSize},
            {"GetColorCount", TokenType.GetColorCount},
            {"IsBrushColor", TokenType.IsBrushColor},
            {"IsBrushSize", TokenType.IsBrushSize},
            {"IsCanvasColor", TokenType.IsCanvasColor},
            {"<-", TokenType.LeftArrow},
            {"<=", TokenType.LessOrEqual},
            {">=", TokenType.GreaterOrEqual},
            {"==", TokenType.Equal},
            {"**", TokenType.Power},
            {"&&", TokenType.And},
            {"||", TokenType.Or},
            {"+", TokenType.Plus},
            {"-", TokenType.Minus},
            {"*", TokenType.Multiply},
            {"/", TokenType.Divide},
            {"%", TokenType.Module},
            {"<", TokenType.Less},
            {">", TokenType.Greater},
            {"(", TokenType.LeftParenthesis},
            {")", TokenType.RightParenthesis},
            {"[", TokenType.LeftBracket},
            {"]", TokenType.RightBracket},
            {",", TokenType.Comma}
        };

        public override string ToString() => $"{Type} '{Lexeme}' at line {Line}";
    }

    public enum TokenType
    {
        Spawn, Color, Size, DrawLine, DrawCircle, DrawRectangle, Fill, GoTo,
        GetActualX, GetActualY, GetCanvasSize, GetColorCount, IsBrushColor, IsBrushSize, IsCanvasColor,
        LeftArrow, LessOrEqual, GreaterOrEqual, Equal, Power, And, Or, Plus, Minus, Multiply, Divide, Module, Less, Greater,
        LeftParenthesis, RightParenthesis, LeftBracket, RightBracket, Comma,
        Number, ColorLit,
        True, False,
        Identifier, Label,
        NewLine, EndOfFile
    }

    public enum ASType
    {
        Num, Bool, ColorP, Evaluation, Chunk, Spawn, Color, Size, DrawLine, DrawCircle, Fill, DrawRectangle, 
        GoTo, GetActualX, GetActualY, GetColorCount, GetCanvasSize, IsBrushColor, IsBrushSize, IsCanvasColor
    }

    public enum PixelColor
    {
        Transparent, White, Black, Green, Blue, Yellow, Red, Orange, Purple
    }

}
