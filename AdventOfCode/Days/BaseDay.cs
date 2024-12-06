using AdventOfCode.Enums;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public abstract class BaseDay
    {
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
    }
}
