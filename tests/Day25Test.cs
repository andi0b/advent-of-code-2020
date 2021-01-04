using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day25Test
    {
        [Theory,
         InlineData(17807724, 5764801),
         InlineData(5764801, 17807724)]
        public void Part1(int a, int b) => new Day25(new[] {a, b}).Part1().Should().Be(14897079);
        
        [Theory,
         InlineData(7, 8, 5764801),
         InlineData(7, 11, 17807724)]
        public void Transform(int subjectNumber, int loopSize, int expected) =>
            Day25.Transform(subjectNumber, loopSize).Should().Be(expected);

        [Theory,
         InlineData(5764801, 8),
         InlineData(17807724, 11)]
        public void BruteForce(int num, int loopSize) =>
            Day25.BruteForceLoopSize(7, num).Should().Be(loopSize);
    }
}