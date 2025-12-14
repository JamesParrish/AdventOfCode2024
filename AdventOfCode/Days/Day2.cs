using AdventOfCode.Enums;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public class Day2 : BaseDay, IDay
    {
        public override DayEnum Day => DayEnum.Day2;

        public Day2(IFileHelper fileHelper) : base(fileHelper) { }

        protected override string SolveOneStar(IEnumerable<string> lines)
        {
            var ranges = GetRanges(lines.First());

            long invalidIdTotal = 0;

            foreach (var (start, end) in ranges)
            {
                invalidIdTotal += GetInvalidIdTotal(start, end, false);
            }

            return invalidIdTotal.ToString();
        }

        protected override string SolveTwoStar(IEnumerable<string> lines)
        {
            var ranges = GetRanges(lines.First());

            long invalidIdTotal = 0;

            foreach (var (start, end) in ranges)
            {
                invalidIdTotal += GetInvalidIdTotal(start, end, true);
            }

            return invalidIdTotal.ToString();
        }

        private IEnumerable<(long, long)> GetRanges(string line)
        {
            var ranges = new List<(long, long)>();
            var rangeStrings = line.Split(',');

            foreach (var rangeString in rangeStrings)
            {
                var startAndEndStrings = rangeString.Split('-');

                long.TryParse(startAndEndStrings[0], out var startValue);
                long.TryParse(startAndEndStrings[1], out var endValue);

                ranges.Add((startValue, endValue));
            }

            return ranges;
        }

        private long GetInvalidIdTotal(long start, long end, bool checkMultiplePartIds)
        {
            long invalidIdTotal = 0;

            for (var i = start; i <= end; i++)
            {
                if (IsFakeId(i, checkMultiplePartIds))
                {
                    invalidIdTotal += i;
                }
            }

            return invalidIdTotal;
        }

        private bool IsFakeId(long value, bool checkMultiplePartIds)
        {
            if (!checkMultiplePartIds)
            {
                var splitValues = SplitValues(value, 2);
            }

            var maximumSplitCount = checkMultiplePartIds ? value.ToString().Length : 2;

            for (var i = 2; i <= maximumSplitCount; i++)
            {
                var splitValues = SplitValues(value, i);

                if (splitValues == null)
                {
                    continue;
                }

                if (splitValues.Distinct().Count() == 1)
                {
                    return true;
                }
            }

            return false;
        }

        private IEnumerable<string>? SplitValues(long value, int parts)
        {
            var stringValue = value.ToString();

            if (stringValue.Length % parts != 0)
            {
                return null;
            }

            var splitLength = stringValue.Length / parts;

            var substrings = new List<string>();

            for (int i = 0; i < parts; i++)
            {
                var startPosition = i * splitLength;

                substrings.Add(stringValue.Substring(startPosition, splitLength));
            }

            return substrings;
        }
    }
}
