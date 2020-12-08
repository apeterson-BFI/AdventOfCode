using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day31
    {
        public List<List<string>> paragraphs;

        public Day31()
        {
            paragraphs = DayInput.readDayLinesAsParagraphs(31, true);
        }

        public long getPart1Answer()
        {
            return paragraphs.Select(p => p.SelectMany(s => s.ToCharArray()).ToHashSet().Count).Sum();
        }

        public long getIntersectionCount(IEnumerable<char[]> chsets)
        {
            var elements = chsets.SelectMany(c => c).Distinct();

            return
                elements.Where(e => chsets.All(ca => ca.Contains(e))).Count();
        }

        public long getPart2Answer()
        {
            return
                paragraphs.Select(p => p.Select(s => s.ToCharArray())).Sum(getIntersectionCount);
        }
    }
}
