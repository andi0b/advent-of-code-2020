using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day15Test
    {
        [Theory,
         InlineData(new[] {0, 3, 6}, 436),
         InlineData(new[] {1, 3, 2}, 1),
         InlineData(new[] {2, 1, 3}, 10),
         InlineData(new[] {1, 2, 3}, 27),
         InlineData(new[] {2, 3, 1}, 78),
         InlineData(new[] {3, 2, 1}, 438),
         InlineData(new[] {3, 1, 2}, 1836),
        ]
        void Part1(int[] startingNumbers, int expected) => new Day15(startingNumbers).Part1().Should().Be(expected);
    }
}