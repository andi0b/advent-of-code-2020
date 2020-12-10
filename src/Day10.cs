using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;

namespace aoc_runner
{
    public record Day10(int[] Input)
    {
        private readonly int[] _adapterJolts =
            Input.Append(Input.Max() + 3)
                 .OrderBy(x => x)
                 .ToArray();
        
        public int Part1() =>
            _adapterJolts.Aggregate(
                (prev: 0, diff1: 0, diff3: 0),
                (acc, next) => (next - acc.prev) switch
                {
                    1 => (next, acc.diff1 + 1, acc.diff3),
                    3 => (next, acc.diff1, acc.diff3 + 1),
                    _ => throw new ArgumentOutOfRangeException()
                },
                acc => acc.diff1 * acc.diff3);

        public long Part2() =>
            _adapterJolts.Aggregate(
                (prevJolt: 0, sums: new[] {1L, 0, 0}),
                (acc, nextJolt) =>
                {
                    var joltDiff = nextJolt - acc.prevJolt;
                    var relevantSums = acc.sums[..(4 - joltDiff)];
                    return (nextJolt,
                        Enumerable.Repeat(0L, joltDiff - 1)
                                  .Prepend(relevantSums.Sum())
                                  .Concat(relevantSums)
                                  .ToArray());
                },
                acc => acc.sums.First());
        
        public long Part2_Alternative() =>
            Enumerable.Range(1, _adapterJolts.Max())
                      .Select(possibleJolt => _adapterJolts.Contains(possibleJolt))
                      .Aggregate(new[] {1L},
                                 (acc, isJolt) => acc.Take(2).Prepend(isJolt ? acc.Sum() : 0).ToArray(),
                                 acc => acc.First(x => x != 0));
    }
}