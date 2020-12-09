using System;
using System.Linq;
using aoc_runner.Infrastructure;

var availableDays = DayRunner.AvailableDays.ToArray();

if (int.TryParse(args.FirstOrDefault(), out var dayParam))
{
    if (availableDays.Contains(dayParam))
        foreach (var type in DayRunner.DayTypes[dayParam])
            new DayRunner(dayParam, type).Run();
    else
        Console.WriteLine($"Can't find Day {dayParam}, exiting");
}
else
    foreach (var day in availableDays)
    foreach (var type in DayRunner.DayTypes[day])
        new DayRunner(day,type).Run();