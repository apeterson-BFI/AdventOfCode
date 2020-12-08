using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day26
    {
        List<long> vals;

        public Day26()
        {
            vals = DayInput.readDayLinesAsLongs(26, true);
        }

        public long getPart1Answer()
        {
            long checkval = 2020;
            long residue;
            long product;

            for(int i = 0; i < vals.Count; i++)
            {
                if (vals[i] >= checkval)
                    continue;

                residue = checkval - vals[i];
                product = vals[i] * residue;

                if(vals.Where((l,ind) => ind != i && l == residue).Any())
                {
                    Console.WriteLine("{0} + {1} = {2}", vals[i], residue, checkval);
                    Console.WriteLine("{0} * {1} = {2}", vals[i], residue, product);

                    return product;
                }
            }

            Console.WriteLine("No values totally {0} found", checkval);
            throw new ArgumentException();
        }

        public long getPart2Answer()
        {
            long checkval = 2020;
            long val1;
            long val2;
            long val3;
            long residue;
            long product;

            for(int i = 0; i < vals.Count; i++)
            {
                val1 = vals[i];

                for(int j = 0; j < vals.Count; j++)
                {
                    if (i == j)
                        continue;

                    val2 = vals[j];

                    if (val1 + val2 >= checkval)
                        continue;
                    residue = checkval - val1 - val2;
                    product = val1 * val2 * residue;

                    if(vals.Where((l,ind) => ind != i && ind != j && l == residue).Any())
                    {
                        Console.WriteLine("{0} + {1} + {2} = {3}", val1, val2, residue, checkval);
                        Console.WriteLine("{0} * {1} * {2} = {3}", val1, val2, residue, product);
                        return product;
                    }
                }
            }

            Console.WriteLine("No values totally {0} found", checkval);
            throw new ArgumentException();
        }
    }
}
