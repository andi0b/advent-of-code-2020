using System.Linq;
using System.Net.NetworkInformation;

namespace aoc_runner
{
    public record Day10(int[] Adapters)
    {
        public int Part1() =>
            Adapters.Concat(new[] {Adapters.Max() + 3})
                    .OrderBy(x=>x)
                    .Aggregate((prev: 0, diff1: 0, diff3: 0),
                               (acc, next) => (next - acc.prev) switch
                               {
                                   1 => (next, acc.diff1 + 1, acc.diff3),
                                   3 => (next, acc.diff1, acc.diff3 + 1),
                                   _ => (next, acc.diff1, acc.diff3)
                               },
                               acc => acc.diff1 * acc.diff3);
                 


        public int Part2()=>0;
    }
}