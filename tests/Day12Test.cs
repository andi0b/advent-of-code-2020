using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day12Test
    {
        private string[] input =
        {
            "F10",
            "N3",
            "F7",
            "R90",
            "F11",
        };

        [Fact] Day12 GetInstance() => new Day12(input);

        [Fact] void Part1() => GetInstance().Part1().Should().Be(25);
    }
}