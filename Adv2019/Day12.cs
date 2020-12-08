using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MathNet;

namespace Adv2020
{
    public class Day12
    {
        private static Regex orbitRegex = new Regex("<x=([-+]?[0-9]*), y=([-+]?[0-9]*), z=([-+]?[0-9]*)");

        public List<Orbiter> Orbiters { get; set; }

        public List<Orbiter> baseOrbiters { get; set; }

        public Day12()
        {
            // <x=-3, y=18, z=9>


            baseOrbiters = DayInput.readDayLines(12, true)
                                             .Select(x => readLine(x))
                                             .ToList();

            Orbiters = new List<Orbiter>(baseOrbiters);
        }

        public void reinitialize()
        {
            Orbiters = new List<Orbiter>(baseOrbiters);
        }

        public int getPart1Answer()
        {
            for(int i = 0; i < 1000; i++)
            {
                step();
            }

            return getEnergy();
        }

        public void getPart2Answer()
        {
            int xBase = 0;
            int xCycle = 0;
            int yBase = 0;
            int yCycle = 0;
            int zBase = 0;
            int zCycle = 0;

            Dictionary<Tuple<int, int, int, int, int, int, Tuple<int,int>>, int> coordinateHistory = new Dictionary<Tuple<int, int, int, int, int, int,Tuple<int,int>>, int>();

            int stepN = 0;

            Tuple<int, int, int, int, int,int,Tuple<int, int>> hist = new Tuple<int, int, int, int, int, int,Tuple<int,int>>(Orbiters[0].X, Orbiters[1].X, Orbiters[2].X, Orbiters[3].X, Orbiters[0].Xp, Orbiters[1].Xp, new Tuple<int,int>(Orbiters[2].Xp, Orbiters[3].Xp));

            while (!coordinateHistory.ContainsKey(hist))
            {
                coordinateHistory.Add(hist, stepN);

                step();
                stepN++;

                hist = new Tuple<int, int, int, int, int, int, Tuple<int,int>>(Orbiters[0].X, Orbiters[1].X, Orbiters[2].X, Orbiters[3].X, Orbiters[0].Xp, Orbiters[1].Xp, new Tuple<int,int>(Orbiters[2].Xp, Orbiters[3].Xp));
            }

            xBase = coordinateHistory[hist];
            xCycle = stepN - xBase;

            stepN = 0;
            reinitialize();
            coordinateHistory = new Dictionary<Tuple<int, int, int, int, int, int, Tuple<int, int>>, int>();

            hist = new Tuple<int, int, int, int, int, int, Tuple<int,int>>(Orbiters[0].Y, Orbiters[1].Y, Orbiters[2].Y, Orbiters[3].Y, Orbiters[0].Yp, Orbiters[1].Yp, new Tuple<int,int>(Orbiters[2].Yp, Orbiters[3].Yp));

            while (!coordinateHistory.ContainsKey(hist))
            {
                coordinateHistory.Add(hist, stepN);

                step();
                stepN++;

                hist = new Tuple<int, int, int, int, int, int, Tuple<int, int>>(Orbiters[0].Y, Orbiters[1].Y, Orbiters[2].Y, Orbiters[3].Y, Orbiters[0].Yp, Orbiters[1].Yp, new Tuple<int, int>(Orbiters[2].Yp, Orbiters[3].Yp));
            }

            yBase = coordinateHistory[hist];
            yCycle = stepN - yBase;

            stepN = 0;
            reinitialize();
            coordinateHistory = new Dictionary<Tuple<int, int, int, int, int, int, Tuple<int, int>>, int>();

            hist = new Tuple<int, int, int, int, int, int, Tuple<int, int>>(Orbiters[0].Z, Orbiters[1].Z, Orbiters[2].Z, Orbiters[3].Z, Orbiters[0].Zp, Orbiters[1].Zp, new Tuple<int,int>(Orbiters[2].Zp, Orbiters[3].Zp));

            while (!coordinateHistory.ContainsKey(hist))
            {
                coordinateHistory.Add(hist, stepN);

                step();
                stepN++;

                hist = new Tuple<int, int, int, int, int, int, Tuple<int, int>>(Orbiters[0].Z, Orbiters[1].Z, Orbiters[2].Z, Orbiters[3].Z, Orbiters[0].Zp, Orbiters[1].Zp, new Tuple<int, int>(Orbiters[2].Zp, Orbiters[3].Zp));
            }

            zBase = coordinateHistory[hist];
            zCycle = stepN - zBase;

            // x_p = (stepN - Xb) % Xcyc
            // y_p = (stepN - Yb) % Ycyc
            // z_p = (stepN - Zb) % Zcyc

            // assumping Xb = Yb = Zb = 0
            // 12, 15, 20 -> LCD (Xcyc, Ycyc, Zcyc) = first recurrence.

            long reoccurrence = MathNet.Numerics.Euclid.LeastCommonMultiple(xCycle, yCycle, zCycle);

            Console.WriteLine("Xb, Xcyc: {0},{1}", xBase, xCycle);
            Console.WriteLine("Yb, Ycyc: {0},{1}", yBase, yCycle);
            Console.WriteLine("Zb, Zcyc: {0},{1}", zBase, zCycle);

            bool reTruth = Orbiters[0].same(baseOrbiters[0]) && Orbiters[1].same(baseOrbiters[1])
                && Orbiters[2].same(baseOrbiters[2]) && Orbiters[3].same(baseOrbiters[3]);

            Console.WriteLine("Reoccurence at {0} : {1}", reoccurrence, reTruth);
        }

