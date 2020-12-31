using System;
using System.Diagnostics;
using System.Linq;

namespace aoc_runner
{
    public record Day25(int[] Input)
    {

        public int Part1()
        {
            var loopSize0 = BruteForceLoopSize(7, Input[0]);
            return Transform(Input[1], loopSize0);
        }


        public static int BruteForceLoopSize(int subjectNumber, int transformationResult)
        {
            var num = 1L;
            for (var loopSize = 1;; loopSize++)
                if (transformationResult == (num = num * subjectNumber % 20201227))
                    return loopSize;
        }

        public static (int subjectNumber, int loopSize) BruteForce2(int transformationResult)
        {
            for (var subjectNumber = 1;; subjectNumber++)
            {
                for (var loopSize = 1; loopSize <= subjectNumber; loopSize++)
                {
                    if (Transform(subjectNumber, loopSize) == transformationResult) return (subjectNumber, loopSize);
                }

                for (var subjectNumber2 = 1; subjectNumber2 < subjectNumber; subjectNumber2++)
                {
                    if (Transform(subjectNumber2, subjectNumber) == transformationResult) return (subjectNumber2, subjectNumber);
                }
            }
        }

        public static int Transform2(int subjectNumber, int loopSize)
        {
            var num = 1L;
            for (var i = 0; i < loopSize; i++)
            {
                num = num * subjectNumber % 20201227;
            }

            return (int) num;
        }
        
        public static int Transform(int subjectNumber, int loopSize) =>
            Enumerable.Repeat(1, loopSize).Aggregate(1L, (acc, _) => acc * subjectNumber % 20201227, acc => (int) acc);
    }
}