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

            Reaction extReaction = new Reaction();
            Reaction reqdReaction;

            Reaction fuelReaction = Reactions.Where(r => r.Product.Product == "FUEL").FirstOrDefault();
            extReaction.Product = new Commodity() { Product = extReaction.Product.Product, Quantity = extReaction.Product.Quantity };

            while(!extReaction.oreOnly())
            {
                for(int i = 0; i < extReaction.Reactants.Count; i++)
                {
                    if(extReaction.Reactants[i].Quantity > 0)
                    {
                        reqdReaction = Reactions.Where(r => r.Product.Product == extReaction.Reactants[i].Product).FirstOrDefault();

                        long needed = extReaction.Reactants[i].Quantity;
                        long provided = reqdReaction.Product.Quantity;
                        long reactQty = (needed % provided == 0 ? needed / provided : needed / provided + 1);
                        // a / b = integer division = floor( a / b). 
                        // we need ceiling (a / b).
                        // if mod = 0, then a / b else a / b + 1.

                        
                    }
                }
            }
        }
    }

    public class Reaction
    {
        public List<Commodity> Reactants { get; set; }

        public Commodity Product { get; set; }

        public static Reaction parseLine(string line)
        {
            var mainSplit = line.Split(new string[] { "=>" }, StringSplitOptions.None);

            Reaction nr = new Reaction();

            nr.Product = Commodity.parse(mainSplit[1]);
            nr.Reactants = mainSplit[0].Split(',').Select(x => Commodity.parse(x)).ToList();

            return nr;
        }

        public bool oreOnly()
        {
            foreach(Commodity m in Reactants)
            {
                if (m.Quantity > 0 && m.Product != "ORE")
                    return false;
            }

            return true;
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
