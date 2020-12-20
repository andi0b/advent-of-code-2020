using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace aoc_runner
{
    public class Day18
    {
        public static int Solve(string input)
        {
        }
        
        

        
        
        
        private static string ParanthesisHack(string input)
            => "((" + input.Replace("+", ")+(").Replace("*", "))*((") + "))";
    }
}