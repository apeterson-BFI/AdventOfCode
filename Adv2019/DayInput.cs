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
            string filename;

            if (day < 0)
            {
                filename = string.Format(@"day{0}sample.txt", -day);
            }
            else
            {
                filename = string.Format(@"day{0}input.txt", day);
            }

            return File.ReadAllLines(filename);
        }

        public static string readDayLine(int day)
        {
            return readDayLines(day)[0];
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

        public static List<List<long>> readDayLinesAsLongCSV(int day, char split)
        {
            var lines = readDayLines(day);

            return lines.Select(l => splitLineToLongCells(l, split))
                        .ToList();
        }

        public static List<List<string>> readDayLinesAsTextCSV(int day, char split)
        {
            var lines = readDayLines(day);

            return lines.Select(l => splitLineToTextCells(l, split))
                        .ToList();
        }

        public static List<SignedPoint> readLinesAsPoints(int day, char present)
        {
            List<SignedPoint> points = new List<SignedPoint>();
            string[] dayLines = readDayLines(day);

            for (int y = 0; y < dayLines.Length; y++)
            {
                for (int x = 0; x < dayLines[y].Length; x++)
                {
                    if (dayLines[y][x] == present)
                    {
                        points.Add(new SignedPoint() { X = x, Y = y });
                    }
                }
            }

            return points;
        }

        public static IntCode readLinesAsIntCode(int day, int memSize = 65536)
        {
            var rom = DayInput.readDayLinesAsLongCSV(day, ',')[0];

            IntCode intCode = new IntCode(rom, memSize);

            return intCode;
        }

        private static List<int> splitLineToIntCells(string line, char split)
        {
            var splits = line.Split(split);

            return splits.Select(s => Int32.Parse(s)).ToList();
        }

        private static List<long> splitLineToLongCells(string line, char split)
        {
            var splits = line.Split(split);

            return splits.Select(s => Int64.Parse(s)).ToList();
        }

        private static List<string> splitLineToTextCells(string line, char split)
        {
            var splits = line.Split(split);
            return splits.ToList();
        }

        

    }
}
