using SharedTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchBuilder {
    public partial class PatchBuilder : Form {
        PatchProcessor patchProcessor;
        string defaultDescription = "Enter description here...";
        PatchLog log;
        public PatchBuilder() {
            InitializeComponent();
            log = new PatchLog();
            patchProcessor = new PatchProcessor();
            patchProcessor.UppendLog += log.UppendLog;
            patchProcessor.UpdateProgress += log.UpdateProgress;
            patchProcessor.Finished += () => {
                this.Invoke((Action)(() => {
                    SavePatch.Enabled = true;
                    SaveLog.Enabled = true;
                    ShowLog.Enabled = true;
                    log.Hide();
                    Update();
                }));
            };
            patchProcessor.FinishedError += (err) => {
                this.Invoke((Action)(() => {
                    SaveLog.Enabled = true;
                    ShowLog.Enabled = true;
                    log.Hide();
                    UnlockControls();
                    Update();
                    MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            };
        }


        private void SelectGameLogo_Click(object sender, EventArgs e) {
            var dialog = new OpenFileDialog {
                Title = "Choose image for logo",
                Filter = "Image files (*.png, *.jpg, *.gif, *.bmp)|*.png;*.jpg;*.gif;*.bmp|All files (*.*)|*.*",
                FilterIndex = 0,
            };
            if (dialog.ShowDialog() == DialogResult.OK) {
                patchProcessor.LogoPath = dialog.FileName;
                GameLogo.Load(dialog.FileName);
                GameLogoName.Text = dialog.FileName;
                SelectSourceFolder.Enabled = true;
            }
        }

        private void SelectSourceFolder_Click(object sender, EventArgs e) {
            var dialog = new FolderSelectDialog();
            dialog.Title = "Choose folder containing source images";
            dialog.InitialDirectory = Path.GetDirectoryName(patchProcessor.LogoPath);
            if (dialog.Show(Handle)) {
                if (!patchProcessor.AddSource(dialog.FileName, out string err)) {
                    MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SourceFolderName.Text = dialog.FileName;
                SelectFolderPatched.Enabled = true;
            }
        }

        private void SelectFolderPatched_Click(object sender, EventArgs e) {
            var dialog = new FolderSelectDialog();
            dialog.Title = "Choose folder containing patched images";
            dialog.InitialDirectory = patchProcessor.SourceFolder;
            if (dialog.Show(Handle)) {
                if (patchProcessor.SourceFolder == dialog.FileName) {
                    MessageBox.Show("Patched must be different from source", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!patchProcessor.AddPatched(dialog.FileName, out string err)) {
                    MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                PatchedFolderName.Text = dialog.FileName;
                MakePatch.Enabled = true;
            }
        }

        private void MakePatch_Click(object sender, EventArgs e) {
            if (GameDescription.Text == defaultDescription) {
                patchProcessor.Description = "";
            } else {
                patchProcessor.Description = GameDescription.Text;
            }
            LockControls();
            //this.Hide();
            patchProcessor.Run();
            log.Show();
        }

        private void LockControls() {
            GameDescription.Enabled = false;
            SelectGameLogo.Enabled = false;
            SelectFolderPatched.Enabled = false;
            SelectSourceFolder.Enabled = false;
            MakePatch.Enabled = false;
        }

        private void UnlockControls() {
            GameDescription.Enabled = true;
            SelectGameLogo.Enabled = true;
            SelectFolderPatched.Enabled = true;
            SelectSourceFolder.Enabled = true;
        }

        private void ShowLog_Click(object sender, EventArgs e) {
            //MessageBox.Show(log.LogData.Text);
            log.KeyPreview = true;
            log.Show();
        }

        private void SaveLog_Click(object sender, EventArgs e) {
            var dialog = new SaveFileDialog();
            dialog.Title = "Save log to text file";
            dialog.DefaultExt = "txt";
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK) {
                using (var fs = dialog.OpenFile()) {
                    var data = Encoding.ASCII.GetBytes(log.LogData.Text);
                    fs.Write(data, 0, data.Length);
                }
            }
        }

        private void PatchBuilder_FormClosing(object sender, FormClosingEventArgs e) {
            log.Close();
        }

        private void SavePatch_Click(object sender, EventArgs e) {
            var dialog = new SaveFileDialog {
                Title = "Choose where to save patch",
                Filter = "Patch files (*.ptch)|*.ptch|All files (*.*)|*.*",
                FilterIndex = 0,
                DefaultExt = "ptch"
            };
            if (dialog.ShowDialog() == DialogResult.OK) {
                patchProcessor.Save(dialog.FileName);
            }
        }
    }
}
