using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc_runner
{
    public record Day07(Day07.BagRule[] BagRules) 
    {
        public Day07(string[] input) :this (input.Select(BagRule.Parse).ToArray()){}

        public int Part1() => AllParentBags("shiny gold").Count();
        
        IEnumerable<BagRule> AllParentBags(string color)
        {
            var directParentBags = DirectParentBags(color).ToArray();
            return directParentBags.Concat(directParentBags.Select(r => r.Color).SelectMany(AllParentBags)).Distinct();
        }

        IEnumerable<BagRule> DirectParentBags(string color)
            => BagRules.Where(r => r.CanContain.Any(x => x.color == color));

        public int Part2()
        {
            var bagDict = BagRules.ToDictionary(x => x.Color, x => x);

            return ContainingBagsNum("shiny gold") - 1;

            int ContainingBagsNum(string color)
                => 1 + bagDict[color].CanContain.Sum(x => x.num * ContainingBagsNum(x.color));
        }

        public record BagRule(string Color, (string color, int num)[] CanContain)
        {
            private static Regex Regex = new(@"(\d+) (\S+ \S+)");
            public static BagRule Parse(string input)
            {
                var parts = input.TrimEnd('.').Split("contain");
                var color = parts[0].Replace("bags", "").Trim();

                var canContain =
                    from numberedBag in parts[1].Split(',')
                    let match = Regex.Match(numberedBag)
                    where match.Success
                    select (match.GetGroupValue(2), match.GetGroupValue<int>(1));

                return new BagRule(color, canContain.ToArray());
            }
        }
    }
}