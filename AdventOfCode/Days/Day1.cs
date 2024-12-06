using System.Text.RegularExpressions;
using AdventOfCode.Enums;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public class Day1 : BaseDay, IDay
    {
        public DayEnum Day => DayEnum.Day1;

        public Day1(IFileHelper fileHelper) : base(fileHelper) { }

        public void Process1Star()
        {
            Console.WriteLine("Processing Day 1 - 1 Star");

            var lines = GetLines(Day, StageEnum.Stage1);

            var totalDifference = CalculateTotalDifference(lines);

            Console.WriteLine($"Total difference of {lines.Count()} lines: {totalDifference}");
        }

        public void Process2Star()
        {
            Console.WriteLine("Processing Day 1 - 2 Star");

            var lines = GetLines(Day, StageEnum.Stage2);

            var totalDifference = CalculateTotalSimilarityDifference(lines);

            Console.WriteLine($"Total difference of {lines.Count()} lines: {totalDifference}");
        }

        private int CalculateTotalDifference(IEnumerable<string> lines)
        {
            var (orderedListA, orderedListB) = GetOrderedLists(lines);

            var totalDifference = 0;

            for (int i = 0; i < orderedListA.Count(); i++)
            {
                var difference = Math.Abs(orderedListA[i] - orderedListB[i]);
                totalDifference += difference;
            }

            return totalDifference;
        }

        private int CalculateTotalSimilarityDifference(IEnumerable<string> lines)
        {
            var (orderedListA, orderedListB) = GetOrderedLists(lines);

            var comparisonLookup = GetComparisonLookup(orderedListB);

            var totalSimilarityScore = 0;

            foreach (var value in orderedListA)
            {
                var similarityScore = GetSimilarityScore(value, comparisonLookup);
                totalSimilarityScore += similarityScore;
            }

            return totalSimilarityScore;
        }

        private (IList<int>, IList<int>) GetOrderedLists(IEnumerable<string> lines)
        {
            var listA = new List<int>();
            var listB = new List<int>();

            foreach (var line in lines)
            {
                var (valueA, valueB) = GetValues(line);
                listA.Add(valueA);
                listB.Add(valueB);
            }

            listA.Sort();
            listB.Sort();

            return (listA, listB);
        }

        private (int, int) GetValues(string input)
        {
            const string digitPattern = @"\d+";
            var digits = Regex.Matches(input, digitPattern);

            var firstValue = Int32.Parse(digits.First().Value);
            var secondValue = Int32.Parse(digits.Last().Value);

            return (firstValue, secondValue);
        }

        private IDictionary<int, int> GetComparisonLookup(IEnumerable<int> input)
        {
            var comparisonLookup = new Dictionary<int, int>();

            foreach (var i in input)
            {
                if (comparisonLookup.ContainsKey(i))
                {
                    comparisonLookup[i]++;
                }
                else
                {
                    comparisonLookup.Add(i, 1);
                }
            }

            return comparisonLookup;
        }

        private int GetSimilarityScore(int value, IDictionary<int, int> comparisonLookup)
        {
            if (!comparisonLookup.ContainsKey(value))
            {
                return 0;
            }

            var numberOfInstancesInComparisonLookup = comparisonLookup[value];

            return value * numberOfInstancesInComparisonLookup;
        }
    }
}
