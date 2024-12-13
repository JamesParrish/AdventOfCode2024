using AdventOfCode.Enums;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public class Day5(IFileHelper fileHelper) : BaseDay(fileHelper), IDay
    {
        public DayEnum Day => DayEnum.Day5;

        private const char FirstCharacter = 'X';
        private const char SecondCharacter = 'M';
        private const char ThirdCharacter = 'A';
        private const char FourthCharacter = 'S';

        public void Process1Star()
        {
            Console.WriteLine("Processing Day 5 - 1 Star");

            var lines = GetLines(Day, StageEnum.Stage1);

            var (rules, data) = MapData(lines);

            var total = CalculateTotal(rules, data);

            Console.WriteLine($"Total: {total}");

            //Console.WriteLine($"{rules.Count} rules (78 is {string.Join('/', rules[78])}");
            //Console.WriteLine($"{data.Count()} datodes (first is {string.Join('/', data.First())}");
        }

        public void Process2Star()
        {
            Console.WriteLine("Processing Day 5 - 2 Star");

            var lines = GetLines(Day, StageEnum.Stage2);

            var total = 0;

            Console.WriteLine($"Total: {total}");
        }

        private (IDictionary<int, IList<int>>, IEnumerable<IList<int>>) MapData(IEnumerable<string> lines)
        {
            var rules = new Dictionary<int, IList<int>>();
            var data = new List<IList<int>>();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                };

                if (line.Contains('|'))
                {
                    var ruleValues = line.Split('|');

                    // Assume always only two values
                    var firstValue = Int32.Parse(ruleValues.First());
                    var secondValue = Int32.Parse(ruleValues.Last());

                    if (rules.ContainsKey(firstValue))
                    {
                        rules[firstValue].Add(secondValue);
                    }
                    else
                    {
                        rules.Add(firstValue, [secondValue]);
                    }
                }
                else if (line.Contains(','))
                {
                    var rawValues = line.Split(",");
                    var values = rawValues.Select(v => Int32.Parse(v));

                    data.Add(values.ToList());
                }
            }

            return (rules, data);
        }

        private int CalculateTotal(IDictionary<int, IList<int>> rules, IEnumerable<IList<int>> data)
        {
            var total = 0;

            foreach (var pageValues in data)
            {
                if (PageOrderIsValid(rules, pageValues))
                {
                    var middlePageValue = GetMiddlePageValue(pageValues);

                    total += middlePageValue;
                }
            }

            return total;
        }

        private bool PageOrderIsValid(IDictionary<int, IList<int>> rules, IList<int> pageValues)
        {
            //foreach (var pageValue in pageValues)
            for (int i = 1; i < pageValues.Count; i++)
            {
                var pageValue = pageValues[i];

                if (!rules.ContainsKey(pageValue))
                {
                    continue;
                }

                var pagesPageMustPrecede = rules[pageValue];

                for (int j = i - 1; j >= 0; j--)
                {
                    var futurePageValue = pageValues[j];
                    if (pagesPageMustPrecede.Contains(futurePageValue))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int GetMiddlePageValue(IList<int> pageValues)
        {
            var pageCount = pageValues.Count();

            // Integer division auto-floors
            var middlePageIndex = pageCount / 2;

            return pageValues[middlePageIndex];
        }
    }
}
