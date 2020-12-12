using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day18
    {
        internal string[] lines;
        KeySetLoc target;

        internal int maxy;
        internal int maxx;

        internal int starty;
        internal int startx;

        public Day18()
        {
            lines = DayInput.readDayLines(18, true);
            target = new KeySetLoc() { Locked = "", Keys = "" };
        }

        /*
        public long getPart1Answer()
        {
            maxy = lines.Length;
            maxx = lines[0].Length;

            for(int i = 0; i < maxy; i++)
            {
                for(int j = 0; j < maxy; j++)
                {
                    if(lines[i][j] == '@')
                    {
                        starty = i;
                        startx = j;
                    }
                }
            }

            KeySetLoc start = new KeySetLoc() { Locked = "ABCDEFGHIJKLMNOPQRSTUVWXYZ", Keys = "", R = starty, C = startx };
            return bfs(start);
        }

        /
         * public long bfs(KeySetLoc start)
        {

        }
        */
    }

    public struct KeySetLoc
    {
        public int R { get; set; }

        public int C { get; set; }

        public string Locked { get; set; }

        public string Keys { get; set; }
    }
}
