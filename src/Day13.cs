using System.Linq;

namespace aoc_runner
{
    public record Day13(int Earliest, int[] BusNumbers)
    {
        public Day13(params string[] input):this(
            int.Parse(input[0]),
            input[1].Split(',').Select(x=>int.TryParse(x, out var parsed) ? parsed : 0).ToArray()) { }

        public int Part1()
        {

            var departures =
                from bn in BusNumbers
                where bn > 0
                select (departureTime: NextDepartureTime(Earliest, bn), bn);

            var (departureTime, busNumber) = departures.Min();
            return (departureTime - Earliest) * busNumber;
        }
        
        private int NextDepartureTime(int afterTimestamp, int busNumber) => (afterTimestamp / busNumber + 1) * busNumber;

        public long Part2() => 0;
    }
}