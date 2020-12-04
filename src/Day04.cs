using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc_runner
{
    public class Day04
    {
        private readonly Passport[] _passports;

        public Day04(string input)
        {
            var passportInputs = input.Split(Environment.NewLine + Environment.NewLine);
            
            var propertyRegex = new Regex(@"(?<key>\S*):(?<value>\S*)\s?");
            var passportMatches = passportInputs.Select(x => propertyRegex.Matches(x));

            _passports = (
                from passportMatch in passportMatches
                select new Passport(
                    
                    from match in passportMatch
                    let groups = match.Groups
                    let key = groups["key"].Value
                    let value = groups["value"].Value
                    select (key, value)
                    
                )
            ).ToArray();
        }

        public int Part1()
            => _passports.Count(pp => pp.ContainsAllProperties(new[] {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"}));

        public int Part2()
            => _passports.Count(pp => pp.IsValid());
        
        record Passport(IReadOnlyDictionary<string, string> Properties)
        {
            public Passport(IEnumerable<(string key, string value)> kv)
                : this(kv.ToDictionary(x => x.key, x => x.value))
            {
            }
            
            public bool ContainsAllProperties(IEnumerable<string> properties)
                => !properties.Except(Properties.Keys).Any();

            public bool IsValid() =>
                //byr (Birth Year) - four digits; at least 1920 and at most 2002.
                GetIntValue("byr") is >=1920 and <= 2002 &&

                //iyr (Issue Year) - four digits; at least 2010 and at most 2020.
                GetIntValue("iyr") is >=2010 and <= 2020 &&

                //eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
                GetIntValue("eyr") is >=2020 and <= 2030 &&

                //hgt (Height) - a number followed by either cm or in:
                //    If cm, the number must be at least 150 and at most 193.
                //    If in, the number must be at least 59 and at most 76.
                ParseHeight(GetValue("hgt")) switch
                {
                    var (height, unit) when unit == "cm" && height is >=150 and <= 193 => true,
                    var (height, unit) when unit == "in" && height is >=59 and <= 76 => true,
                    _ => false,
                } &&

                //hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
                ValidateHexColor(GetValue("hcl")) &&
                
                //ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
                GetValue("ecl") is "amb" or "blu" or "brn" or "gry" or "grn" or "hzl" or "oth" &&

                //pid (Passport ID) - a nine-digit number, including leading zeroes.
                GetValue("pid")?.Length == 9 && GetValue("pid")?.All(char.IsDigit) == true

                //cid (Country ID) - ignored, missing or not.
            ;

            (int? height, string unit) ParseHeight(string? height)
            {
                if (height == null) return default;
                
                string str(IEnumerable<char> chars) => new (chars.ToArray());

                var leadingDigits = str(height.TakeWhile(char.IsDigit));
                var trailingLetters = str(height.Reverse().TakeWhile(char.IsLetter).Reverse());

                return (ToInt(leadingDigits), trailingLetters);
            }

            bool ValidateHexColor(string? color)
                => color != null && color.Length == 7 && color[0] == '#' &&
                   color.Skip(1).All(c => char.IsDigit(c) || c is 'a' or 'b' or 'c' or 'd' or 'e' or 'f');
            
            string? GetValue(string propertyName) => Properties.TryGetValue(propertyName, out var val) ? val : null;

            int? GetIntValue(string propertyName) => ToInt(GetValue(propertyName));
            int? ToInt(string? value) => int.TryParse(value, out var parsed) ? parsed : null;
        }
    }
}