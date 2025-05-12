using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Wall_E_Compiler;

namespace Wall_E_Compiler
{
    public class Global
    {
        public Dictionary<string, Statement> Labels {  get; set; }
        public Dictionary<string, Expression> Variables { get; set; }
        public List<string> Errors { get; set; }

        public Global()
        {
            Labels = new Dictionary<string, Statement>();
            Variables = new Dictionary<string, Expression>();
            Errors = new List<string>();
        }

        public void AddErrors(string message)
        {
            Errors.Add(message);
        }

        public void AddVariable(string name, Expression variable)
        {
            if(Variables.ContainsKey(name)) Variables[name] = variable;
            else Variables.Add(name, variable);
        }

        public Expression GetVariable(string name, int line)
        {
            if (!Variables.ContainsKey(name))
            {
                AddErrors($"Non assigned variable called at line {line}.");
                return null;
            }
            
            return Variables[name];
        }

        public void AddLabel(string name, Statement label)
        {
            if (Labels.ContainsKey(name))
            {
                AddErrors($"Tried to assign an already existing label at line {label.Line}");
            }
            else Labels.Add(name, label);
        }
        public Statement GetLabel(string name, int line)
        {
            if (!Labels.ContainsKey(name))
            {
                AddErrors($"Non assigned label called at line {line}.");
                return null;
            }
            return Labels[name];
        }
    }
}
