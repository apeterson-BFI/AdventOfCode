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

        // -1 unknown
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

        internal int tx;
        internal int ty;

        internal Random rnm;

        public Stack<Tuple<int, int>> pastLocations;

        public Day15()
        {
            pastLocations = new Stack<Tuple<int, int>>();

            rnm = new Random();
            intCode = DayInput.readLinesAsIntCode(15, true, 100000);
            map = new int[200, 200];
            
            for(int i = 0; i < 200; i++)
            {
                for(int j = 0; j < 200; j++)
                {
                    map[i, j] = -1;
                }
            }

            x = 100;
            y = 100;

            dir = 2;
            dx = 0;
            dy = -1;

            tx = x + dx;
            ty = y + dy;
        }

        public long getPart1Answer()
        {
            // label repair droid start
            map[x, y] = 3;

            intCode.inputProvider = provideInput;
            intCode.outputSink = receiveOutput;

            intCode.process();

            return 404L;
        }

        public long getPart2Answer()
        {
            // label repair droid start
            map[x, y] = 3;

            intCode.inputProvider = provideInput;
            intCode.outputSink = receiveOutput;

            intCode.process();

            int count = 0;
            int v;

            int steps = 0;

            map[100, 100] = 1;
            map[foundx, foundy] = 5;

            do
            {
                count = 0;

                for (int i = 0; i < 200; i++)
                {
                    for (int j = 0; j < 200; j++)
                    {
                        v = map[i, j];

                        if (v == -1 || v == 0)
                            continue;

                        if (i > 0 && map[i - 1, j] == 5)
                        {
                            map[i, j] = 6;
                        }
                        else if (i < 199 && map[i + 1, j] == 5)
                        {
                            map[i, j] = 6;
                        }
                        else if (j > 0 && map[i, j - 1] == 5)
                        {
                            map[i, j] = 6;
                        }
                        else if (j < 199 && map[i, j + 1] == 5)
                        {
                            map[i, j] = 6;
                        }
                        else if (map[i, j] == 1)
                        {
                            count++;
                        }
                    }
                }

                for(int i = 0; i < 200; i++)
                {
                    for(int j = 0; j < 200; j++)
                    {
                        if (map[i, j] == 6)
                            map[i, j] = 5;
                    }
                }

                steps++;
            } while (count > 0);

            return steps;
        }

        // allow imperative call for input / output into intcode
        // then  bfs easy


        public long provideInput()
        {
            setDeltas();
            tx = x + dx;
            ty = y + dy;

            return dir;
        }

        public void receiveOutput(long output)
        {
            map[tx, ty] = (int)output;

            x = (output == 0 ? x : tx);
            y = (output == 0 ? y : ty);
            
            Console.WriteLine("[{0},{1}] {2}", x, y, output);
        }

        private void nextDir()
        {
            switch (dir)
            {
                case 1:
                    dir = 3; break;
                case 3:
                    dir = 2; break;
                case 2:
                    dir = 4; break;
                case 4:
                    dir = 1; break;
            }
        }

        private void setDeltas()
        {
            switch (dir)
            {
                case 1: dx = 0; dy = -1; break;
                case 2: dx = 0; dy = 1; break;
                case 3: dx = -1; dy = 0; break;
                case 4: dx = 1; dy = 0; break;
            }
        }

    }
}
