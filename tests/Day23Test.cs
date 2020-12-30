using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day23Test
    {
        [Fact]
        public void Part1() => new Day23("389125467").Part1().Should().Be("67384529");
        [Fact]
        public void Part2() => new Day23("389125467").Part2().Should().Be(149245887792);

        [Fact]
        public void CupCircle_RightOf() =>
            new Day23.CupCircle("389125467", 9).RightOf(1, 8).Should().BeEquivalentTo(new[] {2, 5, 4, 6, 7, 3, 8, 9});

        [Fact]
        public void CupCircle_RightOf_2() =>
            new Day23.CupCircle("321", 5).RightOf(1, 4).Should().BeEquivalentTo(new[] {4, 5, 3, 2});

    }
}