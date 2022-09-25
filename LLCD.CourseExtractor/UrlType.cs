using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LLCD.CourseExtractor
{
    public enum UrlType
    {
        Course,
        LearningPath,
        Invalid
    }

    public static class UrlTypeHelper
    {
        /// <summary>
        /// Categeorizes supplied url to UrlType
        /// </summary>
        /// <param name="url">url to categorize</param>
        /// <param name="slug">output: slug extracted from the supplied url. sets to "" if url is invalid</param>
        /// <returns>UrlType, either [Course, LearningPath, Invalid]</returns>
        public static UrlType CategorizeUrl(string url, out string slug)
        {
            slug = "";
            if (!url.Contains("https://") || !url.Contains("http://"))
            {
                url = "https://" + url;
            }

            Regex patternLearningPathUrl = new Regex(@"https?:\/\/(?:www\.)?linkedin\.com\/learning\/path\/(?<slug>[a-zA-Z0-9-]+)");

            if (patternLearningPathUrl.IsMatch(url))
            {
                slug = patternLearningPathUrl.Match(url).Groups["slug"].Value;
                return UrlType.LearningPath;
            }

            Regex patternCourseUrl = new Regex(@"https?:\/\/(?:www\.)?linkedin\.com\/learning\/(?<slug>[a-zA-Z0-9-]+)");

            if (patternCourseUrl.IsMatch(url))
            {
                slug = patternCourseUrl.Match(url).Groups["slug"].Value;
                return UrlType.Course;
            }
            return UrlType.Invalid;
        }

        /// <summary>
        /// Categeorizes supplied url to UrlType
        /// </summary>
        /// <param name="url">url to categorize</param>
        /// <returns>UrlType, either [Course, LearningPath, Invalid]</returns>
        public static UrlType CategorizeUrl(string url)
        {
            if (!url.Contains("https://") || !url.Contains("http://"))
            {
                url = "https://" + url;
            }

            Regex patternLearningPathUrl = new Regex(@"https?:\/\/(?:www\.)?linkedin\.com\/learning\/paths\/(?<slug>[a-zA-Z0-9-]+)");

            if (patternLearningPathUrl.IsMatch(url))
            {
                return UrlType.LearningPath;
            }

            Regex patternCourseUrl = new Regex(@"https?:\/\/(?:www\.)?linkedin\.com\/learning\/(?<slug>[a-zA-Z0-9-]+)");

            if (patternCourseUrl.IsMatch(url))
            {
                return UrlType.Course;
            }
            return UrlType.Invalid;
        }
    }
}
