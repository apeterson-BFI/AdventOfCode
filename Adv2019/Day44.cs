using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Adv2020
{
    public class Day44
    {
        public string[] lines;

        public Dictionary<int, Rule44> rules;

        public static Regex RuleRegex = new Regex(@"(\d+):");
        public static Regex ConstRegex = new Regex(@"(\d+): ""(\w)""");

        public Day44()
        {
            lines = DayInput.readDayLines(44, true);
            rules = new Dictionary<int, Rule44>();
        }

        public void setup1()
        {
            List<string> testLines = new List<string>();
            Match m;
            int index;
            string letter;
            Rule44 rule;

            string indexText;
            string rem;

            foreach(string line in lines)
            {
                m = ConstRegex.Match(line);
                
                if(m.Success)
                {
                    index = Int32.Parse(m.Groups[1].Value);
                    letter = m.Groups[2].Value;
                    rule = new Rule44() { processed = true, procString = letter };
                    rules.Add(index, rule);
                    continue;
                }

                m = RuleRegex.Match(line);

                if (m.Success)
                {
                    indexText = m.Groups[1].Value;
                    index = Int32.Parse(indexText);

                    rem = line.Substring(indexText.Length + 2);

                    rule = new Rule44();
                    rule.processed = false;

                    rule.options = rem.Split('|').Select(p => p.Split(' ').Where(o => o != "").Select(o => Int32.Parse(o)).ToList()).ToList();

                    rules.Add(index, rule);
                }
                else if (line == "")
                {
                    continue;
                }
                else
                {
                    testLines.Add(line);
                }
            }

            lines = testLines.ToArray();
        }

        public int getPart1Answer()
        {
            setup1();

            Rule44 rule;
            int index;
            List<string> alternates;

            List<KeyValuePair<int, Rule44>> ruleList;

            while(rules.Where(ru => !ru.Value.processed).Count() > 0)
            {
                ruleList = rules.ToList();

                for(int i = 0; i < ruleList.Count; i++)
                {
                    if (ruleList[i].Value.processed)
                        continue;

                    rule = ruleList[i].Value;
                    index = ruleList[i].Key;

                    if(rule.options.SelectMany(rp => rp).All(ind => rules[ind].processed))
                    {
                        alternates = rule.options.Select(clist => string.Join("", clist.Select(c => rules[c].procString))).ToList();
                        rule.procString = "(" + string.Join("|", alternates) + ")";
                        rule.processed = true;
                        rules[index] = rule;
                    }
                }
            }

            rule = rules[0];
            Regex r = new Regex("^" + rule.procString + "$");

            return lines.Where(li => r.IsMatch(li)).Count();
        }
        
        public int getPart2Answer()
        {
            setup1();

            Rule44 rule;
            int index;
            List<string> alternates;

            List<KeyValuePair<int, Rule44>> ruleList;

            while (rules.Where(ru => !ru.Value.processed).Count() > 0)
            {
                ruleList = rules.ToList();

                for (int i = 0; i < ruleList.Count; i++)
                {
                    if (ruleList[i].Value.processed)
                        continue;

                    rule = ruleList[i].Value;
                    index = ruleList[i].Key;

                    // 8 : 42 - 42 8 
                    // 8 -- any number of 42s
                    // 11 -- any number of 42s followed by the same number of 31s. balancing group

                    if (index == 8)
                    {
                        if(rules[42].processed)
                        {
                            rule.procString = "(" + rules[42].procString + "+)";
                            rule.processed = true;
                        }
                    }
                    else if(index == 11)
                    {
                        if(rules[42].processed && rules[31].processed)
                        {
                            rule.procString = string.Format(@"((?<r42>{0})+(?<r31-r42>{1})+)", rules[42].procString, rules[31].procString);
                            rule.processed = true;
                        }
                    }
                    else if (rule.options.SelectMany(rp => rp).All(ind => rules[ind].processed))
                    {
                        alternates = rule.options.Select(clist => string.Join("", clist.Select(c => rules[c].procString))).ToList();
                        rule.procString = "(" + string.Join("|", alternates) + ")";
                        rule.processed = true;
                        rules[index] = rule;
                    }
                }
            }

            rule = rules[0];
            Regex r = new Regex("^" + rule.procString + "$");

            return lines.Where(li => r.IsMatch(li)).Count();
        }
    }

    public class Rule44
    {
        public bool processed;

        public string procString;

        public List<List<int>> options;
    }

}
