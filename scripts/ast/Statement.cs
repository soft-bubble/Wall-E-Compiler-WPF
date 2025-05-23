﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Wall_E_Compiler;

namespace Wall_E_Compiler
{
    public abstract class Statement : ASTNode
    {
        public Statement(ASType type, int line) : base(type, line) { }
        public abstract void Evaluate(Global global);
    }

    public class GoTo : Statement
    {
        public string Label { get; private set; }
        public Expression Condition { get; private set; }

        public GoTo(ASType type, int line, string label, Expression condition)
            : base(type, line)
        {
            Label = label;
            Condition = condition;
        }

        public override bool IsValid(Global global) 
            => global.Labels.ContainsKey(Label) && Condition.MatchType(ASType.Bool);

        public override void Evaluate(Global global)
        {
            if (!(bool)Condition.Evaluate(global)) return;
            global.Labels[Label].Evaluate(global);
        }
    }

    public class Declaration : Statement
    {
        public string Name { get; private set; }
        public Expression Variable { get; private set; }

        public Declaration(ASType type, string name, int line, Expression variable) : base(type, line)
        {
            Name = name;
            Variable = variable;
        }

        public override bool IsValid(Global global) 
            => Variable.IsValid(global);

        public override void Evaluate(Global global) => global.AddVariable(Name, Variable);
    }
}
