using CellularAutomata.Cells;

namespace CellularAutomata.Rules
{
    public interface IRule<State>
    {
        Cell<State> Apply(Cell<State> cell);
    }
}