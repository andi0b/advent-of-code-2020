using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_runner
{
    public record Day17(Day17.PocketDimension InitialDimension)
    {

        public Day17(string input) :this(PocketDimension.Parse(input)) { }

        public int Part1() => Solve(InitialDimension);
        public int Part2() => Solve(InitialDimension with {Enable4ThDimension = true});

        public int Solve(PocketDimension dimension) =>
            Enumerable.Repeat(true, 6)
                      .Aggregate(dimension, (acc, _) => acc.NextIteration())
                      .CubesOn.Count;


        public record PocketDimension(HashSet<(int x, int y, int z, int w)> CubesOn, bool Enable4ThDimension=false)
        {
            public PocketDimension NextIteration()
            {
                var neighbourDirections = GetNeighbourDirections();
                
                var (boundsX, boundsY, boundsZ, boundsW) = GetBounds();

                var next = new HashSet<(int x, int y, int z, int w)>();
                
                if (!Enable4ThDimension) boundsW = (1, -1);
                for (var w = boundsW.min - 1; w <= boundsW.max + 1; w++)
                for (var z = boundsZ.min - 1; z <= boundsZ.max + 1; z++)
                for (var y = boundsY.min - 1; y <= boundsY.max + 1; y++)
                for (var x = boundsX.min - 1; x <= boundsX.max + 1; x++)
                {
                    var onNeighbour =
                        from direction in neighbourDirections
                        let neighbour = (x + direction.x, y + direction.y, z + direction.z, w + direction.w)
                        where CubesOn.Contains(neighbour)
                        select neighbour;
                    
                    // just count until we reach 4
                    var onNeighbourCount = onNeighbour.Take(4).Count();

                    var cur = (x, y, z, w);
                    var curValue = CubesOn.Contains(cur);

                    var newValue = (onNeighbourCount, curValue) switch
                    {
                        (2 or 3, true) => true,
                        (3, false) => true,
                        _ => false
                    };

                    if (newValue)
                        next.Add(cur);
                }

                return this with { CubesOn = next};
            }

            public static PocketDimension Parse(string input)
                => new(new HashSet<(int, int, int, int)>(
                           from line in input.Split(Environment.NewLine).Select((value, num) => (value, num))
                           from chr in line.value.Select((value, num) => (value, num))
                           where chr.value == '#'
                           select (chr.num, line.num, 0, 0))
                );

            private ((int min, int max) x, (int min, int max) y, (int min, int max) z, (int min, int max) w) GetBounds() =>
                CubesOn.Aggregate((
                                      x: (min: int.MaxValue, max: int.MinValue),
                                      y: (min: int.MaxValue, max: int.MinValue),
                                      z: (min: int.MaxValue, max: int.MinValue),
                                      w: (min: int.MaxValue, max: int.MinValue)
                                  ),
                                  (acc, next) => (
                                      (Math.Min(acc.x.min, next.x), Math.Max(acc.x.max, next.x)),
                                      (Math.Min(acc.y.min, next.y), Math.Max(acc.y.max, next.y)),
                                      (Math.Min(acc.z.min, next.z), Math.Max(acc.z.max, next.z)),
                                      (Math.Min(acc.w.min, next.w), Math.Max(acc.w.max, next.w))
                                  ));

            private (int x, int y, int z, int w)[] GetNeighbourDirections() => (
                from x in Enumerable.Range(-1, 3)
                from y in Enumerable.Range(-1, 3)
                from z in Enumerable.Range(-1, 3)
                from w in Enable4ThDimension ? Enumerable.Range(-1, 3) : new[] {0}
                where !(x == 0 && y == 0 && z == 0 && w == 0)
                select (x, y, z, w)
            ).ToArray();
        }
    }
}