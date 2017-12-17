using System;
using System.Collections.Generic;
using System.Linq;

namespace CellularAutomata.Neighborhoods
{
    public class MooreNeighborhood : INeighborhood
    {
        public Bounds Bounds { get; }

        public MooreNeighborhood(Bounds bounds)
        {
            if (bounds.Dimensions != 2)
            {
                throw new ArgumentException("Not implemented for spaces with dimensions other than 2", nameof(bounds));
            }

            Bounds = bounds;
        }

        public IEnumerable<Vector<int>> Neighbors(Vector<int> point)
        {
            if (point.Length != Bounds.Dimensions)
            {
                throw new ArgumentException($"Dimensions do not match ({point.Length} is not {Bounds.Dimensions})", nameof(point));
            }

            var neighbors = new List<Vector<int>>();

            foreach (var ix in new int[] { -1, 0, 1 })
            {
                foreach (var iy in new int[] { -1, 0, 1 })
                {
                    // Skip if same element
                    if (ix == 0 && iy == 0)
                    {
                        continue;
                    }

                    var neighbor_position = new Vector<int>(point);
                    neighbor_position.Values[0] += ix;
                    neighbor_position.Values[1] += iy;

                    if (Bounds.Contains(neighbor_position))
                    {
                        neighbors.Add(neighbor_position);
                    }
                }
            }

            return neighbors;
        }
    }
}