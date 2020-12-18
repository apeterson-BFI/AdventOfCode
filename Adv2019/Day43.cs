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

                linev = calcLine1(line);
                accum += linev;
                linen++;
            }

            return accum;
        }

        public long getPart2Answer()
        {
            long linev = 0L;
            long accum = 0L;
            int linen = 0;

            foreach (string line in lines)
            {
                index = 0;

                linev = calcLine2(line);
                accum += linev;
                linen++;
            }

            return accum;
        }

        internal long calcLine1(string line)
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
                    left = calcLine1(line);
                    mode = ReadMode.ReadOp;
                }
                else if (mode == ReadMode.ReadRight && work == "(")
                {
                    index++;
                    right = calcLine1(line);

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

        internal long calcLine2(string line)
        {
            ReadMode2 mode = ReadMode2.ReadLeft;

            string work;
            string op1 = "";
            string op2 = "";
            long left = 0L;
            long mid = 0L;
            long right = 0L;

            while (index < line.Length && line[index] != ')')
            {
                work = line.Substring(index, 1);

                if (work == " ")
                {
                    index++;
                    continue;
                }

                if (mode == ReadMode2.ReadOp1 && opRegex.IsMatch(work))
                {
                    op1 = work;
                    index++;
                    mode = ReadMode2.ReadMid;
                }
                else if(mode == ReadMode2.ReadOp2 && opRegex.IsMatch(work))
                {
                    op2 = work;
                    index++;
                    mode = ReadMode2.ReadRight;
                }
                else if (mode == ReadMode2.ReadLeft && digitRegex.IsMatch(work))
                {
                    left = Int64.Parse(work);
                    index++;
                    mode = ReadMode2.ReadOp1;
                }
                else if (mode == ReadMode2.ReadMid && digitRegex.IsMatch(work))
                {
                    mid = Int64.Parse(work);

                    if (op1 == "+")
                    {
                        left = left + mid;
                        mode = ReadMode2.ReadOp1;
                    }
                    else
                    {
                        mode = ReadMode2.ReadOp2;
                    }

                    index++;
                }
                else if(mode == ReadMode2.ReadRight && digitRegex.IsMatch(work))
                {
                    right = Int64.Parse(work);

                    if (op2 == "+")
                    {
                        mid = mid + right;
                        mode = ReadMode2.ReadOp2;
                    }
                    else
                    {
                        left = left * mid;
                        mid = right;

                        mode = ReadMode2.ReadOp2;
                    }

                    index++;
                }
                else if (mode == ReadMode2.ReadLeft && work == "(")
                {
                    index++;
                    left = calcLine2(line);
                    mode = ReadMode2.ReadOp1;
                }
                else if (mode == ReadMode2.ReadMid && work == "(")
                {
                    index++;
                    mid = calcLine2(line);

                    if (op1 == "+")
                    {
                        left = left + mid;
                        mode = ReadMode2.ReadOp1;
                    }
                    else
                    {
                        mode = ReadMode2.ReadOp2;
                    }
                }
                else if(mode == ReadMode2.ReadRight && work == "(")
                {
                    index++;
                    right = calcLine2(line);

                    if (op2 == "+")
                    {
                        mid = mid + right;
                        mode = ReadMode2.ReadOp2;
                    }
                    else
                    {
                        left = left * mid;
                        mid = right;
                        op1 = op2;

                        mode = ReadMode2.ReadOp2;
                    }
                }
                else
                    throw new Exception("Unknown state");
            }

            if (index < line.Length && line[index] == ')')
            {
                index++;
            }

            if (mode == ReadMode2.ReadOp1)
            {
                return left;
            }
            else if(mode == ReadMode2.ReadOp2)
            {
                if(op1 == "+")
                {
                    left = left + mid;
                    return left;
                }
                else if(op1 == "*")
                {
                    left = left * mid;
                    return left;
                }
                else
                {
                    throw new Exception("Unknown op");
                }
            }
            else
            { 
                throw new Exception("Bad parse rparen");
            }
        }
    }

    public enum ReadMode
    { 
        ReadLeft,
        ReadOp,
        ReadRight,
    }

    public enum ReadMode2
    {
        ReadLeft,
        ReadOp1,
        ReadMid,
        ReadOp2,
        ReadRight
    }
}
