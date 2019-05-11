namespace PatchBuilder {
    partial class PatchBuilder {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatchBuilder));
            this.GameLogo = new System.Windows.Forms.PictureBox();
            this.GameDescription = new System.Windows.Forms.TextBox();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.SelectGameLogo = new System.Windows.Forms.Button();
            this.GameLogoName = new System.Windows.Forms.Label();
            this.SelectSourceFolder = new System.Windows.Forms.Button();
            this.SelectFolderPatched = new System.Windows.Forms.Button();
            this.SourceFolderName = new System.Windows.Forms.Label();
            this.MakePatch = new System.Windows.Forms.Button();
            this.PatchedFolderName = new System.Windows.Forms.Label();
            this.SavePatch = new System.Windows.Forms.Button();
            this.ShowLog = new System.Windows.Forms.Button();
            this.SaveLog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GameLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // GameLogo
            // 
            this.GameLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.GameLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameLogo.Location = new System.Drawing.Point(13, 13);
            this.GameLogo.Name = "GameLogo";
            this.GameLogo.Size = new System.Drawing.Size(820, 230);
            this.GameLogo.TabIndex = 0;
            this.GameLogo.TabStop = false;
            this.ToolTips.SetToolTip(this.GameLogo, "Click to choose picture (410x115)");
            // 
            // GameDescription
            // 
            this.GameDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameDescription.Location = new System.Drawing.Point(13, 250);
            this.GameDescription.Multiline = true;
            this.GameDescription.Name = "GameDescription";
            this.GameDescription.Size = new System.Drawing.Size(820, 250);
            this.GameDescription.TabIndex = 1;
            this.GameDescription.Text = "Enter description here...";
            // 
            // SelectGameLogo
            // 
            this.SelectGameLogo.Location = new System.Drawing.Point(840, 13);
            this.SelectGameLogo.Name = "SelectGameLogo";
            this.SelectGameLogo.Size = new System.Drawing.Size(737, 50);
            this.SelectGameLogo.TabIndex = 2;
            this.SelectGameLogo.Text = "1. Select picture (410x115)";
            this.SelectGameLogo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectGameLogo.UseVisualStyleBackColor = true;
            this.SelectGameLogo.Click += new System.EventHandler(this.SelectGameLogo_Click);
            // 
            // GameLogoName
            // 
            this.GameLogoName.Location = new System.Drawing.Point(840, 69);
            this.GameLogoName.Name = "GameLogoName";
            this.GameLogoName.Size = new System.Drawing.Size(736, 50);
            this.GameLogoName.TabIndex = 3;
            this.GameLogoName.Text = "...";
            this.GameLogoName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // SelectSourceFolder
            // 
            this.SelectSourceFolder.Location = new System.Drawing.Point(839, 131);
            this.SelectSourceFolder.Name = "SelectSourceFolder";
            this.SelectSourceFolder.Size = new System.Drawing.Size(737, 50);
            this.SelectSourceFolder.TabIndex = 4;
            this.SelectSourceFolder.Text = "2. Select folder with source images";
            this.SelectSourceFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectSourceFolder.UseVisualStyleBackColor = true;
            this.SelectSourceFolder.Click += new System.EventHandler(this.SelectSourceFolder_Click);
            // 
            // SelectFolderPatched
            // 
            this.SelectFolderPatched.Enabled = false;
            this.SelectFolderPatched.Location = new System.Drawing.Point(840, 250);
            this.SelectFolderPatched.Name = "SelectFolderPatched";
            this.SelectFolderPatched.Size = new System.Drawing.Size(737, 50);
            this.SelectFolderPatched.TabIndex = 6;
            this.SelectFolderPatched.Text = "3. Select folder with patched images";
            this.SelectFolderPatched.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectFolderPatched.UseVisualStyleBackColor = true;
            this.SelectFolderPatched.Click += new System.EventHandler(this.SelectFolderPatched_Click);
            // 
            // SourceFolderName
            // 
            this.SourceFolderName.Location = new System.Drawing.Point(841, 188);
            this.SourceFolderName.Name = "SourceFolderName";
            this.SourceFolderName.Size = new System.Drawing.Size(735, 50);
            this.SourceFolderName.TabIndex = 5;
            this.SourceFolderName.Text = "...";
            this.SourceFolderName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // MakePatch
            // 
            this.MakePatch.Enabled = false;
            this.MakePatch.Location = new System.Drawing.Point(839, 369);
            this.MakePatch.Name = "MakePatch";
            this.MakePatch.Size = new System.Drawing.Size(737, 50);
            this.MakePatch.TabIndex = 8;
            this.MakePatch.Text = "4. Make Patch";
            this.MakePatch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MakePatch.UseVisualStyleBackColor = true;
            this.MakePatch.Click += new System.EventHandler(this.MakePatch_Click);
            // 
            // PatchedFolderName
            // 
            this.PatchedFolderName.Location = new System.Drawing.Point(840, 307);
            this.PatchedFolderName.Name = "PatchedFolderName";
            this.PatchedFolderName.Size = new System.Drawing.Size(737, 50);
            this.PatchedFolderName.TabIndex = 7;
            this.PatchedFolderName.Text = "...";
            this.PatchedFolderName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // SavePatch
            // 
            this.SavePatch.Enabled = false;
            this.SavePatch.Location = new System.Drawing.Point(839, 446);
            this.SavePatch.Name = "SavePatch";
            this.SavePatch.Size = new System.Drawing.Size(174, 50);
            this.SavePatch.TabIndex = 9;
            this.SavePatch.Text = "5. Save Patch";
            this.SavePatch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SavePatch.UseVisualStyleBackColor = true;
            this.SavePatch.Click += new System.EventHandler(this.SavePatch_Click);
            // 
            // ShowLog
            // 
            this.ShowLog.Enabled = false;
            this.ShowLog.Location = new System.Drawing.Point(1030, 446);
            this.ShowLog.Name = "ShowLog";
            this.ShowLog.Size = new System.Drawing.Size(174, 50);
            this.ShowLog.TabIndex = 10;
            this.ShowLog.Text = "Show log";
            this.ShowLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ShowLog.UseVisualStyleBackColor = true;
            this.ShowLog.Click += new System.EventHandler(this.ShowLog_Click);
            // 
            // SaveLog
            // 
            this.SaveLog.Enabled = false;
            this.SaveLog.Location = new System.Drawing.Point(1219, 446);
            this.SaveLog.Name = "SaveLog";
            this.SaveLog.Size = new System.Drawing.Size(174, 50);
            this.SaveLog.TabIndex = 11;
            this.SaveLog.Text = "Save log";
            this.SaveLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveLog.UseVisualStyleBackColor = true;
            this.SaveLog.Click += new System.EventHandler(this.SaveLog_Click);
            // 
            // PatchBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1590, 508);
            this.Controls.Add(this.SaveLog);
            this.Controls.Add(this.ShowLog);
            this.Controls.Add(this.SavePatch);
            this.Controls.Add(this.MakePatch);
            this.Controls.Add(this.PatchedFolderName);
            this.Controls.Add(this.SelectFolderPatched);
            this.Controls.Add(this.SourceFolderName);
            this.Controls.Add(this.SelectSourceFolder);
            this.Controls.Add(this.GameLogoName);
            this.Controls.Add(this.SelectGameLogo);
            this.Controls.Add(this.GameDescription);
            this.Controls.Add(this.GameLogo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PatchBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PatchBuilder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatchBuilder_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.GameLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox GameLogo;
        private System.Windows.Forms.TextBox GameDescription;
        private System.Windows.Forms.ToolTip ToolTips;
        private System.Windows.Forms.Button SelectGameLogo;
        private System.Windows.Forms.Label GameLogoName;
        private System.Windows.Forms.Button SelectSourceFolder;
        private System.Windows.Forms.Button SelectFolderPatched;
        private System.Windows.Forms.Label SourceFolderName;
        private System.Windows.Forms.Button MakePatch;
        private System.Windows.Forms.Label PatchedFolderName;
        private System.Windows.Forms.Button SavePatch;
        private System.Windows.Forms.Button ShowLog;
        private System.Windows.Forms.Button SaveLog;
    }
}

