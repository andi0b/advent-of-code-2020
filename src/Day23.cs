using System;
using System.Linq;

namespace aoc_runner
{
    public class Day23
    {
        public string Part1(string input) =>
            Enumerable.Range(0, 100)
                      .Aggregate(Circle.Parse(input),
                                 (agg, _) => agg.Next(),
                                 agg => agg.Solution);

        public record Circle(int[] Cups, int CurrentCup)
        {
            (int[] taken, int[] remaining) TakeElements(int cupId, int count)
            {
                var cupIndex = Array.FindIndex(Cups, x => x == cupId);
                var indicesToTake = Enumerable.Range(1, count).Select(x => (cupIndex + x) % 9).ToArray();

                return (indicesToTake.Select(i => Cups[i]).ToArray(),
                    Cups.Select((cup, i) => (cup, i)).Where(x => !indicesToTake.Contains(x.i)).Select(x => x.cup).ToArray());
            }

            public Circle Next()
            {
                var (taken, remaining) = TakeElements(CurrentCup, 3);
                var destinationIndex = FindDestinationIndex(remaining);
                var next = remaining[..(destinationIndex + 1)].Concat(taken).Concat(remaining[(destinationIndex + 1)..]).ToArray();
                var nextCurrentCupIndex = (Array.IndexOf(next, CurrentCup) + 1) % 9;

                return new(next, next[nextCurrentCupIndex]);
            }
            
            int FindDestinationIndex(int[] remaining)
            {
                for (var i = 1; i <= 9; i++)
                {
                    var destination = 1 + (9 + CurrentCup - 1 - i) % 9;
                    var destinationIndex = Array.IndexOf(remaining, destination);
                    if (destinationIndex != -1) 
                        return destinationIndex;
                }
                throw new Exception();
            }

            public string Solution => string.Concat(TakeElements(1, 8).taken.Select(x => x.ToString()));
            
            public static Circle Parse(string input) => new(input.Select(x => x - '0').ToArray(), input.First() - '0');

            public override string ToString() => string.Concat(Cups.Select(x => x == CurrentCup ? $" ({x}) " : $"  {x}  "));
        }
    }
}