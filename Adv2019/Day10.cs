using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2019
{
    public class Day10
    {
        internal List<SignedPoint> points;
        internal SignedPoint bestPoint;
        private int bestIndex;
        private Dictionary<Tuple<int, int>, int> dirSQPoints;
        private Dictionary<Tuple<int, int>, List<int>> dirSQLists;
        private SignedPoint[] relPoints;
        


        public Day10()
        {
            points = DayInput.readLinesAsPoints(10, '#');
            dirSQPoints = new Dictionary<Tuple<int, int>, int>();
            relPoints = new SignedPoint[points.Count];
            dirSQLists = new Dictionary<Tuple<int, int>, List<int>>();

            for (int i = 0; i < relPoints.Length; i++)
            {
                relPoints[i] = new SignedPoint();
            }
        }

        public int getPart2Answer()
        {
            Tuple<double, int, int> dp;

            List<Tuple<double, int, int>> angleList = new List<Tuple<double, int, int>>();

            for(int i = 0; i < points.Count; i++)
            {
                if (i == bestIndex)
                    continue;

                relPoints[i].X = points[i].X - points[bestIndex].X;
                relPoints[i].Y = points[i].Y - points[bestIndex].Y;
                relPoints[i].reduce();

                angleList.Add(new Tuple<double, int, int>(renormalizeAngle(Math.Atan2(relPoints[i].RedY, relPoints[i].RedX)), relPoints[i].X, relPoints[i].Y));
            }

            angleList = angleList.Where(a => !angleList.Exists(b => b.Item2 * b.Item2 + b.Item3 * b.Item3 < a.Item2 * a.Item2 + a.Item3 * a.Item3 && a.Item1 == b.Item1))
                                 .OrderBy(a => a.Item1)
                                 .ToList();

            dp = angleList[199];

            for(int i = 0; i < angleList.Count; i++)
            {
                Console.WriteLine("[{0}] ({1},{2}) {3:0.00}", i, angleList[i].Item2, angleList[i].Item3, angleList[i].Item1);
            }

            Console.WriteLine("{0},{1}", points[bestIndex].X, points[bestIndex].Y);

            int nX = dp.Item2 + points[bestIndex].X;
            int nY = dp.Item3 + points[bestIndex].Y;

            return nX * 100 + nY;
        }

        // Set Pi/2 as 0, neg angles get 2pi
        public static double renormalizeAngle(double radians)
        {
            double s1 = radians + Math.PI / 2.0;

            if(s1 < 0.0)
            {
                s1 = s1 + Math.PI * 2.0;
            }

            return s1;
        }

        public int getPart1Answer()
        {
            Tuple<int, int> dir = new Tuple<int, int>(0, 0);

            int magSQ;
            int maxDetections = 0;

            for (int oI = 0; oI < points.Count; oI++)
            {
                dirSQPoints = new Dictionary<Tuple<int, int>, int>();

                for (int iI = 0; iI < points.Count; iI++)
                {
                    if (iI == oI)
                        continue;

                    relPoints[iI].X = points[iI].X - points[oI].X;
                    relPoints[iI].Y = points[iI].Y - points[oI].Y;
                    relPoints[iI].reduce();

                    dir = new Tuple<int, int>(relPoints[iI].RedX, relPoints[iI].RedY);

                    if (dirSQPoints.ContainsKey(dir))
                    {
                        magSQ = dirSQPoints[dir];

                        if (relPoints[iI].sqMag < magSQ)
                        {
                            dirSQPoints[dir] = relPoints[iI].sqMag;
                        }
                    }
                    else
                    {
                        dirSQPoints.Add(dir, relPoints[iI].sqMag);
                    }
                }

                if(dirSQPoints.Count() > maxDetections)
                {
                    bestIndex = oI;
                    bestPoint = points[oI];
                    maxDetections = dirSQPoints.Count();
                }
            }

            return maxDetections;
        }
    }
}
