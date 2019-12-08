using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class AmplifierChain
    {
        private Amplifier[] Amplifiers;

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
                Amplifiers[i].Phase = i + 5;
                Amplifiers[i].BaseProcessor.inputSource = Amplifiers[((i - 1) % 5 + 5) % 5].BaseProcessor;
                Amplifiers[i].BaseProcessor.outputDest = Amplifiers[(i + 1) % 5].BaseProcessor;
            }
        }

        public int cycleRun()
        {
            for(int i = 0; i < Amplifiers.Length; i++)
            {
                Amplifiers[i].Phase = phases[i];
                Amplifiers[i].BaseProcessor.baseInit();
            }

            int ampIndex = 0;

            do
            {
                Amplifiers[ampIndex].BaseProcessor.abort = false;
                Amplifiers[ampIndex].run(ampIndex);
                ampIndex = (ampIndex + 1) % 5;
            } while (Amplifiers[4].BaseProcessor.Output.Count == 0);

            return (Amplifiers[4].BaseProcessor.getDiagnostic());
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
                    phases[index]++;

                    if (phases[index] == 10)
                        phases[index] = 0;

                    if (phases[index] != 0)
                        break;

                    if (phases.Sum() == 0)
                    {
                        return false;
                    }
                }
            } while (phases.Distinct().Count() != 5);

            return true;
        }
    }
}
