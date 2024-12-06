using System.Text.RegularExpressions;
using AdventOfCode.Enums;
using AdventOfCode.Helpers;
using AdventOfCode.Models.Day3;

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

            var inputData = string.Join(null, lines);

            var multiplicationTotal = CalculateConditionalMultiplicationTotal(inputData);

            Console.WriteLine($"Multiplication total: {multiplicationTotal}");
        }

        private int CalculateMultiplicationTotal(string inputData)
        {
            var matchCollection = GetMultiplicationFunctions(inputData);

            int multiplicationSum = GetMultiplicationSum(matchCollection);

            return multiplicationSum;
        }

        private int CalculateConditionalMultiplicationTotal(string inputData)
        {
            var matchCollection = GetMultiplicationFunctions(inputData);

            var doBands = GetDoBands(inputData);

            var matchesWithinDoBands = matchCollection.Where(m => doBands.Any(b => b.StartIndex < m.Index && b.EndIndex > m.Index));

            int multiplicationSum = GetMultiplicationSum(matchesWithinDoBands);

            return multiplicationSum;
        }

        private int GetMultiplicationSum(IEnumerable<Match> matchCollection)
        {
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

        private IEnumerable<DoBand> GetDoBands(string input)
        {
            var doIndexes = GetDoIndexes(input);
            var doNotIndexes = GetDoNotIndexes(input);

            var doBands = new List<DoBand>();

            if (doIndexes.First() > doNotIndexes.First())
            {
                doBands.Add(new DoBand()
                {
                    StartIndex = 0,
                    EndIndex = doNotIndexes.First()
                });
            }

            foreach (var doIndex in doIndexes)
            {
                if (doBands.Exists(b => b.StartIndex < doIndex && b.EndIndex > doIndex))
                {
                    continue;
                }

                var nextDoNotIndex = doNotIndexes.FirstOrDefault(i => i > doIndex);

                doBands.Add(new DoBand
                {
                    StartIndex = doIndex,
                    EndIndex = nextDoNotIndex == 0 ? int.MaxValue : nextDoNotIndex
                });
            }

            return doBands;
        }

        private IEnumerable<int> GetDoIndexes(string input)
        {
            const string doFunctionPattern = @"do\(\)";
            var doFunctions = Regex.Matches(input, doFunctionPattern);


            return doFunctions.Select(m => m.Index);
        }

        private IEnumerable<int> GetDoNotIndexes(string input)
        {
            const string doNotFunctionPattern = @"don\'t\(\)";
            var doNotFunctions = Regex.Matches(input, doNotFunctionPattern);


            return doNotFunctions.Select(m => m.Index);
        }
    }
}
