namespace Adv2020
{
    partial class ArcadeCabinet
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
            this.pnlViewer = new System.Windows.Forms.Panel();
            this.updTimer = new System.Windows.Forms.Timer(this.components);
            this.txtScore = new System.Windows.Forms.TextBox();
            this.txtSteps = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlViewer
            // 
            this.pnlViewer.Location = new System.Drawing.Point(3, 2);
            this.pnlViewer.Name = "pnlViewer";
            this.pnlViewer.Size = new System.Drawing.Size(920, 470);
            this.pnlViewer.TabIndex = 0;
            this.pnlViewer.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlViewer_Paint);
            // 
            // updTimer
            // 
            this.updTimer.Interval = 5;
            this.updTimer.Tick += new System.EventHandler(this.updTimer_Tick);
            // 
            // txtScore
            // 
            this.txtScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScore.Location = new System.Drawing.Point(813, 478);
            this.txtScore.Name = "txtScore";
            this.txtScore.Size = new System.Drawing.Size(100, 21);
            this.txtScore.TabIndex = 1;
            // 
            // txtSteps
            // 
            this.txtSteps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSteps.Location = new System.Drawing.Point(12, 478);
            this.txtSteps.Name = "txtSteps";
            this.txtSteps.Size = new System.Drawing.Size(100, 21);
            this.txtSteps.TabIndex = 2;
            this.txtSteps.Text = "5";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(442, 478);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Start Over";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ArcadeCabinet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 510);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtSteps);
            this.Controls.Add(this.txtScore);
            this.Controls.Add(this.pnlViewer);
            this.Name = "ArcadeCabinet";
            this.Text = "ArcadeCabinet";
            this.Shown += new System.EventHandler(this.ArcadeCabinet_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlViewer;
        private System.Windows.Forms.Timer updTimer;
        private System.Windows.Forms.TextBox txtScore;
        private System.Windows.Forms.TextBox txtSteps;
        private System.Windows.Forms.Button btnReset;
    }
}