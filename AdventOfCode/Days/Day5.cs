using System.Collections.ObjectModel;
using AdventOfCode.Enums;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public class Day5(IFileHelper fileHelper) : BaseDay(fileHelper), IDay
    {
        public DayEnum Day => DayEnum.Day5;

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

            var (rules, data) = MapData(lines);

            var total = CalculateTotalOfReorderedDocuments(rules, data);

            Console.WriteLine($"Total: {total}");
        }

        private (IDictionary<int, Rule>, IEnumerable<IList<int>>) MapData(IEnumerable<string> lines)
        {
            var rules = new Dictionary<int, Rule>();
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
                        rules[firstValue].MustPrecede.Add(secondValue);
                    }
                    else
                    {
                        var rule = new Rule();
                        rule.MustPrecede.Add(secondValue);
                        rules.Add(firstValue, rule);
                    }

                    if (rules.ContainsKey(secondValue))
                    {
                        rules[secondValue].MustBePrecededBy.Add(firstValue);
                    }
                    else
                    {
                        var rule = new Rule();
                        rule.MustBePrecededBy.Add(firstValue);
                        rules.Add(secondValue, rule);
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

        private int CalculateTotal(IDictionary<int, Rule> rules, IEnumerable<IList<int>> data)
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

        private int CalculateTotalOfReorderedDocuments(IDictionary<int, Rule> rules, IEnumerable<IList<int>> data)
        {
            var total = 0;

            foreach (var pageValues in data)
            {
                if (!PageOrderIsValid(rules, pageValues))
                {
                    var correctedPageValues = GetCorrectedPageValuesCollection(rules, pageValues);

                    var middlePageValue = GetMiddlePageValue(correctedPageValues);

                    total += middlePageValue;
                }
            }

            return total;
        }

        private bool PageOrderIsValid(IDictionary<int, Rule> rules, IList<int> pageValues)
        {
            //foreach (var pageValue in pageValues)
            for (int i = 1; i < pageValues.Count; i++)
            {
                var pageValue = pageValues[i];

                if (!rules.ContainsKey(pageValue))
                {
                    continue;
                }

                var pagesPageMustPrecede = rules[pageValue].MustPrecede;

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

        private IList<int> GetCorrectedPageValuesCollection(IDictionary<int, Rule> rules, IList<int> pageValues)
        {
            var pageValueCollection = new ObservableCollection<int>(pageValues);

            for (int i = 0; i < pageValueCollection.Count; i++)
            {
                var pageValue = pageValueCollection.ElementAt(i);

                if (!rules.ContainsKey(pageValue))
                {
                    continue;
                }

                var rule = rules[pageValue];

                for (var j = 0; j < i; j++)
                {
                    var comparisonValue = pageValueCollection.ElementAt(j);

                    if (rule.MustPrecede.Contains(comparisonValue))
                    {
                        pageValueCollection.Move(i, j);
                        i = j + 1;
                        break;
                    }
                }
            }

            return pageValueCollection.ToList();
        }

        private class Rule
        {
            public IList<int> MustPrecede { get; set; } = new List<int>();
            public IList<int> MustBePrecededBy { get; set; } = new List<int>();
        }
    }
}
