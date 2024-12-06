using System.Text.RegularExpressions;
using AdventOfCode.Enums;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public class Day3 : BaseDay, IDay
    {
        public DayEnum Day => DayEnum.Day3;

        public Day3(IFileHelper fileHelper) : base(fileHelper) { }

        public void Process1Star()
        {
            Console.WriteLine("Processing Day 3 - 1 Star");

            var lines = GetLines(Day, StageEnum.Stage1);

            var inputData = string.Join(null, lines);

            var multiplicationTotal = CalculateMultiplicationTotal(inputData);

            Console.WriteLine($"Multiplication total: {multiplicationTotal}");
        }

        public void Process2Star()
        {
            Console.WriteLine("Processing Day 3 - 2 Star");

            var lines = GetLines(Day, StageEnum.Stage2);

            Console.WriteLine($"Day 2 pending");
        }

        private int CalculateMultiplicationTotal(string inputData)
        {
            var matchCollection = GetMultiplicationFunctions(inputData);

            var multiplicationSum = 0;

            foreach (var match in matchCollection)
            {
                var (factorA, factorB) = GetMultiplicationFactors(match.ToString());
                multiplicationSum += factorA * factorB;
            }

            return multiplicationSum;
        }

        private MatchCollection GetMultiplicationFunctions(string input)
        {
            const string multiplicationFunctionPattern = @"mul\(\d+,\d+\)";
            var multiplicationFunctions = Regex.Matches(input, multiplicationFunctionPattern);

            return multiplicationFunctions;
        }

        private (int, int) GetMultiplicationFactors(string input)
        {
            const string factorsPattern = @"\d+";
            var factors = Regex.Matches(input, factorsPattern);

            var firstValue = Int32.Parse(factors.First().Value);
            var secondValue = Int32.Parse(factors.Last().Value);

            return (firstValue, secondValue);
        }
    }
}
