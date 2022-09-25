using LLCD.CourseContent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LLCD.CourseExtractor
{
    public class LearningPathExtractor
    {
        private readonly string _slug;
        private readonly ExtractionSession _session;

        public LearningPathExtractor(string slug, string token)
        {
            _slug = slug;
            _session = new ExtractionSession(token);
        }

        public async Task<LearningPath> GetLearningPath()
        {
            var response = await _session.GetResponse($"https://linkedin.com/learning-api/detailedLearningPaths?learningPathSlug={_slug}&q=slug&version=2");
            var learningPathJson = await response.Content.ReadAsStringAsync();

            LearningPath learningPath = LearningPath.FromJson(learningPathJson);

            return learningPath;
        }
    }
}
