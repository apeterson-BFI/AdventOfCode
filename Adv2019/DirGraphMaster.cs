using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class DirGraphMaster<T> where T : IEquatable<T>
    {
        public Dictionary<T, DirGraphNode<T>> Nodes { get; set; }

        public DirGraphMaster()
        {
            Nodes = new Dictionary<T, DirGraphNode<T>>();
        }

        public void connect(T from, T to)
        {
            if(!Nodes.ContainsKey(from))
            {
                Nodes.Add(from, new DirGraphNode<T>(from));
            }

            if(!Nodes.ContainsKey(to))
            {
                Nodes.Add(to, new DirGraphNode<T>(to));
            }

            Nodes[from].Connections.Add(Nodes[to]);
        }

        public void connectBI(T node1, T node2)
        {
            connect(node1, node2);
            connect(node2, node1);
        }

        public int distance(T source, T destination)
        {
            if(!Nodes.ContainsKey(source) || !Nodes.ContainsKey(destination))
            {
                throw new ArgumentException();
            }

            DirGraphNode<T> sourceNode = Nodes[source];
            DirGraphNode<T> destNode = Nodes[destination];

            return distSearch(sourceNode, destNode, new HashSet<T>());
        }

        private int distSearch(DirGraphNode<T> sourceNode, DirGraphNode<T> destNode, HashSet<T> visited)
        {
            if (EqualityComparer<T>.Default.Equals(sourceNode.Content, destNode.Content))
                return 0;

            visited.Add(sourceNode.Content);

            int minimum = Int32.MaxValue;
            int dist;

            foreach (DirGraphNode<T> n in sourceNode.Connections)
            {
                if(!visited.Contains(n.Content))
                {
                    dist = distSearch(n, destNode, new HashSet<T>(visited));

                    if (dist < minimum)
                        minimum = dist;
                }
            }

            if (minimum == Int32.MaxValue)
                return Int32.MaxValue;
            else
                return minimum + 1;
        }
    }
}
