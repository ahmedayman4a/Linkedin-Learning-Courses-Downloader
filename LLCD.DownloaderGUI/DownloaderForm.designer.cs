namespace LLCD.DownloaderGUI
{
    partial class DownloaderForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloaderForm));
            this.progressBarCourse = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDownloadingVideo = new System.Windows.Forms.Label();
            this.progressBarVideo = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblVideo = new System.Windows.Forms.Label();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.lblCourse = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.progressBarTotal = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBarCourse
            // 
            this.progressBarCourse.Location = new System.Drawing.Point(13, 132);
            this.progressBarCourse.Name = "progressBarCourse";
            this.progressBarCourse.Size = new System.Drawing.Size(631, 32);
            this.progressBarCourse.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarCourse.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Quicksand", 14.25F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.label5.Location = new System.Drawing.Point(10, 101);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 28);
            this.label5.TabIndex = 5;
            this.label5.Text = "Course Progress :";
            // 
            // lblDownloadingVideo
            // 
            this.lblDownloadingVideo.AutoSize = true;
            this.lblDownloadingVideo.Font = new System.Drawing.Font("Quicksand", 14.25F);
            this.lblDownloadingVideo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.lblDownloadingVideo.Location = new System.Drawing.Point(10, 10);
            this.lblDownloadingVideo.Margin = new System.Windows.Forms.Padding(0);
            this.lblDownloadingVideo.Name = "lblDownloadingVideo";
            this.lblDownloadingVideo.Size = new System.Drawing.Size(193, 28);
            this.lblDownloadingVideo.TabIndex = 5;
            this.lblDownloadingVideo.Text = "Downloading Video :";
            // 
            // progressBarVideo
            // 
            this.progressBarVideo.Location = new System.Drawing.Point(13, 41);
            this.progressBarVideo.MarqueeAnimationSpeed = 30;
            this.progressBarVideo.Name = "progressBarVideo";
            this.progressBarVideo.Size = new System.Drawing.Size(582, 32);
            this.progressBarVideo.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarVideo.TabIndex = 6;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.flowLayoutPanel.Controls.Add(this.lblDownloadingVideo);
            this.flowLayoutPanel.Controls.Add(this.lblVideo);
            this.flowLayoutPanel.Controls.Add(this.progressBarVideo);
            this.flowLayoutPanel.Controls.Add(this.lblPercentage);
            this.flowLayoutPanel.Controls.Add(this.label5);
            this.flowLayoutPanel.Controls.Add(this.lblCourse);
            this.flowLayoutPanel.Controls.Add(this.progressBarCourse);
            this.flowLayoutPanel.Controls.Add(this.label1);
            this.flowLayoutPanel.Controls.Add(this.lblTotal);
            this.flowLayoutPanel.Controls.Add(this.progressBarTotal);
            this.flowLayoutPanel.Location = new System.Drawing.Point(15, 15);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(6);
            this.flowLayoutPanel.MaximumSize = new System.Drawing.Size(660, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel.Size = new System.Drawing.Size(657, 268);
            this.flowLayoutPanel.TabIndex = 7;
            // 
            // lblVideo
            // 
            this.lblVideo.AutoSize = true;
            this.lblVideo.Font = new System.Drawing.Font("Quicksand", 14.25F);
            this.lblVideo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.lblVideo.Location = new System.Drawing.Point(203, 10);
            this.lblVideo.Margin = new System.Windows.Forms.Padding(0);
            this.lblVideo.Name = "lblVideo";
            this.lblVideo.Size = new System.Drawing.Size(348, 28);
            this.lblVideo.TabIndex = 7;
            this.lblVideo.Text = "[Video Name] in Chapter [Chapter Id]";
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Font = new System.Drawing.Font("Quicksand", 14.25F);
            this.lblPercentage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.lblPercentage.Location = new System.Drawing.Point(598, 45);
            this.lblPercentage.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(38, 28);
            this.lblPercentage.TabIndex = 5;
            this.lblPercentage.Text = "0%";
            this.lblPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCourse
            // 
            this.lblCourse.AutoSize = true;
            this.lblCourse.Font = new System.Drawing.Font("Quicksand", 14.25F);
            this.lblCourse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.lblCourse.Location = new System.Drawing.Point(174, 101);
            this.lblCourse.Margin = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.Size = new System.Drawing.Size(343, 28);
            this.lblCourse.TabIndex = 8;
            this.lblCourse.Text = "[Current Video]/[Total Videos Count] ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Quicksand", 14.25F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.label1.Location = new System.Drawing.Point(10, 192);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 28);
            this.label1.TabIndex = 12;
            this.label1.Text = "Total Progress :";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Quicksand", 14.25F);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.lblTotal.Location = new System.Drawing.Point(155, 192);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(363, 28);
            this.lblTotal.TabIndex = 14;
            this.lblTotal.Text = "[Current Course]/[Total Courses Count] ";
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(13, 223);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Size = new System.Drawing.Size(631, 32);
            this.progressBarTotal.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarTotal.TabIndex = 13;
            // 
            // DownloaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(687, 297);
            this.Controls.Add(this.flowLayoutPanel);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DownloaderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Downloading [Course Name]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloaderForm_FormClosing);
            this.Load += new System.EventHandler(this.DownloaderForm_Load);
            this.flowLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarCourse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDownloadingVideo;
        private System.Windows.Forms.ProgressBar progressBarVideo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.ProgressBar progressBarTotal;
    }
}