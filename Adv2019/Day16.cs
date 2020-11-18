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

            inDigits = DayInput.readDayLineAsDigitInts(day);
        }

        public void setup2(bool live)
        {
            int day;

            if (live)
                day = 16;
            else
                day = -16;

            inDigits = DayInput.readDayLineAsDigitInts(day, 10000);

            int[] offArray = new int[7];
            Array.Copy(inDigits, offArray, 7);

            offset = Int32.Parse(string.Join("", offArray));
        }
        
        public double pattern(int outputRow, int inElement)
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
        // A^n * v

        // each output digit is a row in a matrix
        // result is matrix multiplication and then mod.
        public Matrix<double> createPatternMatrix()
        {
            return CreateMatrix.Dense<double>(inDigits.Length, inDigits.Length, pattern);

            // 1,0,-1,0,1, 0,-1,0,....
            // 0,1, 1,0,0,-1,-1,
        }

        public Vector<double> calculatePowerProduct(Matrix<double> pattMatrix, int exponent)
        {
            Matrix<double> powMatrix = CreateMatrix.Dense<double>(inDigits.Length, inDigits.Length);
            Vector<double> resVector = CreateVector.Dense<double>(inDigits.Length);

            Vector<double> inVector = CreateVector.Dense<double>(inDigits.Length, i => (double)inDigits[i]);

            pattMatrix.Power(exponent, powMatrix);
            powMatrix.Multiply(inVector, resVector);

            return resVector;
        }

        public void multDN(Matrix<double> pattMatrix, Vector<double> inVect, Vector<double> resVect)
        {
            pattMatrix.Multiply(inVect, resVect);

            
        }


        public string rpt(int repetitions)
        {
            Matrix<double> pattMatrix = createPatternMatrix();
            
            // inVect = in elements -> vect

            Vector<double> resVector = CreateVector.Dense<double>(inVect.Count);


            var t8 = resVector.Take(8);
            var id = t8.Select(x => (int)x);
           
            string res = string.Join("", resVector.Take(8).Select(x => ((int)Math.Abs(x)) % 10));

            return res;
        }

        public string rpt2(int repetitions)
        {
            Matrix<double> pattMatrix = createPatternMatrix();
            Vector<double> resVector = calculatePowerProduct(pattMatrix, repetitions);

            string res = string.Join("", resVector.Skip(offset).Take(8).Select(x => (int)x));

            return res;
        }
    }
}
