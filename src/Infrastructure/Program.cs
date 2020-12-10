using System;
using System.Linq;
using aoc_runner.Infrastructure;

var availableDays = DayRunner.AvailableDays.ToArray();

if (int.TryParse(args.FirstOrDefault(), out var dayParam))
{
    if (availableDays.Contains(dayParam))
        DayRunner.Run(dayParam);
    else
        Console.WriteLine($"Can't find Day {dayParam}, exiting");
}
else
    DayRunner.Run();