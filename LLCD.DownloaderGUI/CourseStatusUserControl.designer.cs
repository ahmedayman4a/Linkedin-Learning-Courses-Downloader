namespace LLCD.DownloaderGUI
{
    partial class CourseStatusUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCourseStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCourseStatus
            // 
            this.lblCourseStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(222)))), ((int)(((byte)(220)))));
            this.lblCourseStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCourseStatus.Font = new System.Drawing.Font("Quicksand", 14.25F);
            this.lblCourseStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(14)))), ((int)(((byte)(11)))));
            this.lblCourseStatus.Location = new System.Drawing.Point(0, 0);
            this.lblCourseStatus.Name = "lblCourseStatus";
            this.lblCourseStatus.Size = new System.Drawing.Size(131, 27);
            this.lblCourseStatus.TabIndex = 1;
            this.lblCourseStatus.Text = "Not running";
            this.lblCourseStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CourseStatusUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.lblCourseStatus);
            this.Name = "CourseStatusUserControl";
            this.Size = new System.Drawing.Size(131, 27);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCourseStatus;
    }
}
