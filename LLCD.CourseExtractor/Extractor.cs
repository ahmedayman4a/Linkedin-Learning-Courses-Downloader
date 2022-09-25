using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using LLCD.CourseContent;
using Microsoft.CSharp;
using Newtonsoft.Json;
using Serilog;

namespace LLCD.CourseExtractor
{
    public class Extractor : IDisposable
    {
        private readonly Quality _quality;
        private readonly int _delay;
        private readonly string _courseSlug;
        private readonly ExtractionSession _extractionSession;
        
        
        private static Random _random = new Random();


        public Extractor(string courseSlug, Quality quality, string token, int delay = 0)
        {
            _courseSlug = courseSlug;
            _quality = quality;
            _delay = delay;
            _extractionSession = new ExtractionSession(token);
        }

        

        public async Task<Course> GetCourse(IProgress<float> progress = null)
        {
            var courseResponse = await _extractionSession.GetResponse($"https://www.linkedin.com/learning-api/detailedCourses?courseSlug={_courseSlug}&fields=chapters,title,exerciseFiles&addParagraphsToTranscript=true&q=slugs");
            var courseResponseText = await courseResponse.Content.ReadAsStringAsync();

            Course course;
            try
            {
                course = Course.FromJson(courseResponseText);
            }
            catch (Exception ex)
            {
                if (courseResponseText.Contains("CSRF check failed"))
                {
                    throw new ArgumentException("Token is expired. Please use a new one.", ex);
                }
                else
                {
                    Log.Error("Course Deserialization failed. \nResponse text : " + courseResponseText);
                    throw;
                }
            }

            course.Slug = _courseSlug;
            float j = 1;
            float totalCount = course.Chapters.SelectMany(c => c.Videos).Count();
            foreach (var chapter in course.Chapters)
            {
               
                for (int i = 0; i < chapter.Videos.Count(); i++, j++)
                {
                    await Retry.Do(async () => 
                    {
                        var video = chapter.Videos[i];
                        string slug = video.Slug;
                        var videoResponse = await _extractionSession.GetResponse($"https://www.linkedin.com/learning-api/detailedCourses?courseSlug={_courseSlug}&resolution=_{_quality.ToHeight()}&q=slugs&fields=selectedVideo&videoSlug={video.Slug}");
                        var videoResponseText = await videoResponse.Content.ReadAsStringAsync();

                        try
                        {
                            video = Video.FromJson(videoResponseText);
                        }
                        catch (Exception)
                        {
                            Log.Error("Video Deserialization failed. \nResponse text : " + videoResponseText);
                            throw;
                        }
                        video.Slug = slug;
                        if (String.IsNullOrWhiteSpace(video.DownloadUrl))
                        {
                            Log.Error($"Failed to extract a course video. Request Uri : {videoResponse.RequestMessage.RequestUri}\nResponseMessage : {videoResponse.StatusCode} - {videoResponse.ReasonPhrase}\nResponseText : {videoResponseText}");
                            throw new ArgumentException("Failed to extract a course video. The provided token is probably invalid");
                        }
                        chapter.Videos[i] = video;
                        progress?.Report(j / totalCount);
                        if (_delay > 0)
                        {
                            double randomDelay = _delay + _random.NextDouble();
                            randomDelay -= _random.NextDouble();
                            var delay = TimeSpan.FromMilliseconds(randomDelay * 1000);
                            await Task.Delay(delay);
                        }
                        
                    },
                    exceptionMessage: "Error occured while getting video inside chapter loop",
                    retries: 3);
            }
            }
            return course;
        }

        public void Dispose()
        {
            _extractionSession?.Dispose();
        }
    }
}
