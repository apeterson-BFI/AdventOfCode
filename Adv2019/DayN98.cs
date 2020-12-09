using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Adv2020
{
    public class DayN98
    {
        public static Regex LineReader = new Regex(@"(\d+)x(\d+)x(\d+)");
        List<Tuple<long, long, long>> lines = new List<Tuple<long, long, long>>();

        public DayN98()
        {
            lines = DayInput.readDayLines(-98, true)
                            .Select(x => LineReader.Match(x))
                            .Select(m => new Tuple<long, long, long>(Int64.Parse(m.Groups[1].Value), Int64.Parse(m.Groups[2].Value), Int64.Parse(m.Groups[3].Value)))
                            .ToList();
        }

        public long getPart1Answer()
        {
            return
                lines.Select(line => new { h = line.Item1, w = line.Item2, d = line.Item3 })
                     .Sum(an => 2L * an.h * an.w + 2L * an.h * an.d + 2L * an.w * an.d + min3(an.h * an.w, an.h * an.d, an.w * an.d));           
        }

        public long getPart2Answer()
        {
            return lines.Select(line => new { h = line.Item1, w = line.Item2, d = line.Item3 })
                        .Sum(an => 2L * (min3(an.h, an.w, an.d) + mid3(an.h, an.w, an.d)) + an.h * an.w * an.d);
        }

        public long min3(long a1, long a2, long a3)
        {
            return Math.Min(Math.Min(a1, a2), a3);
        }

        public long max3(long a1, long a2, long a3)
        {
            return Math.Max(Math.Max(a1, a2), a3);
        }

        public long mid3(long a, long b, long c)
        {
            if (a == b || a == c)
                return a;
            else if (b == c)
                return b;

            // Checking for b 
            if ((a < b && b < c) || (c < b && b < a))
                return b;

            // Checking for a 
            else if ((b < a && a < c) || (c < a && a < b))
                return a;

            else
                return c;
        }
    }
}
