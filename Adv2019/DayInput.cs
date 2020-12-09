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
        public static string[] readDayLines(int day, bool live)
        {
            string filename;

            if (!live)
            {
                filename = string.Format(@"day{0}sample.txt", day);
            }
            else
            {
                filename = string.Format(@"day{0}input.txt", day);
            }

            return File.ReadAllLines(filename);
        }

        public static string readDayLine(int day, bool live)
        {
            return readDayLines(day, live)[0];
        }

        public static List<List<string>> readDayLinesAsParagraphs(int day, bool live)
        {
            string[] lines = readDayLines(day, live);
            List<List<string>> paragraphs = new List<List<string>>();
            List<string> paragraph = new List<string>();

            foreach(var line in lines)
            {
                if (line == "")
                {
                    paragraphs.Add(paragraph);
                    paragraph = new List<string>();
                }
                else
                {
                    paragraph.Add(line);
                }
            }

            if(paragraph.Count > 0)
            {
                paragraphs.Add(paragraph);
            }

            return paragraphs;
        }

        public static int[] readDayLineAsDigitInts(int day, bool live)
        {
            return readDayLine(day, live)
                   .Select(c => (int)Char.GetNumericValue(c))
                   .ToArray();
        }

        public static int[] readDayLineAsDigitInts(int day, bool live, int repeats)
        {
            int[] baseDigits = readDayLineAsDigitInts(day, live);

            int[] resultDigits = new int[baseDigits.Length * repeats];

            int len = baseDigits.Length;

            for(int i = 0; i < repeats; i++)
            {
                Array.Copy(baseDigits, 0, resultDigits, i * len, len);
            }

            return resultDigits;
        }

        public static List<long> readDayLinesAsLongs(int day, bool live)
        {
            return
                readDayLines(day, live)
                .Select(l => Int64.Parse(l))
                .ToList();
        }

        public static List<List<int>> readDayLinesAsIntCSV(int day, bool live, char split)
        {
            var lines = readDayLines(day, live);

            return lines.Select(l => splitLineToIntCells(l, split))
                        .ToList();
        }

        public static List<List<long>> readDayLinesAsLongCSV(int day, bool live, char split)
        {
            var lines = readDayLines(day, live);

            return lines.Select(l => splitLineToLongCells(l, split))
                        .ToList();
        }

        public static List<List<string>> readDayLinesAsTextCSV(int day, bool live, char split)
        {
            var lines = readDayLines(day, live);

            return lines.Select(l => splitLineToTextCells(l, split))
                        .ToList();
        }

        public static List<List<string>> readDayLinesAsCharGrid(int day, bool live)
        {
            var lines = readDayLines(day, live);

            return lines.Select(l => l.ToCharArray().Select(c => new string(c, 1)).ToList()).ToList();
        }

        public static List<SignedPoint> readLinesAsPoints(int day, bool live, char present)
        {
            List<SignedPoint> points = new List<SignedPoint>();
            string[] dayLines = readDayLines(day, live);

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

        public static IntCode readLinesAsIntCode(int day, bool live, int memSize = 65536)
        {
            var rom = DayInput.readDayLinesAsLongCSV(day, live, ',')[0];

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
