using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Adv2020
{
    public class Day45
    {
        public List<Tile> Tiles;

        private string[] lines;
        private Regex tileRegex = new Regex(@"Tile (\d+):");

        public Day45()
        {
            lines = DayInput.readDayLines(45, true);
            Tiles = new List<Tile>();

            setup();
        }

        private void setup()
        {
            long tilenum;
            Match m;
            Tile tile = null;

            foreach(string line in lines)
            {
                m = tileRegex.Match(line);

                if(m.Success)
                {
                    tile = new Tile();
                    tile.Grid = new bool[10, 10];
                    tilenum = Int64.Parse(m.Groups[1].Value);
                }
                else if(line == "")
                {
                    Tiles.Add(tile);
                }
            }
        }

        internal long getPart1Answer()
        {
            throw new NotImplementedException();
        }
    }

    public class Tile
    { 
        public long TileNum { get; set; }

        public bool[,] Grid { get; set; }
    }

}
