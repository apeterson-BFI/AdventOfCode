using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventCSharp.Day10
{
    public class Particle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int XV { get; set; }
        public int YV { get; set; }

        public static Regex inputRegex = new Regex(@"position=<([\s-]+\d+),\s([\s-]+\d+)> velocity=<([\s-]+\d+),\s([\s-]+\d+)>");

        public Particle(string line)
        {
            var m = inputRegex.Match(line);

            X = Int32.Parse(m.Groups[1].Value);
            Y = Int32.Parse(m.Groups[2].Value);
            XV = Int32.Parse(m.Groups[3].Value);
            YV = Int32.Parse(m.Groups[4].Value);
        }
    }
}
