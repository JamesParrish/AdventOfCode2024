using AdventOfCode.Enums;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public class Day2 : BaseDay, IDay
    {
        public DayEnum Day => DayEnum.Day2;

        private const int ToleranceLevel = 3;
        private const int ToleranceDampenerAllowance = 1;

        public Day2(IFileHelper fileHelper) : base(fileHelper) { }

        public void Process1Star()
        {
            Console.WriteLine("Processing Day 2 - 1 Star");

            var lines = GetLines(Day, StageEnum.Stage1);

            var safeReportCount = GetNumberOfSafeReports(lines, 0);

            Console.WriteLine($"Total safe reports: {safeReportCount}");
        }

        public void Process2Star()
        {
            Console.WriteLine("Processing Day 2 - 2 Star");

            var lines = GetLines(Day, StageEnum.Stage1);

            var safeReportCount = GetNumberOfSafeReports(lines, ToleranceDampenerAllowance);

            Console.WriteLine($"Total safe reports: {safeReportCount}");
        }

        private int GetNumberOfSafeReports(IEnumerable<string> lines, int allowedErrors)
        {
            var safeReportCount = 0;

            foreach (var line in lines)
            {
                var report = GetReport(line);
                var isSafe = IsReportSafe(report, allowedErrors);

                if (isSafe)
                {
                    safeReportCount++;
                }
            }

            return safeReportCount;
        }

        private IList<int> GetReport(string line)
        {
            var rawValues = line.Split(' ');
            var numericValues = rawValues.Select(i => Int32.Parse(i));
            return numericValues.ToList();
        }

        private bool IsReportSafe(IList<int> report, int allowedErrors)
        {
            if (report.Count < 2)
            {
                return true;
            }

            if (report[0] > report[1])
            {
                if (IsDescendingReportSafe(report, allowedErrors))
                {
                    return true;
                }

                if (allowedErrors == 0)
                {
                    return false;
                }

                if (report[0] < report[2])
                {
                    var updatedReport = new List<int>(report);
                    updatedReport.RemoveAt(1);
                    return IsAscendingReportSafe(updatedReport, allowedErrors - 1);
                }

                if (report[1] < report[2])
                {
                    var updatedReport = new List<int>(report);
                    updatedReport.RemoveAt(0);
                    return IsAscendingReportSafe(updatedReport, allowedErrors - 1);
                }
            }
            else
            {
                if (IsAscendingReportSafe(report, allowedErrors))
                {
                    return true;
                }

                if (allowedErrors == 0)
                {
                    return false;
                }

                if (report[0] > report[2])
                {
                    var updatedReport = new List<int>(report);
                    updatedReport.RemoveAt(1);
                    return IsDescendingReportSafe(updatedReport, allowedErrors - 1);
                }

                if (report[1] > report[2])
                {
                    var updatedReport = new List<int>(report);
                    updatedReport.RemoveAt(0);
                    return IsDescendingReportSafe(updatedReport, allowedErrors - 1);
                }
            }

            return false;
        }

        private bool IsDescendingReportSafe(IList<int> report, int allowedErrors)
        {
            for (var i = 0; i < report.Count - 1; i++)
            {
                var current = report[i];
                var next = report[i + 1];

                if (current <= next)
                {
                    return IsDescendingReportSafeWithinTolerableRange(report, allowedErrors, i);
                }

                if (current - ToleranceLevel > next)
                {
                    return IsDescendingReportSafeWithinTolerableRange(report, allowedErrors, i);
                }
            }

            return true;
        }

        private bool IsDescendingReportSafeWithinTolerableRange(IList<int> report, int allowedErrors, int errorIndex)
        {
            if (allowedErrors == 0)
            {
                return false;
            }

            var updatedReport = new List<int>(report);
            updatedReport.RemoveAt(errorIndex);

            if (IsDescendingReportSafe(updatedReport, allowedErrors - 1))
            {
                return true;
            }

            updatedReport = new List<int>(report);
            updatedReport.RemoveAt(errorIndex + 1);

            return IsDescendingReportSafe(updatedReport, allowedErrors - 1);
        }

        private bool IsAscendingReportSafe(IList<int> report, int allowedErrors)
        {
            for (var i = 0; i < report.Count - 1; i++)
            {
                var current = report[i];
                var next = report[i + 1];

                if (current >= next)
                {
                    return IsAscendingReportSafeWithinTolerableRange(report, allowedErrors, i);
                }

                if (current + ToleranceLevel < next)
                {
                    return IsAscendingReportSafeWithinTolerableRange(report, allowedErrors, i);
                }
            }

            return true;
        }

        private bool IsAscendingReportSafeWithinTolerableRange(IList<int> report, int allowedErrors, int errorIndex)
        {
            if (allowedErrors == 0)
            {
                return false;
            }

            var updatedReport = new List<int>(report);
            updatedReport.RemoveAt(errorIndex);

            if (IsAscendingReportSafe(updatedReport, allowedErrors - 1))
            {
                return true;
            }

            updatedReport = new List<int>(report);
            updatedReport.RemoveAt(errorIndex + 1);

            return IsAscendingReportSafe(updatedReport, allowedErrors - 1);
        }
    }
}
