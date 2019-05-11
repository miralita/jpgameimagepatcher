using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchBuilder {
    public partial class PatchLog : Form {
        public PatchLog() {
            InitializeComponent();
        }

        public void UppendLog(string data) {
            Debug.WriteLine(data);
            this.BeginInvoke((Action)(() => {
                LogData.AppendText(data);
                LogData.AppendText("\r\n");
                LogData.ScrollToCaret();
                Update();
            }));
        }

        public void UpdateProgress(int percent) {
            this.BeginInvoke((Action)(() => {
                progressBar1.Value = percent;
                Update();
            }));
        }

        private void PatchLog_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) {
                this.Hide();
            }
        }

        private void PatchLog_FormClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
            }
        }
    }
}
