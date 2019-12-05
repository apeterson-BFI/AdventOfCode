using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    class Program
    {
        static void Main(string[] args)
        {
            day5Display();
            Console.ReadLine();
        }

        private static void day1Display()
        {
            Day1 d1 = new Day1();
            long answer = d1.getPart1Answer();

            Console.WriteLine(answer);

            long testp2 = d1.getRecursiveFuelCost(1969L);
            Console.WriteLine(testp2);

            answer = d1.getPart2Answer();
            Console.WriteLine(answer);
        }

        private static void day2Display()
        {
            Day2 d2 = new Day2();
            long answer = d2.getPart1Answer();

            Console.WriteLine("P1 Answer: {0}", answer);

            answer = d2.getPart2Answer();
            Console.WriteLine("P2 Answer: {0}", answer);
        }

        private static void day3Display()
        {
            Day3 d3 = new Day3();
            int answer = d3.getPart1Answer();

            Console.WriteLine("P1 Answer: {0}", answer);

            answer = d3.getPart2Answer();
            Console.WriteLine("P2 Answer: {0}", answer);
        }

        private static void day4Display()
        {
            Day4 d4 = new Day4();
            long answer = d4.getPart1Answer();

            Console.WriteLine("111111 : exp: true, act: {0}", d4.isPart1Valid(111111L));
            Console.WriteLine("223450 : exp: false, act: {0}", d4.isPart1Valid(223450L));
            Console.WriteLine("123789 : exp: false, act: {0}", d4.isPart1Valid(123789L));

            Console.WriteLine("P1 Answer: {0}", answer);

            answer = d4.getPart2Answer();

            Console.WriteLine("P2 Answer: {0}", answer);
        }

        private static void day5Display()
        {
            Day5 d5p1 = new Day5();
            int answer = d5p1.getPart1Answer();

            Console.WriteLine("P1 Answer: {0}", answer);

            Day5 d5p2 = new Day5();
            answer = d5p2.getPart2Answer();

            Console.WriteLine("P2 Answer: {0}", answer);
        }
    }
}
