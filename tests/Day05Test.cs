using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day05Test
    {
        [Theory,
         InlineData("FBFBBFFRLR", 44, 5, 357),
         InlineData("BFFFBBFRRR", 70, 7, 567),
         InlineData("FFFBBBFRRR", 14, 7, 119),
         InlineData("BBFFBBFRLL", 102, 4, 820),
        ]
        void CheckParse(string input, int row, int col, int seatId)
            => Day05.BoardingPass.Parse(input).Should().BeEquivalentTo(new
            {
                Row = row, Column = col, SeatId = seatId
            });
    }
}