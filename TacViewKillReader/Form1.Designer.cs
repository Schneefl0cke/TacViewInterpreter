
namespace TacViewKillReader
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
            this.analyzeTacView_button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // analyzeTacView_button
            // 
            this.analyzeTacView_button.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.analyzeTacView_button.Location = new System.Drawing.Point(34, 31);
            this.analyzeTacView_button.Name = "analyzeTacView_button";
            this.analyzeTacView_button.Size = new System.Drawing.Size(363, 62);
            this.analyzeTacView_button.TabIndex = 0;
            this.analyzeTacView_button.Text = "Analyze TacView";
            this.analyzeTacView_button.UseVisualStyleBackColor = false;
            this.analyzeTacView_button.Click += new System.EventHandler(this.analyzeTacView_button_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 139);
            this.Controls.Add(this.analyzeTacView_button);
            this.Name = "Form1";
            this.Text = "Tac View Analyzer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button analyzeTacView_button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

