using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2019
{
    public class DirGraphNode<T>
    {
        public T Content { get; set; }

        public List<DirGraphNode<T>> Connections { get; set; }

        public DirGraphNode(T content)
        {
            Content = content;
            Connections = new List<DirGraphNode<T>>();
        }

        public int countAllConnected()
        {
            int count = Connections.Count;

            return count + Connections.Sum(c => c.countAllConnected());
        }
    }
}
