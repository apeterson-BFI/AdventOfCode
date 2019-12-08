using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Amplifier
    {
        public IntCode BaseProcessor { get; set; }

        public int Phase { get; set; }

        internal void run(int inp)
        {
            BaseProcessor.baseInit();
            BaseProcessor.Input = new List<int>() { Phase, inp };
            BaseProcessor.process();
        }
    }
}
