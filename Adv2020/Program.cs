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
    }
}
