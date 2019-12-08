using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Adv2020
{
    public class AmplifierChain
    {
        private Amplifier[] Amplifiers;

        private Thread[] conc;

        private int[] phases;

        public AmplifierChain(List<int> rom)
        {
            Amplifiers = new Amplifier[5];
            phases = new int[5];

            for(int i = 0; i < 5; i++)
            {
                phases[i] = i;
                Amplifiers[i] = new Amplifier() { BaseProcessor = new IntCode(rom), Phase = phases[i] };
            }
        }

        public int testRun()
        {
            int outval = 0;

            for (int i = 0; i < Amplifiers.Length; i++)
            {
                Amplifiers[i].Phase = phases[i];
                Amplifiers[i].run(outval);
                outval = Amplifiers[i].BaseProcessor.getDiagnostic();
            }

            return outval;
        }

        public void cycleSetup()
        {
 
            for(int i = 0; i < 5; i++)
            {
                phases[i] = i + 5;
                Amplifiers[i].Phase = phases[i];
                Amplifiers[i].BaseProcessor.inputSource = Amplifiers[((i - 1) % 5 + 5) % 5].BaseProcessor;
                Amplifiers[i].BaseProcessor.outputDest = Amplifiers[(i + 1) % 5].BaseProcessor;
            }
        }

        public int cycleRun()
        {
            conc = new Thread[5];

            for (int i = 0; i < Amplifiers.Length; i++)
            {
                Amplifiers[i].Phase = phases[i];
                conc[i] = new Thread(Amplifiers[i].start);
                conc[i].Start();
            }

            Amplifiers[0].BaseProcessor.Input.Enqueue(0);

            while(!conc.All(c => c.ThreadState == ThreadState.Stopped))
            {
                ;
            }

            return Amplifiers[4].BaseProcessor.getDiagnostic();
        }

        public bool nextPhasePattern()
        {
            do
            {
                for (int index = 4; index >= 0; index--)
                {
                    phases[index] = (phases[index] + 1) % 5;

                    if (phases[index] != 0)
                        break;

                    if(phases.Sum() == 0)
                    {
                        return false;
                    }
                }
            } while (phases.Distinct().Count() != 5);

            return true;
        }

        public bool nextCyclePhasePattern()
        {
            do
            {
                for (int index = 4; index >= 0; index--)
                {
                    if(phases[0] == 5 && phases[1] == 9 && phases[2] == 9 && phases[3] == 9 && phases[4] == 9)
                    {
                        Console.WriteLine("Here");
                    }

                    phases[index]++;

                    if (phases[index] == 10)
                    {
                        phases[index] = 5;

                        if (index == 0)
                            return false;
                    }

                    if (phases[index] != 5)
                        break;
                }
            } while (phases.Distinct().Count() != 5);

            Console.WriteLine("{0}{1}{2}{3}{4}", phases[0], phases[1], phases[2], phases[3], phases[4]);

            return true;
        }
    }
}
