namespace LLCD.DownloaderGUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel = new System.Windows.Forms.Panel();
            this.cmboxBrowser = new System.Windows.Forms.ComboBox();
            this.btnExtractToken = new System.Windows.Forms.Button();
            this.lblCurrentCourse = new System.Windows.Forms.Label();
            this.progressBarCourses = new System.Windows.Forms.ProgressBar();
            this.lblCurrentExtractionOperation = new System.Windows.Forms.Label();
            this.UC_CourseDownloaderStatus = new LLCD.DownloaderGUI.CourseStatusUserControl();
            this.UC_CourseExtractorStatus = new LLCD.DownloaderGUI.CourseStatusUserControl();
            this.progressBarExtractor = new System.Windows.Forms.ProgressBar();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtCourseDirectory = new System.Windows.Forms.TextBox();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.txtCourseUrls = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboxQuality = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBoxExerciseFiles = new System.Windows.Forms.CheckBox();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.cmboxBrowser);
            this.panel.Controls.Add(this.btnExtractToken);
            this.panel.Controls.Add(this.lblCurrentCourse);
            this.panel.Controls.Add(this.progressBarCourses);
            this.panel.Controls.Add(this.lblCurrentExtractionOperation);
            this.panel.Controls.Add(this.UC_CourseDownloaderStatus);
            this.panel.Controls.Add(this.UC_CourseExtractorStatus);
            this.panel.Controls.Add(this.progressBarExtractor);
            this.panel.Controls.Add(this.btnDownload);
            this.panel.Controls.Add(this.btnBrowse);
            this.panel.Controls.Add(this.txtCourseDirectory);
            this.panel.Controls.Add(this.txtToken);
            this.panel.Controls.Add(this.txtCourseUrls);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.cmboxQuality);
            this.panel.Controls.Add(this.label10);
            this.panel.Controls.Add(this.label8);
            this.panel.Controls.Add(this.label5);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.label2);
            this.panel.ForeColor = System.Drawing.Color.White;
            this.panel.Location = new System.Drawing.Point(12, 12);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(666, 554);
            this.panel.TabIndex = 0;
            // 
            // cmboxBrowser
            // 
            this.cmboxBrowser.BackColor = System.Drawing.Color.Black;
            this.cmboxBrowser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboxBrowser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmboxBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboxBrowser.ForeColor = System.Drawing.Color.White;
            this.cmboxBrowser.FormattingEnabled = true;
            this.cmboxBrowser.Items.AddRange(new object[] {
            "From Google Chrome",
            "From Mozilla Firefox",
            "From Microsoft Edge"});
            this.cmboxBrowser.Location = new System.Drawing.Point(338, 88);
            this.cmboxBrowser.Name = "cmboxBrowser";
            this.cmboxBrowser.Size = new System.Drawing.Size(312, 28);
            this.cmboxBrowser.TabIndex = 12;
            // 
            // btnExtractToken
            // 
            this.btnExtractToken.BackColor = System.Drawing.Color.Navy;
            this.btnExtractToken.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnExtractToken.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExtractToken.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnExtractToken.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtractToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExtractToken.ForeColor = System.Drawing.Color.White;
            this.btnExtractToken.Location = new System.Drawing.Point(19, 87);
            this.btnExtractToken.Name = "btnExtractToken";
            this.btnExtractToken.Size = new System.Drawing.Size(312, 36);
            this.btnExtractToken.TabIndex = 11;
            this.btnExtractToken.Text = "Extract token";
            this.btnExtractToken.UseVisualStyleBackColor = false;
            this.btnExtractToken.Click += new System.EventHandler(this.btnExtractToken_Click);
            // 
            // lblCurrentCourse
            // 
            this.lblCurrentCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentCourse.ForeColor = System.Drawing.Color.Gold;
            this.lblCurrentCourse.Location = new System.Drawing.Point(19, 480);
            this.lblCurrentCourse.Name = "lblCurrentCourse";
            this.lblCurrentCourse.Size = new System.Drawing.Size(630, 33);
            this.lblCurrentCourse.TabIndex = 10;
            this.lblCurrentCourse.Text = "...";
            this.lblCurrentCourse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarCourses
            // 
            this.progressBarCourses.Location = new System.Drawing.Point(19, 519);
            this.progressBarCourses.MarqueeAnimationSpeed = 30;
            this.progressBarCourses.Name = "progressBarCourses";
            this.progressBarCourses.Size = new System.Drawing.Size(631, 32);
            this.progressBarCourses.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarCourses.TabIndex = 9;
            // 
            // lblCurrentExtractionOperation
            // 
            this.lblCurrentExtractionOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentExtractionOperation.ForeColor = System.Drawing.Color.Gold;
            this.lblCurrentExtractionOperation.Location = new System.Drawing.Point(20, 392);
            this.lblCurrentExtractionOperation.Name = "lblCurrentExtractionOperation";
            this.lblCurrentExtractionOperation.Size = new System.Drawing.Size(630, 33);
            this.lblCurrentExtractionOperation.TabIndex = 8;
            this.lblCurrentExtractionOperation.Text = "Waiting for input from user";
            this.lblCurrentExtractionOperation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UC_CourseDownloaderStatus
            // 
            this.UC_CourseDownloaderStatus.BackColor = System.Drawing.Color.Black;
            this.UC_CourseDownloaderStatus.Location = new System.Drawing.Point(545, 354);
            this.UC_CourseDownloaderStatus.Name = "UC_CourseDownloaderStatus";
            this.UC_CourseDownloaderStatus.Size = new System.Drawing.Size(105, 29);
            this.UC_CourseDownloaderStatus.Status = LLCD.DownloaderGUI.CourseStatus.NotRunning;
            this.UC_CourseDownloaderStatus.TabIndex = 5;
            // 
            // UC_CourseExtractorStatus
            // 
            this.UC_CourseExtractorStatus.BackColor = System.Drawing.Color.Black;
            this.UC_CourseExtractorStatus.Location = new System.Drawing.Point(181, 354);
            this.UC_CourseExtractorStatus.Name = "UC_CourseExtractorStatus";
            this.UC_CourseExtractorStatus.Size = new System.Drawing.Size(105, 29);
            this.UC_CourseExtractorStatus.Status = LLCD.DownloaderGUI.CourseStatus.NotRunning;
            this.UC_CourseExtractorStatus.TabIndex = 5;
            // 
            // progressBarExtractor
            // 
            this.progressBarExtractor.Location = new System.Drawing.Point(20, 431);
            this.progressBarExtractor.MarqueeAnimationSpeed = 30;
            this.progressBarExtractor.Name = "progressBarExtractor";
            this.progressBarExtractor.Size = new System.Drawing.Size(631, 32);
            this.progressBarExtractor.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarExtractor.TabIndex = 4;
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.DarkGreen;
            this.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnDownload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDownload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.Color.White;
            this.btnDownload.Location = new System.Drawing.Point(19, 296);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(631, 40);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "Start Downloading";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.DarkGray;
            this.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.ForeColor = System.Drawing.Color.White;
            this.btnBrowse.Location = new System.Drawing.Point(615, 137);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(35, 29);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = ". . .";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtCourseDirectory
            // 
            this.txtCourseDirectory.BackColor = System.Drawing.Color.DarkGray;
            this.txtCourseDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCourseDirectory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCourseDirectory.ForeColor = System.Drawing.Color.White;
            this.txtCourseDirectory.Location = new System.Drawing.Point(150, 137);
            this.txtCourseDirectory.Name = "txtCourseDirectory";
            this.txtCourseDirectory.Size = new System.Drawing.Size(459, 29);
            this.txtCourseDirectory.TabIndex = 2;
            // 
            // txtToken
            // 
            this.txtToken.BackColor = System.Drawing.Color.DarkGray;
            this.txtToken.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToken.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToken.ForeColor = System.Drawing.Color.White;
            this.txtToken.Location = new System.Drawing.Point(150, 43);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(500, 29);
            this.txtToken.TabIndex = 2;
            // 
            // txtCourseUrls
            // 
            this.txtCourseUrls.BackColor = System.Drawing.Color.DarkGray;
            this.txtCourseUrls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCourseUrls.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCourseUrls.ForeColor = System.Drawing.Color.White;
            this.txtCourseUrls.Location = new System.Drawing.Point(150, 183);
            this.txtCourseUrls.Multiline = true;
            this.txtCourseUrls.Name = "txtCourseUrls";
            this.txtCourseUrls.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCourseUrls.Size = new System.Drawing.Size(500, 100);
            this.txtCourseUrls.TabIndex = 2;
            this.txtCourseUrls.Text = "One course url per line";
            this.txtCourseUrls.Enter += new System.EventHandler(this.txtCourseUrls_Enter);
            this.txtCourseUrls.Leave += new System.EventHandler(this.txtCourseUrls_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Token Cookie : ";
            // 
            // cmboxQuality
            // 
            this.cmboxQuality.BackColor = System.Drawing.Color.Black;
            this.cmboxQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboxQuality.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmboxQuality.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboxQuality.ForeColor = System.Drawing.Color.White;
            this.cmboxQuality.FormattingEnabled = true;
            this.cmboxQuality.Items.AddRange(new object[] {
            "720 (High)",
            "540 (Medium)",
            "360 (Low)"});
            this.cmboxQuality.Location = new System.Drawing.Point(150, 0);
            this.cmboxQuality.Name = "cmboxQuality";
            this.cmboxQuality.Size = new System.Drawing.Size(181, 28);
            this.cmboxQuality.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(373, 354);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(213, 25);
            this.label10.TabIndex = 0;
            this.label10.Text = "Course Downloader: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(15, 354);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(191, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "Course Extractor : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "Course Location : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Video Quality : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Course Urls : ";
            // 
            // checkBoxExerciseFiles
            // 
            this.checkBoxExerciseFiles.AutoSize = true;
            this.checkBoxExerciseFiles.Checked = true;
            this.checkBoxExerciseFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxExerciseFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxExerciseFiles.Location = new System.Drawing.Point(423, 12);
            this.checkBoxExerciseFiles.Name = "checkBoxExerciseFiles";
            this.checkBoxExerciseFiles.Size = new System.Drawing.Size(238, 28);
            this.checkBoxExerciseFiles.TabIndex = 13;
            this.checkBoxExerciseFiles.Text = "Download Exercise Files";
            this.checkBoxExerciseFiles.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(693, 578);
            this.Controls.Add(this.checkBoxExerciseFiles);
            this.Controls.Add(this.panel);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Linkedin Learning Courses Downloader - By ahmedayman4a";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBarExtractor;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtCourseDirectory;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboxQuality;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private CourseStatusUserControl UC_CourseExtractorStatus;
        private CourseStatusUserControl UC_CourseDownloaderStatus;
        private System.Windows.Forms.TextBox txtCourseUrls;
        private System.Windows.Forms.Label lblCurrentExtractionOperation;
        private System.Windows.Forms.Label lblCurrentCourse;
        private System.Windows.Forms.ProgressBar progressBarCourses;
        private System.Windows.Forms.ComboBox cmboxBrowser;
        private System.Windows.Forms.Button btnExtractToken;
        private System.Windows.Forms.CheckBox checkBoxExerciseFiles;
    }
}

