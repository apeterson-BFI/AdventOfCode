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
    }
}
