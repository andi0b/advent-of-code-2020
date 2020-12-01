using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day01Test
    {
        readonly int[] _sampleInput =
        {
            1721,
            979,
            366,
            299,
            675,
            1456
        };
        
        [Fact] Day01 GetInstance() => new();
        [Fact] void Part1() => GetInstance().Part1(_sampleInput).Should().Be(514579);
        [Fact] void Part2() => GetInstance().Part2(_sampleInput).Should().Be(241861950);
    }
}