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
                        lblCourseStatus.Text = "Not Running";
                        lblCourseStatus.BackColor = Color.Maroon;
                        break;
                    case CourseStatus.Starting:
                    case CourseStatus.Running:
                        lblCourseStatus.Text = value.ToString();
                        lblCourseStatus.BackColor = Color.DarkBlue;
                        break;
                    case CourseStatus.Finished:
                        lblCourseStatus.Text = value.ToString();
                        lblCourseStatus.BackColor = Color.DarkGreen;
                        break;
                    case CourseStatus.Failed:
                        lblCourseStatus.Text = value.ToString();
                        lblCourseStatus.BackColor = Color.DarkRed;
                        break;
                }
                _status = value;
            }
        }
    }

}
