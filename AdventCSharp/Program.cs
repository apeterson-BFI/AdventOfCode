using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventCSharp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // 424 players; last marble is worth 7114400 points
            long bestScore = CircularLinkedList.playGame(424, 7114400L);
            Console.WriteLine(bestScore);

            // 10 players; last marble is worth 1618 points
            long bestScoreTest = CircularLinkedList.playGame(10, 1618L);

            Console.WriteLine(bestScoreTest);
        }
    }
}