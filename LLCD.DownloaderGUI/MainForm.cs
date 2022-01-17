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
        private Control[] _exceptionControls;
        public MainForm()
        {
            InitializeComponent();
            lblCurrentExtractionOperation.Text = "Waiting for input from user";
            UC_CourseExtractorStatus.Status = CourseStatus.NotRunning;
            UCCourseDownloaderStatus.Status = CourseStatus.NotRunning;
            cmboxQuality.SelectedIndex = 0;
            cmboxBrowser.SelectedIndex = 0;
            Focus();
            _exceptionControls = new Control[] { panelStatus };
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            txtCourseDirectory.Text = folderBrowserDialog.SelectedPath;
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            FormHelpers.SetLoadingStatus(true, panelBody, this, _exceptionControls, false);
            UCCourseDownloaderStatus.Status = CourseStatus.NotRunning;
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
                FormHelpers.SetLoadingStatus(false, panelBody, this, _exceptionControls, false);
                return;
            }
            Log.Information("Input Valid");
            UCCourseDownloaderStatus.Status = CourseStatus.NotRunning;
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
                progressBarCourses.Value = 0;
            }
            finally
            {
                FormHelpers.SetLoadingStatus(false, panelBody, this, _exceptionControls, false);
            }
        }
        private void IncrementlblCurrentCourse()
        {
            Regex patternlblCurrentCourse = new Regex(@"Extracting Courses \((?<currentCourse>\d+)\/(?<totalCourses>\d+)\)");
            int currentCourse = int.Parse(patternlblCurrentCourse.Match(lblCurrentExtractionOperation.Text).Groups["currentCourse"].Value);
            int totalCourses = int.Parse(patternlblCurrentCourse.Match(lblCurrentExtractionOperation.Text).Groups["totalCourses"].Value);
            lblCurrentExtractionOperation.Text = $"Extracting Courses ({currentCourse + 1}/{totalCourses})";
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
            progressBarCourses.Step = 100 / extractors.Count;
            await SaveConfig();
            lblCurrentExtractionOperation.Text = $"Extracting Courses (1/{extractors.Count})";
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
            lblCurrentExtractionOperation.Text = "Courses Extracted Successfully";
            UC_CourseExtractorStatus.Status = CourseStatus.Finished;

            var downloaderForm = new DownloaderForm(courses, new DirectoryInfo(txtCourseDirectory.Text), checkBoxExerciseFiles.Checked,checkBoxSubtitles.Checked);
            UCCourseDownloaderStatus.Status = CourseStatus.Running;
            try
            {
                downloaderForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A fatal error occured while downloading the course.\nCheck the logs for more info", "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(ex, "An fatal error occured while downloading the course");
                UCCourseDownloaderStatus.Status = CourseStatus.Failed;
                lblCurrentExtractionOperation.Text = "Course Download Failed";
                return;
            }

            if (downloaderForm.DownloaderStatus == CourseStatus.Finished)
            {
                UCCourseDownloaderStatus.Status = CourseStatus.Finished;
                MessageBox.Show("Course Downloaded Successfully :)", "Hooray", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblCurrentExtractionOperation.Text = "Course Downloaded Successfully";
            }
            else
            {
                UCCourseDownloaderStatus.Status = CourseStatus.Failed;
                lblCurrentExtractionOperation.Text = "Course Download Failed";
            }
        }

        private async Task<Course> ExtractCourse(Extractor extractor)
        {
            UC_CourseExtractorStatus.Status = CourseStatus.Running;

            Course course;
            try
            {
                var progress = new Progress<float>(progressPercent =>
                    {
                        int progressValue = (int)(progressPercent * 100);
                        UpdateUI(() => progressBarExtractor.Value = progressValue == 100 ? 0 : progressValue);
                    });
                course = await extractor.GetCourse(progress);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Course Extraction Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string courses = String.Join(" -- ", txtCourseUrls.Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                Log.Error(ex, "Course Extraction Failed" + courses);
                FormHelpers.SetLoadingStatus(false, panelBody, this, _exceptionControls, false);
                UC_CourseExtractorStatus.Status = CourseStatus.Failed;
                lblCurrentExtractionOperation.Text = "Courses Extraction Failed";
                progressBarCourses.Value = 0;
                progressBarExtractor.Value = 0;
                return null;
            }


            return course;
        }

        private void UpdateUI(Action updateAction)
        {
            if (IsHandleCreated)
            {
                if (InvokeRequired)
                {
                    Invoke(updateAction);
                }
                else
                {
                    updateAction();
                }
            }
            else
            {
                Log.Error("Window handle not created. Can't UpdateUI");
            }
        }
        private T GetFromUI<T>(Func<object> getterAction)
        {
            if (IsHandleCreated)
            {
                if (InvokeRequired)
                {
                    return (T)Invoke(getterAction);
                }
                else
                {
                    return (T)getterAction();
                }
            }
            else
            {
                Log.Error("Window handle not created. Can't GetFromUI");
                return default;
            }
        }


        private async Task SaveConfig()
        {
            lblCurrentExtractionOperation.Text = "Saving config file";
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
                FormHelpers.SetLoadingStatus(false, panelBody, this, _exceptionControls, false);
            }
            try
            {
                await config.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while trying to save config", "Failed to solve", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormHelpers.SetLoadingStatus(false, panelBody, this, _exceptionControls, false);
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
                        UpdaterForm updaterForm = new UpdaterForm(message, updateManager);
                        UpdateUI(() => updaterForm.ShowDialog());
                        restartApp = updaterForm.IsUpdated;
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
            foreach (Control control in this.Controls)
            {
                FormHelpers.SetFonts(control);
            }

            btnBrowse.Font = new Font(FormHelpers.QuicksandFontFamilySemiBold, 12, FontStyle.Bold);
            txtCourseUrls.Font = new Font(FormHelpers.QuicksandFontFamilyRegular, 12);

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

        private void checkBoxDelay_CheckedChanged(object sender, EventArgs e)
        {
            CoupleDelay();
        }

        private void CoupleDelay()
        {
            if (checkBoxDelay.Checked)
            {
                numericUpDownDelay.Enabled = true;
                numericUpDownDelay.Value = 2;
            }
            else
            {
                numericUpDownDelay.Enabled = false;
                numericUpDownDelay.Value = 0;
            }
        }
    }
}
