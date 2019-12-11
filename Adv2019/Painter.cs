using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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

        public void doReceiverTest(List<int> inputs)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                Receiver.Input.Enqueue(inputs[i]);
            }

            Receiver.process();
        }

        public Tuple<int,List<long>> doPaint()
        {
            Thread processorThread = new Thread(new ThreadStart(Processor.process));
            Thread receiverThread = new Thread(new ThreadStart(Receiver.process));

            processorThread.Start();
            receiverThread.Start();

            while(!Processor.abort)
            {
                Thread.Sleep(20);
            }

            Receiver.Input.Enqueue(99L);

            while(!Receiver.abort)
            {
                Thread.Sleep(20);
            }

            HashSet<long> hashLong = new HashSet<long>();

            for(int i = 1005000; i < Receiver.memory.Count; i++)
            {
                if(!hashLong.Contains(Receiver.memory[i]))
                {
                    hashLong.Add(Receiver.memory[i]);
                }
            }

            int count = hashLong.Count();
            long[] storage = new long[1001000];
            Receiver.memory.CopyTo(1000, storage, 0, 1001000);

            return new Tuple<int, List<long>>(count, storage.ToList());
        }

        public Tuple<int, List<long>> doPaint(long startPaint)
        {
            Receiver.memory[501500] = startPaint;
            return doPaint();
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
            // 910 - paint color
            // 911 - turn command (0 turn left) (1 turn right)
            // 920 - comparison storage
            // 925 - temp storage 1
            // 990 - x curr
            // 991 - y curr
            // 501500 - base start : (-500, -500) maps to 1000, (+500, +500) maps to 1002000

            // 1005000 - paint logger start (x + y * 1000)

            List<long> recRom = new List<long>()
            {
                // Setup
                109,        // [0] adjust relative base: Immediate param 501500
                501500,     // [1] relBase: 501500

                1101,       // [2] add -1 0 : store into 901 (yd).
                -1,         // [3] -1 : first immediate param
                0,          // [4] 0 : second immediate param
                901,        // [5] write location

                // Output content of current location
                204,        // [6] output from relative location
                0,          // [7] relBase + 0

                // Read paint color
                3,          // [8] input to positional location
                910,        // [9] paint color storage location

                // Go to Painter core finished?
                // if read color = 99 then 1 is in 920,
                // if 1 is in 920, jump to Code location 200
                1008,       // [10] equals [pos] [immediate] write to [pos]
                910,        // [11] paint color storage location
                99,         // [12] painter end painting code
                920,        // [13] equals result location
                
                1005,       // [14] jump non-zero [pos] [immediate]
                920,        // [15] equals result location
                86,         // [16] code jump address : go to return point.

                // Read turn command
                3,          // [17] input to positional
                911,        // [18] turn command storage location

                // Paint current x,y location
                21001,      // [19] add [pos arg] [immediate arg] to [rel arg]
                910,        // [20] paint color storage location
                0,          // [21] add 0 (makes add into store)
                0,          // [22] write to relBase + 0

                // Write to logger
                1002,       // [23] mult pos immed pos
                991,        // [24] y location
                1000,       // [25] mult 1000
                925,        // [26] temp location

                1,          // [27] add pos pos pos
                990,        // [28] x location
                925,        // [29] temp location (1000 y)
                1005000,    // [30] current logger location

                // Increment logger location
                1001,       // [31] add pos immed pos
                30,         // [32] code location where logger location can be found
                1,          // [33] add 1
                30,         // [34] store back in 30

                // Update turnX, turnY
                // Jump to turnRight handling, if neccessary (jump nz)
                1005,       // [35] jump non-zero [pos] [immed]
                911,        // [36] turn command storage location
                53,         // [37] turnRight location

                // turnLeft
                // yD = xD, xD = -yD
                // 900 - xD, 901 - yD
                
                // into temp location 925
                1001,       // [38] add pos immed pos
                900,        // [39] from xd
                0,          // [40] add 0
                925,        // [41] to temp (925)
               
                1002,       // [42] mult pos immed pos
                901,        // [43] from yd
                -1,         // [44] mult -1
                900,        // [45] into xd

                1001,       // [46] add pos immed pos
                925,        // [47] from temp
                0,          // [48] add 0
                901,        // [49] into yd

                1105,       // [50] jnz [immed] [immed] jump to after turnRight handling
                1,          // [51] non-zero (guarantee jump)
                65,         // [52] to after turnRight

                // turnRight
                // yD = -xD, xD = yD
                1001,       // [53] add pos immed pos
                901,        // [54] from yd
                0,          // [55] add 0
                925,        // [56] to temp (925)                
                
                1002,       // [57] mult pos immed pos
                900,        // [58] from xd
                -1,         // [59] mult -1
                901,        // [60] into yd

                1001,       // [61] add pos immed pos
                925,        // [62] from temp (yd)
                0,          // [63] add 0
                900,        // [64] into xd

                // Save direction offset
                1002,       // [65] mult [901] 1000 to [902]      : yD * 1000
                901,        // [66] first (positional) arg
                1000,       // [67] second (immediate) arg
                902,        // [68] out location

                1,          // [69] write [900] [902] [903]      : xD + yD * 1000
                900,        // [70] xD
                902,        // [71] yD * 1000
                903,        // [72] offset storage

                9,          // [73] Set relative offset (change location of painting cursor)
                903,        // [74] offset is in 903

                // Update x and y used by logger
                1,          // [75] add pos pos pos
                990,        // [76] x curr
                900,        // [77] xD
                990,        // [78] x curr

                1,          // [79] add pos pos pos
                991,        // [80] y curr
                901,        // [81] yD
                991,        // [82] y curr

                1105,       // [83] jnz immed immed : jump to [6] start of loop
                1,          // [84] non-zero guaranteed jump
                6,          // [85] go-to start of loop.

                99          // [86] return
            };

            return new IntCode(recRom, 1100000);
        }
    }
}
