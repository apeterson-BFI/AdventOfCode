using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

        public void doPart1Answers()
        {
            Tuple<int, List<long>> results = Painter.doPaint();

            Console.WriteLine("Lines written: {0}", results.Item1);

            List<long> mem = results.Item2;
            int index = 0;
            long[] row = new long[1000];
            string line;

            using(var sw = File.CreateText("output11.csv"))
            {
                while (index + 1000 <= mem.Count)
                {
                    mem.CopyTo(index, row, 0, 1000);
                    line = string.Join(",", row);
                    sw.WriteLine(line);

                    index += 1000;
                }
            }
        }

        public void doPart2Answers()
        {
            Tuple<int, List<long>> results = Painter.doPaint(1L);

            Console.WriteLine("Lines written: {0}", results.Item1);

            List<long> mem = results.Item2;
            int index = 0;
            long[] row = new long[1000];
            string line;

            using (var sw = File.CreateText("output11.csv"))
            {
                while (index + 1000 <= mem.Count)
                {
                    mem.CopyTo(index, row, 0, 1000);
                    line = string.Join(",", row);
                    sw.WriteLine(line);

                    index += 1000;
                }
            }
        }

        public void testReceiver()
        {
            List<int> inputs = new List<int>() { 1, 0, 1, 0, 1, 1, 0, 1 };
            Painter.doReceiverTest(inputs);
        }
    }
}
