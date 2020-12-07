using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day30
    {
        public string[] boardingPasses;

        public List<long> seatIDs;

        public Day30()
        {
            boardingPasses = DayInput.readDayLines(30);
            seatIDs = boardingPasses.Select(getSeatID).ToList();
        }

        public long getSeatID(string pass)
        {
            return
                Convert.ToInt64(new string(pass.Select(charID).ToArray()), 2);
        }

        public char charID(char p)
        {
            switch(p)
            {
                case 'F': return '0';
                case 'B': return '1';
                case 'L': return '0';
                case 'R': return '1';
                default: throw new ArgumentException();
            }    
        }

        public long getPart1Answer()
        {
            return seatIDs.Max();
        }

        public long getPart2Answer()
        {
            var h = seatIDs.ToHashSet();

            long max = getPart1Answer();

            for(long l = 1; l < max; l++)
            {
                if (!h.Contains(l) && h.Contains(l - 1) && h.Contains(l + 1))
                    return l;
            }

            throw new ArgumentException();
        }
    }
}
