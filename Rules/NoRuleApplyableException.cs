using System;

namespace CellularAutomata.Rules
{
    public class NoRuleApplyableException : Exception
    {
        public NoRuleApplyableException() : base("No applyable rule found")
        {
            
        }
    }
}