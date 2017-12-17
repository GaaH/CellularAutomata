using System;
using System.Linq;

namespace CellularAutomata
{
    public class Bounds
    {
        public Vector<int> Start { get; }
        public Vector<int> End { get; }
        public Vector<int> Size { get; }
        public uint Dimensions { get; }

        public Bounds(Vector<int> start, Vector<int> size)
        {
            if (start.Length != size.Length)
            {
                throw new ArgumentException("Dimensions of point and size do not match");
            }

            Start = start;
            End = new Vector<int>(start.Values.Zip(size.Values, (p, s) => p + s));
            Size = size;
            Dimensions = (uint)start.Length;
        }

        public bool Contains(Vector<int> point)
        {
            if (point.Length != Start.Length)
            {
                throw new ArgumentException("Dimensions of point and bounds do not match", nameof(point));
            }

            return Enumerable.Range(0, point.Length).All(i =>
            {
                var start = Start.Values[i];
                var end = End.Values[i];
                var coord = point.Values[i];

                return ((start <= coord) && (coord <= end)) || ((start >= coord) && (coord >= end));
            });
        }
    }
}