using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class IntCode
    {
        internal List<int> rom;
        internal List<int> memory;
        internal int index;
        internal AddressingMode[] addressingModes;
        internal bool abort;

        public List<int> Input { get; set; }

        public List<int> Output { get; set; }

        public IntCode(List<int> rom)
        {
            this.rom = new List<int>(rom);
            this.memory = new List<int>(rom);
            index = 0;
            addressingModes = new AddressingMode[3];
            abort = false;

            Input = new List<int>();
            Output = new List<int>();
        }

        public Tuple<int, int> goalSeek(int target)
        {
            int result;

            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    result = testLoader(noun, verb);

                    if (result == target)
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
            abort = false;

            for (int i = 0; i < memory.Count; i++)
            {
                memory[i] = rom[i];
            }

            memory[1] = noun;
            memory[2] = verb;
            Input = new List<int>();
            Output = new List<int>();
        }

        public void process()
        {
            int instruction;
            int opcode;
            
            while (!abort)
            {
                instruction = memory[index];
                opcode = instruction % 100;
                setAddressingModes(instruction);

                switch(opcode)
                {
                    case 1: addOp(); break;
                    case 2: multOp(); break;
                    case 3: inputOp(); break;
                    case 4: outputOp(); break;
                    case 5: jumpNZOp(); break;
                    case 6: jumpZOp(); break;
                    case 7: lessThanOp(); break;
                    case 8: equalsOp(); break;
                    case 99: return;
                }
            }
        }

        public int getDiagnostic()
        {
            if (Output.Count == 0)
                throw new ArgumentException();

            return Output[Output.Count - 1];
        }

        private void addOp()
        {
            int param1 = (addressingModes[0] == AddressingMode.Position ? loadPositional(1) : loadImmediate(1));
            int param2 = (addressingModes[1] == AddressingMode.Position ? loadPositional(2) : loadImmediate(2));

            int result = param1 + param2;
            writePositional(3, result);

            index += 4;
        }

        private void multOp()
        {
            int param1 = (addressingModes[0] == AddressingMode.Position ? loadPositional(1) : loadImmediate(1));
            int param2 = (addressingModes[1] == AddressingMode.Position ? loadPositional(2) : loadImmediate(2));

            int result = param1 * param2;
            writePositional(3, result);

            index += 4;
        }

        private void inputOp()
        {
            if (Input.Count == 0)
            {
                abort = true;
                return;
            }

            int inp = Input[0];
            Input.RemoveAt(0);

            writePositional(1, inp);
            index += 2;
        }

        private void outputOp()
        {
            int param1 = (addressingModes[0] == AddressingMode.Position ? loadPositional(1) : loadImmediate(1));

            Output.Add(param1);
            index += 2;
        }

        private void jumpNZOp()
        {
            int param1 = (addressingModes[0] == AddressingMode.Position ? loadPositional(1) : loadImmediate(1));
            int param2 = (addressingModes[1] == AddressingMode.Position ? loadPositional(2) : loadImmediate(2));

            if(param1 != 0)
            {
                index = param2;
            }
            else
            {
                index += 3;
            }
        }

        private void jumpZOp()
        {
            int param1 = (addressingModes[0] == AddressingMode.Position ? loadPositional(1) : loadImmediate(1));
            int param2 = (addressingModes[1] == AddressingMode.Position ? loadPositional(2) : loadImmediate(2));

            if (param1 == 0)
            {
                index = param2;
            }
            else
            {
                index += 3;
            }
        }

        private void lessThanOp()
        {
            int param1 = (addressingModes[0] == AddressingMode.Position ? loadPositional(1) : loadImmediate(1));
            int param2 = (addressingModes[1] == AddressingMode.Position ? loadPositional(2) : loadImmediate(2));

            writePositional(3, (param1 < param2 ? 1 : 0));
            index += 4;
        }

        private void equalsOp()
        {
            int param1 = (addressingModes[0] == AddressingMode.Position ? loadPositional(1) : loadImmediate(1));
            int param2 = (addressingModes[1] == AddressingMode.Position ? loadPositional(2) : loadImmediate(2));

            writePositional(3, (param1 == param2 ? 1 : 0));
            index += 4;
        }

        private int loadPositional(int position)
        {
            if (index + position >= memory.Count)
            {
                abort = true;
                return 0;
            }

            int load = memory[index + position];

            if (load >= memory.Count)
            {
                abort = true;
                return 0;
            }

            int input = memory[load];
            return input;
        }

        private int loadImmediate(int position)
        {
            if (index + position >= memory.Count)
            {
                abort = true;
                return 0;
            }

            int input = memory[index + position];
            return input;
        }

        private void writePositional(int position, int value)
        {
            if (index + position >= memory.Count)
            {
                abort = true;
                return;
            }

            int save = memory[index + position];

            if (save >= memory.Count)
            {
                abort = true;
                return;
            }

            memory[save] = value;
        }

        private void setAddressingModes(int instruction)
        {
            int[] ivals = new int[3];

            for(int i = 0; i < 3; i++)
            {
                ivals[i] = NumTextUtil.extractPlace(instruction, i + 2);
            }

            for(int i = 0; i < 3; i++)
            {
                addressingModes[i] = (ivals[i] == 0 ? AddressingMode.Position : AddressingMode.Immediate);
            }
        }
    }

    public enum AddressingMode
    {
        Position,
        Immediate
    }
}
