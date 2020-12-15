using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day40
    {
        public List<long> input;

        public Dictionary<long, long> firstInputTurns;

        public Dictionary<long, long> secondInputTurns;

        public Day40()
        {
            input = DayInput.readDayLinesAsLongCSV(40, true, ',')[0];
            firstInputTurns = input.Select((l, i) => new KeyValuePair<long, long>(l, (long)i)).ToDictionary(x => x.Key, x => x.Value);
            secondInputTurns = new Dictionary<long, long>();
        }

        public long getPart1Answer()
        {
            long testv;
            long diff;


            for(int turn = input.Count; turn < 2020; turn++)
            {
                testv = input[turn - 1];

                if(secondInputTurns.ContainsKey(testv))
                {
                    diff = secondInputTurns[testv] - firstInputTurns[testv];
                }
                else
                {
                    diff = 0;
                }

                input.Add(diff);

                if(!firstInputTurns.ContainsKey(diff))
                {
                    firstInputTurns.Add(diff, turn);
                }
                else if(!secondInputTurns.ContainsKey(diff))
                {
                    secondInputTurns.Add(diff, turn);
                }
                else
                {
                    firstInputTurns[diff] = secondInputTurns[diff];
                    secondInputTurns[diff] = turn;
                }
            }

            return input[2019];
        }

        public long getPart2Answer()
        {
            long testv;
            long diff;
            long last = input[input.Count - 1];

            for (int turn = input.Count; turn < 30000000; turn++)
            {
                testv = last;

                if (secondInputTurns.ContainsKey(testv))
                {
                    diff = secondInputTurns[testv] - firstInputTurns[testv];
                }
                else
                {
                    diff = 0;
                }

                last = diff;

                if (!firstInputTurns.ContainsKey(diff))
                {
                    firstInputTurns.Add(diff, turn);
                }
                else if (!secondInputTurns.ContainsKey(diff))
                {
                    secondInputTurns.Add(diff, turn);
                }
                else
                {
                    firstInputTurns[diff] = secondInputTurns[diff];
                    secondInputTurns[diff] = turn;
                }
            }

            return last;
        }
    }
}
