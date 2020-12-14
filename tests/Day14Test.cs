using aoc_runner;
using FluentAssertions;
using Xunit;

namespace tests
{
    public class Day14Test
    {
        [Fact]
        void Part1() => new Day14(new []{
            "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
            "mem[8] = 11",
            "mem[7] = 101",
            "mem[8] = 0",
        }).Part1().Should().Be(165);
        
        [Fact]
        void Part2() => new Day14(new []{
            "mask = 000000000000000000000000000000X1001X",
            "mem[42] = 100",
            "mask = 00000000000000000000000000000000X0XX",
            "mem[26] = 1",
        }).Part2().Should().Be(208);
        
        [Theory,
         InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 11, 73),
         InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 101, 101),
         InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 0, 64),
        ]
        public void MaskInstruction_Apply(string mask, int number, int expected) => 
            Day14.MaskInstruction.Parse(mask).ApplyTo(number).Should().Be(expected);

        [Theory,
         InlineData("000000000000000000000000000000X1001X", 42, new long[] {26, 27, 58, 59}),
         InlineData("00000000000000000000000000000000X0XX", 26, new long[] {16,17,18,19,24,25,26,27})]
        public void MaskInstruction_AddressDecoder(string mask, int address, long[] expected) =>
            Day14.MaskInstruction.Parse(mask).AddressDecoder(address).Should().BeEquivalentTo(expected);
    }
}