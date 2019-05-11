﻿using SharedTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

            var patch = Resources.EmbeddedPatch;
            if (patch.Length > 0) {
                try {
                    LoadPatch(patch);
                } catch(Exception ex) {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }

            var path = Path.GetDirectoryName(Application.ExecutablePath);
            var patches = Directory.GetFiles(path, "*.ptch");
            
            if (patches.Length == 1) {
                try {
                    LoadPatch(patches[0]);
                } catch(Exception) { }
            }
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
                    LoadPatch(dialog.FileName);
                } catch (Exception ex) {
                    MessageBox.Show($"Can't load patch file: {ex.Message}. Please select another file", "Can't load patch file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadPatch(string fname) {
            patchProcessor.LoadPatch(fname);
            if (!string.IsNullOrEmpty(patchProcessor.GameDescription)) {
                GameDescription.Text = patchProcessor.GameDescription;
            }
            if (patchProcessor.GameLogo != null) GameLogo.Image = patchProcessor.GameLogo;
            initialDirectory = Path.GetDirectoryName(fname);
            SelectSource.Enabled = true;
            PatchPath.Text = fname;
        }

        private void LoadPatch(byte[] patch) {
            patchProcessor.LoadPatch(patch);
            if (!string.IsNullOrEmpty(patchProcessor.GameDescription)) {
                GameDescription.Text = patchProcessor.GameDescription;
            }
            if (patchProcessor.GameLogo != null) GameLogo.Image = patchProcessor.GameLogo;
            initialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            SelectSource.Enabled = true;
            PatchPath.Text = "[Embedded]";
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
