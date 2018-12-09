using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCSharp
{
    public class CircularLinkListNode
    {
        public CircularLinkListNode Counterclockwise { get; set; }

        public CircularLinkListNode Clockwise { get; set; }

        public long Marble { get; set; }
    }
}
