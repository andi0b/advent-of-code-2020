using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day11Test
    {
        private string[] _input = {
            "L.LL.LL.LL",
            "LLLLLLL.LL",
            "L.L.L..L..",
            "LLLL.LL.LL",
            "L.LL.LL.LL",
            "L.LLLLL.LL",
            "..L.L.....",
            "LLLLLLLLLL",
            "L.LLLLLL.L",
            "L.LLLLL.LL",
        };

        [Fact] Day11 GetInstance() => new Day11(_input);

        [Fact] void Part1() => GetInstance().Part1().Should().Be(37);
        [Fact] void Part2() => GetInstance().Part2().Should().Be(26);
    }
}