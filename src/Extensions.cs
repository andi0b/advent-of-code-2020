using System;
using System.Text.RegularExpressions;

namespace aoc_runner
{
    public static class Extensions
    {
        public static T GetGroupValue<T>(this Match match, int captureId)
            => (T) Convert.ChangeType(match.Groups[captureId].Value, typeof(T));
    }
}