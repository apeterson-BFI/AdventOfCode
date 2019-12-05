using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public static class NumTextUtil
    {
        private static Dictionary<char, int> charInts = new Dictionary<char, int>() { {'0', 0}, {'1',1}, {'2',2}, {'3',3}, {'4',4}, {'5', 5}, {'6', 6}, {'7',7}, {'8',8}, {'9',9} };

        public static int extractPlace(int num, int power)
        {
            string numText = num.ToString();
            int numLength = numText.Length;
            int targetIndex = numLength - 1 - power;

            if (targetIndex < 0)
            {
                return 0;
            }
            else
            {
                char digit = numText[targetIndex];

                return charInts[digit];
            }
        }


    }
}
