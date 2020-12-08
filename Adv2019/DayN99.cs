using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class DayN99
    {
        public string line;

        public DayN99()
        {
            line = DayInput.readDayLine(-99, true);
        }

        public long getPart1Answer()
        {
            long accum = 0L;

            foreach(char c in line)
            {
                if (c == '(')
                    accum++;
                else if (c == ')')
                    accum--;
            }

            return accum;
        }

        public long getPart2Answer()
        {
            long accum = 0L;
            char c;


            for (int i = 0; i < line.Length; i++)
            {
                if (accum == -1L)
                    return i;

                c = line[i];

                if (c == '(')
                    accum++;
                else if (c == ')')
                    accum--;
            }

            if (accum == -1)
                return line.Length;

            throw new Exception("Never there.");
        }

    }
}
