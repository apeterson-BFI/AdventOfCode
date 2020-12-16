using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Adv2020
{
    public class Day41
    {
        public static Regex rulePattern = new Regex(@"([\w\s]+): (\d+)\-(\d+) or (\d+)\-(\d+)");

        private string[] lines;

        public List<Rule> Rules { get; set; }

        public List<List<long>> tickets;
        public List<long> yourTicket;

        public Day41()
        {
            Rules = new List<Rule>();
            tickets = new List<List<long>>();
            
            lines = DayInput.readDayLines(41, true);
            parseLines();
        }

        public long getPart1Answer()
        {
            long totalinvalid = 0L;
            bool rowInvalid = false;

            bool valueValid = false;

            for(int tickIndex = tickets.Count - 1; tickIndex >= 0; tickIndex--)
            {
                rowInvalid = false;

                foreach(long value in tickets[tickIndex])
                {
                    valueValid = false;

                    foreach(Rule r in Rules)
                    {
                        if (value >= r.LowerBound1 && value <= r.UpperBound1)
                        {
                            valueValid = true;
                            break;
                        }
                        else if(value >= r.LowerBound2 && value <= r.UpperBound2)
                        {
                            valueValid = true;
                            break;
                        }
                    }

                    if(!valueValid)
                    {
                        rowInvalid = true;
                        totalinvalid += value;
                    }
                }

                if(rowInvalid)
                {
                    Console.WriteLine("Removing at {0}", tickIndex);
                    tickets.RemoveAt(tickIndex);
                }
            }

            return totalinvalid;
        }

        public long getPart2Answer()
        {
            throw new NotImplementedException();
        }


        private void parseLines()
        {
            bool yours = false;
            bool nearby = false;
            Match m;

            foreach (string line in lines)
            {
                if(line == "your ticket:")
                {
                    yours = true;
                }
                else if(line == "nearby tickets:")
                {
                    nearby = true;
                }
                else if(yours)
                {
                    yourTicket = DayInput.splitLineToLongCells(line, ',');
                    yours = false;
                }
                else if(nearby)
                {
                    tickets.Add(DayInput.splitLineToLongCells(line, ','));
                }
                else if((m = rulePattern.Match(line)).Success)
                {
                    string field = m.Groups[1].Value;

                    long lb1 = Int64.Parse(m.Groups[2].Value);
                    long ub1 = Int64.Parse(m.Groups[3].Value);
                    long lb2 = Int64.Parse(m.Groups[4].Value);
                    long ub2 = Int64.Parse(m.Groups[5].Value);

                    Rule rule = new Rule() { FieldName = field, LowerBound1 = lb1, UpperBound1 = ub1, LowerBound2 = lb2, UpperBound2 = ub2 };
                    Rules.Add(rule);
                }
                else
                {
                    continue;
                }
            }
        }

    }

    public class Rule
    {
        public string FieldName { get; set; }

        public long LowerBound1 { get; set; }

        public long UpperBound1 { get; set; }       // inclusive

        public long LowerBound2 { get; set; }

        public long UpperBound2 { get; set; }
    }
}
