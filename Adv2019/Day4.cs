using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2019
{
    public class Day4
    {
        public long getPart1Answer()
        {
            return (search(isPart1Valid));
        }

        public long getPart2Answer()
        {
            return (search(isPart2Valid));
        }

        private long search(Func<long, bool> validation)
        {
            string line = DayInput.readDayLines(4)[0];

            List<long> bounds = line.Split('-').Select(v => Int64.Parse(v)).ToList();

            // How many numbers between LB and UP have this property
            // Left to Right, The digits never decrease
            // There is at least one pair of the same digits.

            // Ruling out candidates: if left is greater than right, no go.
            // left = right : good candidate
            // 
            // six digit numbers
            //

            int count = 0;
            long curr = bounds[0];
            long limit = bounds[1];

            while (curr <= limit)
            {
                if (validation(curr))
                {
                    count++;
                }

                //curr++;

                curr = getNextSemiValid(curr);
            }

            return count;
        }


        private long getNextSemiValid(long prev)
        {
            long adv = advCompletion(prev);

            if(prev == adv)
            {
                adv++;
                return adv;
            }
            else
            {
                return adv;
            }
        }

        private long advCompletion(long prev)
        {
            char[] digits = toChars(prev);

            long current = prev;
            long completion;

            for (int i = 0; i < digits.Length; i++)
            {
                completion = makeMinimalCompletion(digits, i);

                if (completion > current)
                {
                    digits = completion.ToString().ToCharArray();
                    current = completion;
                }
            }

            return current;
        }

        private long makeMinimalCompletion(char[] digits, int startIndex)
        {
            if(startIndex == 0)
            {
                return 0L;
            }

            char[] digNew = new char[6];

            for(int i = 0; i < 6; i++)
            {
                digNew[i] = digits[i];
            }

            char fillDigit = digits[startIndex - 1];

            for(int i = startIndex; i < 6; i++)
            {
                digNew[i] = fillDigit;
            }

            return fromChars(digNew);
        }

        private long fromChars(char[] digits)
        {
            string s = new string(digits);

            long l = Int64.Parse(s);
            return l;
        }

        private char[] toChars(long l)
        {
            return l.ToString().ToCharArray();
        }

        internal bool isPart1Valid(long l)
        {
            return sorted(l) && dup(l);
        }

        private bool isPart2Valid(long l)
        {
            return sorted(l) && twoset(l);
        }

        private bool sorted(long l)
        {
            char[] digits = toChars(l);

            List<char> d = digits.ToList();
            d.Sort();

            long sv = fromChars(d.ToArray());

            return sv == l;
        }

        private bool dup(long l)
        {
            var lchars = toChars(l).AsEnumerable();
            var dchars = lchars.Distinct();

            return (dchars.Count() < lchars.Count());
        }

        private bool twoset(long l)
        {
            var lchars = toChars(l).AsEnumerable();

            return (lchars.GroupBy(c => c).Any(cg => cg.Count() == 2));
        }
    }
}
