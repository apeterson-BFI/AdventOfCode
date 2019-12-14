using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Adv2020
{
    public class Day14
    {
        private string[] lines;

        private List<Reaction> Reactions;

        public Day14()
        {
            lines = DayInput.readDayLines(14);
            Reactions = lines.Select(l => Reaction.parseLine(l)).ToList();
        }

        public long getPart1Answer()
        {
            // reaction : PROD QTY, PROD2 QTY, ... -> RSULT QTY

        }
    }

    public class Reaction
    {
        List<Commodity> Reactants { get; set; }

        Commodity Product { get; set; }

        public static Reaction parseLine(string line)
        {
            var mainSplit = line.Split(new string[] { "=>" }, StringSplitOptions.None);

            Reaction nr = new Reaction();

            nr.Product = Commodity.parse(mainSplit[1]);
            nr.Reactants = mainSplit[0].Split(',').Select(x => Commodity.parse(x)).ToList();

            return nr;
        }
    }

    public class Commodity
    {
        public string Product { get; set; }

        public long Quantity { get; set; }

        public static Commodity parse(string text)
        {
            var p = text.Split(' ');

            Commodity nc = new Commodity();

            nc.Product = p[0].Trim();
            nc.Quantity = Int64.Parse(p[1]);

            return nc;
        }
    }
}
