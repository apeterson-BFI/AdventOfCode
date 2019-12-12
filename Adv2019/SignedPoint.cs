using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class SignedPoint
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int RedX { get; set; }

        public int RedY { get; set; }

        public int sX
        {
            get
            {
                return Math.Sign(X);
            }
        }

        public int sY
        {
            get
            {
                return Math.Sign(Y);
            }
        }

        public int sqMag
        {
            get
            {
                return X * X + Y * Y;
            }
        }

        public void reduce()
        {
            int aX = Math.Abs(X);
            int aY = Math.Abs(Y);

            int g = gcd(aX, aY);

            RedX = X / g;
            RedY = Y / g;
        }

        public static int gcd(int p1, int p2)
        {
            if (p1 == 0)
                return p2;
            else if (p2 == 0)
                return p1;

            int r = p1 % p2;

            return gcd(p2, r);
        }

        public static int lcm(int p1, int p2)
        {
            return p1 * p2 / gcd(p1, p2);
        }
    }
}
