using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Adv2020
{
    public class Day16
    {
        public int[] inDigits;

        public int[] outDigits;

        public int offset;

        public void setup(bool live)
        {
            int day;

            if (live)
                day = 16;
            else
                day = -16;

            inDigits = DayInput.readDayLineAsDigitInts(day, true);
        }

        public void setup2(bool live)
        {
            int day;

            if (live)
                day = 16;
            else
                day = -16;

            inDigits = DayInput.readDayLineAsDigitInts(day, true, 10000);

            int[] offArray = new int[7];
            Array.Copy(inDigits, offArray, 7);

            offset = Int32.Parse(string.Join("", offArray));

            Console.WriteLine("Digits: {0}, Offset: {1}", inDigits.Length, offset);

            inDigits = inDigits.Skip(offset).ToArray();


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

        // algo analysis
        // 100 transforms * X digits * X digits = 100 x^2 :
        // sample: x = 10000 * 39^2 = 16,000,000,000 mults

        public int[] transform(int[] startVals)
        {
            int[] res = new int[startVals.Length];

            int accum;

            for (int outputRow = 0; outputRow < startVals.Length; outputRow++)
            {
                accum = 0;

                for(int multCol = 0; multCol < startVals.Length; multCol++)
                {
                    accum += pattern(outputRow, multCol) * startVals[multCol];
                }

                res[outputRow] = Math.Abs(accum) % 10;
            }

            return res;
        }

        public int[] transform(int[] startVals, int offset)
        {
            int[] res = new int[startVals.Length];

            int accum;

            for (int outputRow = 0; outputRow < startVals.Length; outputRow++)
            {
                accum = 0;

                for (int multCol = 0; multCol < startVals.Length; multCol++)
                {
                    accum += pattern(outputRow + offset, multCol + offset) * startVals[multCol];
                }

                res[outputRow] = Math.Abs(accum) % 10;
            }

            return res;
        }

        public int[] transform2(int[] startVals)
        {
            int[] res = new int[startVals.Length];

            int total = 0;

            for(int i = startVals.Length - 1; i >= 0; i--)
            {
                total = (total + startVals[i]) % 10;

                res[i] = total;
            }

            return res;
        }


        public string rpt(int repetitions)
        {
            int[] startArr = new int[inDigits.Length];

            Array.Copy(inDigits, startArr, inDigits.Length);

            for(int i = 0; i < repetitions; i++)
            {
                startArr = transform(startArr);
            }

            return string.Join("", startArr);
        }

        public string rpt2(int repetitions)
        {
            int[] startArr = new int[inDigits.Length];

            Array.Copy(inDigits, startArr, inDigits.Length);

            for (int i = 0; i < repetitions; i++)
            {
                startArr = transform2(startArr);
            }
            
            return string.Join("", startArr.Take(8));
        }
    }
}
