using System.Linq;
using CellularAutomata.Cells;
using CellularAutomata.States;

namespace CellularAutomata.Rules.GameOfLife
{
    public class BirthRule : IRule<GameOfLifeState>
    {
        public World<GameOfLifeState> World { get; }

        public BirthRule(World<GameOfLifeState> world)
        {
            World = world;
        }

        public Cell<GameOfLifeState> Apply(Cell<GameOfLifeState> cell)
        {
            if (cell.Internal.IsDead)
            {
                var neighborhood = World.Neighbors(cell);
                var alive_count = neighborhood.Where(c => c.Internal.IsAlive).Count();
                var new_state = alive_count == 3 ? GameOfLifeState.Alive() : GameOfLifeState.Dead();
                return cell.WithState(new_state);
            }

            return null;
        }
    }
}