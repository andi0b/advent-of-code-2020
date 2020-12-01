using System;
using System.Linq;
using aoc_runner.Infrastructure;

var availableDays = DayRunner.AvailableDays.ToArray();

if (int.TryParse(args.FirstOrDefault(), out var dayParam))
{
    if (availableDays.Contains(dayParam))
        new DayRunner(dayParam).Run();
    else
        Console.WriteLine($"Can't find Day {dayParam}, exiting");
}
else
    foreach (var day in availableDays)
        new DayRunner(day).Run();