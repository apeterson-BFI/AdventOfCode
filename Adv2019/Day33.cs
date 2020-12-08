using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day33
    {
        public List<Tuple<CommandType, int>> cmds;

        public int ip;
        public int ac;

        public Day33()
        {
            string[] lines = DayInput.readDayLines(33, true);

            string cmd;
            int num;

            cmds = new List<Tuple<CommandType, int>>();

            foreach(string line in lines)
            {
                cmd = line.Substring(0, 3);
                num = Int32.Parse(line.Substring(4));

                cmds.Add(new Tuple<CommandType, int>((CommandType)Enum.Parse(typeof(CommandType), cmd), num));
            }

            ip = 0;
            ac = 0;
        }

        public bool step()
        {
            var tup = cmds[ip];

            switch (tup.Item1)
            {
                case CommandType.acc: ac += tup.Item2; ip++; break;
                case CommandType.jmp: ip += tup.Item2; break;
                case CommandType.nop: ip++; break;
            }

            if(ip == cmds.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int getPart1Answer()
        {
            HashSet<int> reachedIPs = new HashSet<int>();

            while(!reachedIPs.Contains(ip))
            {
                reachedIPs.Add(ip);
                step();
            }

            return ac;
        }

        public bool runTillStopRepeat()
        {
            bool res = true;
            HashSet<int> reachedIPs = new HashSet<int>();

            while (!reachedIPs.Contains(ip))
            {
                reachedIPs.Add(ip);
                res = step();

                if (!res)
                    break;
            }

            return res;
        }

        public int getPart2Answer()
        {
            CommandType old;
            bool running = true;

            for(int chip = 0; chip < cmds.Count; chip++)
            {
                if(cmds[chip].Item1 == CommandType.jmp || cmds[chip].Item1 == CommandType.nop)
                { 
                    old = cmds[chip].Item1;
                    cmds[chip] = new Tuple<CommandType, int>(CommandType.nop, cmds[chip].Item2);
                    ip = 0;
                    ac = 0;

                    running = runTillStopRepeat();

                    if(!running)
                    {
                        return ac;
                    }

                    cmds[chip] = new Tuple<CommandType, int>(CommandType.jmp, cmds[chip].Item2);
                    ip = 0;
                    ac = 0;

                    running = runTillStopRepeat();

                    if (!running)
                        return ac;

                    cmds[chip] = new Tuple<CommandType, int>(old, cmds[chip].Item2);
                }
            }

            throw new Exception("No comm flip worked");
        }
    }

    public enum CommandType
    {
        acc,
        jmp,
        nop
    }
}
