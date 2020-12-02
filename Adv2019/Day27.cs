using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Adv2020
{

    public class Day27
    {
        public static Regex PasswordPolicyMask = new Regex("([0-9]*)-([0-9]*) ([a-z]): ([a-z]*)");

        public string[] lines;

        public List<PasswordPolicy> policies;


        public Day27()
        {
            lines = DayInput.readDayLines(27);

            policies = new List<PasswordPolicy>();

            Match m;
            PasswordPolicy p;

            foreach(var line in lines)
            {
                m = PasswordPolicyMask.Match(line);

                p = new PasswordPolicy()
                {
                    LowerBound = Int32.Parse(m.Groups[1].Value),
                    UpperBound = Int32.Parse(m.Groups[2].Value),
                    Letter = m.Groups[3].Value,
                    Password = m.Groups[4].Value
                };

                policies.Add(p);
            }
        }

        public int getPart1Answer()
        {
            return
                policies.Count(p => p.P1Valid);
        }

        public int getPart2Answer()
        {
            return policies.Count(p => p.P2Valid);
        }
    }

    public class PasswordPolicy
    {
        public int LowerBound { get; set; }

        public int UpperBound { get; set; }

        public string Letter { get; set; }

        public string Password { get; set; }

        public bool P1Valid
        {
            get
            {
                int match = 0;

                for(int i = 0; i < Password.Length; i++)
                {
                    if (Password.Substring(i, 1) == Letter)
                        match++;
                }

                return (match >= LowerBound && match <= UpperBound);
            }
        }

        public bool P2Valid
        {
            get
            {
                int match = 0;

                if (Password.Substring(LowerBound - 1, 1) == Letter)
                    match++;

                if (Password.Substring(UpperBound - 1, 1) == Letter)
                    match++;

                return (match == 1);
            }
        }
    }
}
