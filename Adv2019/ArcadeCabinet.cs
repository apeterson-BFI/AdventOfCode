using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Adv2020
{
    public partial class ArcadeCabinet : Form
    {
        private IntCode receiver;
        private IntCode processor;

        private List<Tuple<long, long, long>> drawCommands;

        private Graphics g;

        public ArcadeCabinet()
        {
            InitializeComponent();

            processor = DayInput.readLinesAsIntCode(13, 1000000);
            processor.memory[0] = 2;

            // receive X,Y,blockType = one block of specific type
            // receive -1,0,score = score is X

            // determine ball direction and send paddle commands. 
            // Assume ball is going left diagonal or right diagonal so move to be in position
            // 
            // set lastSlope = thisBallX - lastBallX 
            // send input to get position to target paddle on intercept.
            //
            // ignore blocks other than paddle and ball.

            // until we receive the ball in 2 locations, we seek input
            // once we have put our paddle in the right place, go back to seek input mode, until the target location is somewhere else.
            // if y delta is negative, then ignore,
            // if y delta is positive, then calculate intercept and command to reach.
            //0 is an empty tile.No game object appears in this tile.
            //1 is a wall tile.Walls are indestructible barriers.
            //2 is a block tile.Blocks can be broken by the ball.
            //3 is a horizontal paddle tile. The paddle is indestructible.
            //4 is a ball tile.The ball moves diagonally and bounces off objects.

            // [900] : x input, [901] : y input, [902] : cmd input, [903] : eq storage, [904] : lt x, [905] : lt y, [906-909] : tmp1-4 
            // [910] : paddle x, [911] : paddle y, [920] paddle target x
            // [912] : ball x now, [913] : ball y now, [914] : ball x last, [915] : ball y last
            // [916] : ball x delta, [917] : ball y delta,
            // [918] : ball x target, [919] : paddle place?
            // stack pointer: 1000, +3 per input read
            List<long> receiverRom = new List<long>()
            {
                109,                        // [0] Set Heap Pointer to 1000
                1000,
                3,                          // [2] - InputStart:
                900,                        
                3,                          // [4]
                901,
                3,                          // [6]
                902,
                21001,                      // [8] write 900 -> heap[0] : x
                900,
                0,
                0,
                21001,                      // [12] write 901 -> heap[1] : y
                901,
                0,
                1,
                21002,                      // [16] write 902 -> heap[2] : z
                902,
                0,
                2,
                1008,                       // [20] is block a paddle?
                902,
                3,
                903,
                1005,                       // [24] if paddle, go to paddle code
                903,
                37,                         // code index
                1008,                       // [27] {not paddle branch} : is block a ball?
                902,
                4,
                903,                        
                1005,                       // [31] if ball, go to ball code
                903,
                48,                         // code index
                1105,                       // [34] jump always to Input start
                1,
                2,
                1001,                       // [37] {paddle branch} : store paddle x
                900,
                0,
                910,
                1001,                       // [41] store paddle y
                901,
                0,
                911,
                1105,                       // [45] jump always to Input start
                1,
                2,
                1001,                       // [48] move ball now x to ball last x
                912,
                0,
                914,
                1001,                       // [52] move ball now y to ball last y
                913,
                0,
                915,
                1001,                       // [56] move input x to ball now x
                900,
                0,
                912,
                1001,                       // [60] move input y to ball now y
                901,
                0,
                913,
                7,                          // [64] ball now y < ball last y
                913,
                915,
                905,
                1005,                       // [68] jump if now y < last y : that means the ball has hit the paddle and is on the way up.
                905,
                2,
                7,                          // [71] ball now x < ball last x
                912,
                914,
                905,
                1005,                       // [75] jump if ball now x < ball last x
                905,
                9999999,                    // test jump index
                1002,                       // [78] {ball moving right} : tmp1 : ball now y * -1
                913,
                -1,
                906,
                1,                          // [82] : tmp1 := tmp1 + paddle y : 906 holds the number of y tiles between ball and paddle.
                906,
                911,
                906,
                1,                          // [86] : paddle target x = tmp1 + ball now x
                906,
                912,
                920,
                7,                          // [90] : paddle x < paddle target x?
                910,
                920,
                903,
                1005,                       // [94] : jump if x < target
                903,
                9999999,                    // test jump index
                8,                          // [97] : {x >= target} : paddle x = paddle target x?
                910,
                920,
                903,
                1005,                       // [101] : if x = target, we are done, we can go back to input.
                903,
                2,
                                            // [104] : {x > target} : TO DO: increment x until x = target, each time sending output of 1 to processor.
            };

            receiver = new IntCode(new List<long>());

            processor.outputDest = receiver;
            processor.inputSource = receiver;

            drawCommands = new List<Tuple<long, long, long>>();
            g = pnlViewer.CreateGraphics();
        }

        private void pnlViewer_Paint(object sender, PaintEventArgs e)
        {
            foreach (var cmd in drawCommands)
                draw(cmd);
        }

        private void updTimer_Tick(object sender, EventArgs e)
        {
            while (receiver.Input.Count >= 3)
            {
                //0 is an empty tile.No game object appears in this tile.
                //1 is a wall tile.Walls are indestructible barriers.
                //2 is a block tile.Blocks can be broken by the ball.
                //3 is a horizontal paddle tile. The paddle is indestructible.
                //4 is a ball tile.The ball moves diagonally and bounces off objects.

                long x;
                long y;
                long code;
                Tuple<long, long, long> command;

                if (receiver.Input.TryDequeue(out x) && receiver.Input.TryDequeue(out y) && receiver.Input.TryDequeue(out code))
                {
                    command = new Tuple<long, long, long>(x, y, code);
                    drawCommands.Add(command);

                    draw(command);
                }
                else
                {
                    return;
                }
            }
        }

        private void draw(Tuple<long,long,long> command)
        {
            //0 is an empty tile.No game object appears in this tile.
            //1 is a wall tile.Walls are indestructible barriers.
            //2 is a block tile.Blocks can be broken by the ball.
            //3 is a horizontal paddle tile. The paddle is indestructible.
            //4 is a ball tile.The ball moves diagonally and bounces off objects.

            if(command.Item1 == -1 && command.Item2 == 0)
            {
                txtScore.Text = command.Item3.ToString();
                return;
            }

            switch (command.Item3)
            {
                case 0: g.FillRectangle(Brushes.White, command.Item1 * 10, command.Item2 * 10, 10, 10); break;
                case 1: g.FillRectangle(Brushes.Black, command.Item1 * 10, command.Item2 * 10, 10, 10); break;
                case 2: g.FillRectangle(Brushes.Blue, command.Item1 * 10, command.Item2 * 10, 10, 10); break;
                case 3: g.FillRectangle(Brushes.Red, command.Item1 * 10, command.Item2 * 10 + 7, 10, 3); break;
                case 4: g.DrawEllipse(Pens.Green, command.Item1 * 10 + 2, command.Item2 * 10 + 2, 8, 8); break;
            }
        }

        private void ArcadeCabinet_Shown(object sender, EventArgs e)
        {
            updTimer.Start();
        }

        

        private void btnReset_Click(object sender, EventArgs e)
        {
            processor.baseInit();
            processor.memory[0] = 2L;
            drawCommands = new List<Tuple<long, long, long>>();
            received = false;
            paddleDir = 0;
            txtScore.Text = "";
            txtSteps.Text = "5";
        }
    }
}
