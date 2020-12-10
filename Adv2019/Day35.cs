using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day35
    {
        public List<long> lines;

        public Day35()
        {
            lines = DayInput.readDayLinesAsLongs(35, true);


        }

        public long getPart1Answer()
        {
            lines.Sort();

            long jolt1 = 0L;
            long jolt3 = 1L;
            long target = 0;

            for(int i = 0; i < lines.Count; i++)
            {
                if (lines[i] - target > 3)
                    throw new ArgumentException("Bad jolt, bad algorithm");
                else if (lines[i] - target == 3)
                    jolt3++;
                else if (lines[i] - target == 1)
                    jolt1++;

                target = lines[i];
            }

            return jolt1 * jolt3;
        }

        public long getPart2Answer()
        {
            throw new NotImplementedException(); // excel
        }
    }
}
