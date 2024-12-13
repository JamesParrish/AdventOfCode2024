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

            var lines = GetLines(Day, StageEnum.Stage2);

            var totalCrossedWordCount = CalculateTotalCrossedWordCount(lines.ToList());

            Console.WriteLine($"Total crossed word count: {totalCrossedWordCount}");
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

        private int CalculateTotalCrossedWordCount(IList<string> lines)
        {
            var totalCrossedWordCount = 0;

            for (int l = 0; l < lines.Count(); l++)
            {
                var line = lines[l];

                for (var c = 0; c < line.Length; c++)
                {
                    if (char.Equals(line[c], SecondCharacter))
                    {
                        var crossedWordCountStartingAtCharacter = GetCrossedWordCountStartingFromCharacter(lines, l, c);
                        totalCrossedWordCount += crossedWordCountStartingAtCharacter;
                    }
                }
            }

            return totalCrossedWordCount;
        }

        private int GetWordCountStartingFromCharacter(IList<string> lines, int lineIndex, int characterIndex)
        {
            var wordCount = 0;

            for (var lineStep = -1; lineStep <= 1; lineStep++)
            {
                for (var columnStep = -1; columnStep <= 1; columnStep++)
                {
                    if (HasWord(lines, lineIndex, characterIndex, lineStep, columnStep, [SecondCharacter, ThirdCharacter, FourthCharacter]))
                    {
                        wordCount++;
                    }
                }
            }

            return wordCount;
        }

        private int GetCrossedWordCountStartingFromCharacter(IList<string> lines, int lineIndex, int characterIndex)
        {
            var wordCount = 0;

            foreach (var step in new[] { -1, 1 })
            {
                if (!HasWord(lines, lineIndex, characterIndex, step, step, [ThirdCharacter, FourthCharacter]))
                {
                    continue;
                }

                if (HasCrossedWordCompletion(lines, lineIndex, characterIndex, step))
                {
                    wordCount++;
                }
            }

            return wordCount;
        }

        private bool HasWord(IList<string> lines, int lineIndex, int characterIndex, int lineStep, int characterStep, IList<char> charactersToFind)
        {
            var endingLineIndex = lineIndex + (lineStep * charactersToFind.Count);

            if (endingLineIndex < 0 || endingLineIndex > lines.Count - 1)
            {
                return false;
            }

            var endingCharacterIndex = characterIndex + (characterStep * charactersToFind.Count);

            if (endingCharacterIndex < 0 || endingCharacterIndex > lines.First().Length - 1)
            {
                return false;
            }

            for (var i = 0; i < charactersToFind.Count; i++)
            {
                var character = lines[lineIndex + (lineStep * (i + 1))][characterIndex + (characterStep * (i + 1))];

                if (!char.Equals(character, charactersToFind[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool HasCrossedWordCompletion(IList<string> lines, int lineIndex, int characterIndex, int step)
        {
            var firstCharacterToCheck = lines[lineIndex + (step * 2)][characterIndex];

            if (char.Equals(firstCharacterToCheck, 'M'))
            {
                var secondCharacterToCheck = lines[lineIndex][characterIndex + (step * 2)];

                if (char.Equals(secondCharacterToCheck, 'S'))
                {
                    return true;
                }
            }
            else if (char.Equals(firstCharacterToCheck, 'S'))
            {
                var secondCharacterToCheck = lines[lineIndex][characterIndex + (step * 2)];

                if (char.Equals(secondCharacterToCheck, 'M'))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
