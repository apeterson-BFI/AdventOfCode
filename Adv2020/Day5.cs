using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            intCode.Input = new List<int>() { 1 };
            intCode.process();

            return intCode.getDiagnostic();
        }

        public int getPart2Answer()
        {
            intCode.Input = new List<int>() { 5 };
            intCode.process();

            return intCode.getDiagnostic();
        }
    }
}
