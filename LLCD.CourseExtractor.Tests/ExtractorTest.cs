using Microsoft.VisualStudio.TestTools.UnitTesting;
using LLCD.CourseExtractor;
using LLCD.CourseContent;
using System.Threading.Tasks;
using System.Collections.Generic;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;

namespace LLCD.CourseExtractor.Tests
{
    [TestClass]
    public class ExtractorTest
    {

        [TestMethod]
        [TestCategory("Token Validity")]
        public async Task ExtractToken_ValidToken_ReturnsTrue()
        {
            Assert.IsTrue(await Validator.IsTokenValid(Constant.ValidFirefoxToken));
        }

        [TestMethod]
        [TestCategory("Token Validity")]
        public async Task ExtractToken_InvalidToken_ReturnsFalse()
        {
           Assert.IsFalse(await Validator.IsTokenValid(Constant.InvalidFirefoxToken));
        }

        [TestMethod]
        [TestCategory("Course Extraction")]
        public async Task GetCourse_ValidCourse_ReturnsEqualCourseData()
        {
            var extractor = new Extractor("learning-to-be-assertive", Quality.Low, Constant.ValidFirefoxToken);
            //var progress = new Progress<float>(progressPercent => ConsoleOutput.Instance.WriteLine((progressPercent * 100).ToString(), OutputLevel.Information));
            var course = await extractor.GetCourse();
            CompareLogic compareLogic = new CompareLogic();
            compareLogic.Config.IgnoreProperty<Video>(x => x.DownloadUrl);
            compareLogic.Config.IgnoreProperty<Video>(x => x.TranscriptLines);
            compareLogic.Config.IgnoreProperty<ExerciseFile>(x => x.DownloadUrl);
            ComparisonResult comparisonResult = compareLogic.Compare(Constant.ValidCourse, course);
            Assert.AreEqual(Constant.ValidCourse, course, comparisonResult.DifferencesString);
        }

        [TestMethod]
        [TestCategory("Course Extraction")]
        public async Task GetCourse_CoursesExtractionStressTest_ReturnsNumberOfExtractions()
        {
            string[] links = new string[]
            {
                "web-development-foundations-web-technologies",
                "react-native-essential-training",
                "android-app-development-design-patterns-for-mobile-architecture",
                "html-essential-training-4",
                "html-and-css-linking",
                "developing-for-web-performance",
                "learning-google-flutter-for-mobile-developers",
                "react-native-building-mobile-apps-3",
                "ios-app-development-design-patterns-for-mobile-architecture",
                "view-source",
                "programming-foundations-databases-2",
                "learning-sql-programming-8382385",
                "learning-mysql-development-2",
                "learning-mongodb",
                "sql-data-reporting-and-analysis-2",
                "essential-math-for-machine-learning-python-edition/explore-core-mathematical-concepts",
                "applied-machine-learning-algorithms",
                "web-scraping-with-python",
                "python-for-data-visualization",
                "python-data-analysis-2"
            };
            int i = 0;
            foreach (var link in links)
            {
                i++;
                var extractor = new Extractor(link, Quality.Low, Constant.ValidFirefoxToken);
                var course = await extractor.GetCourse();
            }
            ConsoleOutput.Instance.WriteLine($"Extracted {i} courses", OutputLevel.Information);
        }

        [TestMethod]
        [TestCategory("Course Extraction")]
        public async Task GetCourse_InValidCourse_ReturnsNonEqualCourseData()
        {
            var extractor = new Extractor("https://www.linkedin.com/learning/learning-to-be-assertive", Quality.Low, Constant.ValidFirefoxToken);
            var course = await extractor.GetCourse();
            Assert.AreNotEqual(Constant.InvalidCourse, course);
        }

       

    }
}

//foreach (var chapter in course.Chapters)
//{
//    foreach (var video in chapter.Videos)
//    {
//        video.TranscriptLines = null;
//    }
//}
//string courseCode = ObjectDumper.Dump(course, DumpStyle.CSharp);