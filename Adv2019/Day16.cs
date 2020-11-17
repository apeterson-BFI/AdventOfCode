using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day16
    {
        public int[] inDigits;

        public int[] oldDigits;

        public int[] currDigits;

        public void setup(bool live)
        {
            int day;

            if (live)
                day = 16;
            else
                day = -16;

            inDigits = DayInput.readDayLineAsDigitInts(day);

            oldDigits = new int[inDigits.Length];
            currDigits = new int[inDigits.Length];

            Array.Copy(inDigits, oldDigits, inDigits.Length);
        }

        public int pattern(int outputRow, int inElement)
        {
            int oi = inElement + 1;
            int pset = oi / (outputRow + 1);

            int r = pset % 4;

            if (r == 0)
                return 0;
            else if (r == 1)
                return 1;
            else if (r == 2)
                return 0;
            else if (r == 3)
                return -1;
            else
                return 0;
        }

        public void transform()
        {
            int sum = 0;

            for(int i = 0; i < currDigits.Length; i++)
            {
                sum = 0;

                for(int j = 0; j < currDigits.Length; j++)
                {
                    sum += pattern(i, j) * oldDigits[j];
                }

                // digit value, -7 -> 7, 73 ->3, -38 -> 8

                Console.Write("{0} ", sum);

                currDigits[i] = Math.Abs(sum % 10);
            }

            Array.Copy(currDigits, oldDigits, currDigits.Length);
        }

        public string rpt(int repetitions)
        {
            for(int i = 0; i < repetitions; i++)
            {
                transform();
            }

            string res = string.Join("", currDigits);
            return res;
        }
    }
}
