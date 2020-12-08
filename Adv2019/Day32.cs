using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day32
    {
        public static Regex LineRegex = new Regex(@"(.+?) bags contain (.+)");
        public static Regex EmptyRegex = new Regex(@"no other bags");
        public static Regex LinkedRegex = new Regex(@"(\d+) (.+?) (bag|bags)");
        public string[] lines;

        public List<WeightedGraphNode> Nodes { get; set; }

        public Day32()
        {
            lines = DayInput.readDayLines(32, true);
            setup();
        }

        public void setup()
        {
            Match m;

            string bagName;
            string otherBagText;
            List<Tuple<string, long>> otherBags;
            Tuple<string, long> otherBag;
            string oBagName;
            long qty;

            List<WeightedGraphNode> wgNodes = new List<WeightedGraphNode>();

            foreach (var line in lines)
            {
                m = LineRegex.Match(line);
                otherBags = new List<Tuple<string, long>>();

                bagName = m.Groups[1].Value;
                otherBagText = m.Groups[2].Value;

                if(!EmptyRegex.Match(otherBagText).Success)
                {
                    var oBags = otherBagText.Split(',');

                    foreach(var oBag in oBags)
                    {
                        m = LinkedRegex.Match(oBag);

                        oBagName = m.Groups[2].Value;
                        qty = Int64.Parse(m.Groups[1].Value);
                        otherBag = new Tuple<string, long>(oBagName, qty);

                        otherBags.Add(otherBag);
                    }
                }

                wgNodes.Add(new WeightedGraphNode() { Name = bagName, RawBags = otherBags });
            }

            Dictionary<string, WeightedGraphNode> bagDict = wgNodes.ToDictionary(x => x.Name);

            for(int i = 0; i < wgNodes.Count; i++)
            {
                wgNodes[i].updateToLinks(bagDict);
            }

            Nodes = wgNodes;
        }

        public long getPart1Answer()
        {
            WeightedGraphNode shinyGold = Nodes.First(x => x.Name == "shiny gold");

            bool found = true;

            Dictionary<string, WeightedGraphNode> nodeFound = new Dictionary<string, WeightedGraphNode>();
            nodeFound.Add("shiny gold", shinyGold);

            while (found)
            {
                found = false;

                foreach(var wg in Nodes)
                {
                    foreach(var l in wg.Links)
                    {
                        if(nodeFound.ContainsKey(l.Item1.Name) && !nodeFound.ContainsKey(wg.Name))
                        {
                            found = true;
                            nodeFound.Add(wg.Name, wg);
                            break;
                        }
                    }
                }
            }

            return nodeFound.Count() - 1;
        }

        public long getPart2Answer()
        {
            WeightedGraphNode shinyGold = Nodes.First(x => x.Name == "shiny gold");

            List<Tuple<WeightedGraphNode, long>> unprocessed = new List<Tuple<WeightedGraphNode,long>>() { new Tuple<WeightedGraphNode, long>(shinyGold,1L) };

            List<Tuple<WeightedGraphNode, long>> processed = new List<Tuple<WeightedGraphNode, long>>();

            while(unprocessed.Count >= 1)
            {
                var t = unprocessed[0];

                WeightedGraphNode w0 = t.Item1;
                long qty = t.Item2;
                unprocessed.RemoveAt(0);

                foreach(Tuple<WeightedGraphNode, long> l in w0.Links)
                {
                    unprocessed.Add(new Tuple<WeightedGraphNode, long>(l.Item1, l.Item2 * qty));
                }

                processed.Add(t);
            }

            return processed.Sum(t => t.Item2) - 1;
        }
    }
}
