using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Adv2020
{
    public class IntCode
    {
        public int MemSize { get; set; }

        internal List<long> rom;
        internal List<long> memory;
        internal int index;
        internal int relBase;
        internal AddressingMode[] addressingModes;
        internal bool abort;
        
        internal IntCode inputSource;
        internal IntCode outputDest;

        public ConcurrentQueue<long> Input { get; set; }

        public List<long> Output { get; set; }

        public IntCode(List<long> rom, int memSize = 65536)
        {
            this.rom = new List<long>(rom);
            this.memory = new List<long>();
            this.MemSize = memSize;

            for(int i = 0; i < MemSize; i++)
            {
                if(i < rom.Count)
                {
                    memory.Add(rom[i]);
                }
                else
                {
                    memory.Add(0L);
                }
            }

            index = 0;
            relBase = 0;
            addressingModes = new AddressingMode[3];
            abort = false;

            Input = new ConcurrentQueue<long>();
            Output = new List<long>();
        }

        public Tuple<long, long> goalSeek(long target)
        {
            long result;

            for (long noun = 0; noun <= 99; noun++)
            {
                for (long verb = 0; verb <= 99; verb++)
                {
                    result = testLoader(noun, verb);

                    if (result == target)
                    {
                        return new Tuple<long, long>(noun, verb);
                    }
                }
            }

            throw new ArgumentException("Fail");
        }

        public long testLoader(long noun, long verb)
        {
            nvInit(noun, verb);

            process();
            return memory[0];
        }

        public void nvInit(long noun, long verb)
        {
            baseInit();

            memory[1] = noun;
            memory[2] = verb;
        }

        public void baseInit()
        {
            index = 0;
            relBase = 0;
            abort = false;

            for (int i = 0; i < MemSize; i++)
            {
                if (i < rom.Count)
                {
                    memory[i] = rom[i];
                }
                else
                {
                    memory[i] = 0L;
                }
            }

            Input = new ConcurrentQueue<long>();
            Output = new List<long>();
        }

        public void process()
        {
            while (!abort)
            {
                doInstruction();
            }
        }

        private void doInstruction()
        {
            long instruction;
            long opcode;

            instruction = memory[index];
            opcode = instruction % 100L;
            setAddressingModes(instruction);

            switch (opcode)
            {
                case 1: addOp(); break;
                case 2: multOp(); break;
                case 3: inputOp(); break;
                case 4: outputOp(); break;
                case 5: jumpNZOp(); break;
                case 6: jumpZOp(); break;
                case 7: lessThanOp(); break;
                case 8: equalsOp(); break;
                case 9: adjustRelBase(); break;
                case 99: abort = true;  return;
            }
        }

        public long getDiagnostic()
        {
            if (Output.Count == 0)
                throw new ArgumentException();

            return Output[Output.Count - 1];
        }

        private void addOp()
        {
            long param1 = loadParameter(1);
            long param2 = loadParameter(2);

            long result = param1 + param2;
            writeParameter(3, result);

            index += 4;
        }

        private void multOp()
        {
            long param1 = loadParameter(1);
            long param2 = loadParameter(2);

            long result = param1 * param2;
            writeParameter(3, result);

            index += 4;
        }

        private void inputOp()
        {
            long inp;

            while(!Input.TryDequeue(out inp))
            {
                if(inputSource == null)
                {
                    abort = true;
                    return;
                }
                else
                {
                    Thread.Sleep(0);
                }
            }

            writeParameter(1, inp);
            index += 2;
        }

        private void outputOp()
        {
            long param1 = loadParameter(1);

            Output.Add(param1);
            //Console.WriteLine(param1);

            if(outputDest != null)
            {
                outputDest.Input.Enqueue(param1);
            }

            index += 2;
        }

        private void jumpNZOp()
        {
            long param1 = loadParameter(1);
            long param2 = loadParameter(2);

            if (param1 != 0)
            {
                index = (int)param2;
            }
            else
            {
                index += 3;
            }
        }

        private void jumpZOp()
        {
            long param1 = loadParameter(1);
            long param2 = loadParameter(2);

            if (param1 == 0)
            {
                index = (int)param2;
            }
            else
            {
                index += 3;
            }
        }

        private void lessThanOp()
        {
            long param1 = loadParameter(1);
            long param2 = loadParameter(2);

            writeParameter(3, (param1 < param2 ? 1 : 0));
            index += 4;
        }

        private void equalsOp()
        {
            long param1 = loadParameter(1);
            long param2 = loadParameter(2);

            writeParameter(3, (param1 == param2 ? 1 : 0));
            index += 4;
        }

        private void adjustRelBase()
        {
            long param1 = loadParameter(1);

            relBase += (int)param1;
            index += 2;
        }

        private long loadParameter(int index)
        {
            switch (addressingModes[index - 1])
            {
                case AddressingMode.Immediate: return loadImmediate(index);
                case AddressingMode.Position: return loadPositional(index);
                case AddressingMode.Relative: return loadRelative(index);
                default: throw new ArgumentException();
            }
        }

        private void writeParameter(int index, long value)
        {
            switch (addressingModes[index - 1])
            {
                case AddressingMode.Immediate: throw new ArgumentException("Cannot write immediate");
                case AddressingMode.Position: writePositional(index, value); break;
                case AddressingMode.Relative: writeRelative(index, value); break;
                default: throw new ArgumentException();
            }
        }

        private long loadPositional(int position)
        {
            if (index + position >= memory.Count)
            {
                abort = true;
                return 0;
            }

            long load = memory[index + position];

            if (load >= memory.Count)
            {
                abort = true;
                return 0;
            }

            long input = memory[(int)load];
            return input;
        }

        private long loadRelative(int position)
        {
            if (index + position >= memory.Count)
            {
                abort = true;
                return 0;
            }

            long load = memory[index + position];

            if (load + relBase >= memory.Count)
            {
                abort = true;
                return 0;
            }

            long input = memory[(int)(load) + relBase];
            return input;
        }

        private long loadImmediate(int position)
        {
            if (index + position >= memory.Count)
            {
                abort = true;
                return 0;
            }

            long input = memory[index + position];
            return input;
        }

        private void writePositional(int position, long value)
        {
            if (index + position >= memory.Count)
            {
                abort = true;
                return;
            }

            long save = memory[index + position];

            if (save >= memory.Count)
            {
                abort = true;
                return;
            }

            memory[(int)save] = value;
        }

        private void writeRelative(int position, long value)
        {
            if (index + position >= memory.Count)
            {
                abort = true;
                return;
            }

            long save = memory[index + position];

            if (save + relBase >= memory.Count)
            {
                abort = true;
                return;
            }

            memory[(int)save + relBase] = value;
        }

        private void setAddressingModes(long instruction)
        {
            int[] ivals = new int[3];

            for(int i = 0; i < 3; i++)
            {
                ivals[i] = NumTextUtil.extractPlaceLong(instruction, i + 2);
            }

            for(int i = 0; i < 3; i++)
            {
                switch (ivals[i])
                {
                    case 0: addressingModes[i] = AddressingMode.Position; break;
                    case 1: addressingModes[i] = AddressingMode.Immediate; break;
                    case 2: addressingModes[i] = AddressingMode.Relative; break;
                }
            }
        }
    }

    public enum AddressingMode
    {
        Position,
        Immediate,
        Relative
    }
}
