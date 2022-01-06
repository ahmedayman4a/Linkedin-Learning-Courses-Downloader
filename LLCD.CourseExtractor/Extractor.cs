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
    public class Extractor
    {
        public delegate void LinksExtractionEventHandler();
        private readonly Quality _quality;
        private readonly int _delay;
        private string _courseUrl;
        private string _courseSlug;
        private HttpClient _client;
        private CookieContainer _cookieContainer;
        private string _linkedinHomeRaw;
        private bool _isTokenChecked = false;

        public string EnterpriseProfileHash { get; set; }

        public Extractor(string courseUrl, Quality quality, string token, int delay = 0)
        {
            _courseUrl = courseUrl;
            _quality = quality;
            _delay = delay;
            _cookieContainer = new CookieContainer();
            _cookieContainer.Add(new Cookie("li_at", token, "/", ".www.linkedin.com"));
            var clienthandler = new HttpClientHandler { UseCookies = true, CookieContainer = _cookieContainer };
            _client = new HttpClient(clienthandler);
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:88.0) Gecko/20100101 Firefox/88.0");
        }

        public static string ExtractToken(Browser browser)
        {
            var cookieExtractor = new CookiesExtractor(".www.linkedin.com");
            List<DBCookie> cookies;
            switch (browser)
            {
                case Browser.Chrome:
                    cookies = cookieExtractor.ReadChromeCookies();
                    break;
                case Browser.Firefox:
                    cookies = cookieExtractor.ReadFirefoxCookies();
                    break;
                case Browser.Edge:
                    cookies = cookieExtractor.ReadEdgeCookies();
                    break;
                default:
                    throw new ArgumentException("browser");
            }
            if (cookies.Where(c => c.Name == "li_at").Count() != 0)
            {
                return cookies.Where(c => c.Name == "li_at").First().Value;
            }
            return null;
        }

        public async Task<Course> GetCourse(IProgress<float> progress = null)
        {
            if (!HasValidUrl())
            {
                throw new ArgumentException("Invalid Course Url : " + _courseUrl);
            }
            if (!_isTokenChecked && !await HasValidToken())
            {
                throw new ArgumentException("Invalid Token");
            }
            EnterpriseProfileHash = await ExtractEnterpriseProfileHash();
            if (!String.IsNullOrEmpty(EnterpriseProfileHash))
            {
                _client.DefaultRequestHeaders.Add("x-li-identity", EnterpriseProfileHash);
            }
            var courseResponse = await _client.GetAsync($"https://www.linkedin.com/learning-api/detailedCourses?courseSlug={_courseSlug}&fields=chapters,title,exerciseFiles&addParagraphsToTranscript=true&q=slugs");
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
                    throw new ArgumentException("Token is expired. Please use the latest one.", ex);
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
                    var video = chapter.Videos[i];
                    string slug = video.Slug;
                    var videoResponse = await _client.GetAsync($"https://www.linkedin.com/learning-api/detailedCourses?courseSlug={_courseSlug}&resolution=_{_quality.ToHeight()}&q=slugs&fields=selectedVideo&videoSlug={video.Slug}");
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
                        var cookies = _cookieContainer.GetCookies(new Uri("https://www.linkedin.com/learning-api"));
                        throw new ArgumentException("Failed to extract a course video. The provided token is probably invalid");
                    }
                    chapter.Videos[i] = video;
                    progress?.Report(j / totalCount);
                    await Task.Delay(_delay * 1000);
                }
            }
            return course;
        }

        public async Task<bool> HasValidToken()
        {
            Regex patternTrialLink = new Regex(@"nav__button-tertiary.*\n?.\r?.*Start free trial");
            if (_linkedinHomeRaw is null)
            {
                var response = await _client.GetAsync("https://www.linkedin.com/learning");
                _linkedinHomeRaw = await response.Content.ReadAsStringAsync();
                _linkedinHomeRaw = WebUtility.HtmlDecode(_linkedinHomeRaw);
            }

            if (patternTrialLink.IsMatch(_linkedinHomeRaw))
            {
                return false;
            }
            var cookies = _cookieContainer.GetCookies(new Uri("https://www.linkedin.com/learning"));
            var jsession = cookies["JSESSIONID"].Value;
            _client.DefaultRequestHeaders.Add("Csrf-Token", jsession);
            _isTokenChecked = true;
            return true;

        }

        private async Task<String> ExtractEnterpriseProfileHash()
        {
            Regex patternEnterpriseProfileHash = new Regex(@"enterpriseProfileHash"":""(?<enterpriseProfileHash>.*?)""");
            if (_linkedinHomeRaw is null)
            {
                var response = await _client.GetAsync("https://www.linkedin.com/learning");
                _linkedinHomeRaw = await response.Content.ReadAsStringAsync();
                _linkedinHomeRaw = WebUtility.HtmlDecode(_linkedinHomeRaw);
            }

            if (patternEnterpriseProfileHash.IsMatch(_linkedinHomeRaw))
            {
                return patternEnterpriseProfileHash.Match(_linkedinHomeRaw).Groups["enterpriseProfileHash"].Value;
            }
            return null;
        }
        public bool HasValidUrl()
        {
            if (!_courseUrl.Contains("https://") || !_courseUrl.Contains("http://"))
            {
                _courseUrl = "https://" + _courseUrl;
            }
            Regex patternCourseUrl = new Regex(@"https?:\/\/(?:www\.)?linkedin\.com\/learning\/(?<courseSlug>[a-zA-Z0-9-]+)");

            if (patternCourseUrl.IsMatch(_courseUrl))
            {
                _courseSlug = patternCourseUrl.Match(_courseUrl).Groups["courseSlug"].Value;
                return true;
            }
            return false;
        }

    }
}
