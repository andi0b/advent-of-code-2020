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

        public int Part2(string[] input)
        {
            var blackTiles = (
                from move in input.Select(ParseMoves)
                group move by move
                into flips
                where flips.Count() % 2 != 0
                select flips.First()
            ).ToHashSet();
            
            for (var i = 0; i < 100; i++)
            {
                var interestingTiles = blackTiles.SelectMany(x => AllAdjacent(x).Prepend(x)).Distinct();

                blackTiles = (
                    from tile in interestingTiles
                    let isBlack = blackTiles.Contains(tile)
                    let adjacentBlackCount = AllAdjacent(tile).Count(t => blackTiles.Contains(t))
                    let isBlackNext = (isBlack, adjacentBlackCount) switch
                    {
                        (true, 0 or >2) => false,
                        (false, 2)      => true,
                        _               => isBlack,
                    }
                    where isBlackNext
                    select tile
                ).ToHashSet();
            }

            return blackTiles.Count;
        }

        public static (int x, int y) ParseMoves(string input) => (
            from pair in input.Prepend(' ').Zip(input)
            select pair switch
            {
                ('s', 'e') => (+1, -1),
                ('s', 'w') => (+0, -1),
                ('n', 'e') => (+0, +1),
                ('n', 'w') => (-1, +1),
                (_, 'e')   => (+1, +0),
                (_, 'w')   => (-1, +0),
                _          => (+0, +0)
            }
        ).Aggregate((0, 0), (acc, next) => (acc.Item1 + next.Item1, acc.Item2 + next.Item2));

        public IEnumerable<(int x, int y)> AllAdjacent((int x, int y) tile) =>
            new (int x, int y)[] {(+1, -1), (+0, -1), (+0, +1), (-1, +1), (+1, +0), (-1, +0)}
               .Select(direction => (tile.x + direction.x, tile.y + direction.y));
    }
}