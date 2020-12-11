using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using System.Data;

namespace Adv2020
{
    public class Day36
    {
        public List<StringBuilder> grid;

        public List<Tuple<int, int>> rays;

        int minx;
        int maxx;
        int miny;
        int maxy;

        public Day36()
        {
            grid = DayInput.readDayLines(36, true).Select(s => new StringBuilder(s)).ToList();

            minx = 0;
            miny = 0;

            maxy = grid.Count - 1;
            maxx = grid[0].Length - 1;
        }

        public long getPart1Answer()
        {
            while(updateGridP1())
            {
                ;
            }

            long count = 0;

            for(int r = 0; r <= maxy; r++)
            {
                for(int c = 0; c <= maxx; c++)
                {
                    if (grid[r][c] == '#')
                        count++;
                }
            }

            return count;
        }

        public long getPart2Answer()
        {
            rays = new List<Tuple<int, int>>()
                {
                    new Tuple<int, int>(1, 1),
                    new Tuple<int, int>(1, 0),
                    new Tuple<int, int>(1, -1),
                    new Tuple<int, int>(0, -1),
                    new Tuple<int,int>(0, 1),
                    new Tuple<int, int>(-1, 1),
                    new Tuple<int, int>(-1, 0),
                    new Tuple<int, int>(-1,-1)};


            while (updateGridP2())
            {
                ;
            }

            long count = 0;

            for (int r = 0; r <= maxy; r++)
            {
                for (int c = 0; c <= maxx; c++)
                {
                    if (grid[r][c] == '#')
                        count++;
                }
            }

            return count;
        }

        public bool updateGridP1()
        {
            List<StringBuilder> ng = new List<StringBuilder>();

            for(int i = 0; i < grid.Count; i++)
            {
                ng.Add(new StringBuilder(grid[i].ToString()));
            }

            int count;
            int changes = 0;

            for(int r = 0; r <= maxy; r++)
            {
                for(int c = 0; c <= maxx; c++)
                {
                    count = neighbors(r, c);

                    if(grid[r][c] == 'L' && count == 0)
                    {
                        changes++;
                        ng[r][c] = '#';
                    }
                    else if(grid[r][c] == '#' && count >= 4)
                    {
                        changes++;
                        ng[r][c] = 'L';
                    }
                }
            }

            grid = ng;

            return (changes > 0);
        }

        public bool updateGridP2()
        {
            List<StringBuilder> ng = new List<StringBuilder>();

            for (int i = 0; i < grid.Count; i++)
            {
                ng.Add(new StringBuilder(grid[i].ToString()));
            }

            int count;
            int changes = 0;

            for (int r = 0; r <= maxy; r++)
            {
                for (int c = 0; c <= maxx; c++)
                {
                    count = seen(r, c);

                    if (grid[r][c] == 'L' && count == 0)
                    {
                        changes++;
                        ng[r][c] = '#';
                    }
                    else if (grid[r][c] == '#' && count >= 5)
                    {
                        changes++;
                        ng[r][c] = 'L';
                    }
                }
            }

            grid = ng;

            return (changes > 0);
        }


        public int seen(int r, int c)
        {
            int count = 0;

            for(int i = 0; i < rays.Count; i++)
            {
                if (raytrace(r, c, rays[i].Item1, rays[i].Item2))
                    count++;
            }

            return count;
        }

        public bool raytrace(int r, int c, int rd, int cd)
        {
            r += rd;
            c += cd;

            while(r <= maxy && r >= miny && c <= maxx && c >= minx)
            {
                if (grid[r][c] == '#')
                    return true;
                else if (grid[r][c] == 'L')
                    return false;

                r += rd;
                c += cd;
            }

            return false;
        }



        public int neighbors(int r, int c)
        {
            var n1 = new Tuple<int, int>(r - 1, c - 1);
            var n2 = new Tuple<int, int>(r - 1, c);
            var n3 = new Tuple<int, int>(r - 1, c + 1);
            var n4 = new Tuple<int, int>(r, c - 1);
            var n5 = new Tuple<int, int>(r, c + 1);
            var n6 = new Tuple<int, int>(r + 1, c - 1);
            var n7 = new Tuple<int, int>(r + 1, c);
            var n8 = new Tuple<int, int>(r + 1, c + 1);

            return new List<Tuple<int, int>>() { n1, n2, n3, n4, n5, n6, n7, n8 }
            .Where(tup => tup.Item1 >= miny && tup.Item1 <= maxy && tup.Item2 >= minx && tup.Item2 <= maxx && grid[tup.Item1][tup.Item2] == '#')
            .Count();
        }
    }
}
