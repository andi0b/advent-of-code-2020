using System;
using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day16Test
    {
        private readonly string _demoInput = @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";

        [Fact]
        void Parse()
        {
            var (rules, myTicket, nearbyTickets) = new Day16(_demoInput);

            rules.Should().BeEquivalentTo(new Day16.Rule[]
            {
                new("class", new(1, 3), new(5, 7)),
                new("row", new(6, 11), new(33, 44)),
                new("seat", new(13, 40), new(45, 50))
            });
            
            myTicket.Should().BeEquivalentTo(new[] {7, 1, 14});

            nearbyTickets.Should().BeEquivalentTo(new int[][]
            {
                new[] {7, 3, 47},
                new[] {40, 4, 50},
                new[] {55, 2, 20},
                new[] {38, 6, 12}
            });
        }

        [Fact]
        void Part1() => new Day16(_demoInput).Part1().Should().Be(71);
    }
}