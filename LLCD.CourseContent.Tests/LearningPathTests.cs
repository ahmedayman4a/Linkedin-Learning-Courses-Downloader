using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LLCD.CourseContent;
using System.IO;

namespace LLCD.CourseContent.Tests
{
    [TestClass]
    public class LearningPathTests
    {
        [TestMethod]
        [TestCategory("Serialization")]
        public void CoursesLinksSerialization_Valid_ReturnsEqualLinks()
        {
            string LearningPathJson = File.ReadAllText("./path.json");
            var learningpath = LearningPath.FromJson(LearningPathJson);
            CollectionAssert.AreEqual(learningpath.CoursesSlugs, ValidSlugs);
        }

        readonly List<string> ValidSlugs = new()
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
