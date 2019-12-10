using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day10
    {
        private List<SignedPoint> points;
        private SignedPoint bestPoint;
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
            Tuple<int, int> dir = new Tuple<int, int>(0, 0);

            for(int i = 0; i < points.Count; i++)
            {
                if (i == bestIndex)
                    continue;

                relPoints[i].X = points[i].X - points[bestIndex].X;
                relPoints[i].Y = points[i].Y - points[bestIndex].Y;
                relPoints[i].reduce();

                dir = new Tuple<int, int>(relPoints[i].RedX, relPoints[i].RedY);

                if(!dirSQLists.ContainsKey(dir))
                {
                    dirSQLists.Add(dir, new List<int>());
                }

                dirSQLists[dir].Add(relPoints[i].sqMag);
            }

            double angle;
            int rX;
            int rY;

            List<Tuple<double, List<Tuple<int, int, int>>>> rays = new List<Tuple<double, List<Tuple<int, int, int>>>>();

            List<Tuple<int, int, int>> rayList = new List<Tuple<int, int, int>>();

            foreach(var kvp in dirSQLists)
            {
                angle = renormalizeAngle(Math.Atan2(kvp.Key.Item2, kvp.Key.Item1));
                rX = kvp.Key.Item1;
                rY = kvp.Key.Item2;

                rayList = 
                    kvp.Value.Select(v => new Tuple<int, int, int>(rX, rY, v))
                             .OrderBy(tup => tup.Item3)
                             .ToList();

                rays.Add(new Tuple<double, List<Tuple<int, int, int>>>(angle, rayList));
            }

            rays = rays.OrderBy(r => r.Item1).ToList();

            int pickN = 0;
            int pass = 0;
            int angleIndex = 0;
            Tuple<int, int, int> dp = new Tuple<int, int, int>(0, 0, 0);

            Console.WriteLine(rays.Count());

            while(pickN < 200)
            {
                if(pass < rays[angleIndex].Item2.Count())
                {
                    dp = rays[angleIndex].Item2[pass];
                    pickN++;
                    Console.WriteLine("[{0}] ({1},{2}) {3:0.00}", pickN, dp.Item1, dp.Item2, rays[angleIndex].Item1);
                }

                angleIndex++;

                if(angleIndex == rays.Count)
                {
                    angleIndex = 0;
                    pass++;
                }
            }

            Console.WriteLine("{0},{1}", points[bestIndex].X, points[bestIndex].Y);

            int nX = dp.Item1 + points[bestIndex].X;
            int nY = dp.Item2 + points[bestIndex].Y;



            return nX * 100 + nY;
        }

        // Set Pi/2 as 0, neg angles get 2pi
        public static double renormalizeAngle(double radians)
        {
            double s1 = Math.PI / 2.0 - radians;

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
