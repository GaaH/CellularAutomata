using System;
using System.Collections.Generic;
using System.Linq;

namespace CellularAutomata
{
    public class Vector<T>
    {
        public T[] Values { get; }
        public int Length => Values.Length;

        public Vector(params T[] values)
        {
            Values = values;
        }

        public Vector(IEnumerable<T> values)
        {
            Values = values.ToArray();
        }

        public Vector(Vector<T> vec)
        {
            Values = new T[vec.Length];
            Array.Copy(vec.Values, Values, vec.Length);
        }
    }
}