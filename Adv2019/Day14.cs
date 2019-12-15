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
            return getNFuel(1);
        }

        private long getNFuel(long n)
        {
            // reaction : PROD QTY, PROD2 QTY, ... -> RSULT QTY

            Reaction extReaction = new Reaction();
            Reaction reqdReaction;
            bool reactantFound = false;

            Reaction fuelReaction = Reactions.Where(r => r.Product.Product == "FUEL").FirstOrDefault();
            extReaction.Product = new Commodity() { Product = fuelReaction.Product.Product, Quantity = n };
            extReaction.Reactants = new List<Commodity>();

            for (int i = 0; i < fuelReaction.Reactants.Count; i++)
            {
                extReaction.Reactants.Add(new Commodity() { Product = fuelReaction.Reactants[i].Product, Quantity = fuelReaction.Reactants[i].Quantity * n});
            }

            while (!extReaction.oreOnly())
            {
                for (int i = 0; i < extReaction.Reactants.Count; i++)
                {
                    if (extReaction.Reactants[i].Quantity > 0 && extReaction.Reactants[i].Product != "ORE")
                    {
                        reqdReaction = Reactions.Where(r => r.Product.Product == extReaction.Reactants[i].Product).FirstOrDefault();

                        long needed = extReaction.Reactants[i].Quantity;
                        long provided = reqdReaction.Product.Quantity;

                        // a / b = integer division = floor( a / b). 
                        // we need ceiling (a / b).
                        // if mod = 0, then a / b else a / b + 1.
                        long reactQty = (needed % provided == 0 ? needed / provided : needed / provided + 1);

                        extReaction.Reactants[i].Quantity -= provided * reactQty;

                        for (int j = 0; j < reqdReaction.Reactants.Count; j++)
                        {
                            reactantFound = false;

                            for (int k = 0; k < extReaction.Reactants.Count; k++)
                            {
                                if (reqdReaction.Reactants[j].Product == extReaction.Reactants[k].Product)
                                {
                                    reactantFound = true;
                                    extReaction.Reactants[k].Quantity += reqdReaction.Reactants[j].Quantity * reactQty;
                                    break;
                                }
                            }

                            if (!reactantFound)
                            {
                                extReaction.Reactants.Add(new Commodity() { Product = reqdReaction.Reactants[j].Product, Quantity = reqdReaction.Reactants[j].Quantity * reactQty });
                            }
                        }
                    }
                }
            }

            return extReaction.Reactants.Where(r => r.Product == "ORE").FirstOrDefault().Quantity;
        }

        public long getPart2Answer()
        {
            Console.WriteLine("Guess the amount of fuel you can produce with 1 trillion ore");
            string line;
            long guess = 0;
            long result;

            while((line = Console.ReadLine()) != "q")
            {
                if(Int64.TryParse(line, out guess))
                {
                    result = getNFuel(guess);
                    Console.WriteLine("Guess {0} : Result {1} (net: {2}", guess, result, (result - 1000000000000L));
                }
            }

            return guess;
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
            var p = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Commodity nc = new Commodity();

            nc.Product = p[1].Trim();
            nc.Quantity = Int64.Parse(p[0]);

            return nc;
        }
    }
}
