using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Adv2020
{
    public class Day8
    {
        public List<Layer> Layers { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Day8(int width, int height)
        {
            Width = width;
            Height = height;

            string line = DayInput.readDayLine(8);
            char[] chars = line.ToCharArray();

            Layers = new List<Layer>();
            int trackIndex = 0;

            while(chars.Length >= trackIndex + width * height)
            {
                Layers.Add(new Layer(chars, trackIndex, height, width));
                trackIndex += width * height;
            }
        }

        public int getPart1Answer()
        {
            Layer minLayer = Layers.OrderBy(l => l.countDigit('0')).First();

            return (minLayer.countDigit('1') * minLayer.countDigit('2'));
        }

        public void getPart2Answer()
        {
            //    0 is black, 1 is white, and 2 is transparent.
            //    transparent blocks in lower layers allow the first non-transparent layer to be the color.

            Layer l = new Layer() { Rows = new Row[Height] };

            for(int i = 0; i < Height; i++)
            {
                l.Rows[i] = new Row();
                l.Rows[i].Cells = new char[Width];
            }

            for(int r = 0; r < Height; r++)
            {
                for(int c = 0; c < Width; c++)
                {
                    for(int li = 0; li < Layers.Count; li++)
                    {
                        if(Layers[li].Rows[r].Cells[c] == '0')
                        {
                            l.Rows[r].Cells[c] = '0';
                            break;
                        }
                        else if(Layers[li].Rows[r].Cells[c] == '1')
                        {
                            l.Rows[r].Cells[c] = '1';
                            break;
                        }
                    }
                }
            }

            using (var sw = File.CreateText("day8output.csv"))
            {
                for(int r = 0; r < l.Rows.Length; r++)
                {
                    sw.WriteLine(string.Join(",", l.Rows[r].Cells));
                }
            }
        }
    }

    public class Layer
    {
        public Row[] Rows { get; set; }

        public Layer()
        {

        }

        public Layer(char[] chars, int index, int height, int width)
        {
            Rows = new Row[height];
            int trackIndex = index;

            for(int i = 0; i < height; i++)
            {
                Rows[i] = new Row(chars, trackIndex, width);
                trackIndex += width;
            }
        }

        public int countDigit(char val)
        {
            return Rows.Sum(r => r.countDigit(val));
        }
    }

    public class Row
    {
        public char[] Cells { get; set; }

        public Row()
        {

        }

        public Row(char[] chars, int index, int width)
        {
            Cells = new char[width];

            for(int i = 0; i < width; i++)
            {
                Cells[i] = chars[index + i];
            }
        }

        public int countDigit(char val)
        {
            return Cells.Count(c => c == val);
        }
    }
}
