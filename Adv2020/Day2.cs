using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day2
    {
        internal List<int> rom;
        internal List<int> memory;
        internal int index;

        public Day2()
        {
            rom = DayInput.readDayLinesAsIntCSV(2, ',')[0];
            memory = new List<int>(rom);
            index = 0;
        }

        public long getPart1Answer()
        {
            return testLoader(12, 2);
        }

        public long getPart2Answer()
        {
            var tup = goalSeek(19690720);

            return (100 * tup.Item1 + tup.Item2);
        }

        public Tuple<int, int> goalSeek(int target)
        {
            int result;

            for(int noun = 0; noun <= 99; noun++)
            {
                for(int verb = 0; verb <= 99; verb++)
                {
                    result = testLoader(noun, verb);

                    if(result == target)
                    {
                        return new Tuple<int, int>(noun, verb);
                    }
                }
            }

            throw new ArgumentException("Fail");
        }

        public int testLoader(int noun, int verb)
        {
            initialize(noun, verb);

            process();
            return memory[0];
        }

        public void initialize(int noun, int verb)
        {
            index = 0;

            for(int i = 0; i < memory.Count; i++)
            {
                memory[i] = rom[i];
            }

            memory[1] = noun;
            memory[2] = verb;
        }

        public void process()
        {
            int instruction;
            int load1;
            int load2;
            int save;
            int input1;
            int input2;
            int result;

            while(true)
            {
                instruction = memory[index];
                load1 = memory[index + 1];
                load2 = memory[index + 2];
                save = memory[index + 3];

                if(load1 >= memory.Count || load2 >= memory.Count || save >= memory.Count)
                {
                    return;
                }

                input1 = memory[load1];
                input2 = memory[load2];

                switch (instruction)
                {
                    case 1: result = input1 + input2; break;
                    case 2: result = input1 * input2; break;
                    case 99: return;
                    default: return;
                }

                memory[save] = result;
                index += 4;
            }
        }
    }
}