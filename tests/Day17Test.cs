using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day17Test
    {
        private string _input = @".#.
..#
###";

        [Fact]
        public void Part1() => new Day17(_input).Part1().Should().Be(112);
        
        [Fact]
        public void Part2() => new Day17(_input).Part2().Should().Be(848);
    }
}