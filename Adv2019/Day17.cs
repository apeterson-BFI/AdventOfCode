using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day17
    {
        internal IntCode intCode;

        internal int x;
        internal int y;

        internal Random rnm;

        internal List<List<string>> ascii;

        internal long outputVal;

        internal Queue<int> inputBuffer;

        internal List<Tuple<int, int, string>> map;

        public Day17()
        {
            rnm = new Random();
            intCode = DayInput.readLinesAsIntCode(17, 100000);
            ascii = new List<List<string>>();
            ascii.Add(new List<string>());

            inputBuffer = new Queue<int>();

            outputVal = -1;
        }

        public long getPart1Answer()
        {
            // LIVE

            intCode.inputProvider = provideInput;
            intCode.outputSink = receiveOutput;

            intCode.process();

            // TEST
            //ascii = DayInput.readDayLinesAsCharGrid(-17);

            for(int i = ascii.Count -1; i >= 0; i--)
            {
                if (ascii[i].Count == 0)
                    ascii.RemoveAt(i);
            }

            return countIntersections(ascii);
        }

        public long getPart2Answer()
        {
            string mainFunc;
            string aFunc;
            string bFunc;
            string cFunc;

            do
            {
                resetP2();

                Console.WriteLine("Main Function:");
                mainFunc = Console.ReadLine();
                Console.WriteLine("A Function:");
                aFunc = Console.ReadLine();
                Console.WriteLine("B Function:");
                bFunc = Console.ReadLine();
                Console.WriteLine("C Function:");
                cFunc = Console.ReadLine();

                sendStringToAsciiInputBuffer(mainFunc);
                sendStringToAsciiInputBuffer(aFunc);
                sendStringToAsciiInputBuffer(bFunc);
                sendStringToAsciiInputBuffer(cFunc);
                sendStringToAsciiInputBuffer("n");

                intCode.process();
            } while (outputVal == -1);

            return outputVal;
        }

        internal void sendStringToAsciiInputBuffer(string s)
        {
            if(s[s.Length - 1] != '\n')
            {
                s = s + "\n";
            }

            int cval;

            for(int i = 0; i < s.Length; i++)
            {
                cval = Char.ConvertToUtf32(s.Substring(i, 1), 0);
                inputBuffer.Enqueue(cval);
            }
        }

        internal void resetP2()
        {
            intCode = DayInput.readLinesAsIntCode(17, 100000);
            intCode.inputProvider = provideInput;
            intCode.outputSink = receiveOutput;
            intCode.memory[0] = 2;
        }

        public long countIntersections(List<List<string>> map)
        {
            long accum = 0L;

            foreach(var ls in map)
            {
                Console.WriteLine(string.Join("", ls));
            }

            int minY = 0;
            int maxY = map.Count - 1;
            int minX = 0;
            int maxX = map[0].Count - 1;

            for(int y = minY + 1; y < maxY; y++)
            {
                for(int x = minX + 1; x < maxX; x++)
                {
                    if(map[y][x] == "#" && map[y-1][x] == "#" && map[y+1][x] == "#" && map[y][x-1] == "#" && map[y][x+1] == "#")
                    {
                        //Console.WriteLine("{0}*{1}={2}", x, y, x * y);
                        accum += x * y;
                    }
                }
            }

            return accum;
        }

        public long provideInput()
        {
            return inputBuffer.Dequeue();
        }

        public void receiveOutput(long output)
        {
            if(output >= 256)
            {
                outputVal = output;
                return;
            }

            string s;

            if (output == 10)
            {
                ascii.Add(new List<string>());
            }
            else
            {
                s = Char.ConvertFromUtf32((int)output);
                ascii[ascii.Count - 1].Add(s);
            }
        }
    }
}
