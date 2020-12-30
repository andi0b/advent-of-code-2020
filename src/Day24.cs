using System.Collections.Generic;
using System.Linq;

namespace aoc_runner
{
    public class Day24
    {

        public int Part1(string[] input) => (
            from move in input.Select(ParseMoves)
            group move by move
        ).Count(x => x.Count() % 2 != 0);

        public static (int x, int y) ParseMoves(string input) => (
            from pair in input.Prepend(' ').Zip(input)
            select pair switch
            {
                ('s', 'e') => (+1, -1),
                ('s', 'w') => (+0, -1),
                ('n', 'e') => (+0, +1),
                ('n', 'w') => (-1, +1),
                (_, 'e') => (+1, +0),
                (_, 'w') => (-1, +0),
                _ => (0, 0)
            }).Aggregate((0, 0), (acc, next) => (acc.Item1 + next.Item1, acc.Item2 + next.Item2));
    }
}