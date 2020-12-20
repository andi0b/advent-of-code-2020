using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day18Test
    {
        [Theory,
         InlineData("11 + 22 * 10", 330),
         InlineData("1 + 2 * 3 + 4 * 5 + 6", 71),
         InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51),
         InlineData("2 * 3 + (4 * 5)", 26),
         InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437),
         InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240),
         InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
        public void Solve(string input, int expected) => Day18.Expression.Parse(input).Solve().Should().Be(expected);
        
        
        [Theory,
         InlineData("11 + 22 * 10", 330),
         InlineData("1 + 2 * 3 + 4 * 5 + 6", 231),
         InlineData("2 * 3 + (4 * 5)", 46),
         InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51),
         InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445),
         InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060),
         InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
        public void Solve_Part2(string input, int expected) => Day18.Expression.Parse(Day18.ParenthesisHack(input)).Solve().Should().Be(expected);

    }
}