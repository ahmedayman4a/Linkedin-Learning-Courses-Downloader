using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LLCD.DownloaderGUI
{
    public partial class IsLoggedinUserControl : UserControl
    {
        public IsLoggedinUserControl()
        {
            InitializeComponent();
        }

        private bool _isLoggedin;

        public bool IsLoggedin
        {
            get { return _isLoggedin; }
            set
            {
                _isLoggedin = value;
                if (_isLoggedin)
                {
                    lblLoggedin.BackColor = Color.DarkGreen;
                    lblLoggedin.Text = "Yes";
                }
                else
                {
                    lblLoggedin.BackColor = Color.Maroon;
                    lblLoggedin.Text = "No";
                }
            }
        }
    }
}
