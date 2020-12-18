using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day43
    {
        public static Regex digitRegex = new Regex(@"\d");
        public static Regex opRegex = new Regex(@"[\+\*]");

        public string[] lines;

        public int index;

        public Day43()
        {
            lines = DayInput.readDayLines(43, true);
            index = 0;
        }

        public long getPart1Answer()
        {
            long linev = 0L;
            long accum = 0L;
            int linen = 0;

            foreach(string line in lines)
            {
                index = 0;

                linev = calcLine(line);
                Console.WriteLine("Line {0} = {1}", linen, linev);
                accum += linev;
                linen++;
            }

            return accum;
        }

        public long getPart2Answer()
        {

        }

        internal long calcLine(string line)
        {
            ReadMode mode = ReadMode.ReadLeft;

            string work;
            string op = "";
            long left = 0L;
            long right = 0L;

            while(index < line.Length && line[index] != ')')
            {
                work = line.Substring(index, 1);

                if(work == " ")
                {
                    index++;
                    continue;
                }

                if (mode == ReadMode.ReadOp && opRegex.IsMatch(work))
                {
                    op = work;
                    index++;
                    mode = ReadMode.ReadRight;
                }
                else if (mode == ReadMode.ReadLeft && digitRegex.IsMatch(work))
                {
                    left = Int64.Parse(work);
                    index++;
                    mode = ReadMode.ReadOp;
                }
                else if (mode == ReadMode.ReadRight && digitRegex.IsMatch(work))
                {
                    right = Int64.Parse(work);

                    if (op == "+")
                    {
                        left = left + right;
                    }
                    else if (op == "*")
                    {
                        left = left * right;
                    }
                    else
                        throw new Exception("Bad parse op");

                    index++;
                    mode = ReadMode.ReadOp;
                }
                else if (mode == ReadMode.ReadLeft && work == "(")
                {
                    index++;
                    left = calcLine(line);
                    mode = ReadMode.ReadOp;
                }
                else if (mode == ReadMode.ReadRight && work == "(")
                {
                    index++;
                    right = calcLine(line);

                    if (op == "+")
                    {
                        left = left + right;
                    }
                    else if (op == "*")
                    {
                        left = left * right;
                    }
                    else
                        throw new Exception("Bad parse op");

                    mode = ReadMode.ReadOp;
                }
                else if (mode == ReadMode.ReadOp && work == "(")
                    throw new Exception("Bad parse op lparen");
                else
                    throw new Exception("Unknown state");
            }

            if(mode == ReadMode.ReadLeft || mode == ReadMode.ReadRight)
            {
                throw new Exception("Bad parse rparen");
            }
            else
            {
                if (index < line.Length && line[index] == ')')
                {
                    index++;
                }

                return left;
            }
        }
    }

    public enum ReadMode
    { 
        ReadLeft,
        ReadOp,
        ReadRight,
    }
}
