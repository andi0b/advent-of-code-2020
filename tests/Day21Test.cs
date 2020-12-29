using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day21Test
    {
        private string[] _sample =
        {
            "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)",
            "trh fvjkl sbzzf mxmxvkd (contains dairy)",
            "sqjhc fvjkl (contains soy)",
            "sqjhc mxmxvkd sbzzf (contains fish)",
        };

        [Fact]
        public Day21 GetInstance() => new(_sample);

        [Fact]
        public void Part1() => GetInstance().Part1().Should().Be(5);

        [Fact]
        public void Part2() => GetInstance().Part2().Should().Be("mxmxvkd,sqjhc,fvjkl");
    }
}