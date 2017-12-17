using System;
using System.Linq;
using System.Text;
using CellularAutomata.Neighborhoods;
using CellularAutomata.Rules;
using CellularAutomata.Rules.GameOfLife;
using CellularAutomata.States;

namespace CellularAutomata
{
    class Program
    {
        public static string AsString(World<GameOfLifeState> world)
        {
            var sb = new StringBuilder();
            var enumerator = world.Cells.GetEnumerator();

            var width = world.Bounds.Size.Values[0];
            var height = world.Bounds.Size.Values[1];

            sb.AppendLine(new String('#', (int)width + 2));

            for (var y = 0 ; y < height ; ++y)
            {
                sb.Append('#');
                for (var x = 0 ; x < width ; ++x)
                {
                    enumerator.MoveNext();
                    var c = enumerator.Current;
                    sb.Append(c.Internal.IsAlive ? '@' : ' ');
                }
                sb.Append("#\n");
            }

            sb.Append(new String('#', (int)width + 2));
            return sb.ToString();
        }

        static void Main(string[] args)
        {
            var rng = new Random();
            var width = 100;
            var height = 20;
            var living_rate = 0.25;
            var bounds = new Bounds(new Vector<int>(0, 0), new Vector<int>(width, height));

            var states = Enumerable
                .Range(0, width * height)
                .Select(i => rng.NextDouble() <= living_rate ? GameOfLifeState.Alive() : GameOfLifeState.Dead());

            var neighborhood = new MooreNeighborhood(bounds);
            var world = new World<GameOfLifeState>(states, bounds, neighborhood);
            var rules = new RuleSet<GameOfLifeState>(new BirthRule(world), new DeathRule(world));
            world.SetRules(rules);

            var running = true;

            while(running)
            {
                Console.WriteLine(AsString(world));
                world = world.NextStep();

                var key = Console.ReadKey();
                running = key.KeyChar != 'q';
            }
        }
    }
}
