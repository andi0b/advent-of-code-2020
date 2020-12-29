using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day22Test
    {
        private static string _example = @"Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10";

        [Fact]
        void Part1() => new Day22().Part1(_example).Should().Be(306);
        
        [Fact]
        void Part2() => new Day22().Part2(_example).Should().Be(291);
    }
}