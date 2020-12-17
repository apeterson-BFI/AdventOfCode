using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day42
    {
        public string[] lines;

        public HashSet<Tuple<int, int, int>> activeCubes;

        public HashSet<Tuple<int, int, int, int>> active4D;

        public Day42()
        {
            lines = DayInput.readDayLines(42, true);
            activeCubes = new HashSet<Tuple<int, int, int>>();
            active4D = new HashSet<Tuple<int, int, int, int>>();

            for(int y = 0; y < lines.Length; y++)
            {
                for(int x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == '#')
                    {
                        activeCubes.Add(new Tuple<int, int, int>(x, y, 0));
                        active4D.Add(new Tuple<int, int, int, int>(x, y, 0, 0));
                    }
                }
            }
        }

        public int getPart1Answer()
        {
            int maxX = 0;
            int minX = 0;
            int maxY = 0;
            int minY = 0;
            int maxZ = 0;
            int minZ = 0;
            int count;
            Tuple<int, int, int> t;

            for (int cycle = 0; cycle < 6; cycle++)
            {
                minX = activeCubes.Min(c => c.Item1) - 1;
                maxX = activeCubes.Max(c => c.Item1) + 1;
                minY = activeCubes.Min(c => c.Item2) - 1;
                maxY = activeCubes.Max(c => c.Item2) + 1;
                minZ = activeCubes.Min(c => c.Item3) - 1;
                maxZ = activeCubes.Max(c => c.Item3) + 1;

                HashSet<Tuple<int, int, int>> newCubes = new HashSet<Tuple<int, int, int>>();

                for (int x = minX; x <= maxX; x++)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        for (int z = minZ; z <= maxZ; z++)
                        {
                            count = neighborCount(x, y, z);

                            t = new Tuple<int, int, int>(x, y, z);

                            if(activeCubes.Contains(t))
                            {
                                if(count == 2 || count == 3)
                                {
                                    newCubes.Add(t);
                                }
                            }
                            else
                            {
                                if(count == 3)
                                {
                                    newCubes.Add(t);
                                }
                            }
                        }
                    }
                }

                activeCubes = newCubes;
            }

            return (activeCubes.Count);
        }

        public int getPart2Answer()
        {
            int maxX = 0;
            int minX = 0;
            int maxY = 0;
            int minY = 0;
            int maxZ = 0;
            int minZ = 0;
            int maxW = 0;
            int minW = 0;
            int count;
            Tuple<int, int, int, int> t;

            for (int cycle = 0; cycle < 6; cycle++)
            {
                minX = active4D.Min(c => c.Item1) - 1;
                maxX = active4D.Max(c => c.Item1) + 1;
                minY = active4D.Min(c => c.Item2) - 1;
                maxY = active4D.Max(c => c.Item2) + 1;
                minZ = active4D.Min(c => c.Item3) - 1;
                maxZ = active4D.Max(c => c.Item3) + 1;
                minW = active4D.Min(c => c.Item4) - 1;
                maxW = active4D.Max(c => c.Item4) + 1;

                HashSet<Tuple<int, int, int, int>> new4D = new HashSet<Tuple<int, int, int, int>>();

                for (int x = minX; x <= maxX; x++)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        for (int z = minZ; z <= maxZ; z++)
                        {
                            for (int w = minW; w <= maxW; w++)
                            {
                                count = neighborCount(x, y, z, w);

                                t = new Tuple<int, int, int, int>(x, y, z, w);

                                if (active4D.Contains(t))
                                {
                                    if (count == 2 || count == 3)
                                    {
                                        new4D.Add(t);
                                    }
                                }
                                else
                                {
                                    if (count == 3)
                                    {
                                        new4D.Add(t);
                                    }
                                }
                            }
                        }
                    }
                }

                active4D = new4D;
            }

            return (active4D.Count);
        }


        public int neighborCount(int x, int y, int z)
        {
            int count = 0;
            Tuple<int, int, int> t;

            for(int dx = -1; dx <= 1; dx++)
            {
                for(int dy = -1; dy <= 1; dy++)
                {
                    for(int dz = -1; dz <= 1; dz++)
                    {
                        if(dx == 0 && dy == 0 && dz == 0)
                        {
                            continue;
                        }

                        t = new Tuple<int, int, int>(x + dx, y + dy, z + dz);
                        
                        if(activeCubes.Contains(t))
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        public int neighborCount(int x, int y, int z, int w)
        {
            int count = 0;
            Tuple<int, int, int, int> t;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        for (int dw = -1; dw <= 1; dw++)
                        {

                            if (dx == 0 && dy == 0 && dz == 0 && dw == 0)
                            {
                                continue;
                            }

                            t = new Tuple<int, int, int, int>(x + dx, y + dy, z + dz, w + dw);

                            if (active4D.Contains(t))
                            {
                                count++;
                            }
                        }
                    }
                }
            }

            return count;
        }

    }
}