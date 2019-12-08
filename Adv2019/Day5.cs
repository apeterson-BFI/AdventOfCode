using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Adv2020
{
    public class Day5
    {
        private IntCode intCode;

        public Day5()
        {
            intCode = new IntCode(DayInput.readDayLinesAsIntCSV(5, ',')[0]);
        }

        public int getPart1Answer()
        {
            intCode.Input = new ConcurrentQueue<int>();
            intCode.Input.Enqueue(1);
            intCode.process();

            return intCode.getDiagnostic();
        }

        public int getPart2Answer()
        {
            intCode.Input = new ConcurrentQueue<int>();
            intCode.Input.Enqueue(5);
            intCode.process();

            return intCode.getDiagnostic();
        }
    }
}
