using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day11
    {
        private Painter Painter;

        public Day11()
        {
            var intCode = DayInput.readLinesAsIntCode(11, 1000000);

            Painter = new Painter(intCode);
        }
    }
}
