using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Adv2019
{
    public class Day5
    {
        private IntCode intCode;

        public Day5()
        {
            intCode = new IntCode(DayInput.readDayLinesAsLongCSV(5, ',')[0]);
        }

        public long getPart1Answer()
        {
            intCode.Input = new ConcurrentQueue<long>();
            intCode.Input.Enqueue(1L);
            intCode.process();

            return intCode.getDiagnostic();
        }

        public long getPart2Answer()
        {
            intCode.Input = new ConcurrentQueue<long>();
            intCode.Input.Enqueue(5L);
            intCode.process();

            return intCode.getDiagnostic();
        }
    }
}
