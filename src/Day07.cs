using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc_runner
{
    public record Day07(Day07.BagRule[] BagRules) 
    {
        public Day07(string[] input) :this (input.Select(BagRule.Parse).ToArray()){}
        
        public int Part1()
        {
            var lookup = (
                from rule in BagRules
                from contains in rule.Contains
                select (contains.color, rule)
            ).ToLookup(x => x.color, x => x.rule);

            return AllParentBags("shiny gold").Count();

            IEnumerable<BagRule> AllParentBags(string color) =>
                lookup![color].Concat(
                                   lookup![color].Select(r => r.Color).SelectMany(AllParentBags)
                               )
                              .Distinct();
        }

        public int Part2()
        {
            var bagDict = BagRules.ToDictionary(x => x.Color, x => x);

            return ContainingBagsNum("shiny gold") - 1;

            int ContainingBagsNum(string color)
                => 1 + bagDict[color].Contains.Sum(x => x.num * ContainingBagsNum(x.color));
        }

        public record BagRule(string Color, (string color, int num)[] Contains)
        {
            public static BagRule Parse(string input) =>
                new(
                    Color: Regex.Match(input, @"^(\S+ \S+)").Groups[0].Value,
                    Contains: (
                        from match in Regex.Matches(input, @"(\d+) (\S+ \S+)")
                        select (match.Groups[2].Value, int.Parse(match.Groups[1].Value))
                    ).ToArray()
                );
        }
    }
}