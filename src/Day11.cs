using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace aoc_runner
{
    public record Day11(Day11.SeatMap Seats)
    {
        public Day11(string[] input) : this(SeatMap.Parse(input))
        {
        }

        public int Part1()
        {
            var (permutation, isdifferent) = Seats.NextPermutation();
            for (var i = 1;; i++)
            {
                if (!isdifferent) return permutation.Map.OfType<SeatType>().Count(x=>x == SeatType.Taken);
                (permutation, isdifferent) = permutation.NextPermutation();
            }
        }
        

        public record SeatMap(SeatType[,] Map, int Width, int Height)
        {
            public (SeatMap permutation, bool isdifferent) NextPermutation()
            {
                var newMap = new SeatType[Width, Height];

                var isDifferent = false;

                for (var x = 0; x < Width; x++)
                for (var y = 0; y < Height; y++)
                {
                    newMap[x, y] = Map[x, y] switch
                    {
                        SeatType.Empty when AdjacentSeats(x, y).All(x => x != SeatType.Taken) => SeatType.Taken,
                        SeatType.Taken when AdjacentSeats(x, y).Count(x => x == SeatType.Taken) >= 4 => SeatType.Empty,
                        var seatType => seatType
                    };

                    if (newMap[x, y] != Map[x, y])
                        isDifferent = true;
                }

                return (this with{Map = newMap}, isDifferent);
            }

            public IEnumerable<SeatType> AdjacentSeats(int x, int y) =>
                from xOff in new[] {-1, 0, 1}
                from yOff in new[] {-1, 0, 1}
                where !(xOff == 0 && yOff == 0)
                let adjX = xOff + x
                let adjY = yOff + y
                where adjX >= 0 && adjX < Width
                               && adjY >= 0 && adjY < Height
                select Map[adjX, adjY];
            
            public static SeatMap Parse(string[] input)
            {
                var width = input[0].Length;
                var height = input.Length;

                var map = new SeatType[width, height];

                for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    map[x, y] = Parse(input[y][x]);

                return new(map, width, height);
            }

            static SeatType Parse(char c) => c switch
            {
                '#' => SeatType.Taken,
                'L' => SeatType.Empty,
                _ => SeatType.Floor
            };

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (var y = 0; y < Height; y++)
                {
                    for (var x = 0; x < Width; x++)
                    {
                        sb.Append(Map[x, y]switch
                        {
                            SeatType.Empty => 'L',
                            SeatType.Taken => '#',
                            _ => '.'
                        });
                    }

                    if (y != Height - 1) sb.AppendLine();
                }

                return sb.ToString();
            }
        }

        public enum SeatType
        {
            Floor,
            Empty,
            Taken
        };
    }
}