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
            day10display();
            Console.ReadLine();
        }

        #region Old Days

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
            long answer = d5p1.getPart1Answer();

            Console.WriteLine("P1 Answer: {0}", answer);

            Day5 d5p2 = new Day5();
            answer = d5p2.getPart2Answer();

            Console.WriteLine("P2 Answer: {0}", answer);
        }

        private static void day7Display()
        {
            //Day7 d7p1 = new Day7();
            //int answer = d7p1.getPart1Answer();

            //Console.WriteLine("P1 Answer: {0}", answer);

            Day7 d7p2 = new Day7();
            long answer = d7p2.getPart2Answer();

            Console.WriteLine("P2 Answer: {0}", answer);
        }

        private static void day8Display()
        {
            Day8 d8p1 = new Day8(width: 25, height: 6);
            int answer = d8p1.getPart1Answer();

            Console.WriteLine("P1 Answer: {0}", answer);

            d8p1.getPart2Answer();
        }

        private static void day9display()
        {
            Day9 d9p1 = new Day9();
            long answer = d9p1.getPart1Answer();

            Console.WriteLine("P1 Answer: {0}", answer);

            Day9 d9p2 = new Day9();
            answer = d9p2.getPart2Answer();

            Console.WriteLine("P2 Answer: {0}", answer);
        }

        #endregion

        public static void day10display()
        {
            Day10 d10p1 = new Day10();
            int answer = d10p1.getPart1Answer();

            Console.WriteLine("Day 10 P1: {0}", answer);

            answer = d10p1.getPart2Answer();
            Console.WriteLine("Day 10 P2: {0}", answer);

            Console.WriteLine("Ren Angle: -1, 10: {0}", Day10.renormalizeAngle(Math.Atan2(10, -1)));
            Console.WriteLine("Ren Angle: 1, -1: {0}", Day10.renormalizeAngle(Math.Atan2(-1, 1)));
            Console.WriteLine("Ren Angle, 0, 1: {0}", Day10.renormalizeAngle(Math.Atan2(1, 0)));
        }
    }
}
