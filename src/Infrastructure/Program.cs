using System;
using System.Linq;

namespace aoc_runner.Infrastructure
{
    class Program
    {
        static void Main(string[] args)
        {
            var availableDays = DayRunner.AvailableDays.ToArray();

            if (int.TryParse(args.FirstOrDefault(), out var dayParam))
            {
                if (!availableDays.Contains(dayParam))
                {
                    Console.WriteLine($"Can't find Day {dayParam}, exiting");
                    return;
                }
                    
                new DayRunner(dayParam).Run();
                return;
            }

            foreach (var day in availableDays)
                new DayRunner(day).Run();
        }
    }
}