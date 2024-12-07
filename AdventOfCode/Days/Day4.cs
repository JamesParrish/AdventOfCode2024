using AdventOfCode.Enums;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public class Day4(IFileHelper fileHelper) : BaseDay(fileHelper), IDay
    {
        public DayEnum Day => DayEnum.Day4;

        private const char FirstCharacter = 'X';
        private const char SecondCharacter = 'M';
        private const char ThirdCharacter = 'A';
        private const char FourthCharacter = 'S';

        public void Process1Star()
        {
            Console.WriteLine("Processing Day 4 - 1 Star");

            var lines = GetLines(Day, StageEnum.Stage1);

            var totalWordCount = CalculateTotalWordCount(lines.ToList());

            Console.WriteLine($"Total word count: {totalWordCount}");
        }

        public void Process2Star()
        {
            Console.WriteLine("Processing Day 4 - 2 Star");

            //var lines = GetLines(Day, StageEnum.Stage2);

            Console.WriteLine($"Pending");
        }

        private int CalculateTotalWordCount(IList<string> lines)
        {
            var totalWordCount = 0;

            for (int l = 0; l < lines.Count(); l++)
            {
                var line = lines[l];

                for (var c = 0; c < line.Length; c++)
                {
                    if (char.Equals(line[c], FirstCharacter))
                    {
                        var wordCountStartingAtCharacter = GetWordCountStartingFromCharacter(lines, l, c);
                        totalWordCount += wordCountStartingAtCharacter;
                    }
                }
            }

            return totalWordCount;
        }

        private int GetWordCountStartingFromCharacter(IList<string> lines, int lineIndex, int characterIndex)
        {
            var wordCount = 0;

            for (var lineStep = -1; lineStep <= 1; lineStep++)
            {
                for (var columnStep = -1; columnStep <= 1; columnStep++)
                {
                    if (HasWord(lines, lineIndex, characterIndex, lineStep, columnStep))
                    {
                        wordCount++;
                    }
                }
            }

            return wordCount;
        }

        private bool HasWord(IList<string> lines, int lineIndex, int characterIndex, int lineStep, int characterStep)
        {
            var endingLineIndex = lineIndex + (lineStep * 3);

            if (endingLineIndex < 0 || endingLineIndex > lines.Count - 1)
            {
                return false;
            }

            var endingCharacterIndex = characterIndex + (characterStep * 3);

            if (endingCharacterIndex < 0 || endingCharacterIndex > lines.First().Length - 1)
            {
                return false;
            }

            var secondCharacter = lines[lineIndex + lineStep][characterIndex + characterStep];

            if (!char.Equals(secondCharacter, SecondCharacter))
            {
                return false;
            }

            var thirdCharacter = lines[lineIndex + (lineStep * 2)][characterIndex + (characterStep * 2)];

            if (!char.Equals(thirdCharacter, ThirdCharacter))
            {
                return false;
            }

            var fourthCharacter = lines[lineIndex + (lineStep * 3)][characterIndex + (characterStep * 3)];

            if (!char.Equals(fourthCharacter, FourthCharacter))
            {
                return false;
            }

            return true;
        }
    }
}
