using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Adv2020
{
    public static class DayInput
    {
        public static string[] readDayLines(int day)
        {
            string filename = string.Format(@"day{0}input.txt", day);

            return File.ReadAllLines(filename);
        }

        public static List<long> readDayLinesAsLongs(int day)
        {
            return
                readDayLines(day)
                .Select(l => Int64.Parse(l))
                .ToList();
        }

        public static List<List<int>> readDayLinesAsIntCSV(int day, char split)
        {
            var lines = readDayLines(day);

            return lines.Select(l => splitLineToIntCells(l, split))
                        .ToList();
        }

        public static List<List<string>> readDayLinesAsTextCSV(int day, char split)
        {
            var lines = readDayLines(day);

            return lines.Select(l => splitLineToTextCells(l, split))
                        .ToList();
        }

        private static List<int> splitLineToIntCells(string line, char split)
        {
            var splits = line.Split(split);

            return splits.Select(s => Int32.Parse(s)).ToList();
        }

        private static List<string> splitLineToTextCells(string line, char split)
        {
            var splits = line.Split(split);
            return splits.ToList();
        }
    }
}
