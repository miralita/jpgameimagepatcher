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

namespace JPGamePatcherS {
    public partial class Patcher : Form {
        PatchProcessor patchProcessor;
        string initialDirectory = "";
        public Patcher() {
            InitializeComponent();
        }

        private void Patcher_Load(object sender, EventArgs e) {
            patchProcessor = new PatchProcessor();
            patchProcessor.UpdateProgress += UpdateProgress;
            patchProcessor.Finished += Finished;
            patchProcessor.FinishedError += FinishedError;
        }

        private void SelectPatch_Click(object sender, EventArgs e) {
            var dialog = new OpenFileDialog {
                Title = "Choose patch",
                Filter = "Patch files (*.ptch)|*.ptch|All files (*.*)|*.*",
                FilterIndex = 0,
                DefaultExt = "ptch"
            };
            if (dialog.ShowDialog() == DialogResult.OK) {
                try {
                    patchProcessor.LoadPatch(dialog.FileName);
                    GameDescription.Text = patchProcessor.GameDescription;
                    GameLogo.Image = patchProcessor.GameLogo;
                    initialDirectory = Path.GetDirectoryName(dialog.FileName);
                    SelectSource.Enabled = true;
                    PatchPath.Text = dialog.FileName;
                } catch (Exception ex) {
                    MessageBox.Show($"Can't load patch file: {ex.Message}. Please select another file", "Can't load patch file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SelectSource_Click(object sender, EventArgs e) {
            var dialog = new FolderSelectDialog {
                Title = "Choose folder containing source images",
                InitialDirectory = initialDirectory
            };
            if (dialog.Show(Handle)) {
                try {
                    patchProcessor.SetSourceFolder(dialog.FileName);
                    SourceFolder.Text = dialog.FileName;
                    SelectDestination.Enabled = true;
                } catch (Exception ex) {
                    MessageBox.Show($"Can't load source folder: {ex.Message}. Please select another one", "Can't load source folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RestoreInitialState() {
            GameLogo.Image = null;
            GameDescription.Text = "";
            SelectSource.Enabled = false;
            SelectPatch.Enabled = true;
            PatchPath.Text = "...";
            SourceFolder.Text = "...";
            SelectDestination.Enabled = false;
            DestinationFolder.Text = "...";
            Patch.Enabled = false;
            Progress.Value = 0;
        }

        private void SelectDestination_Click(object sender, EventArgs e) {
            var dialog = new FolderSelectDialog {
                Title = "Choose folder to save patched images",
                InitialDirectory = initialDirectory
            };
            if (dialog.Show(Handle)) {
                patchProcessor.SetDestinationFolder(dialog.FileName);
                if (patchProcessor.NeedOverwrite) {
                    var res = MessageBox.Show("Destination folder is not empty and contains files named as source files. Overwrite this files?", "Destination folder is not empty", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (res == DialogResult.Cancel) return;
                }
                DestinationFolder.Text = dialog.FileName;
                Patch.Enabled = true;
            }
        }

        private void Patch_Click(object sender, EventArgs e) {
            Progress.Maximum = patchProcessor.TotalFiles;
            SelectPatch.Enabled = false;
            SelectSource.Enabled = false;
            SelectDestination.Enabled = false;
            Patch.Enabled = false;
            
            patchProcessor.Patch();
        }

        private void UpdateProgress(int n) {
            BeginInvoke((Action)(() => Progress.Value = n));
        }

        private void FinishedError(string err) {
            BeginInvoke((Action)(() => {
                MessageBox.Show(err, "Patch interrupted with error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SelectPatch.Enabled = true;
                SelectSource.Enabled = true;
                SelectDestination.Enabled = true;
                Progress.Value = 0;
            }));
        }

        private void Finished() {
            BeginInvoke((Action)(() => {
                MessageBox.Show("Patch finished successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RestoreInitialState();
            }));
        }
    }
}
