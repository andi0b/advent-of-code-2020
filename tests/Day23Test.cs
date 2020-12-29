using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day23Test
    {
        [Fact]
        public void Part1() => new Day23().Part1("389125467").Should().Be("67384529");
    }
}