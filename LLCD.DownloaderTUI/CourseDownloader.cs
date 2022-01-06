using LLCD.CourseContent;
using LLCD.CourseExtractor;
using Serilog;
using ShellProgressBar;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;

namespace LLCD.DownloaderTUI
{
    public static class CourseDownloader
    {
        private static ChildProgressBar pbarVideo;
        private static ChildProgressBar pbarExerciseFiles;
        private static string currentVideo;
        private static string currentExerciseFile;
        private static int currentIndex;
        #region ProgressBarOptions


        private readonly static ProgressBarOptions optionsChapter = new ProgressBarOptions
        {
            ScrollChildrenIntoView = true,
            ForegroundColor = ConsoleColor.Blue,
            ForegroundColorDone = ConsoleColor.DarkGreen,
            BackgroundColor = ConsoleColor.Gray,
            ProgressBarOnBottom = true,
            CollapseWhenFinished = false
        };

        private readonly static ProgressBarOptions optionsVideo = new ProgressBarOptions
        {
            ScrollChildrenIntoView = true,
            ForegroundColor = ConsoleColor.Yellow,
            ForegroundColorDone = ConsoleColor.DarkGreen,
            BackgroundColor = ConsoleColor.DarkGray,
            ProgressCharacter = '\u2593',
            ProgressBarOnBottom = true,
            CollapseWhenFinished = true
        };
        private readonly static ProgressBarOptions optionsCourse = new ProgressBarOptions
        {
            ScrollChildrenIntoView = true,
            ForegroundColor = ConsoleColor.DarkGray,
            ForegroundColorDone = ConsoleColor.DarkGreen,
            BackgroundColor = ConsoleColor.White,
            ProgressBarOnBottom = true,
            CollapseWhenFinished = false
        };
        #endregion
        private static string ToSafeFileName(string fileName) => string.Concat(fileName.Split(Path.GetInvalidFileNameChars()));
        public static void DownloadCourse(Course course, DirectoryInfo courseRootDirectory)
        {
            try
            {
                int exerciseFilesCount = course.ExerciseFiles is null ? 0 : course.ExerciseFiles.Count;
                using var pbarCourse = new ProgressBar(course.Chapters.ToList().Count + +exerciseFilesCount, "Downloading Course : " + course.Title, optionsCourse);
                var courseDirectory = courseRootDirectory.CreateSubdirectory(ToSafeFileName(course.Title));

                for (int i = 0; i < course.Chapters.Count; i++)
                {
                    var chapter = course.Chapters[i];
                    var chapterDirectory = courseDirectory.CreateSubdirectory($"{(i + 1):D2} - {ToSafeFileName(chapter.Title)}");
                    using var pbarChapter = pbarCourse.Spawn(chapter.Videos.ToList().Count, $"Downloading Chapter {i + 1} : {chapter.Title}", optionsChapter);
                    for (int j = 0; j < chapter.Videos.Count; j++)
                    {
                        var video = chapter.Videos[j];
                        currentVideo = video.Title;
                        currentIndex = j + 1;
                        DownloadVideo(chapterDirectory, pbarChapter, video);
                        pbarChapter.Tick();
                    }
                    pbarChapter.Message = $"Chapter {i + 1} : {chapter.Title} chapter has been downloaded successfully";
                    pbarCourse.Tick();
                }
                if (course.ExerciseFiles != null && course.ExerciseFiles.Count > 0)
                {
                    foreach (var exerciseFile in course.ExerciseFiles)
                    {
                        DownloadExerciseFile(courseDirectory, pbarCourse, exerciseFile);
                        pbarCourse.Tick();
                    }
                }
                pbarCourse.Message = $"{course.Title} course has been downloaded successfully";

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Course downloaded successfully :)");
                Console.ResetColor();
                Log.Information("Course downloaded successfully");
            }
            catch (Exception ex)
            {
                TUI.ShowError("An error occured while downloading the course");
                TUI.ShowError("Error details : " + ex.Message);
                TUI.ShowError("Trying again...");
                Log.Error(ex, "Error while Downloading");
                DownloadCourse(course, courseRootDirectory);
            }
        }

