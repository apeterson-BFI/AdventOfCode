using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Adv2020
{
    public class Amplifier
    {
        public IntCode BaseProcessor { get; set; }

        public int Phase { get; set; }

        internal void run(int inp)
        {
            BaseProcessor.baseInit();
            BaseProcessor.Input = new ConcurrentQueue<int>();
            BaseProcessor.Input.Enqueue(Phase);
            BaseProcessor.Input.Enqueue(inp);
            BaseProcessor.process();
        }

        internal void start()
        {
            BaseProcessor.baseInit();
            BaseProcessor.Input = new ConcurrentQueue<int>();
            BaseProcessor.Input.Enqueue(Phase);
            BaseProcessor.process();
        }
    }
}
