using System.Collections.Generic;

namespace CellularAutomata.Neighborhoods
{
    public interface INeighborhood
    {
        IEnumerable<Vector<int>> Neighbors(Vector<int> point);
    }
}