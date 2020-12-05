using System;
using System.Linq;

namespace aoc_runner
{
    public class Day05
    {
        public int Part1(string[] input) =>
            input.Select(BoardingPass.Parse).Max(x => x.SeatId);
        
        public record BoardingPass(int Row, int Column)
        {
            public int SeatId => 8 * Row + Column;

            public static BoardingPass Parse(string input) =>
                new(ParseBinary(input[..7]), ParseBinary(input[7..]));

            static int ParseBinary(string str) =>
                Convert.ToInt32(str.Replace('F', '0').Replace('B', '1').Replace('R', '1').Replace('L', '0'), 2);
        }
    }
}