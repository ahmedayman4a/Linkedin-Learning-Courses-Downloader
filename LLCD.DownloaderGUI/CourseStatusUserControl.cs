using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace LLCD.DownloaderGUI
{
    public partial class CourseStatusUserControl : UserControl
    {
        public CourseStatusUserControl()
        {
            InitializeComponent();
        }
        public CourseStatus _status;
        public CourseStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                switch (value)
                {
                    case CourseStatus.NotRunning:
                        lblCourseStatus.Text = "Not running";
                        lblCourseStatus.BackColor = Color.FromArgb(249, 222, 220);
                        lblCourseStatus.ForeColor = Color.FromArgb(65, 14, 11);
                        break;
                    case CourseStatus.Starting:
                    case CourseStatus.Running:
                        lblCourseStatus.Text = value.ToString();
                        lblCourseStatus.BackColor = Color.FromArgb(3, 218, 198);
                        lblCourseStatus.ForeColor = Color.Black;
                        break;
                    case CourseStatus.Finished:
                        lblCourseStatus.Text = value.ToString();
                        lblCourseStatus.BackColor = Color.FromArgb(183, 243, 151);
                        lblCourseStatus.ForeColor = Color.FromArgb(4, 33, 0);
                        break;
                    case CourseStatus.Failed:
                        lblCourseStatus.Text = value.ToString();
                        lblCourseStatus.BackColor = Color.FromArgb(207, 102, 121);
                        lblCourseStatus.ForeColor = Color.Black;
                        break;
                }
                _status = value;
            }
        }
    }

}
