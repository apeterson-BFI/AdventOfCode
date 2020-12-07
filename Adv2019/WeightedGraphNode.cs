using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class WeightedGraphNode
    {
        public string Name { get; set; }

        public List<Tuple<string, long>> RawBags { get; set; }

        public List<Tuple<WeightedGraphNode, long>> Links { get; set; }

        public void updateToLinks(Dictionary<string, WeightedGraphNode> dict)
        {
            Links = new List<Tuple<WeightedGraphNode, long>>();
            WeightedGraphNode wgn;

            foreach(var tup in RawBags)
            {
                wgn = dict[tup.Item1];

                Links.Add(new Tuple<WeightedGraphNode, long>(wgn, tup.Item2));
            }
        }
    }
}
