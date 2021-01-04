using System;
using System.Linq;
using static MoreLinq.Extensions.ScanExtension;
using static MoreLinq.Extensions.RepeatExtension;
using static MoreLinq.Extensions.TakeUntilExtension;

namespace aoc_runner
{
    public class Day18
    {
        public long Part1(string[] input) =>
            input.Select(x => Expression.Parse(x).Solve())
                 .Sum();

        public long Part2(string[] input) =>
            input.Select(ParenthesisHack)
                 .Select(x => Expression.Parse(x).Solve())
                 .Sum();

        public record Expression(object[] Items)
        {
            private static long GetValue(object expressionOrNumber) => expressionOrNumber switch
            {
                Expression e => e.Solve(),
                long l       => l,
                _            => throw new ArgumentOutOfRangeException(nameof(expressionOrNumber), expressionOrNumber, null)
            };

            public long Solve() => Items.Zip(Items.Append(null).Skip(1)).Aggregate(
                long.MinValue,
                (acc, next) => next switch
                {
                    ({ } lhs, _) when acc == long.MinValue => GetValue(lhs),
                    ('+', { } rhs)                         => acc + GetValue(rhs),
                    ('*', { } rhs)                         => acc * GetValue(rhs),
                    _                                      => acc
                }
            );

            public static Expression Parse(string input)
            {
                var parsers = new Func<char[], (object?, char[])>[]
                {
                    // digits
                    str =>
                    {
                        var digits = str.TakeWhile(char.IsDigit).ToArray();
                        return long.TryParse(new string(digits), out var num)
                            ? (num, str[digits.Length ..])
                            : (null, str);
                    },

                    // operators
                    str => str[0] is '+' or '*' ? (str[0], str[1..]) : (null, str),

                    // subexpressions
                    str =>
                    {
                        var bracketCount = 0;
                        var expr = str.TakeWhile(x => x switch
                            {
                                '(' => ++bracketCount,
                                ')' => --bracketCount,
                                _   => bracketCount
                            } != 0
                        ).ToArray();

                        return expr.Any()
                            ? (Parse(new string(expr[1..])), str[(expr.Length + 1) ..])
                            : (null, str);
                    },

                    // spaces
                    str => (null, str[0] == ' ' ? str[1..] : str)
                };

                var items =
                    parsers
                       .Repeat()
                       .Scan((item: (object?) null, remaining: input.ToCharArray()), (acc, next) => next(acc.remaining))
                       .TakeUntil(x => !x.remaining.Any())
                       .Where(x => x.item != null)
                       .Select(x => x.item!);

                return new Expression(items.ToArray());
            }
        }

        public static string ParenthesisHack(string input) =>
            "((" +
            input.Replace("(", "((")
                 .Replace(")", "))")
                 .Replace("+", ") + (")
                 .Replace("*", ")) * ((")
            + "))";
    }
}