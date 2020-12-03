using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day03Test
    {
        private readonly string[] _sampleInput =
        {
            "..##.......",
            "#...#...#..",
            ".#....#..#.",
            "..#.#...#.#",
            ".#...##..#.",
            "..#.##.....",
            ".#.#.#....#",
            ".#........#",
            "#.##...#...",
            "#...##....#",
            ".#..#...#.#",
        };
        
        [Fact] Day03 GetInstance() => new();

        [Fact] void Part1() => GetInstance().Part1(_sampleInput).Should().Be(7);
        
        [Fact] void Part2() => GetInstance().Part2(_sampleInput).Should().Be(336);
    }
}