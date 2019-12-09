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
            chain = new AmplifierChain(DayInput.readDayLinesAsLongCSV(7, ',')[0]);
        }

        public long getPart1Answer()
        {
            long bestAnswer = Int64.MinValue;
            long answer;

            do
            {
                answer = chain.testRun();

                if (answer > bestAnswer)
                    bestAnswer = answer;

            } while (chain.nextPhasePattern());

            return bestAnswer;
        }

        public long getPart2Answer()
        {
            long bestAnswer = Int64.MinValue;
            long answer;

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
