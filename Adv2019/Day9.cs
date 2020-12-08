using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Adv2020
{
    public class Day9
    {
        public IntCode IntCode { get; set; }

        public Day9()
        {
            var rom = DayInput.readDayLinesAsLongCSV(9, true, ',')[0];

            IntCode = new IntCode(rom);
        }

        public long getPart1Answer()
        {
            IntCode.Input = new ConcurrentQueue<long>();
            IntCode.Input.Enqueue(1L);
            IntCode.process();

            return IntCode.getDiagnostic();
        }

        public long getPart2Answer()
        {
            IntCode.Input = new ConcurrentQueue<long>();
            IntCode.Input.Enqueue(2L);
            IntCode.process();

            return IntCode.getDiagnostic();
        }
    }
}
