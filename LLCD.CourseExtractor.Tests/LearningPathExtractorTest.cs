using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LLCD.CourseExtractor.Tests
{
    [TestClass]
    public class LearningPathExtractorTest
    {
        [TestMethod]
        public async Task GetSlugsInLearningPath_ValidSlugs_ReturnsSameSlugsList()
        {
            var learningPathExtractor = new LearningPathExtractor("become-a-c-sharp-developer", Constant.ValidFirefoxToken);
            var learningPath = await learningPathExtractor.GetLearningPath();
            CollectionAssert.AreEqual(learningPath.CoursesSlugs, ValidSlugs);
        }

        readonly List<string> ValidSlugs = new List<string>()
        {
            "c-sharp-essential-training-1-types-and-control-flow",
            "c-sharp-essential-training-2-generics-collections-and-linq",
            "code-clinic-c-sharp-2018",
            "c-sharp-algorithms",
            "c-sharp-design-patterns-part-1-14140825",
            "nail-your-c-sharp-developer-interview"
        };
    }
}
