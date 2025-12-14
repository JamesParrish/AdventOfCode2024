using System.Text;
using AdventOfCode.Enums;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public class Day3 : BaseDay, IDay
    {
        public override DayEnum Day => DayEnum.Day3;

        public Day3(IFileHelper fileHelper) : base(fileHelper) { }

        protected override string SolveOneStar(IEnumerable<string> lines)
        {
            long totalJoltage = 0;

            foreach (var line in lines)
            {
                totalJoltage += GetMaximumJoltage(line, 2);
            }

            return totalJoltage.ToString();
        }

        protected override string SolveTwoStar(IEnumerable<string> lines)
        {
            long totalJoltage = 0;

            foreach (var line in lines)
            {
                totalJoltage += GetMaximumJoltage(line, 12);
            }

            return totalJoltage.ToString();
        }

        private long GetMaximumJoltage(string batteryBank, int batteryCount)
        {
            var batteries = batteryBank.ToCharArray();

            var activatedBatteryIndexes = new int[batteryCount];

            for (int i = 0; i < batteryCount; i++)
            {
                int? previousBatteryIndex = i == 0 ? null : activatedBatteryIndexes[i - 1];

                var batteriesToConsider = i == 0 ?
                    batteries :
                    batteryBank.Substring(activatedBatteryIndexes[i - 1] + 1).ToCharArray();

                for (int x = 9; x >= 1; x--)
                {
                    var firstIndex = GetFirstIndex(x, batteriesToConsider);

                    if (firstIndex > -1 && firstIndex < batteriesToConsider.Length - batteryCount + i + 1)
                    {
                        activatedBatteryIndexes[i] = i == 0 ? firstIndex : firstIndex + previousBatteryIndex.Value + 1;
                        break;
                    }
                }
            }

            var maximumJoltageStringBuilder = new StringBuilder();

            foreach (var activatedBatteryIndex in activatedBatteryIndexes)
            {
                maximumJoltageStringBuilder.Append(batteries[activatedBatteryIndex]);
            }

            long.TryParse(maximumJoltageStringBuilder.ToString(), out var maximumJoltage);

            //Console.WriteLine(maximumJoltage);
            return maximumJoltage;
        }

        private int GetFirstIndex(int value, char[] batteries)
        {
            if (value < 0 || value > 9)
            {
                throw new ArgumentOutOfRangeException("Battery value must be 0-9");
            }

            var valueCharacter = value.ToString()[0];

            var firstIndex = batteries.ToList().IndexOf(valueCharacter);

            return firstIndex;
        }
    }
}
