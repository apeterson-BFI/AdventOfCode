using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day7
    {
        private AmplifierChain chain;

        public Day7()
        {
            chain = new AmplifierChain(DayInput.readDayLinesAsIntCSV(7, ',')[0]);
        }

        public int getPart1Answer()
        {
            int bestAnswer = Int32.MinValue;
            int answer;

            do
            {
                answer = chain.testRun();

                if (answer > bestAnswer)
                    bestAnswer = answer;

            } while (chain.nextPhasePattern());

            return bestAnswer;
        }

        public int getPart2Answer()
        {
            int bestAnswer = Int32.MinValue;
            int answer;

            chain.cycleSetup();

            do
            {
                answer = chain.cycleRun();

                if (answer > bestAnswer)
                    bestAnswer = answer;

            } while (chain.nextCyclePhasePattern());

            return bestAnswer;
        }
    }
}
