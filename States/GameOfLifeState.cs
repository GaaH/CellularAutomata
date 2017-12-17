using System;

namespace CellularAutomata.States
{
    public class GameOfLifeState : IEquatable<GameOfLifeState>
    {
        private bool Value { get; }
        public bool IsAlive => Value;
        public bool IsDead => !Value;

        private GameOfLifeState(bool value)
        {
            Value = value;
        }

        public static GameOfLifeState Alive()
        {
            return new GameOfLifeState(true);
        }

        public static GameOfLifeState Dead()
        {
            return new GameOfLifeState(false);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var obj_state = obj as GameOfLifeState;
            return Equals(obj_state);
        }

        public bool Equals(GameOfLifeState obj)
        {
            return Value == obj.Value;
        }

        public override int GetHashCode()
        {
            return Value ? 1 : 0;
        }

        public static bool operator==(GameOfLifeState obj1, GameOfLifeState obj2)
        {
            if (((object) obj1) == null || ((object) obj2) == null)
            {
                Object.Equals(obj1, obj2);
            }

            return obj1.Equals(obj2);
        }

        public static bool operator!=(GameOfLifeState obj1, GameOfLifeState obj2)
        {
            return !(obj1 == obj2);
        }
    }
}