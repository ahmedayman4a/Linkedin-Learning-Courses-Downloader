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
            this.panelBody = new System.Windows.Forms.Panel();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.UCCourseDownloaderStatus = new LLCD.DownloaderGUI.CourseStatusUserControl();
            this.UC_CourseExtractorStatus = new LLCD.DownloaderGUI.CourseStatusUserControl();
            this.lblCurrentExtractionOperation = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.progressBarCourses = new System.Windows.Forms.ProgressBar();
            this.progressBarExtractor = new System.Windows.Forms.ProgressBar();
            this.panelInput = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtUrls = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCourseDirectory = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExtractToken = new System.Windows.Forms.Button();
            this.cmboxQuality = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboxBrowser = new System.Windows.Forms.ComboBox();
            this.numericUpDownDelay = new System.Windows.Forms.NumericUpDown();
            this.checkBoxExerciseFiles = new System.Windows.Forms.CheckBox();
            this.checkBoxDelay = new System.Windows.Forms.CheckBox();
            this.checkBoxSubtitles = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panelBody.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.panelInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBody
            // 
            this.panelBody.Controls.Add(this.panelStatus);
            this.panelBody.Controls.Add(this.panelInput);
            this.panelBody.ForeColor = System.Drawing.Color.White;
            this.panelBody.Location = new System.Drawing.Point(12, 12);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(697, 690);
            this.panelBody.TabIndex = 0;
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.panelStatus.Controls.Add(this.UCCourseDownloaderStatus);
            this.panelStatus.Controls.Add(this.UC_CourseExtractorStatus);
            this.panelStatus.Controls.Add(this.lblCurrentExtractionOperation);
            this.panelStatus.Controls.Add(this.label10);
            this.panelStatus.Controls.Add(this.label8);
            this.panelStatus.Controls.Add(this.progressBarCourses);
            this.panelStatus.Controls.Add(this.progressBarExtractor);
            this.panelStatus.Location = new System.Drawing.Point(13, 479);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(671, 207);
            this.panelStatus.TabIndex = 22;
            // 
            // UCCourseDownloaderStatus
            // 
            this.UCCourseDownloaderStatus.BackColor = System.Drawing.Color.Black;
            this.UCCourseDownloaderStatus.Location = new System.Drawing.Point(520, 20);
            this.UCCourseDownloaderStatus.Name = "UCCourseDownloaderStatus";
            this.UCCourseDownloaderStatus.Size = new System.Drawing.Size(130, 29);
            this.UCCourseDownloaderStatus.Status = LLCD.DownloaderGUI.CourseStatus.NotRunning;
            this.UCCourseDownloaderStatus.TabIndex = 5;
            // 
            // UC_CourseExtractorStatus
            // 
            this.UC_CourseExtractorStatus.BackColor = System.Drawing.Color.Black;
            this.UC_CourseExtractorStatus.Location = new System.Drawing.Point(177, 19);
            this.UC_CourseExtractorStatus.Name = "UC_CourseExtractorStatus";
            this.UC_CourseExtractorStatus.Size = new System.Drawing.Size(130, 29);
            this.UC_CourseExtractorStatus.Status = LLCD.DownloaderGUI.CourseStatus.NotRunning;
            this.UC_CourseExtractorStatus.TabIndex = 5;
            // 
            // lblCurrentExtractionOperation
            // 
            this.lblCurrentExtractionOperation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.lblCurrentExtractionOperation.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentExtractionOperation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.lblCurrentExtractionOperation.Location = new System.Drawing.Point(16, 65);
            this.lblCurrentExtractionOperation.Name = "lblCurrentExtractionOperation";
            this.lblCurrentExtractionOperation.Size = new System.Drawing.Size(634, 53);
            this.lblCurrentExtractionOperation.TabIndex = 6;
            this.lblCurrentExtractionOperation.Text = "Waiting for login data";
            this.lblCurrentExtractionOperation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.label10.Location = new System.Drawing.Point(333, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(192, 28);
            this.label10.TabIndex = 0;
            this.label10.Text = "Course downloader: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.label8.Location = new System.Drawing.Point(11, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(172, 28);
            this.label8.TabIndex = 0;
            this.label8.Text = "Course extractor : ";
            // 
            // progressBarCourses
            // 
            this.progressBarCourses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(218)))), ((int)(((byte)(197)))));
            this.progressBarCourses.Location = new System.Drawing.Point(16, 170);
            this.progressBarCourses.MarqueeAnimationSpeed = 30;
            this.progressBarCourses.Name = "progressBarCourses";
            this.progressBarCourses.Size = new System.Drawing.Size(634, 20);
            this.progressBarCourses.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarCourses.TabIndex = 9;
            // 
            // progressBarExtractor
            // 
            this.progressBarExtractor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(218)))), ((int)(((byte)(197)))));
            this.progressBarExtractor.Location = new System.Drawing.Point(16, 138);
            this.progressBarExtractor.MarqueeAnimationSpeed = 30;
            this.progressBarExtractor.Name = "progressBarExtractor";
            this.progressBarExtractor.Size = new System.Drawing.Size(634, 20);
            this.progressBarExtractor.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarExtractor.TabIndex = 4;
            // 
            // panelInput
            // 
            this.panelInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.panelInput.Controls.Add(this.label1);
            this.panelInput.Controls.Add(this.btnDownload);
            this.panelInput.Controls.Add(this.btnBrowse);
            this.panelInput.Controls.Add(this.txtUrls);
            this.panelInput.Controls.Add(this.label2);
            this.panelInput.Controls.Add(this.txtCourseDirectory);
            this.panelInput.Controls.Add(this.label4);
            this.panelInput.Controls.Add(this.txtToken);
            this.panelInput.Controls.Add(this.label5);
            this.panelInput.Controls.Add(this.btnExtractToken);
            this.panelInput.Controls.Add(this.cmboxQuality);
            this.panelInput.Controls.Add(this.label3);
            this.panelInput.Controls.Add(this.cmboxBrowser);
            this.panelInput.Controls.Add(this.numericUpDownDelay);
            this.panelInput.Controls.Add(this.checkBoxExerciseFiles);
            this.panelInput.Controls.Add(this.checkBoxDelay);
            this.panelInput.Controls.Add(this.checkBoxSubtitles);
            this.panelInput.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelInput.Location = new System.Drawing.Point(13, 0);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(671, 459);
            this.panelInput.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.label1.Location = new System.Drawing.Point(11, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 84);
            this.label1.TabIndex = 21;
            this.label1.Text = "Courses and/or \r\nLearning paths\r\nurls :";
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(134)))), ((int)(((byte)(252)))));
            this.btnDownload.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("Quicksand Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.Color.Black;
            this.btnDownload.Location = new System.Drawing.Point(429, 392);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(221, 45);
            this.btnDownload.TabIndex = 17;
            this.btnDownload.Text = "DOWNLOAD";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(134)))), ((int)(((byte)(252)))));
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Quicksand", 12F, System.Drawing.FontStyle.Bold);
            this.btnBrowse.ForeColor = System.Drawing.Color.Black;
            this.btnBrowse.Location = new System.Drawing.Point(615, 149);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(35, 31);
            this.btnBrowse.TabIndex = 17;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtUrls
            // 
            this.txtUrls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.txtUrls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUrls.Font = new System.Drawing.Font("Quicksand", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.txtUrls.Location = new System.Drawing.Point(187, 192);
            this.txtUrls.Multiline = true;
            this.txtUrls.Name = "txtUrls";
            this.txtUrls.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUrls.Size = new System.Drawing.Size(463, 126);
            this.txtUrls.TabIndex = 20;
            this.txtUrls.Text = "One url per line";
            this.txtUrls.Enter += new System.EventHandler(this.txtCourseUrls_Enter);
            this.txtUrls.Leave += new System.EventHandler(this.txtCourseUrls_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.label2.Location = new System.Drawing.Point(409, 332);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "seconds";
            // 
            // txtCourseDirectory
            // 
            this.txtCourseDirectory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.txtCourseDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCourseDirectory.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCourseDirectory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.txtCourseDirectory.Location = new System.Drawing.Point(187, 149);
            this.txtCourseDirectory.Name = "txtCourseDirectory";
            this.txtCourseDirectory.Size = new System.Drawing.Size(422, 31);
            this.txtCourseDirectory.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.label4.Location = new System.Drawing.Point(11, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 28);
            this.label4.TabIndex = 0;
            this.label4.Text = "Video quality : ";
            // 
            // txtToken
            // 
            this.txtToken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.txtToken.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToken.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToken.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.txtToken.Location = new System.Drawing.Point(187, 55);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(463, 31);
            this.txtToken.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.label5.Location = new System.Drawing.Point(11, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(166, 28);
            this.label5.TabIndex = 0;
            this.label5.Text = "Download folder: ";
            // 
            // btnExtractToken
            // 
            this.btnExtractToken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(134)))), ((int)(((byte)(252)))));
            this.btnExtractToken.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnExtractToken.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtractToken.Font = new System.Drawing.Font("Quicksand Medium", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnExtractToken.ForeColor = System.Drawing.Color.Black;
            this.btnExtractToken.Location = new System.Drawing.Point(16, 100);
            this.btnExtractToken.Name = "btnExtractToken";
            this.btnExtractToken.Size = new System.Drawing.Size(316, 43);
            this.btnExtractToken.TabIndex = 18;
            this.btnExtractToken.Text = "Import Token";
            this.btnExtractToken.UseVisualStyleBackColor = false;
            this.btnExtractToken.Click += new System.EventHandler(this.btnExtractToken_Click);
            // 
            // cmboxQuality
            // 
            this.cmboxQuality.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.cmboxQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboxQuality.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmboxQuality.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboxQuality.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.cmboxQuality.FormattingEnabled = true;
            this.cmboxQuality.Items.AddRange(new object[] {
            "720 (High)",
            "540 (Medium)",
            "360 (Low)"});
            this.cmboxQuality.Location = new System.Drawing.Point(187, 10);
            this.cmboxQuality.Name = "cmboxQuality";
            this.cmboxQuality.Size = new System.Drawing.Size(463, 36);
            this.cmboxQuality.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.label3.Location = new System.Drawing.Point(11, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 28);
            this.label3.TabIndex = 0;
            this.label3.Text = "Token cookie : ";
            // 
            // cmboxBrowser
            // 
            this.cmboxBrowser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.cmboxBrowser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboxBrowser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmboxBrowser.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboxBrowser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.cmboxBrowser.FormattingEnabled = true;
            this.cmboxBrowser.Items.AddRange(new object[] {
            "From Google Chrome",
            "From Mozilla Firefox",
            "From Microsoft Edge"});
            this.cmboxBrowser.Location = new System.Drawing.Point(338, 104);
            this.cmboxBrowser.Name = "cmboxBrowser";
            this.cmboxBrowser.Size = new System.Drawing.Size(312, 36);
            this.cmboxBrowser.TabIndex = 12;
            // 
            // numericUpDownDelay
            // 
            this.numericUpDownDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.numericUpDownDelay.Enabled = false;
            this.numericUpDownDelay.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.numericUpDownDelay.Location = new System.Drawing.Point(338, 330);
            this.numericUpDownDelay.Name = "numericUpDownDelay";
            this.numericUpDownDelay.Size = new System.Drawing.Size(65, 31);
            this.numericUpDownDelay.TabIndex = 16;
            this.numericUpDownDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownDelay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDownDelay_KeyPress);
            // 
            // checkBoxExerciseFiles
            // 
            this.checkBoxExerciseFiles.AutoSize = true;
            this.checkBoxExerciseFiles.Checked = true;
            this.checkBoxExerciseFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxExerciseFiles.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxExerciseFiles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.checkBoxExerciseFiles.Location = new System.Drawing.Point(16, 367);
            this.checkBoxExerciseFiles.Name = "checkBoxExerciseFiles";
            this.checkBoxExerciseFiles.Size = new System.Drawing.Size(237, 32);
            this.checkBoxExerciseFiles.TabIndex = 13;
            this.checkBoxExerciseFiles.Text = "Download exercise files";
            this.checkBoxExerciseFiles.UseVisualStyleBackColor = true;
            // 
            // checkBoxDelay
            // 
            this.checkBoxDelay.AutoSize = true;
            this.checkBoxDelay.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.checkBoxDelay.Location = new System.Drawing.Point(19, 329);
            this.checkBoxDelay.Name = "checkBoxDelay";
            this.checkBoxDelay.Size = new System.Drawing.Size(320, 32);
            this.checkBoxDelay.TabIndex = 15;
            this.checkBoxDelay.Text = "Delay between video extractions";
            this.checkBoxDelay.UseVisualStyleBackColor = true;
            this.checkBoxDelay.CheckedChanged += new System.EventHandler(this.checkBoxDelay_CheckedChanged);
            // 
            // checkBoxSubtitles
            // 
            this.checkBoxSubtitles.AutoSize = true;
            this.checkBoxSubtitles.Checked = true;
            this.checkBoxSubtitles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSubtitles.Font = new System.Drawing.Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSubtitles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(210)))), ((int)(((byte)(214)))));
            this.checkBoxSubtitles.Location = new System.Drawing.Point(16, 405);
            this.checkBoxSubtitles.Name = "checkBoxSubtitles";
            this.checkBoxSubtitles.Size = new System.Drawing.Size(197, 32);
            this.checkBoxSubtitles.TabIndex = 14;
            this.checkBoxSubtitles.Text = "Download subtitles";
            this.checkBoxSubtitles.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(717, 711);
            this.Controls.Add(this.panelBody);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Linkedin Learning Courses Downloader - By ahmedayman4a";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelBody.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBarExtractor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboxQuality;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private CourseStatusUserControl UC_CourseExtractorStatus;
        private CourseStatusUserControl UCCourseDownloaderStatus;
        private System.Windows.Forms.ProgressBar progressBarCourses;
        private System.Windows.Forms.ComboBox cmboxBrowser;
        private System.Windows.Forms.CheckBox checkBoxExerciseFiles;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.TextBox txtUrls;
        private System.Windows.Forms.TextBox txtCourseDirectory;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Button btnExtractToken;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.NumericUpDown numericUpDownDelay;
        private System.Windows.Forms.CheckBox checkBoxDelay;
        private System.Windows.Forms.CheckBox checkBoxSubtitles;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblCurrentExtractionOperation;
        private System.Windows.Forms.Label label1;
    }
}

