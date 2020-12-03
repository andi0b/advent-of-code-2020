using System.Collections.Generic;
using System.Linq;

namespace aoc_runner
{
    public class Day03
    {
        public int Part1(string[] input)
        {
            var forest = Forest.Parse(input);
            return TreeCountOnSlope(forest, 3, 1);
        }

        public long Part2(string[] input)
        {
            var forest = Forest.Parse(input);

            (int right, int down)[] slopeDefinitions =
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };

            return slopeDefinitions.Select(s => (long) TreeCountOnSlope(forest, s.right, s.down))
                                   .Aggregate((agg, next) => agg * next);
        }

        private int TreeCountOnSlope(Forest forest, int right, int down)
            => TobogganPositions(forest.RowCount, right, down)
               .Count(pos => forest[pos.x, pos.y]);
        
        public IEnumerable<(int x, int y)> TobogganPositions(int count, int right, int down)
            => from i in Enumerable.Range(0, count)
               select (right * i, down * i);
        
        public record Forest(bool[][] IsTreeMapYx, int PatternWidth)
        {
            // allow out of bounds access (only downward is possible)
            // there are no trees outside our slope
            public bool this[int x, int y]
                => y < RowCount && IsTreeMapYx[y][x % PatternWidth];

            public int RowCount => IsTreeMapYx.Length;
            
            public static Forest Parse(string[] input)
            {
                var isTreeMapYx = (
                    from row in input
                    select (from chr in row
                            select chr == '#').ToArray()
                ).ToArray();

                // we can assume there is at last on row, and all rows are same width
                var patternWidth = isTreeMapYx[0].Length;

                return new Forest(isTreeMapYx, patternWidth);
            }
        }
    }

}