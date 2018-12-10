using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;

namespace AdventCSharp.Day10
{
    public class ParticleSystem
    {
        public Particle[] Particles { get; set; }
        public Point[] Points { get; set; }

        public static Pen blackPen = Pens.Black;

        private int minx;
        private int maxx;
        private int miny;
        private int maxy;

        public ParticleSystem()
        {
            var inLines = File.ReadAllLines("Day10input.txt");

            Particles =
                inLines.Select(s => new Particle(s))
                       .ToArray();

            Points = new Point[Particles.Length];


            updateAtTime(0);

        }

        public void updateAtTime(int time)
        {
            for(int i = 0; i < Particles.Length; i++)
            {
                Points[i].X = Particles[i].X + Particles[i].XV * time;
                Points[i].Y = Particles[i].Y + Particles[i].YV * time;
            }
        }

        public Tuple<int, int, int, int> getBounding()
        {
            int minx = Int32.MaxValue;
            int maxx = Int32.MinValue;
            int miny = Int32.MaxValue;
            int maxy = Int32.MinValue;

            for(int i = 0; i < Points.Length; i++)
            {
                if (Points[i].X > maxx)
                    maxx = Points[i].X;

                if (Points[i].X < minx)
                    minx = Points[i].X;

                if (Points[i].Y > maxy)
                    maxy = Points[i].Y;

                if (Points[i].Y < miny)
                    miny = Points[i].Y;
            }

            return new Tuple<int, int, int, int>(minx, maxx, miny, maxy);
        }

        public void refreshBounding()
        {
            var bound = getBounding();
            minx = bound.Item1;
            maxx = bound.Item2;
            miny = bound.Item3;
            maxy = bound.Item4;
        }

        public void draw(Graphics gr, int width, int height)
        {
            float px;
            float py;

            
            for (int i = 0; i < Points.Length; i++)
            {
                px = (float)(Points[i].X - minx) / (float)(maxx - minx) * (float)width;
                py = (float)(Points[i].Y - miny) / (float)(maxy - miny) * (float)height;

                gr.DrawRectangle(blackPen, px - 1, py - 1, 3, 3);
            }
        }
    }
}
