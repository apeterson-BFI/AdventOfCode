using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day13
    {
        private IntCode intCode;

        public Day13()
        {
            intCode = DayInput.readLinesAsIntCode(13);
        }

        public void getPart2Answer()
        {
            intCode.baseInit();

            intCode.memory[0] = 2;


            for (int i = 0; i < 400; i++)
            {
                intCode.Input.Enqueue(0L);
            }

            intCode.process();            
        }

    }
}
