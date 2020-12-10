using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace aoc_runner
{
    public class Day10
    {
        private readonly int[] _adapters;

        public Day10(int[] input)
        {
            _adapters = input.Concat(new[] {0, input.Max() + 3})
                            .OrderBy(x => x)
                            .ToArray();
        }

        public int Part1() =>
            _adapters
               .Skip(1) // skip socket, as we use it as seed for aggregate
               .Aggregate((prev: 0, diff1: 0, diff3: 0),
                               (acc, next) => (next - acc.prev) switch
                               {
                                   1 => (next, acc.diff1 + 1, acc.diff3),
                                   3 => (next, acc.diff1, acc.diff3 + 1),
                                   _ => throw new ArgumentOutOfRangeException()
                               },
                               acc => acc.diff1 * acc.diff3);


        public long Part2()
        {
            var rightPossibilities = new long[_adapters.Length];
            rightPossibilities[_adapters.Length - 1] = 1;

            for (var i = _adapters.Length - 2; i >= 0; i--)
            {
                rightPossibilities[i] = DirectRightPossibilitiesIndices(i).Select(x => rightPossibilities[x]).Sum();
            }

            return rightPossibilities[0];
        }

        private IEnumerable<int> DirectRightPossibilitiesIndices(int index) =>
            from j in Enumerable.Range(index + 1, 3)
            where j < _adapters.Length
            where _adapters[j] - _adapters[index] <= 3
            select j;
    }
}