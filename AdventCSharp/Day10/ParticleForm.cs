using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventCSharp.Day10
{
    public partial class ParticleForm : Form
    {
        public int Time { get; set; }

        public ParticleSystem PartSystem { get; set; }

        private Graphics grps;

        private DrawingMode drawingMode;

        public ParticleForm()
        {
            InitializeComponent();

            Time = 7991;
            PartSystem = new ParticleSystem();

            grps = pnlDraw.CreateGraphics();
            drawingMode = DrawingMode.Play;   
        }

        // Drawable area x : 0 - 1199, y : 0 - 799 
        public void draw()
        {
            switch (drawingMode)
            {
                case DrawingMode.Play:
                    Time++;
                    break;
                case DrawingMode.StepBack:
                    Time--;
                    break;
                case DrawingMode.StepForward:
                    Time++;
                    break;
                case DrawingMode.Stop:
                    break;
            }

            if (drawingMode == DrawingMode.Stop)
                return;

            PartSystem.updateAtTime(Time);

            if (drawingMode == DrawingMode.NoDraw)
                return;

            PartSystem.draw(grps, 1199, 799);
        }

        private void pnlDraw_Paint(object sender, PaintEventArgs e)
        {
            if (drawingMode == DrawingMode.Play || drawingMode == DrawingMode.NoDraw)
            {
                draw();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(drawingMode == DrawingMode.Play || drawingMode == DrawingMode.NoDraw)
            {
                draw();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.Play;
            pnlDraw.Refresh();
            timer1.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.Stop;
            timer1.Stop();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.StepForward;
            draw();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.StepBack;
            draw();
        }

        private void ParticleForm_Shown(object sender, EventArgs e)
        {
            PartSystem.refreshBounding();
            timer1.Start();
        }

        private void btnCl_Click(object sender, EventArgs e)
        {
            grps.Clear(SystemColors.Control);
            pnlDraw.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(drawingMode == DrawingMode.NoDraw)
            {
                drawingMode = DrawingMode.Play;
            }
            else
            {
                drawingMode = DrawingMode.NoDraw;
            }
        }

        private void btnBounding_Click(object sender, EventArgs e)
        {
            PartSystem.refreshBounding();
            grps.Clear(SystemColors.Control);
        }
    }

    public enum DrawingMode
    {
        Play,
        StepForward,
        StepBack,
        Stop,
        NoDraw
    }
}
