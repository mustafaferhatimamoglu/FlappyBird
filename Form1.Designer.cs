namespace FlappyBird
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FP = new ScottPlot.WinForms.FormsPlot();
            SuspendLayout();
            // 
            // FP
            // 
            FP.DisplayScale = 1F;
            FP.Dock = DockStyle.Fill;
            FP.Location = new Point(0, 0);
            FP.Margin = new Padding(0);
            FP.Name = "FP";
            FP.Size = new Size(960, 540);
            FP.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 540);
            Controls.Add(FP);
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(0, 540);
            Name = "Form1";
            StartPosition = FormStartPosition.Manual;
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot FP;
    }
}
