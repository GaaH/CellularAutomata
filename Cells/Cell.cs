namespace CellularAutomata.Cells
{
    public class Cell<State>
    {
        public State Internal { get; }
        public Vector<int> Position { get; }

        public Cell(State state, Vector<int> position)
        {
            Internal = state;
            Position = position;
        }

        public Cell<State> WithState(State state)
        {
            return new Cell<State>(state, Position);
        }
    }
}