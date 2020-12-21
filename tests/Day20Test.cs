using System.Dynamic;
using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day20Test
    {
        [Fact]
        Day20 GetInstance() => new Day20(_demoInput);

        [Fact]
        void Part1() => GetInstance().Part1().Should().Be(20899048083289);
        
        [Fact]
        void Part2() => GetInstance().Part2().Should().Be(273);

        [Fact]
        void Tile_Parse_ToString() => Day20.Tile.Parse(_tile2311).Format().Should().Be(_tile2311);
        
        [Fact]
        void Tile_Parse_Flip() => Day20.Tile.Parse(_tile2311).Flip().Format().Should().Be(@"Tile 2311:
.#..#.##..
.....#..##
.#..##...#
#...#.####
.###.##.##
###.#...##
##..#.#.#.
..#....#..
.#.#...###
###..###..");

        [Fact]
        void AllPermutations()
        {
            var tile = Day20.Tile.Parse(_tile2311);
            var permutations = tile.AllPermutations();

            permutations.Should().BeEquivalentTo(new[]
            {
                tile,
                tile.TurnClockwise(),
                tile.TurnClockwise().TurnClockwise(),
                tile.TurnClockwise().TurnClockwise().TurnClockwise(),
                tile.Flip(),
                tile.Flip().TurnClockwise(),
                tile.Flip().TurnClockwise().TurnClockwise(),
                tile.Flip().TurnClockwise().TurnClockwise().TurnClockwise(),
            }, o => o.ComparingByMembers<Day20.Tile>());
        }

        [Fact]
        void Tile_Turn_Around()
        {
            var tile = Day20.Tile.Parse(_tile2311);
            tile.TurnClockwise().TurnClockwise().TurnClockwise().TurnClockwise().Should().BeEquivalentTo(tile, o=>o.ComparingByMembers<Day20.Tile>());
        }

        [Fact]
        void Tile_Flip_Ids()
        {
            var tile = Day20.Tile.Parse(_tile2311);
            var flipped = tile.Flip();
            flipped.LeftId.Should().Be(tile.RightId);
            flipped.RightId.Should().Be(tile.LeftId);
        } 

        private string _tile2311 = @"Tile 2311:
..##.#..#.
##..#.....
#...##..#.
####.#...#
##.##.###.
##...#.###
.#.#.#..##
..#....#..
###...#.#.
..###..###";
        
        private string _demoInput = @"Tile 2311:
..##.#..#.
##..#.....
#...##..#.
####.#...#
##.##.###.
##...#.###
.#.#.#..##
..#....#..
###...#.#.
..###..###

Tile 1951:
#.##...##.
#.####...#
.....#..##
#...######
.##.#....#
.###.#####
###.##.##.
.###....#.
..#.#..#.#
#...##.#..

Tile 1171:
####...##.
#..##.#..#
##.#..#.#.
.###.####.
..###.####
.##....##.
.#...####.
#.##.####.
####..#...
.....##...

Tile 1427:
###.##.#..
.#..#.##..
.#.##.#..#
#.#.#.##.#
....#...##
...##..##.
...#.#####
.#.####.#.
..#..###.#
..##.#..#.

Tile 1489:
##.#.#....
..##...#..
.##..##...
..#...#...
#####...#.
#..#.#.#.#
...#.#.#..
##.#...##.
..##.##.##
###.##.#..

Tile 2473:
#....####.
#..#.##...
#.##..#...
######.#.#
.#...#.#.#
.#########
.###.#..#.
########.#
##...##.#.
..###.#.#.

Tile 2971:
..#.#....#
#...###...
#.#.###...
##.##..#..
.#####..##
.#..####.#
#..#.#..#.
..####.###
..#.#.###.
...#.#.#.#

Tile 2729:
...#.#.#.#
####.#....
..#.#.....
....#..#.#
.##..##.#.
.#.####...
####.#.#..
##.####...
##..#.##..
#.##...##.

Tile 3079:
#.#.#####.
.#..######
..#.......
######....
####.#..#.
.#...#.##.
#.#####.##
..#.###...
..#.......
..#.###...";
    }
}