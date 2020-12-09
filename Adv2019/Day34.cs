using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day34
    {
        public List<long> numbers;

        public Day34()
        {
            numbers = DayInput.readDayLinesAsLongs(34, true);
        }

        public long getPart1Answer()
        {
            long at;

            bool foundSum;

            for(int i = 25; i < numbers.Count; i++)
            {
                foundSum = false;
                at = numbers[i];


                //for(int j = i - 25; j < i; j++)
                //{
                //    for(int k = i - 25; k < i; k++)
                //    {
                //        if (j == k)
                //            continue;

                //        if (numbers[j] + numbers[k] == at)
                //            foundSum = true;
                //    }
                //}

                foundSum = find(at, i);

                if (!foundSum)
                    return at;
            }

            throw new Exception("Never found");
        }

        public long getPart2Answer(long at)
        {
            long accum;

            for(int i = 0; i < numbers.Count; i++)
            {
                accum = numbers[i];

                for(int j = i + 1; j < numbers.Count; j++)
                {
                    accum += numbers[j];

                    if(accum == at)
                    {
                        var subs = numbers.Skip(i).Take(j - i + 1);

                        long min = subs.Min();
                        long max = subs.Max();

                        return min + max;
                    }
                }
            }

            throw new Exception("Never found");
        }

        public bool find(long target, int index)
        {
            var p25 = numbers.Skip(index - 25).Take(25);

            return 
                p25.SelectMany((v1, i1) => p25.Where((v2, i2) => i1 != i2 && v1 + v2 == target)).Any();
        }
    }
}
