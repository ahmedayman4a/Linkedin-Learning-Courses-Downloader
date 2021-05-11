using LLCD.CourseContent;
using LLCD.CourseExtractor;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LLCD.DownloaderGUI
{
    public partial class DownloaderForm : Form
    {
        private List<Course> _courses;
        private DirectoryInfo _courseRootDirectory;
        private readonly bool _toDownloadExerciseFiles;
        private int _videosCount;
        private int _currentVideoIndex = 1;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken _cancellationToken;
        private Downloader _downloader = new Downloader();

        public CourseStatus DownloaderStatus { get; set; } = CourseStatus.Running;

        public DownloaderForm(List<Course> courses, DirectoryInfo courseRootDirectory,bool toDownloadExerciseFiles, Font font)
        {
            _courses = courses;
            _courseRootDirectory = courseRootDirectory;
            _toDownloadExerciseFiles = toDownloadExerciseFiles;
            _cancellationToken = _cancellationTokenSource.Token;
            InitializeComponent();
            Text = "Downloading Courses";
            foreach (var control in flowLayoutPanel.Controls)
            {
                switch (control)
                {
                    case Label lbl:
                        lbl.Font = font;
                        break;
                }
            }
        }

        private async void DownloaderForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < _courses.Count; i++)
            {
                _currentVideoIndex = 1;
                var course = _courses[i];
                lblTotal.Text = $"Downloading Course : {course.Title} [{i + 1}/{_courses.Count}]";
                if (_cancellationToken.IsCancellationRequested) return;
                await DownloadCourse(course);
                progressBarTotal.Value = (i + 1) * 100 / _courses.Count;
            }
            DownloaderStatus = CourseStatus.Finished;
            Close();
        }
        private async Task DownloadExerciseFiles(Course course, DirectoryInfo courseDirectory)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            if (_cancellationToken.IsCancellationRequested) return;
            foreach (var exerciseFile in course.ExerciseFiles)
            {
                lblDownloadingVideo.Visible = false;
                lblVideo.Text = "Downloading exercise file : " + exerciseFile.FileName;
                using (var fileStream = File.Create(Path.Combine(courseDirectory.FullName, exerciseFile.FileName)))
                {
                    await _downloader.DownloadFileAsync(new Uri(exerciseFile.DownloadUrl), fileStream, _cancellationToken, DownloadProgressChanged);
                }

                if (_currentVideoIndex <= _videosCount)
                {
                    UpdateUI(() => progressBarCourse.Value = _currentVideoIndex * 100 / _videosCount);
                }
            }
            lblDownloadingVideo.Visible = true;
        }

        private async Task DownloadCourse(Course course)
        {
            try
            {
                _videosCount = course.Chapters.SelectMany(ch => ch.Videos).Count();
                var courseDirectory = _courseRootDirectory.CreateSubdirectory(ToSafeFileName(course.Title));
                int i = 1;
                foreach (var chapter in course.Chapters)
                {
                    var chapterDirectory = courseDirectory.CreateSubdirectory($"[{i}] {ToSafeFileName(chapter.Title)}");
                    int j = 1;
                    foreach (var video in chapter.Videos)
                    {
                        if (_cancellationToken.IsCancellationRequested) return;
                        await Retry.Do(async () =>
                        {
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                            lblVideo.Text = video.Title + " - [Chapter " + i + "]";
                            lblCourse.Text = _currentVideoIndex++ + "/" + _videosCount;

                            string videoName = $"[{j}] { ToSafeFileName(video.Title)}.mp4";
                            if (String.IsNullOrWhiteSpace(video.Transcript))
                            {
                                string captionName = $"[{j}] { ToSafeFileName(video.Title)}.srt";
                                await SaveSubtitles(Path.Combine(chapterDirectory.FullName, ToSafeFileName(captionName)), video.Transcript);
                            }
                            using (var fileStream = File.Create(Path.Combine(chapterDirectory.FullName, videoName)))
                            {
                                await _downloader.DownloadFileAsync(new Uri(video.DownloadUrl), fileStream, _cancellationToken, DownloadProgressChanged);
                            }
                            if (_currentVideoIndex <= _videosCount)
                            {
                                UpdateUI(() => progressBarCourse.Value = _currentVideoIndex * 100 / _videosCount);
                            }
                        },
                        exceptionMessage: "Failed to download video with title " + video.Title,
                        actionOnError: () =>
                        {
                            UpdateUI(() => progressBarVideo.Value = 0);
                            _currentVideoIndex--;
                        },
                        actionOnFatal: () =>
                        {
                            DownloaderStatus = CourseStatus.Failed;
                            Close();
                        });
                        j++;
                    }
                    i++;
                }
                if (_toDownloadExerciseFiles && course.ExerciseFiles != null && course.ExerciseFiles.Count > 0)
                {
                    await DownloadExerciseFiles(course, courseDirectory);
                }
            }
            catch (Exception ex)
            {
                DownloaderStatus = CourseStatus.Failed;
                Close();
                throw ex;
            }
        }

        private void DownloadProgressChanged(long downloadedBytes, long totalBytes)
        {
            int progressPercentage = (int)((double)downloadedBytes / (double)totalBytes * 100);
            UpdateUI(() =>
            {
                if (totalBytes == -1) //sometimes linkedin api doesn't return the file size
                {
                    if (progressBarVideo.Style == ProgressBarStyle.Continuous)
                    {
                        progressBarVideo.Style = ProgressBarStyle.Marquee;
                        lblPercentage.Text = "";
                    }
                }
                else
                {
                    if (progressBarVideo.Style == ProgressBarStyle.Marquee)
                        progressBarVideo.Style = ProgressBarStyle.Continuous;
                    progressBarVideo.Value = progressPercentage;
                    lblPercentage.Text = progressPercentage + "%";
                }
            });
        }

        private void UpdateUI(Action updateAction)
        {
            if (!_cancellationToken.IsCancellationRequested)
                Invoke(updateAction);
        }

        private static string ToSafeFileName(string fileName) => string.Concat(fileName.Split(Path.GetInvalidFileNameChars()));

        private async Task SaveSubtitles(string filePath, string subtitles)
        {
            using (var streamWriter = new StreamWriter(filePath, false))
                await streamWriter.WriteAsync(subtitles);
        }

        private void DownloaderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DownloaderStatus != CourseStatus.Finished)
            {
                DownloaderStatus = CourseStatus.Failed;
                _cancellationTokenSource.Cancel();
                _downloader.Dispose();
            }
        }
    }
}
