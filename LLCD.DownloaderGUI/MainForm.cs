using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing;
using System.Threading.Tasks;
using Serilog;
using Squirrel;
using LLCD.CourseContent;
using LLCD.CourseExtractor;
using LLCD.DownloaderConfig;
using Newtonsoft.Json;

namespace LLCD.DownloaderGUI
{
    public partial class MainForm : Form
    {
        private Font _font;
        private readonly PrivateFontCollection _fontCollection = new PrivateFontCollection();
        public MainForm()
        {
            InitializeComponent();
            lblCurrentExtractionOperation.Text = "Waiting for input from user";
            UC_CourseExtractorStatus.Status = CourseStatus.NotRunning;
            UC_CourseDownloaderStatus.Status = CourseStatus.NotRunning;
            cmboxQuality.SelectedIndex = 0;
            cmboxBrowser.SelectedIndex = 0;
            Focus();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            txtCourseDirectory.Text = folderBrowserDialog.SelectedPath;
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            UC_CourseDownloaderStatus.Status = CourseStatus.NotRunning;
            UC_CourseExtractorStatus.Status = CourseStatus.NotRunning;
            progressBarExtractor.Value = 0;
            progressBarCourses.Value = 0;
            txtCourseDirectory.Text = txtCourseDirectory.Text.Trim();
            txtCourseUrls.Text = txtCourseUrls.Text.Trim();
            txtToken.Text = txtToken.Text.Trim();
            var extractors = new List<Extractor>();
            Quality quality = (Quality)cmboxQuality.SelectedIndex;
            foreach (var courseUrl in txtCourseUrls.Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                extractors.Add(new Extractor(courseUrl.Trim(), quality, txtToken.Text.Trim()));
            }
            UC_CourseExtractorStatus.Status = CourseStatus.Starting;
            Log.Information("Validating Input");
            lblCurrentExtractionOperation.Text = "Validating Input";

            if (!await IsInputValid(extractors))
            {
                UC_CourseExtractorStatus.Status = CourseStatus.NotRunning;
                lblCurrentExtractionOperation.Text = "Please recheck your input";
                Log.Information("Input Invalid");
                return;
            }
            Log.Information("Input Valid");
            UC_CourseDownloaderStatus.Status = CourseStatus.NotRunning;
            EnableControls(false);
            try
            {
                await ExtractAndDownloadAsync(extractors);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unknown error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string courses = String.Join(" -- ", txtCourseUrls.Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                Log.Error(ex, "Unknown error occured. Courses : " + courses);
                UC_CourseExtractorStatus.Status = CourseStatus.Failed;
                lblCurrentExtractionOperation.Text = "Course Extraction Failed";
                lblCurrentCourse.Text = "...";
                progressBarCourses.Value = 0;
            }

            EnableControls(true);
        }
        private void IncrementlblCurrentCourse()
        {
            Regex patternlblCurrentCourse = new Regex(@"Extracting Courses \((?<currentCourse>\d+)\/(?<totalCourses>\d+)\)");
            int currentCourse = int.Parse(patternlblCurrentCourse.Match(lblCurrentCourse.Text).Groups["currentCourse"].Value);
            int totalCourses = int.Parse(patternlblCurrentCourse.Match(lblCurrentCourse.Text).Groups["totalCourses"].Value);
            lblCurrentCourse.Text = $"Extracting Courses ({currentCourse + 1}/{totalCourses})";
            progressBarCourses.PerformStep();
        }
        private async Task<bool> IsInputValid(List<Extractor> extractors)
        {
            int numberOfErrors = 0;
            StringBuilder sb = new StringBuilder();

            if (String.IsNullOrWhiteSpace(txtCourseUrls.Text) || txtCourseUrls.Text == "One course url per line")
            {
                sb.Append("• ");
                sb.AppendLine("Course Url");
                numberOfErrors++;
            }
            if (String.IsNullOrWhiteSpace(txtToken.Text))
            {
                sb.Append("• ");
                sb.AppendLine("Token Cookie");
                numberOfErrors++;
            }
            if (String.IsNullOrWhiteSpace(txtCourseDirectory.Text))
            {
                sb.Append("• ");
                sb.AppendLine("Course Location");
                numberOfErrors++;
            }

            if (numberOfErrors == 1)
            {
                sb.Insert(0, "Please don't leave the following field empty:\n");
                MessageBox.Show(sb.ToString(), "Fields can't be empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (numberOfErrors > 1)
            {
                sb.Insert(0, "Please don't leave the following fields empty:\n");
                MessageBox.Show(sb.ToString(), "Fields can't be empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!await extractors[0].HasValidToken())
            {
                MessageBox.Show("The token you supplied is invalid", "Invalid token", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            foreach (var extractor in extractors)
            {
                if (!extractor.HasValidUrl())
                {
                    MessageBox.Show("One or more course urls are not valid", "Invalid url", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

            }
            return true;
        }
        private async Task ExtractAndDownloadAsync(List<Extractor> extractors)
        {
            lblCurrentCourse.Text = $"Extracting Courses (1/{extractors.Count})";
            progressBarCourses.Step = 100 / extractors.Count;
            await SaveConfig();
            var courses = new List<Course>();
            foreach (var extractor in extractors)
            {
                var course = await ExtractCourse(extractor);
                if (course is null)
                    return;
                courses.Add(course);
                IncrementlblCurrentCourse();
            }

            progressBarCourses.Value = progressBarCourses.Maximum;
            progressBarExtractor.Value = progressBarExtractor.Maximum;
            lblCurrentCourse.Text = "Courses Extracted Successfully";
            lblCurrentExtractionOperation.Text = $"Extracted Courses({extractors.Count}/{extractors.Count})";
            UC_CourseExtractorStatus.Status = CourseStatus.Finished;

            var downloaderForm = new DownloaderForm(courses, new DirectoryInfo(txtCourseDirectory.Text), checkBoxExerciseFiles.Checked, _font);
            UC_CourseDownloaderStatus.Status = CourseStatus.Running;
            try
            {
                downloaderForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A fatal error occured while downloading the course.\nCheck the logs for more info", "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(ex, "An fatal error occured while downloading the course");
                UC_CourseDownloaderStatus.Status = CourseStatus.Failed;
                lblCurrentExtractionOperation.Text = "Course Download Failed";
                return;
            }

            if (downloaderForm.DownloaderStatus == CourseStatus.Finished)
            {
                UC_CourseDownloaderStatus.Status = CourseStatus.Finished;
                MessageBox.Show("Course Downloaded Successfully :)", "Hooray", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblCurrentExtractionOperation.Text = "Course Downloaded Successfully";
            }
            else
            {
                UC_CourseDownloaderStatus.Status = CourseStatus.Failed;
                lblCurrentExtractionOperation.Text = "Course Download Failed";
            }
        }

        private async Task<Course> ExtractCourse(Extractor extractor)
        {
            lblCurrentExtractionOperation.Text = $"Extracting Course Download Links...";
            UC_CourseExtractorStatus.Status = CourseStatus.Running;

            Course course;
            try
            {
                var progress = new Progress<float>(progressPercent => UpdateUI(() => progressBarExtractor.Value = (int)(progressPercent * 100)));
                course = await extractor.GetCourse(progress);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Course Extraction Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string courses = String.Join(" -- ", txtCourseUrls.Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                Log.Error(ex, "Course Extraction Failed" + courses);
                EnableControls(true);
                UC_CourseExtractorStatus.Status = CourseStatus.Failed;
                lblCurrentExtractionOperation.Text = "Course Extraction Failed";
                lblCurrentCourse.Text = "...";
                progressBarCourses.Value = 0;
                progressBarExtractor.Value = 0;
                return null;
            }


            return course;
        }

        private void UpdateUI(Action updateAction)
        {
            Invoke(updateAction);
        }
        private T GetFromUI<T>(Func<object> getterAction)
        {
            return (T)Invoke(getterAction);
        }
        private void EnableControls(bool isEnabled)
        {
            txtCourseDirectory.Enabled = isEnabled;
            txtCourseUrls.Enabled = isEnabled;
            txtToken.Enabled = isEnabled;
            cmboxQuality.Enabled = isEnabled;
            btnDownload.Enabled = isEnabled;
            btnBrowse.Enabled = isEnabled;
            btnExtractToken.Enabled = isEnabled;
            checkBoxExerciseFiles.Enabled = isEnabled;
            cmboxBrowser.Enabled = isEnabled;
        }

        private async Task SaveConfig()
        {
            UpdateUI(() => lblCurrentExtractionOperation.Text = "Saving config file");
            Config config = new Config
            {
                AuthenticationToken = txtToken.Text,
                Quality = (Quality)cmboxQuality.SelectedIndex
            };
            try
            {
                config.CourseDirectory = new DirectoryInfo(txtCourseDirectory.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem with the course directory you entered.\nPlease enter another one", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnableControls(true);
            }
            try
            {
                await config.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while trying to save config", "Failed to solve", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnableControls(true);
                Log.Error(ex, "An error occured while trying to save config");
            }

        }


        private async Task CheckForUpdates()
        {
            bool restartApp = false;
            try
            {
                using (var githubUpdateManager = UpdateManager.GitHubUpdateManager("https://github.com/ahmedayman4a/Linkedin-Learning-Courses-Downloader.UpdateManager"))
                using (var updateManager = await githubUpdateManager)
                {
                    Log.Information("Checking for updates...");
                    var updateInfo = await updateManager.CheckForUpdate();
                    if (updateInfo.ReleasesToApply.Any())
                    {
                        var versionCount = updateInfo.ReleasesToApply.Count;
                        Log.Information($"{versionCount} update(s) found.");

                        var versionWord = versionCount > 1 ? "versions" : "version";
                        var message = new StringBuilder().AppendLine($"App is {versionCount} {versionWord} behind.")
                            .AppendLine("If you choose to update, the app will automatically restart after the update.")
                            .AppendLine($"Would you like to update?")
                            .ToString();
                        UpdaterForm updaterForm = new UpdaterForm(message, updateManager, _font);
                        UpdateUI(() => updaterForm.ShowDialog());
                        restartApp = updaterForm.isUpdated;
                    }
                    else
                    {
                        Log.Information("No updates detected.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"There was an issue during the update process! {ex.Message}");
            }

            if (restartApp)
            {
                UpdateManager.RestartApp();
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            _fontCollection.AddFontFile("./fonts/Barlow.ttf");
            _fontCollection.AddFontFile("./fonts/SegoeUI.ttf");
            var fontBarlow16 = new Font(_fontCollection.Families[0], 16);
            _font = fontBarlow16;
            var fontBarlow20 = new Font(_fontCollection.Families[0], 20);
            var fontBarlow12 = new Font(_fontCollection.Families[0], 12);
            var fontBarlow14 = new Font(_fontCollection.Families[0], 14);
            var fontSegoeUI12 = new Font(_fontCollection.Families[1], 12);
            foreach (var control in panel.Controls)
            {
                switch (control)
                {
                    case Label lbl:
                        lbl.Font = fontBarlow16;
                        break;
                    case Button btn:
                        btn.Font = fontBarlow16;
                        break;
                    case TextBox txt:
                        txt.Font = fontSegoeUI12;
                        break;
                    case ComboBox cmbox:
                        cmbox.Font = fontBarlow12;
                        break;
                    case UserControl uc:
                        (uc.Controls[0] as Label).Font = fontBarlow16;
                        break;
                    case CheckBox chbox:
                        chbox.Font = fontBarlow16;
                        break;
                }
            }
            btnBrowse.Font = fontBarlow12;
            cmboxBrowser.Font = fontBarlow14;
            btnExtractToken.Font = fontBarlow14;
            lblCurrentExtractionOperation.Font = fontBarlow20;
            lblCurrentCourse.Font = fontBarlow20;
            if (File.Exists("./Config.json"))
            {
                try
                {
                    var config = await Config.Fill();
                    txtCourseDirectory.Text = config.CourseDirectory.FullName;
                    txtToken.Text = config.AuthenticationToken;
                    cmboxQuality.SelectedIndex = (int)config.Quality;
                    Log.Information("Acquired data from config");
                }
                catch (JsonSerializationException ex)
                {
                    MessageBox.Show("Config file is corrupt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Log.Error(ex, "Config file is corrupt");
                }
            }
            else
            {
                Log.Information("No Config is found");
            }
            await CheckForUpdates();
        }


        private void txtCourseUrls_Enter(object sender, EventArgs e)
        {
            if (txtCourseUrls.Text == "One course url per line")
                txtCourseUrls.Text = "";
        }

        private void txtCourseUrls_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtCourseUrls.Text))
                txtCourseUrls.Text = "One course url per line";
        }


        private void btnExtractToken_Click(object sender, EventArgs e)
        {
            string token = Extractor.ExtractToken((Browser)cmboxBrowser.SelectedIndex);
            if (token is null)
            {
                MessageBox.Show($"No linkedin learning token is found for {cmboxBrowser.SelectedItem.ToString().Replace("From ", "")}.\nPlease make sure that you are logged into linkedin.com/learning on the browser's default profile", "Token not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                txtToken.Text = token;
            }

        }
    }
}
