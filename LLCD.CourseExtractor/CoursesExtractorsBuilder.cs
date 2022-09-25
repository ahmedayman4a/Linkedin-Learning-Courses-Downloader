using LLCD.CourseContent;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LLCD.CourseExtractor
{
    public static class CoursesExtractorsBuilder
    {

        public static async Task<List<Extractor>> BuildExtractors(IEnumerable<string> urls, Quality quality, string token, int delay = 0)
        {
            var extractors = new List<Extractor>();

            foreach (var url in urls)
            {
                var urlType = UrlTypeHelper.CategorizeUrl(url,out string slug);
                switch (urlType)
                {
                    case UrlType.Course:
                        var extractor = new Extractor(slug, quality,token,delay);
                        extractors.Add(extractor);
                        break;
                    case UrlType.LearningPath:
                        var learningPathExtractor = new LearningPathExtractor(slug, token);
                        var learningPath = await learningPathExtractor.GetLearningPath();
                        foreach (string courseSlug in learningPath.CoursesSlugs)
                        {
                            var courseExtractor = new Extractor(courseSlug, quality,token,delay);
                            extractors.Add(courseExtractor);
                        }
                        break;
                    case UrlType.Invalid:
                    default:
                        continue;
                }
            }
            return extractors;
        }

    }
}
