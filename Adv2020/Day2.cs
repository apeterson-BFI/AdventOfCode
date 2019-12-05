using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day2
    {
        private IntCode intCode;

        public Day2()
        {
            intCode = new IntCode(DayInput.readDayLinesAsIntCSV(2, ',')[0]);
        }

        public long getPart1Answer()
        {
            return intCode.testLoader(12, 2);
        }

        public long getPart2Answer()
        {
            var tup = intCode.goalSeek(19690720);

            return (100 * tup.Item1 + tup.Item2);
        }
    }
}