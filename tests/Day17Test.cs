using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day17Test
    {
        [Fact]
        public void Part1() => new Day17(@".#.
..#
###").Part1().Should().Be(112);
    }
}