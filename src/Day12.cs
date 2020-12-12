using System;
using System.Diagnostics;
using System.Linq;

namespace aoc_runner
{
    public record Day12((char, int)[] Instructions)
    {
        public Day12(string[] input) : this(input.Select(i => (i[0], int.Parse(i[1..]))).ToArray())
        {
        }

        public int Part1() => Instructions.Aggregate(new ShipPosition(), (acc,nextInstruction) => acc.NextPosition(nextInstruction), acc=>acc.ManhattanDistanceTo());
        public int Part2() => 1;

        record ShipPosition(int X = 0, int Y = 0, int HeadingDegrees = 90)
        {
            public ShipPosition NextPosition((char, int) instruction) => instruction switch
            {
                ('R', { } degrees) => this with {HeadingDegrees = (HeadingDegrees + degrees) % 360},
                ('L', { } degrees) => this with {HeadingDegrees = (360 + HeadingDegrees - degrees) % 360},
                ('F', { } distance) => Travel(HeadingDirection, distance),
                ({ } direction, { } distance) => Travel(direction, distance)
            };

            private char HeadingDirection => HeadingDegrees switch
            {
                0 => 'N',
                90 => 'E',
                180 => 'S',
                270 => 'W'
            };

            public ShipPosition Travel(char direction, int distance) => direction switch
            {
                'N' => this with{Y = Y - distance},
                'S' => this with{Y = Y + distance},
                'W' => this with{X = X - distance},
                'E' => this with{X = X + distance},
            };

            public int ManhattanDistanceTo(int x = 0, int y = 0) => Math.Abs(X - x) + Math.Abs(Y - y);
        }
    }
}