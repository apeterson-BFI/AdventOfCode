using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day4
    {
        public long getPart1Answer()
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
        }
    }
}
