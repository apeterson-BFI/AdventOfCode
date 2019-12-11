using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Painter
    {
        public IntCode Receiver { get; set; }
        public IntCode Processor { get; set; }


        public Painter(IntCode processor)
        {
            this.Processor = processor;
            this.Receiver = buildReceiver();

            Processor.outputDest = Receiver;
            Processor.inputSource = Receiver;
            Receiver.inputSource = Processor;
            Receiver.outputDest = Processor;
        }

        private IntCode buildReceiver()
        {
            // y - 1000s
            // x - 1s
            // [900 : 999] - working memory
            // 900 - x direction
            // 901 - y direction
            // 902 - y * 1000
            // 903 - x + y * 1000
            // 501500 - base start : (-500, -500) maps to 1000, (+500, +500) maps to 1002000

            List<long> recRom = new List<long>()
            {
                109,        // [0] adjust relative base: Immediate param 501500
                501500,     // [1] relBase: 501500
                1101,       // [2] add -1 0 : store into 901.
                -1,         // [3] -1 : first immediate param
                0,          // [4] 0 : second immediate param
                901,        // [5] write location
                1002,       // [6] mult [901] 1000 to [902]      : calculate dir base
                901,        // [7] first (positional) arg
                1000,       // [8] second (immediate) arg
                902,        // [9] out location
                1,          // [10] write [900] [902] [903]      : write painter offset
                900,        // [11]
                902,        // [12]
                903,        // [13]

            }
        }
    }
}