        private int getEnergy()
        {
            int energy = 0;

            for(int i = 0; i < 4; i++)
            {
                energy += Orbiters[i].energy();
            }

            return energy;
        }

        public void step()
        {
            for(int i = 0; i < Orbiters.Count; i++)
            {
                for(int j = 0; j < Orbiters.Count; j++)
                {
                    if (i == j)
                        continue;

                    Orbiters[i].Xp += Math.Sign(Orbiters[j].X - Orbiters[i].X);
                    Orbiters[i].Yp += Math.Sign(Orbiters[j].Y - Orbiters[i].Y);
                    Orbiters[i].Zp += Math.Sign(Orbiters[j].Z - Orbiters[i].Z);
                }
            }

            for(int i = 0; i < Orbiters.Count; i++)
            {
                Orbiters[i].X += Orbiters[i].Xp;
                Orbiters[i].Y += Orbiters[i].Yp;
                Orbiters[i].Z += Orbiters[i].Zp;
            }
        }


        private static Orbiter readLine(string line)
        {
            var m = orbitRegex.Match(line);

            int x = Int32.Parse(m.Groups[1].Value);
            int y = Int32.Parse(m.Groups[2].Value);
            int z = Int32.Parse(m.Groups[3].Value);

            return new Orbiter() { X = x, Y = y, Z = z, Xp = 0, Yp = 0, Zp = 0 };
        }
    }

    public class Orbiter
    {
        public int X;
        public int Y;
        public int Z;
        public int Xp;
        public int Yp;
        public int Zp;

        public Orbiter()
        {

        }

        public Orbiter(Orbiter o)
        {
            this.X = o.X;
            this.Xp = o.Xp;
            this.Y = o.Y;
            this.Yp = o.Yp;
            this.Z = o.Z;
            this.Zp = o.Zp;
        }

        public bool same(Orbiter o)
        {
            return this.X == o.X &&
                    this.Xp == o.Xp &&
                    this.Y == o.Y &&
                    this.Yp == o.Yp &&
                    this.Z == o.Z &&
                    this.Zp == o.Zp;
        }

        public int energy()
        {
            return (Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z)) * (Math.Abs(Xp) + Math.Abs(Yp) + Math.Abs(Zp));
        }
    }
}
