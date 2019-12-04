using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day1
    {
        public long getPart1Answer()
        {
            List<long> modules = DayInput.readDayLinesAsLongs(1);

            return modules.Sum(m => getFuelCost(m));
        }

        public long getPart2Answer()
        {
            List<long> modules = DayInput.readDayLinesAsLongs(1);

            return modules.Sum(m => getRecursiveFuelCost(m));
        }

        private long getFuelCost(long mass)
        {
            return Math.Max(0L, (mass / 3L - 2L));
        }

        internal long getRecursiveFuelCost(long mass)
        {
            if (mass == 0L)
            {
                return 0L;
            }
            else
            {
                long baseCost = getFuelCost(mass);

                return (baseCost + getRecursiveFuelCost(baseCost));
            }
        }
    }
}