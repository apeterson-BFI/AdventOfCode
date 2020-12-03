using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day28
    {
        public List<List<string>> grid;

        public Day28()
        {
            grid = DayInput.readDayLinesAsCharGrid(28);
        }

        public long getPart1Answer()
        {
            return getSlopeCollides(3, 1);
        }

        public long getSlopeCollides(int xs, int ys)
        {
            int yw = grid.Count;
            int xw = grid[0].Count;

            int x = xs;
            int y = ys;
            long count = 0L;

            while(y < yw)
            {
                if (grid[y][x % xw] == "#")
                    count++;

                x += xs;
                y += ys;
            }

            return count;
        }

        public long getPart2Answer()
        {

            //Right 1, down 1.
            //Right 3, down 1. (This is the slope you already checked.)

            //Right 5, down 1.
            //Right 7, down 1.
            //Right 1, down 2.

            long r1 = getSlopeCollides(1, 1);
            long r2 = getSlopeCollides(3, 1);

            Console.WriteLine("Check P1: {0}", r2);

            long r3 = getSlopeCollides(5, 1);
            long r4 = getSlopeCollides(7, 1);
            long r5 = getSlopeCollides(1, 2);

            return r1 * r2 * r3 * r4 * r5;
        }
    }
}
