using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day10Test
    {
        [Fact]
        Day10 GetInstance() => new Day10(new[]
        {
            28, 33, 18, 42, 31, 14, 46, 20, 48, 47, 24, 23, 49, 45, 19, 38, 39, 11, 1, 32, 25, 35, 8, 17, 7, 9, 4, 2, 34, 10, 3,
        });

        [Fact] void Part1() => GetInstance().Part1().Should().Be(220);
        [Fact] void Part2() => GetInstance().Part2().Should().Be(19208);
    }
}