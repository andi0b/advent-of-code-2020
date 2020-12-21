using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace aoc_runner
{
    public record Day20(Day20.Tile[] Tiles)
    {
        public Day20(string input) 
            : this(input.Split(Environment.NewLine+Environment.NewLine).Select(Tile.Parse).ToArray()) { }

        public long Part1()
        {
            var allPermutations = Tiles.SelectMany(x => x.AllPermutations()).ToArray();

            var cornerTiles = allPermutations.Where(i => !allPermutations.Any(j => j.RightId == i.LeftId && j.TileId != i.TileId) &&
                                                         !allPermutations.Any(j => j.TopId == i.BottomId && j.TileId != i.TileId))
                                             .ToArray();

            var ids = cornerTiles.Select(x => x.TileId).Distinct().ToArray();

            Debug.Assert(ids.Length == 4);

            return ids.Aggregate(1L, (agg, next) => agg * next);
        }
        
        public long Part2()
        {
            var placedTiles = new Dictionary<(int x, int y), Tile>();

            bool IsPlaced(Tile tile) => placedTiles!.Values.Any(x => x.TileId == tile.TileId);
            
            var allPermutations = Tiles.SelectMany(x => x.AllPermutations()).ToArray();
            var leftLookup = allPermutations.ToLookup(x => x.LeftId);
            var topLookup = allPermutations.ToLookup(x => x.RightId);


            var topLeftTiles = allPermutations.Where(i => !allPermutations.Any(j => j.RightId == i.LeftId && j.TileId != i.TileId) &&
                                                          !allPermutations.Any(j => j.TopId == i.BottomId && j.TileId != i.TileId))
                                              .ToArray();


            placedTiles[(0, 0)] = allPermutations.Last();

            var x = 1;
            var y = 0;

            while (true)
            {
                placedTiles.TryGetValue((x - 1, y), out var leftTile);
                placedTiles.TryGetValue((x, y - 1), out var topTile);

                if (leftTile == null && topTile == null)
                {
                    break;
                }
                
                var matchingTiles = allPermutations.Where(i => (leftTile is null || i.LeftId == leftTile.RightId) &&
                                                               (topTile is null || i.TopId == topTile.BottomId))
                                                   .Where(i => !IsPlaced(i));

                var firstMatching = matchingTiles.FirstOrDefault();

                if (firstMatching is not null)
                    placedTiles[(x, y)] = firstMatching;
                else
                    y++;

                x++;
            }
            
            return 1;
        }
        
        public record Tile(int TileId, char[][] Pixels)
        {
            private const int Size = 10;

            public int TopId { get; } = GetId(Pixels, z => (z, 0));
            public int BottomId { get; } = GetId(Pixels, z => (z, Size - 1));
            public int LeftId { get; } = GetId(Pixels, z => (0, z));
            public int RightId { get; } = GetId(Pixels, z => (Size - 1, z));

            private static int GetId(char[][] pixels, Func<int, (int x, int y)> selectorFunc) => (
                from z in Enumerable.Range(0, Size)
                let coordinates = selectorFunc(z)
                let pixel = pixels[coordinates.y][coordinates.x]
                select (pixel == '#' ? 1 : 0) << z
            ).Aggregate(0, (acc, next) => acc | next);

            public Tile Transform(Action<int, int, char[][]> transformFunc)
            {
                var newPixels = Enumerable.Repeat(false, Size).Select(_ => new char[Size]).ToArray();

                for (var y = 0; y < Size; y++)
                for (var x = 0; x < Size; x++)
                {
                    transformFunc(x, y, newPixels);
                }

                return new(TileId, newPixels);
            }

            public Tile TurnClockwise() => Transform((x, y, newPixels) => newPixels[x][Size - 1 - y] = Pixels[y][x]);
            public Tile Flip() => Transform((x, y, newPixels) => newPixels[y][Size - 1 - x] = Pixels[y][x]);

            public IEnumerable<Tile> AllPermutations()
            {
                var tile = this;
                for (var i = 1; i <= 8; i++)
                {
                    yield return tile;

                    if (i == 4)
                        tile = this.Flip();
                    else if (i == 8)
                        yield break;

                    tile = tile.TurnClockwise();
                }
            }

            public static Tile Parse(string ínput)
            {
                var lines = ínput.Split(Environment.NewLine);

                var pixels = lines.Skip(1).Select(l => l.ToArray()).ToArray();

                return new Tile(
                    int.Parse(Regex.Match(lines[0], @"Tile (\d*):").Groups[1].Value),
                    pixels
                );
            }

            public string Format()
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Tile {TileId}:");
                foreach (var line in Pixels)
                    sb.AppendLine(new string(line));

                var nlSize = Environment.NewLine.Length;
                sb.Remove(sb.Length - nlSize, nlSize);
                return sb.ToString();
            }
        }
    }
}