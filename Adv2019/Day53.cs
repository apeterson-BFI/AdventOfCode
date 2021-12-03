using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv2020
{
    public class Day53
    {
        public List<UInt32> dayLines;

        public UInt32 gamma;
        public UInt32 epsilon;
        public UInt32 oxygen;
        public UInt32 co2;

        public Day53()
        {
            gamma = 0u;
            epsilon = 0u;
            oxygen = 0u;
            co2 = 0u;

            dayLines = DayInput.readBinaryDayLine(53, true);
        }

        public UInt32 doPart1()
        {
            gamma = 0u;
            epsilon = 0u;
            UInt32 control = 0x800u;
            UInt32 negcontrol = ~control;
            int count0 = 0;
            int count1 = 0;
            UInt32 v;

            while(control >= 1u)
            {
                count0 = 0;
                count1 = 0;

                foreach(UInt32 u in dayLines)
                {
                    v = u & control;

                    if (v == 0u)
                        count0++;
                    else
                        count1++;
                }

                if(count1 > count0)
                {
                    gamma = gamma | control;
                    epsilon = epsilon & negcontrol;
                }    
                else
                {
                    gamma = gamma & negcontrol;
                    epsilon = epsilon | control;
                }

                control = control >> 1;
                negcontrol = ~control;
            }

            return gamma * epsilon;
        }

        public UInt32 doPart2()
        {
            UInt32 control = 0x800u;
            UInt32 negcontrol = ~control;
            int count0 = 0;
            int count1 = 0;
            UInt32 v;

            List<UInt32> oxLines = new List<uint>(dayLines);

            while (control >= 1u && oxLines.Count > 0)
            {
                if(oxLines.Count == 1)
                {
                    oxygen = oxLines[0];
                }

                count0 = 0;
                count1 = 0;

                foreach (UInt32 u in oxLines)
                {
                    v = u & control;

                    if (v == 0u)
                        count0++;
                    else
                        count1++;
                }

                if (count1 >= count0)
                {
                    oxLines = oxLines.Where(x => (x & control) != 0u).ToList();
                }
                else
                {
                    oxLines = oxLines.Where(x => (x & control) == 0u).ToList();
                }

                control = control >> 1;
                negcontrol = ~control;
            }

            if (oxLines.Count == 1)
                oxygen = oxLines[0];

            control = 0x800u;
            List<UInt32> co2Lines = new List<uint>(dayLines);

            while (control >= 1u && oxLines.Count > 0)
            {
                if (co2Lines.Count == 1)
                {
                    co2 = co2Lines[0];
                }

                count0 = 0;
                count1 = 0;

                foreach (UInt32 u in co2Lines)
                {
                    v = u & control;

                    if (v == 0u)
                        count0++;
                    else
                        count1++;
                }

                if (count1 >= count0)
                {
                    co2Lines = co2Lines.Where(x => (x & control) == 0u).ToList();
                }
                else
                {
                    co2Lines = co2Lines.Where(x => (x & control) != 0u).ToList();
                }

                control = control >> 1;
                negcontrol = ~control;
            }

            if (co2Lines.Count == 1)
                co2 = co2Lines[0];

            return oxygen * co2;
        }

    }
}
