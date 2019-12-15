using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day13
    {
        private static char[] blockDisplay = new char[5] { '-', 'W', 'B', '_', 'o' };

        private IntCode intCode;

        public List<Tuple<long, long, long>> blocks = new List<Tuple<long, long, long>>();

        public long score;

        private int readNext;
        private long x;
        private long y;
        private long blockType;

        private long lastX;

        private bool humanDone;

        public Day13()
        {
            intCode = DayInput.readLinesAsIntCode(13);
            readNext = 0;
            lastX = 22;
            humanDone = false;
        }

        public long getPart2Answer()
        {
            intCode.baseInit();

            intCode.memory[0] = 2;
            intCode.inputProvider = provideInput;
            intCode.outputSink = receiveOutput;

            intCode.process();

            return intCode.getDiagnostic();
        }

        public long provideInput()
        {
            StringBuilder[] codeBlock = new StringBuilder[24];

            for(int i = 0; i < 24; i++)
            {
                codeBlock[i] = new StringBuilder(new string(' ', 45));
            }

            foreach(var tup in blocks)
            {
                codeBlock[(int)tup.Item2][(int)tup.Item1] = blockDisplay[(int)tup.Item3];
            }

            for(int i = 0; i < 24; i++)
            {
                Console.WriteLine(codeBlock[i].ToString());
            }

            Console.WriteLine();
            Console.WriteLine("[SCORE] {0} Left: -1, Stay:0, Right: 1", score);
            //string inp = Console.ReadLine();
            //Console.WriteLine();

            var paddle = blocks.Where(bl => bl.Item3 == 3).Last();
            var ball = blocks.Where(bl => bl.Item3 == 4).Last();

            long ballX = ball.Item1;
            long ballY = ball.Item2;
            long paddleX = paddle.Item1;
            long res;
            long targetX;

            
            targetX = ballX + Math.Sign(ballX - lastX);
            lastX = ballX;

            string inp;

            if (humanDone)
                inp = "";
            else
                inp = Console.ReadLine();

            if(inp == "")
                return (targetX - paddleX);
            else if (inp == "-1" || inp == ",")
                return -1L;
            else if (inp == "1" || inp == ".")
                return 1L;
            else if (inp == "d")
            {
                humanDone = true;
                return (targetX - paddleX);
            }
            else
                return 0L;

        }

        public void receiveOutput(long v)
        {
            if (readNext == 0)
            {
                x = v;
                readNext++;
            }
            else if(readNext == 1)
            {
                y = v;
                readNext++;
            }
            else if(readNext == 2)
            {
                blockType = v;
                readNext = 0;

                if(x == -1 && y == 0)
                {
                    score = blockType;
                    return;
                }
                else
                {
                    blocks.Add(new Tuple<long, long, long>(x, y, blockType));
                }
            }
        }
    }
}
