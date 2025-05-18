using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wall_E_Compiler
{
    public abstract class BinaryExpression : Expression
    {
        public Expression Left { get; private set; }
        public Expression Right { get; private set; }
        public Token Operator { get; private set; }

        public BinaryExpression(Expression left, Expression right, Token operation, ASType type) : base(type, left.Line)
        {
            Left = left;
            Right = right;
            Operator = operation;
        }
    }

    public class NumericOperation : BinaryExpression
    {
        public NumericOperation(Expression left, Expression right, Token operation) : base(left, right, operation, ASType.Num) { }
        public override bool IsValid(Global global)
        {
            bool types = Left.IsValid(global) && Right.IsValid(global);
            string operation = (string)Operator.LiteralValue;
            bool op = operation == "+" || operation == "-" || operation == "*" || operation == "/" || operation == "**" || operation == "%";
            return types && op;
        }

        public override object Evaluate(Global Global)
        {
            if (!IsValid(Global)) Global.AddErrors($"Error ocured at line: {Line}.");

            int left = (int)Left.Evaluate(Global);
            int right = (int)Right.Evaluate(Global);

            string op = (string)Operator.LiteralValue;

            switch (op)
            {
                case ("-"):
                    return left - right;
                case ("+"):
                    return left + right;
                case ("*"):
                    return left * right;
                case ("/"):
                    if (right == 0) Global.AddErrors($"Illegal operation at line: {Right.Line}");
                    return left / right;
                case ("**"):
                    return Math.Pow(left, right);
                case ("%"):
                    return left % right;
                default:
                    return left + right;
            }
        }
    }

    public class BooleanOperation : BinaryExpression
    {
        public BooleanOperation(Expression left, Expression right, Token operation) : base(left, right, operation, ASType.Bool) { }
        public override bool IsValid(Global global)
        {
            string op = (string)Operator.LiteralValue;
            bool output = true;

            if (op == "||" || op == "&&") output = Left.MatchType(ASType.Bool) && Right.MatchType(ASType.Bool);

            if (op == "<" || op == ">" || op == "<=" || op == ">=" || op == "==") output = Left.MatchType(ASType.Num) && Right.MatchType(ASType.Num);

            return output && Left.IsValid(global) && Right.IsValid(global);
        }

        public override object Evaluate(Global global)
        {
            string op = (string)Operator.LiteralValue;
            if(op == "||" || op == "&&")
            {
                bool left = (bool)Left.Evaluate(global);
                bool right = (bool)Right.Evaluate(global);
                switch (op)
                {
                    case("||"): 
                        return left || right;
                    case ("&&"):
                        return left && right;
                    default:
                        return left || right;
                }
            }
            else
            {
                int left = (int)Left.Evaluate(global);
                int right = (int)Right.Evaluate(global);
                switch (op)
                {
                    case ("<"):
                        return left < right;
                    case (">"):
                        return left > right;
                    case ("<="):
                        return left <= right;
                    case (">="):
                        return left >= right;
                    case ("=="):
                        return left == right;
                    default:
                        return left == right;
                }
            }
        }
    }
}
