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
            lines = DayInput.readDayLines(32);
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

            int newCount = 1;

            Dictionary<string, WeightedGraphNode> nodeFound = new Dictionary<string, WeightedGraphNode>();
            nodeFound.Add("shiny gold", shinyGold);

            List<WeightedGraphNode> newNodes = new List<WeightedGraphNode>();
            newNodes.Add(shinyGold);

            List<WeightedGraphNode> unqNodes = new List<WeightedGraphNode>();

            while(newCount != 0)
            {
                newCount = 0;
                newNodes = newNodes.SelectMany(n => n.Links.Select(l => l.Item1)).ToList();

                unqNodes = new List<WeightedGraphNode>();

                foreach(var w in newNodes)
                {
                    if(!nodeFound.ContainsKey(w.Name))
                    {
                        nodeFound.Add(w.Name, w);
                        newCount++;
                        unqNodes.Add(w);
                    }

                    newNodes = unqNodes;
                }
            }


            return nodeFound.Count() - 1;

        }
    }
}
