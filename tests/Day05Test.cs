using System.Runtime.CompilerServices;
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
        void CheckParse_old(string input, int row, int col, int seatId)
            => Day05_old.BoardingPass.Parse(input).Should().BeEquivalentTo(new
            {
                Row = row, Column = col, SeatId = seatId
            });

        [Theory,
         InlineData("FBFBBFFRLR", 357),
         InlineData("BFFFBBFRRR", 567),
         InlineData("FFFBBBFRRR", 119),
         InlineData("BBFFBBFRLL", 820),
        ]
        void CheckParse(string input, int seatId) => Day05.ParseSeatId(input).Should().Be(seatId);
    }
}