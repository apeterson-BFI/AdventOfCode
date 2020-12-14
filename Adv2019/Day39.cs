using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Adv2020
{
    public class Day39
    {
        public static Regex memRegex = new Regex(@"mem\[(\d+)\] = (\d+)");

        public long andmask;       // 0 only with explicit 0
        public long ormask;        // 1 only with explicit 1
        public string scrammask;

        public Dictionary<long, long> memory;

        public string[] lines;

        public Day39()
        {
            memory = new Dictionary<long, long>();
            lines = DayInput.readDayLines(39, true);
            
        }

        public void setup1()
        {
            long index;
            long val;
            Match m;

            foreach(string line in lines)
            {
                if(line.StartsWith("mask = "))
                {
                    readMask(line.Substring(7));
                }
                else
                {
                    m = memRegex.Match(line);
                    index = Int64.Parse(m.Groups[1].Value);
                    val = Int64.Parse(m.Groups[2].Value);

                    readMemory(index, val);
                }
            }
        }

        public void readMask(string line)
        {
            string ands = line.Replace('X', '1');
            string ors = line.Replace('X', '0');

            andmask = Convert.ToInt64(ands, 2);
            ormask = Convert.ToInt64(ors, 2);
        }

        public void readMemory(long index, long val)
        {
            long res = val & andmask;
            res = res | ormask;

            if(memory.ContainsKey(index))
            {
                memory[index] = res;
            }
            else
            {
                memory.Add(index, res);
            }
        }

        public void setup2()
        {
            long index;
            long val;
            Match m;

            foreach (string line in lines)
            {
                if (line.StartsWith("mask = "))
                {
                    readMask2(line.Substring(7));
                }
                else
                {
                    m = memRegex.Match(line);
                    index = Int64.Parse(m.Groups[1].Value);
                    val = Int64.Parse(m.Groups[2].Value);

                    readMemory2(index, val);
                }
            }
        }

        public void readMask2(string line)
        {
            scrammask = line;
        }

        public void readMemory2(long index, long val)
        {
            string ors = scrammask.Replace('X', '0');
            ormask = Convert.ToInt64(ors, 2);

            index |= ormask;

            readMemRec(0, scrammask, index, val);
        }

        public void readMemRec(int readIndex, string mask, long index, long val)
        {
            if(readIndex >= mask.Length)
            {
                if (memory.ContainsKey(index))
                {
                    memory[index] = val;
                }
                else
                {
                    memory.Add(index, val);
                }
            }
            else if(mask[readIndex] == 'X')
            {
                long index1 = index & ~(0x1L << (35 - readIndex));
                long index2 = index | (0x1L << (35 - readIndex));

                readMemRec(readIndex + 1, mask, index1, val);
                readMemRec(readIndex + 1, mask, index2, val);
            }
            else
            {
                readMemRec(readIndex + 1, mask, index, val);
            }
        }

        public long getPart1Answer()
        {
            setup1();

            return memory.Sum(x => x.Value);
        }

        public long getPart2Answer()
        {
            setup2();

            return memory.Sum(x => x.Value);
        }
    }
}
