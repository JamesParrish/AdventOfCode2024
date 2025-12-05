using AdventOfCode.Enums;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public abstract class BaseDay
    {
        public abstract DayEnum Day { get; }
        private readonly IFileHelper _fileHelper;

        public BaseDay(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }

        public IEnumerable<string> GetLines(DayEnum day, StageEnum stage)
        {
            var filename = $"Inputs/{day}/{stage}.txt";
            var lines = _fileHelper.GetFileLines(filename);
            return lines;
        }

        public void Process1Star()
        {
            Console.WriteLine("Processing 1 Star");

            var lines = GetLines(Day, StageEnum.Stage1);

            var solution = SolveOneStar(lines);

            Console.WriteLine($"Solution: {solution}");
        }

        public void Process2Star()
        {
            Console.WriteLine("Processing 2 Star");

            var lines = GetLines(Day, StageEnum.Stage2);

            var solution = SolveTwoStar(lines);

            Console.WriteLine($"Solution: {solution}");
        }

        protected abstract string SolveOneStar(IEnumerable<string> lines);
        protected abstract string SolveTwoStar(IEnumerable<string> lines);
    }
}
