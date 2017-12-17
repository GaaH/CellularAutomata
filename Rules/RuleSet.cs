using System;
using System.Collections.Generic;
using System.Linq;
using CellularAutomata.Cells;

namespace CellularAutomata.Rules
{
    public class RuleSet<State>
    {
        public IEnumerable<IRule<State>> Rules { get; }

        public RuleSet(IEnumerable<IRule<State>> rules)
        {
            if (rules == null)
            {
                throw new ArgumentNullException("The set of rules cannot be null", nameof(rules));
            }
            if (rules.Count() == 0)
            {
                throw new ArgumentException("The set of rules cannot be empty", nameof(rules));
            }

            Rules = rules;
        }

        public RuleSet(params IRule<State>[] rules) : this(rules as IEnumerable<IRule<State>>)
        {

        }

        public Cell<State> Apply(Cell<State> cell)
        {
            foreach (var rule in Rules)
            {
                var new_cell = rule.Apply(cell);
                if (new_cell != null)
                {
                    return new_cell;
                }
            }

            throw new NoRuleApplyableException();
        }

        public void AddRule(IRule<State> rule)
        {
            Rules.Append(rule);
        }
    }
}