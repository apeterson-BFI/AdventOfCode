using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public static class Parser
    {
        public static int parseInt(string val, int defaultVal)
        {
            int r;

            bool succ = Int32.TryParse(val, out r);

            if (!succ)
                r = defaultVal;

            return r;
        }

        public static int parseInt(string val, int defaultVal, int reqdLength)
        {
            if (val.Length != reqdLength)
                return defaultVal;
            else
                return parseInt(val, defaultVal);
        }

        public static long parseLong(string val, long defaultVal)
        {
            long r;

            bool succ = Int64.TryParse(val, out r);

            if (!succ)
                r = defaultVal;

            return r;
        }
    }
}
