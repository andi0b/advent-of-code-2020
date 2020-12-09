using System;
using System.Linq;

namespace aoc_runner
{
    public class Day09
    {
        private readonly long[] _numbers;
        public Day09(long[] Numbers) => _numbers = Numbers;

        // needed for the reflection based runner that doesn't support default parameters
        public long Part1() => SolvePart1(25);
        public long Part2() => SolvePart2(25);

        public long SolvePart1(int preambleLength)
        {
            for (int i = preambleLength; i < _numbers.Length; i++)
            {
                var num = _numbers[i];
                var reference = _numbers.AsSpan(i - preambleLength, preambleLength);
                var isValid = IsValid(num, ref reference);
                if (!isValid) return num;
            }

            throw new Exception("no solution");
        }

        public long SolvePart2(int preambleLength)
        {
            var invalidNumber = SolvePart1(preambleLength);

            for (var length=2;length<_numbers.Length; length++)
            for (var offset = 0; offset < _numbers.Length-1; offset++)
            {
                if (offset+length>=_numbers.Length) continue;

                var span = _numbers.AsSpan(offset, length);

                if (Sum(ref span) == invalidNumber)
                    return span.ToArray().Min() + span.ToArray().Max();
            }
            
            throw new Exception("no solution");

            static long Sum(ref Span<long> span)
            {
                long sum = 0;
                for (var i = 0; i < span.Length; i++)
                    sum += span[i];
                return sum;
            }
        }

        public bool IsValid(long number, ref Span<long> referenceNumbers)
        {
            for (var a = referenceNumbers.Length - 1; a >= 0; a--)
            for (var b = referenceNumbers.Length - 1; b >= 0; b--)
            {
                if (a == b) continue;
                if (referenceNumbers[a] + referenceNumbers[b] == number)
                    return true;
            }

            return false;
        }
    }
}