namespace PatchBuilder {
    partial class PatchLog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.LogData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 802);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1108, 50);
            this.progressBar1.TabIndex = 10;
            this.progressBar1.Value = 1;
            // 
            // LogData
            // 
            this.LogData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogData.Location = new System.Drawing.Point(0, 0);
            this.LogData.Multiline = true;
            this.LogData.Name = "LogData";
            this.LogData.ReadOnly = true;
            this.LogData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogData.Size = new System.Drawing.Size(1108, 802);
            this.LogData.TabIndex = 11;
            // 
            // PatchLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 852);
            this.ControlBox = false;
            this.Controls.Add(this.LogData);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "PatchLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PatchLog - Press ESC to hide";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatchLog_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PatchLog_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        internal System.Windows.Forms.TextBox LogData;
    }
}