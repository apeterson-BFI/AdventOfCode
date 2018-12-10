namespace AdventCSharp.Day10
{
    partial class ParticleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.pnlDraw = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnCl = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnBounding = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(3, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(34, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = ">";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(43, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(34, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "| |";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Location = new System.Drawing.Point(97, 2);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(34, 23);
            this.btnPlus.TabIndex = 2;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.Location = new System.Drawing.Point(137, 2);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(34, 23);
            this.btnMinus.TabIndex = 3;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // pnlDraw
            // 
            this.pnlDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDraw.Location = new System.Drawing.Point(13, 32);
            this.pnlDraw.Name = "pnlDraw";
            this.pnlDraw.Size = new System.Drawing.Size(1200, 800);
            this.pnlDraw.TabIndex = 4;
            this.pnlDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDraw_Paint);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnCl
            // 
            this.btnCl.Location = new System.Drawing.Point(195, 2);
            this.btnCl.Name = "btnCl";
            this.btnCl.Size = new System.Drawing.Size(34, 23);
            this.btnCl.TabIndex = 5;
            this.btnCl.Text = "CL";
            this.btnCl.UseVisualStyleBackColor = true;
            this.btnCl.Click += new System.EventHandler(this.btnCl_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(235, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(42, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "D/ND";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnBounding
            // 
            this.btnBounding.Location = new System.Drawing.Point(283, 2);
            this.btnBounding.Name = "btnBounding";
            this.btnBounding.Size = new System.Drawing.Size(42, 23);
            this.btnBounding.TabIndex = 7;
            this.btnBounding.Text = "[ ]";
            this.btnBounding.UseVisualStyleBackColor = true;
            this.btnBounding.Click += new System.EventHandler(this.btnBounding_Click);
            // 
            // ParticleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 849);
            this.Controls.Add(this.btnBounding);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnCl);
            this.Controls.Add(this.pnlDraw);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "ParticleForm";
            this.Text = "ParticleForm";
            this.Shown += new System.EventHandler(this.ParticleForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Panel pnlDraw;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnCl;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnBounding;
    }
}