        private static void DownloadVideo(DirectoryInfo chapterDirectory, ChildProgressBar pbarChapter, Video video)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            using (pbarVideo = pbarChapter.Spawn(100, $"Downloading Video {currentIndex} : {currentVideo}", optionsVideo))
            {
                Retry.Do(() =>
                {
                    using var downloadClient = new WebClient();
                    downloadClient.DownloadProgressChanged += VideoDownloadClient_DownloadProgressChanged;
                    downloadClient.DownloadFileCompleted += VideoDownloadClient_DownloadFileCompleted;
                    string videoName = $"{currentIndex:D2} - { ToSafeFileName(video.Title)}.mp4";
                    if (!String.IsNullOrWhiteSpace(video.Transcript))
                    {
                        string captionName = $"{currentIndex:D2} - { ToSafeFileName(video.Title)}.srt";
                        File.WriteAllText($"{Path.Combine(chapterDirectory.FullName, ToSafeFileName(captionName))}", video.Transcript);
                    }
                    downloadClient.DownloadFileTaskAsync(new Uri(video.DownloadUrl), Path.Combine(chapterDirectory.FullName, videoName)).Wait();
                },
                exceptionMessage: "Failed to download video with title " + video.Title,
                actionOnError: () =>
                {
                    var progress = pbarVideo.AsProgress<float>();
                    progress?.Report(0);
                });
            }

        }

        private static void VideoDownloadClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            pbarVideo.Message = $"Video {currentIndex} : {currentVideo} has been downloaded successfully";
            pbarVideo.AsProgress<float>().Report(1);
        }

        private static void VideoDownloadClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            float KbReceived = e.BytesReceived / 1024;
            float TotalKbToReceive = e.TotalBytesToReceive / 1024;
            pbarVideo.Message = String.Format($"Downloading Video {currentIndex} : {currentVideo} {KbReceived}KB out of {TotalKbToReceive}KB");
            float percentage = KbReceived / TotalKbToReceive;
            var progress = pbarVideo.AsProgress<float>();
            progress?.Report(percentage);
        }

        private static void DownloadExerciseFile(DirectoryInfo courseDirectory, ProgressBar pbarCourse, ExerciseFile exerciseFile)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            using (pbarExerciseFiles = pbarCourse.Spawn(100, $"Downloading Video {currentIndex} : {currentVideo}", optionsVideo))
            {
                Retry.Do(() =>
                {
                    using (var downloadClient = new WebClient())
                    {
                        downloadClient.DownloadProgressChanged += ExerciseFileDownloadClient_DownloadProgressChanged;
                        downloadClient.DownloadFileCompleted += ExerciseFileDownloadClient_DownloadFileCompleted;
                        currentExerciseFile = exerciseFile.FileName;
                        downloadClient.DownloadFileTaskAsync(new Uri(exerciseFile.DownloadUrl), Path.Combine(courseDirectory.FullName, exerciseFile.FileName)).Wait();
                    }
                },
                exceptionMessage: "Failed to download exerciseFile with name " + exerciseFile.FileName,
                actionOnError: () =>
                {
                    var progress = pbarExerciseFiles.AsProgress<float>();
                    progress?.Report(0);
                });
            }

        }

        private static void ExerciseFileDownloadClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            pbarExerciseFiles.Message = $"Exercise File {currentExerciseFile} has been downloaded successfully";
            pbarExerciseFiles.AsProgress<float>().Report(1);
        }

        private static void ExerciseFileDownloadClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            float mbReceived = e.BytesReceived / 1024f / 1024f;
            float TotalmbToReceive = e.TotalBytesToReceive / 1024f / 1024f;
            pbarExerciseFiles.Message = $"Downloading Exercise File {currentExerciseFile} {mbReceived:f2}MB out of {TotalmbToReceive:f2}MB";
            float percentage = mbReceived / TotalmbToReceive;
            var progress = pbarExerciseFiles.AsProgress<float>();
            progress?.Report(percentage);
        }
    }
}
