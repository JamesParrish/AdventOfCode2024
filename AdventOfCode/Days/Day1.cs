using AdventOfCode.Enums;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public class Day1 : BaseDay, IDay
    {
        public override DayEnum Day => DayEnum.Day1;

        public Day1(IFileHelper fileHelper) : base(fileHelper) { }

        protected override string SolveOneStar(IEnumerable<string> lines)
        {
            var zeroes = 0;
            var position = 50;

            foreach (var line in lines)
            {
                var clockwiseMoveAmount = GetClockwiseMoveAmount(line);
                position += clockwiseMoveAmount;

                var isOnZero = position % 100 == 0;

                if (isOnZero)
                {
                    zeroes++;
                }
            }

            return zeroes.ToString();
        }

        protected override string SolveTwoStar(IEnumerable<string> lines)
        {
            var zeroPasses = 0;
            var position = 50;

            foreach (var line in lines)
            {
                var clockwiseMoveAmount = GetClockwiseMoveAmount(line);

                var fullCircles = clockwiseMoveAmount / 100;

                clockwiseMoveAmount -= fullCircles * 100;

                var newPosition = position + clockwiseMoveAmount;

                if (fullCircles < 0)
                {
                    fullCircles *= -1;
                }

                zeroPasses += fullCircles;

                if (position != 0)
                {
                    if (newPosition >= 100 || newPosition <= 0)
                    {
                        zeroPasses++;
                    }
                }

                if (newPosition >= 100)
                {
                    newPosition -= 100;
                }

                if (newPosition < 0)
                {
                    newPosition += 100;
                }

                position = newPosition;
            }

            return zeroPasses.ToString();
        }

        private int GetClockwiseMoveAmount(string line)
        {
            var moveString = line[1..];
            int.TryParse(moveString, out var moveAmount);

            var moveDirectionString = line[..1];

            if (moveDirectionString == "L")
            {
                moveAmount *= -1;
            }

            return moveAmount;
        }
    }
}
