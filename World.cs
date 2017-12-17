using System;
using System.Collections.Generic;
using System.Linq;
using CellularAutomata.Cells;
using CellularAutomata.Neighborhoods;
using CellularAutomata.Rules;

namespace CellularAutomata
{
    public class World<State>
    {
        public IEnumerable<Cell<State>> Cells { get; }
        public RuleSet<State> Rules { get; private set; }
        public INeighborhood Neighborhood { get; }
        public Bounds Bounds { get; }

        public World(IEnumerable<State> states, Bounds bounds, INeighborhood neighborhood)
        {
            Cells = states.Select((st, i) => new Cell<State>(st, IndexToPosition(i)));
            Bounds = bounds;
            Neighborhood = neighborhood;
        }

        public World(IEnumerable<Cell<State>> cells, Bounds bounds, INeighborhood neighborhood)
        {
            Cells = cells;
            Bounds = bounds;
            Neighborhood = neighborhood;
        }

        public World(IEnumerable<State> states, Bounds bounds, INeighborhood neighborhood, RuleSet<State> rules)
        {
            Cells = states.Select((st, i) => new Cell<State>(st, IndexToPosition(i)));
            Bounds = bounds;
            Neighborhood = neighborhood;
            Rules = rules;
        }

            public World(IEnumerable<Cell<State>> cells, Bounds bounds, INeighborhood neighborhood, RuleSet<State> rules)
        {
            Cells = cells;
            Bounds = bounds;
            Neighborhood = neighborhood;
            Rules = rules;
        }

        public World<State> NextStep()
        {
            if (Rules == null)
            {
                throw new Exception("Rules not defined, use the SetRules method");
            }

            var next_cells = Cells
                .Select(Rules.Apply);

            return WithCells(next_cells);
        }

        public World<State> WithRules(RuleSet<State> rules)
        {
            return new World<State>(Cells, Bounds, Neighborhood, rules);
        }

        public World<State> WithCells(IEnumerable<Cell<State>> cells)
        {
            return new World<State>(cells, Bounds, Neighborhood, Rules);
        }

        public int PositionToIndex(Vector<int> position)
        {
            var cumprod = new int[Bounds.Dimensions];
            cumprod[0] = 1;
            for (var i = 1 ; i < Bounds.Dimensions ; ++i)
            {
                cumprod[i] = Bounds.Size.Values[i] * cumprod[i-1];
            }

            return position.Values.Zip(cumprod, (v, p) => v * p).Sum();
        }

        public Vector<int> IndexToPosition(int index)
        {
            var cumprod = new int[Bounds.Dimensions];
            cumprod[0] = Bounds.Size.Values[0];
            for (var i = 1 ; i < Bounds.Dimensions ; ++i)
            {
                cumprod[i] = Bounds.Size.Values[i] * cumprod[i-1];
            }

            var position = new int[Bounds.Dimensions];
            position[0] = index % cumprod[0];
            for (var i = 1 ; i < Bounds.Dimensions ; ++i)
            {
                position[i] = position[i] / cumprod[i];
            }

            return new Vector<int>(position);
        }

        public IEnumerable<Cell<State>> Neighbors (Cell<State> cell)
        {
            var neighbors_position = Neighborhood.Neighbors(cell.Position);
            return neighbors_position
                .Select(PositionToIndex)
                .Select(Cells.ElementAt);
        }

        public void SetRules(RuleSet<State> ruleset)
        {
            Rules = ruleset;
        }
    }
}