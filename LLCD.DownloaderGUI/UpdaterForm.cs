using LLCD.DownloaderConfig;
using Serilog;
using Squirrel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LLCD.DownloaderGUI
{
    public partial class UpdaterForm : Form
    {
        private UpdateManager _updateManager;
        public bool IsUpdated { get; set; }
        public UpdaterForm(string message ,UpdateManager updateManager)
        {
            _updateManager = updateManager;
            InitializeComponent();
            lblMessage.Text = message;
            FormHelpers.SetFonts(flowLayoutPanel);
        }

        private async void btnYes_Click(object sender, EventArgs e)
        {
            btnNo.Visible = false;
            btnYes.Visible = false;
            lblMessage.Visible = false;
            gifLoading.Visible = true;
            Text = "Updating...";
            Log.Information("Downloading updates");
            var updateResult = await _updateManager.UpdateApp();
            Log.Information($"Download complete. App will restart into version {updateResult.Version} after backing up config file.");
            Config.Backup();
            IsUpdated = true;
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            Log.Information("Update declined by user.");
            IsUpdated = false;
            Close();
        }
    }
}
