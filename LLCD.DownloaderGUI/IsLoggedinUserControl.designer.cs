namespace LLCD.DownloaderGUI
{
    partial class IsLoggedinUserControl
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
            this.lblLoggedin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLoggedin
            // 
            this.lblLoggedin.AutoSize = true;
            this.lblLoggedin.BackColor = System.Drawing.Color.Maroon;
            this.lblLoggedin.Font = new System.Drawing.Font("Barlow Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoggedin.ForeColor = System.Drawing.Color.White;
            this.lblLoggedin.Location = new System.Drawing.Point(0, 0);
            this.lblLoggedin.Name = "lblLoggedin";
            this.lblLoggedin.Size = new System.Drawing.Size(31, 27);
            this.lblLoggedin.TabIndex = 1;
            this.lblLoggedin.Text = "No";
            // 
            // IsLoggedinUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.lblLoggedin);
            this.Name = "IsLoggedinUserControl";
            this.Size = new System.Drawing.Size(37, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLoggedin;
    }
}
