using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day15
    {
        internal IntCode intCode;

        // 0 wall
        // 1 open
        // 2 oxygen
        // 3 repair droid
        internal int[,] map;

        internal int x;
        internal int y;

        internal int foundx;
        internal int foundy;

        // 1 north, 2 south, 3 west, 4 east
        internal long dir;

        internal int dx;
        internal int dy;

        internal Random rnm;

        public Day15()
        {
            rnm = new Random();
            intCode = DayInput.readLinesAsIntCode(15, 100000);
            map = new int[200, 200];
            x = 100;
            y = 100;

            dir = 1;
            dx = 0;
            dy = 1;
        }

        public long getPart1Answer()
        {
            // label repair droid start
            map[x, y] = 3;

            intCode.inputProvider = provideInput;
            intCode.outputSink = receiveOutput;

            intCode.process();

            return search(100, 100, 0, 2, new HashSet<int>());
        }

        public long getPart2Answer()
        {
            // find entire map full of locations
            // and then do oxygen spread of entire map

            // avoid 114, 114, which is the oxygen, may end the processor.

            // do a depth first search - except pausable because we are running intcode.
            // in DFS:
            // 
            // our current level
            // where we were in a past levels
        }

        public int search(int x, int y, int taken, int goalValue, HashSet<int> visited)
        {
            visited.Add(x + y * 200);

            int ns = Int32.MaxValue;
            int es = Int32.MaxValue;
            int ss = Int32.MaxValue;
            int ws = Int32.MaxValue;

            int nx = x;
            int ny = y - 1;

            if(y - 1 >= 0 && !visited.Contains(nx + ny * 200) && map[nx, ny] != 0)
            {
                if(map[nx, ny] == 1)
                {
                    ns = search(nx, ny, taken + 1, goalValue, new HashSet<int>(visited));
                }
                else if(map[nx, ny] == 2)
                {
                    return taken + 1;
                }
            }

            int ex = x + 1;
            int ey = y;

            if(x + 1 < 200 && !visited.Contains(ex + ey * 200) && map[ex, ey] != 0)
            {
                if (map[ex, ey] == 1)
                {
                    es = search(ex, ey, taken + 1, goalValue, new HashSet<int>(visited));
                }
                else if (map[ex, ey] == 2)
                {
                    return taken + 1;
                }
            }

            int sx = x;
            int sy = y + 1;

            if (y + 1 < 200 && !visited.Contains(sx + sy * 200) && map[sx, sy] != 0)
            {
                if (map[sx, sy] == 1)
                {
                    ss = search(sx, sy, taken + 1, goalValue, new HashSet<int>(visited));
                }
                else if (map[sx, sy] == 2)
                {
                    return taken + 1;
                }
            }

            int wx = x - 1;
            int wy = y;

            if (x - 1 >= 0 && !visited.Contains(wx + wy * 200) && map[wx, wy] != 0)
            {
                if (map[wx, wy] == 1)
                {
                    ws = search(wx, wy, taken + 1, goalValue, new HashSet<int>(visited));
                }
                else if (map[wx, wy] == 2)
                {
                    return taken + 1;
                }
            }

            return Math.Min(ns, Math.Min(es, Math.Min(ss, ws)));
        }

        public long provideInput()
        {
            setDirection();
            return dir;
        }

        public void receiveOutput(long output)
        {
            if(output == 0)
            {
                map[x + dx, y + dy] = 0;
            }
            else             
            {
                if (output == 2)
                {
                    intCode.abort = true;

                    foundx = x + dx;
                    foundy = y + dy;
                    Console.WriteLine("Found it");
                }

                x += dx;
                y += dy;

                map[x, y] = (int)output;
            }
        }

        private void setDirection()
        {
            dir = rnm.Next(1, 5);

            switch(dir)
            {
                case 1: dx = 0; dy = -1; break;
                case 2: dx = 0; dy = 1; break;
                case 3: dx = -1; dy = 0; break;
                case 4: dx = 1; dy = 0; break;
            }
        }
    }
}
