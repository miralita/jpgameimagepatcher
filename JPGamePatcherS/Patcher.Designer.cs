namespace JPGamePatcherS {
    partial class Patcher {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Patcher));
            this.GameLogo = new System.Windows.Forms.PictureBox();
            this.GameDescription = new System.Windows.Forms.Label();
            this.SelectPatch = new System.Windows.Forms.Button();
            this.SelectSource = new System.Windows.Forms.Button();
            this.SelectDestination = new System.Windows.Forms.Button();
            this.Patch = new System.Windows.Forms.Button();
            this.PatchPath = new System.Windows.Forms.Label();
            this.SourceFolder = new System.Windows.Forms.Label();
            this.DestinationFolder = new System.Windows.Forms.Label();
            this.Progress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.GameLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // GameLogo
            // 
            this.GameLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.GameLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameLogo.Location = new System.Drawing.Point(12, 12);
            this.GameLogo.Name = "GameLogo";
            this.GameLogo.Size = new System.Drawing.Size(820, 230);
            this.GameLogo.TabIndex = 1;
            this.GameLogo.TabStop = false;
            // 
            // GameDescription
            // 
            this.GameDescription.Location = new System.Drawing.Point(12, 249);
            this.GameDescription.Name = "GameDescription";
            this.GameDescription.Size = new System.Drawing.Size(820, 114);
            this.GameDescription.TabIndex = 2;
            this.GameDescription.Text = "...";
            this.GameDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SelectPatch
            // 
            this.SelectPatch.Location = new System.Drawing.Point(12, 386);
            this.SelectPatch.Name = "SelectPatch";
            this.SelectPatch.Size = new System.Drawing.Size(287, 50);
            this.SelectPatch.TabIndex = 3;
            this.SelectPatch.Text = "1. Open Patch";
            this.SelectPatch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectPatch.UseVisualStyleBackColor = true;
            this.SelectPatch.Click += new System.EventHandler(this.SelectPatch_Click);
            // 
            // SelectSource
            // 
            this.SelectSource.Enabled = false;
            this.SelectSource.Location = new System.Drawing.Point(12, 492);
            this.SelectSource.Name = "SelectSource";
            this.SelectSource.Size = new System.Drawing.Size(287, 50);
            this.SelectSource.TabIndex = 4;
            this.SelectSource.Text = "2. Select Source Folder";
            this.SelectSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectSource.UseVisualStyleBackColor = true;
            this.SelectSource.Click += new System.EventHandler(this.SelectSource_Click);
            // 
            // SelectDestination
            // 
            this.SelectDestination.Enabled = false;
            this.SelectDestination.Location = new System.Drawing.Point(12, 598);
            this.SelectDestination.Name = "SelectDestination";
            this.SelectDestination.Size = new System.Drawing.Size(287, 50);
            this.SelectDestination.TabIndex = 5;
            this.SelectDestination.Text = "3. Select Destination Folder";
            this.SelectDestination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectDestination.UseVisualStyleBackColor = true;
            this.SelectDestination.Click += new System.EventHandler(this.SelectDestination_Click);
            // 
            // Patch
            // 
            this.Patch.Enabled = false;
            this.Patch.Location = new System.Drawing.Point(12, 704);
            this.Patch.Name = "Patch";
            this.Patch.Size = new System.Drawing.Size(287, 50);
            this.Patch.TabIndex = 6;
            this.Patch.Text = "4. GO!";
            this.Patch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Patch.UseVisualStyleBackColor = true;
            this.Patch.Click += new System.EventHandler(this.Patch_Click);
            // 
            // PatchPath
            // 
            this.PatchPath.Location = new System.Drawing.Point(12, 439);
            this.PatchPath.Name = "PatchPath";
            this.PatchPath.Size = new System.Drawing.Size(820, 50);
            this.PatchPath.TabIndex = 7;
            this.PatchPath.Text = "...";
            this.PatchPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SourceFolder
            // 
            this.SourceFolder.Location = new System.Drawing.Point(12, 545);
            this.SourceFolder.Name = "SourceFolder";
            this.SourceFolder.Size = new System.Drawing.Size(818, 50);
            this.SourceFolder.TabIndex = 8;
            this.SourceFolder.Text = "...";
            this.SourceFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DestinationFolder
            // 
            this.DestinationFolder.Location = new System.Drawing.Point(12, 651);
            this.DestinationFolder.Name = "DestinationFolder";
            this.DestinationFolder.Size = new System.Drawing.Size(818, 50);
            this.DestinationFolder.TabIndex = 9;
            this.DestinationFolder.Text = "...";
            this.DestinationFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(12, 771);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(820, 50);
            this.Progress.TabIndex = 10;
            // 
            // Patcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 836);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.DestinationFolder);
            this.Controls.Add(this.SourceFolder);
            this.Controls.Add(this.PatchPath);
            this.Controls.Add(this.Patch);
            this.Controls.Add(this.SelectDestination);
            this.Controls.Add(this.SelectSource);
            this.Controls.Add(this.SelectPatch);
            this.Controls.Add(this.GameDescription);
            this.Controls.Add(this.GameLogo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Patcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Japanese Game Patcher (simple)";
            this.Load += new System.EventHandler(this.Patcher_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GameLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox GameLogo;
        private System.Windows.Forms.Label GameDescription;
        private System.Windows.Forms.Button SelectPatch;
        private System.Windows.Forms.Button SelectSource;
        private System.Windows.Forms.Button SelectDestination;
        private System.Windows.Forms.Button Patch;
        private System.Windows.Forms.Label PatchPath;
        private System.Windows.Forms.Label SourceFolder;
        private System.Windows.Forms.Label DestinationFolder;
        private System.Windows.Forms.ProgressBar Progress;
    }
}

