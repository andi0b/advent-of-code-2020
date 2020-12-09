﻿using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day09Test
    {
        private long[] _demoInput = new long[]
        {
            35,
            20,
            15,
            25,
            47,
            40,
            62,
            55,
            65,
            95,
            102,
            117,
            150,
            182,
            127,
            219,
            299,
            277,
            309,
            576,
        };

        [Fact] public Day09 GetInstance() => new Day09(_demoInput);
        [Fact] public void Part1() => GetInstance().SolvePart1(5).Should().Be(127);
        [Fact] public void Part2() => GetInstance().SolvePart2(5).Should().Be(62);
    }
}