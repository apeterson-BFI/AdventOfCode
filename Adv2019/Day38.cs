using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day38
    {
        public long target;

        public List<long> buses;

        public Day38()
        {
            string[] lines = DayInput.readDayLines(38, true);

            target = Int64.Parse(lines[0]);

            buses = new List<long>();

            string[] busText = lines[1].Split(',');

            foreach(string bus in busText)
            {
                if (bus == "x")
                    buses.Add(0L);
                else
                    buses.Add(Int64.Parse(bus));
            }
        }

        public long getPart1Answer()
        {
            long min = 2 * target;
            long fbus = 0;
            long work1;
            long work2;

            foreach(long bus in buses)
            {
                if (bus == 0)
                    continue;

                work1 = (target / bus) * bus;

                if (work1 == target)
                {
                    min = work1;
                    fbus = bus;
                }
                else
                {
                    work2 = work1 + bus;

                    if(work2 < min)
                    {
                        min = work2;
                        fbus = bus;
                    }
                }
            }

            work1 = min - target;

            return work1 * fbus;
        }


    }
}
