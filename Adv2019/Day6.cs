using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day6
    {
        private List<string[]> rawConnections;

        private DirGraphMaster<string> master;

        public Day6()
        {
            master = new DirGraphMaster<string>();

            rawConnections = DayInput.readDayLines(6).Select(l => l.Split(')')).ToList();
        }

        public void part1Setup()
        {
            string to;
            string from;

            foreach (var t in rawConnections)
            {
                to = t[0];
                from = t[1];

                master.connect(from, to);
            }
        }

        public void part2Setup()
        {
            string to;
            string from;

            foreach (var t in rawConnections)
            {
                to = t[0];
                from = t[1];

                master.connectBI(from, to);
            }
        }

        public int getPart1Answer()
        {
            part1Setup();
            return master.Nodes.Sum(x => x.Value.countAllConnected());
        }

        public int getPart2Answer()
        {
            part2Setup();

            return (master.distance("YOU", "SAN") - 2);
        }
    }
}
