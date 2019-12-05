using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day3
    {
        public List<Wire> Wires;

        public Day3()
        {
            Wires = readLinesAsWires();
        }

        public int getPart1Answer()
        {
            List<Tuple<int, int>> coords = Wires[0].getOccupied().ToList();
            HashSet<Tuple<int, int>> occW2 = Wires[1].getOccupied();

            return coords.Where(tup => occW2.Contains(tup)).Min(tup => Math.Abs(tup.Item1) + Math.Abs(tup.Item2));
        }

        public int getPart2Answer()
        {
            List<Tuple<int, int>> coords = Wires[0].getOccupied().ToList();

            Dictionary<Tuple<int, int>, int> wire0Delay = Wires[0].getSigDelay();
            Dictionary<Tuple<int, int>, int> wire1Delay = Wires[1].getSigDelay();

            return coords.Where(tup => wire0Delay.ContainsKey(tup) && wire1Delay.ContainsKey(tup))
                         .Select(tup => wire0Delay[tup] + wire1Delay[tup])
                         .Min();
        }

        public List<Wire> readLinesAsWires()
        {
            var textCSV = DayInput.readDayLinesAsTextCSV(3, ',');

            return
                textCSV.Select(l => new Wire() { Instructions = l.Select(c => new DirInstruction(c[0], Int32.Parse(c.Substring(1)))).ToList() }).ToList();
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    internal class DirInstruction
    {
        public Direction Direction { get; set; }

        public int Amount { get; set; }

        public DirInstruction(char c, int a)
        {
            Direction = parseDirection(c);
            Amount = a;
        }

        public Direction parseDirection(char c)
        {
            switch (c)
            {
                case 'U': return Direction.Up;
                case 'D': return Direction.Down;
                case 'L': return Direction.Left;
                case 'R': return Direction.Right;
                default: throw new ArgumentException();
            }
            
        }

        public Tuple<int, int> getDirectionDelta()
        {
            switch (Direction)
            {
                case Direction.Up: return new Tuple<int, int>(0, -1);
                case Direction.Down: return new Tuple<int, int>(0, 1);
                case Direction.Left: return new Tuple<int, int>(-1, 0);
                case Direction.Right: return new Tuple<int, int>(1, 0);
                default: throw new ArgumentException();
            }
        }
    }

    public class Wire
    {
        internal List<DirInstruction> Instructions { get; set; }

        public HashSet<Tuple<int, int>> getOccupied()
        {
            int remainingLength;
            int currX = 0;
            int currY = 0;
            int deltaX = 0;
            int deltaY = 0;
            Tuple<int, int> intTup;

            HashSet<Tuple<int, int>> occ = new HashSet<Tuple<int, int>>();

            foreach(var instruct in Instructions)
            {
                remainingLength = instruct.Amount;

                intTup = instruct.getDirectionDelta();
                deltaX = intTup.Item1;
                deltaY = intTup.Item2;

                while (remainingLength > 0)
                {
                    currX += deltaX;
                    currY += deltaY;
                    remainingLength--;
                    intTup = new Tuple<int, int>(currX, currY);

                    if(!occ.Contains(intTup))
                    {
                        occ.Add(intTup);
                    }
                }
            }

            return occ;
        }

        public Dictionary<Tuple<int, int>, int> getSigDelay()
        {
            int remainingLength;
            int currX = 0;
            int currY = 0;
            int deltaX = 0;
            int deltaY = 0;
            int delay = 0;
            Tuple<int, int> intTup;

            Dictionary<Tuple<int, int>, int> delayDic = new Dictionary<Tuple<int, int>, int>();

            foreach (var instruct in Instructions)
            {
                remainingLength = instruct.Amount;

                intTup = instruct.getDirectionDelta();
                deltaX = intTup.Item1;
                deltaY = intTup.Item2;

                while (remainingLength > 0)
                {
                    currX += deltaX;
                    currY += deltaY;
                    remainingLength--;
                    delay++;
                    intTup = new Tuple<int, int>(currX, currY);

                    if (!delayDic.ContainsKey(intTup))
                    {
                        delayDic.Add(intTup, delay);
                    }
                }
            }

            return delayDic;
        }
    }
}
