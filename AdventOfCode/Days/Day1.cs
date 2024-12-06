using AdventOfCode.Enums;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public class Day1 : IDay
    {
        public DayEnum Day => DayEnum.Day1;

        private readonly IFileHelper _fileHelper;

        public Day1(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }

        public void Process1Star()
        {
            Console.WriteLine("Processing Day 1 - 1 Star");
        }

        public void Process2Star()
        {
            Console.WriteLine("Processing Day 1 - 2 Star");
        }
    }
}
