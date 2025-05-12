using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Wall_E_Compiler;

namespace Wall_E_Compiler
{
    public abstract class Expression : ASTNode
    {
        public Expression(ASType type, int line) : base(type, line) { }
        public abstract object? Evaluate();
    }

    public class Boolean : Expression
    {
        public bool Value { get; private set; }
        public Boolean(bool value, int linea) : base(ASType.Bool, linea) { Value = value; }

        public override bool IsValid(Global global) => Type == ASType.Bool;
        public override object? Evaluate() { throw new NotImplementedException(); }
    }

    public class Number : Expression
    {
        public int Value { get; private set; }
        public Number(int value, int linea) : base(ASType.Num, linea) { Value = value; }

        public override bool IsValid(Global global) => Type == ASType.Num;
        public override object? Evaluate() { throw new NotImplementedException(); }
    }

    public class ColorExp : Expression
    {
        public string Value { get; private set; }
        public ColorExp(string value, int linea) : base(ASType.Color, linea) { Value = value; }

        public override bool IsValid(Global global) => Type == ASType.Color;
        public override object? Evaluate() { throw new NotImplementedException(); }
    }

    public class Variable : Expression
    {
        public string Name { get; private set; }
        public Expression Value { get; private set; }
        
        public Variable(string name, Expression value, int line) : base (value.Type, value.Line)
        { Name = name; Value = value; }

        public void ChangeValue(Expression node)
        {
            Value = node;
        }

        public override bool IsValid(Global global) => Value.IsValid(global);
        public override object? Evaluate() { throw new NotImplementedException() ; }
    }

}
