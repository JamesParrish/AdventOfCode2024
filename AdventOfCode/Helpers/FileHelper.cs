namespace AdventOfCode.Helpers
{
    public class FileHelper : IFileHelper
    {
        public IEnumerable<string> GetFileLines(string filename)
        {
            var lines = File.ReadAllLines(filename);
            return lines;
        }
    }
}
