using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day37
    {
        public string[] lines;

        public List<Tuple<FerryCommand, int>> commands;

        int x;
        int y;

        int wx;
        int wy;

        public Day37()
        {
            lines = DayInput.readDayLines(37, true);
            commands = new List<Tuple<FerryCommand, int>>();

            int length;
            FerryCommand command = FerryCommand.Forward;
            char s;

            foreach(var line in lines)
            {
                length = Int32.Parse(line.Substring(1));

                s = line[0];

                switch (s)
                {
                    case 'F': command = FerryCommand.Forward; break;
                    case 'L': command = FerryCommand.Left; break;
                    case 'R': command = FerryCommand.Right; break;
                    case 'N': command = FerryCommand.North; break;
                    case 'E': command = FerryCommand.East; break;
                    case 'S': command = FerryCommand.South; break;
                    case 'W': command = FerryCommand.West; break;
                }

                commands.Add(new Tuple<FerryCommand, int>(command, length));
            }
        }

        public int getPart1Answer()
        {
            x = 0;
            y = 0;
            wx = 1;
            wy = 0;
            FerryCommand cmd;
            int length;

            foreach(var tup in commands)
            {
                cmd = tup.Item1;
                length = tup.Item2;

                switch(cmd)
                {
                    case FerryCommand.Forward: x += wx * length; y += wy * length; break;
                    case FerryCommand.Left:
                        if (length % 90 != 0)
                            throw new Exception("Problems");

                        while(length > 0)
                        {
                            turnLeft();
                            length -= 90;
                        }

                        break;

                    case FerryCommand.Right:
                        if (length % 90 != 0)
                            throw new Exception("Problems");

                        while(length > 0)
                        {
                            turnRight();
                            length -= 90;
                        }

                        break;

                    case FerryCommand.North: y += length; break;
                    case FerryCommand.East: x += length; break;
                    case FerryCommand.South: y -= length; break;
                    case FerryCommand.West: x -= length; break;
                    default: throw new Exception("what");
                }
            }

            return Math.Abs(x) + Math.Abs(y);
        }

        public int getPart2Answer()
        {
            x = 0;
            y = 0;
            wx = 10;
            wy = 1;
            FerryCommand cmd;
            int length;

            foreach (var tup in commands)
            {
                cmd = tup.Item1;
                length = tup.Item2;

                switch (cmd)
                {
                    case FerryCommand.Forward: x += wx * length; y += wy * length; break;
                    case FerryCommand.Left:
                        if (length % 90 != 0)
                            throw new Exception("Problems");

                        while (length > 0)
                        {
                            turnLeft();
                            length -= 90;
                        }

                        break;

                    case FerryCommand.Right:
                        if (length % 90 != 0)
                            throw new Exception("Problems");

                        while (length > 0)
                        {
                            turnRight();
                            length -= 90;
                        }

                        break;

                    case FerryCommand.North: wy += length; break;
                    case FerryCommand.East: wx += length; break;
                    case FerryCommand.South: wy -= length; break;
                    case FerryCommand.West: wx -= length; break;
                    default: throw new Exception("what");
                }
            }

            return Math.Abs(x) + Math.Abs(y);

        }

        public void turnRight()
        {
            int nx = wy;
            int ny = -wx;

            wx = nx;
            wy = ny;
        }

        public void turnLeft()
        {
            int ny = wx;
            int nx = -wy;

            wx = nx;
            wy = ny;
        }
    }

    public enum FerryCommand
    {
        Forward,
        Left,
        Right,
        North,
        East,
        South,
        West
    }
}
