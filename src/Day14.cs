using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_runner
{
    public record Day14(Day14.Instruction[] Instructions)
    {
        public Day14(string[] input) : this(input.Select(Instruction.Parse).ToArray()) { }
        
        public long Part1()
        {
            var memory = new Memory();
            var mask = new MaskInstruction(0L, 0L, 0L);
            foreach (var instruction in Instructions)
            {
                if (instruction is MaskInstruction mi)
                    mask = mi;
                
                else if (instruction is SetInstruction si)
                {
                    memory[si.Address] = mask.ApplyTo(si.Value);
                }
            }

            return memory.Sum();
        }
        
        public long Part2()
        {
            var memory = new Memory();
            var mask = new MaskInstruction(0L, 0L, 0L);
            foreach (var instruction in Instructions)
            {
                if (instruction is MaskInstruction mi)
                    mask = mi;
                
                else if (instruction is SetInstruction si)
                {
                    foreach (var address in mask.AddressDecoder(si.Address))
                        memory[address] = si.Value;
                }
            }

            return memory.Sum();
        }

        public record Instruction
        {
            public static Instruction Parse(string input)
            {
                var parts = input.Split(" = ");
                if (parts[0] == "mask") return MaskInstruction.Parse(parts[1]);
                return new SetInstruction(int.Parse(parts[0][4..^1]), long.Parse(parts[1]));
            }
        }

        public record SetInstruction(int Address, long Value) : Instruction;
        
        public record MaskInstruction(long OrMask, long AndMask, long FloatMask) : Instruction
        {
            public long ApplyTo(long number) => (number | OrMask) & AndMask;
            public long ApplyTo2(long number) => number | OrMask;

            public IEnumerable<long> AddressDecoder(long address)
            {
                var changedAddress = ApplyTo2(address);

                var setBits = Enumerable.Range(0, 36)
                                        .Where(x => (1L << x & FloatMask) != 0)
                                        .ToArray();

                for (int i = 0; i < 1 << setBits.Length; i++)
                {
                    var str = Enumerable.Repeat('X', 36).ToArray();
                    for (var j = 0; j < setBits.Length; j++)
                    {
                        str[setBits[j]] = ((i >> j) & 1) == 0 ? '0' : '1';
                    }
                    
                    yield return Parse(new string(str.Reverse().ToArray())).ApplyTo(changedAddress);
                }
            }

            public new static MaskInstruction Parse(string input) => new(
                Convert.ToInt64(input.Replace('X', '0'), 2),
                Convert.ToInt64(input.Replace('X', '1'), 2),
                Convert.ToInt64(input.Replace('1', '0').Replace('X', '1'), 2)
            );
        }

        public record Memory
        {
            private readonly Dictionary<long, long> _memory = new();

            public long Sum() => _memory.Values.Sum();
            
            public long this[long address]
            {
                get => _memory.TryGetValue(address, out var value) ? value : 0L;
                set => _memory[address] = value;
            }
        }
    }
}