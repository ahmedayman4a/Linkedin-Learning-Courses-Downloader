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
            var extractor = new Extractor("https://www.linkedin.com/learning/learning-to-be-assertive?autoplay=true&u=104942210", Quality.Low, Constant.ValidFirefoxToken);
            Assert.IsTrue(await extractor.HasValidToken());
        }

        [TestMethod]
        [TestCategory("Token Validity")]
        public async Task ExtractToken_InvalidToken_ReturnsFalse()
        {
            var extractor = new Extractor("https://www.linkedin.com/learning/learning-to-be-assertive?autoplay=true&u=104942210", Quality.Low, Constant.InvalidFirefoxToken);
            Assert.IsFalse(await extractor.HasValidToken());
        }

        [TestMethod]
        [TestCategory("Course Extraction")]
        public async Task GetCourse_ValidCourse_ReturnsEqualCourseData()
        {
            var extractor = new Extractor("https://www.linkedin.com/learning/learning-to-be-assertive?autoplay=true&u=104942210", Quality.Low, Constant.ValidFirefoxToken);
            var progress = new Progress<float>(progressPercent => ConsoleOutput.Instance.WriteLine((progressPercent * 100).ToString(), OutputLevel.Information));
            var course = await extractor.GetCourse(progress);
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
                "https://www.linkedin.com/learning/web-development-foundations-web-technologies",
                "https://www.linkedin.com/learning/react-native-essential-training",
                "https://www.linkedin.com/learning/android-app-development-design-patterns-for-mobile-architecture",
                "https://www.linkedin.com/learning/html-essential-training-4",
                "https://www.linkedin.com/learning/html-and-css-linking",
                "https://www.linkedin.com/learning/developing-for-web-performance",
                "https://www.linkedin.com/learning/learning-google-flutter-for-mobile-developers",
                "https://www.linkedin.com/learning/react-native-building-mobile-apps-3",
                "https://www.linkedin.com/learning/ios-app-development-design-patterns-for-mobile-architecture",
                "https://www.linkedin.com/learning/view-source",
                "https://www.linkedin.com/learning/programming-foundations-databases-2",
                "https://www.linkedin.com/learning/learning-sql-programming-8382385",
                "https://www.linkedin.com/learning/learning-mysql-development-2",
                "https://www.linkedin.com/learning/learning-mongodb",
                "https://www.linkedin.com/learning/sql-data-reporting-and-analysis-2",
                "https://www.linkedin.com/learning/essential-math-for-machine-learning-python-edition/explore-core-mathematical-concepts",
                "https://www.linkedin.com/learning/applied-machine-learning-algorithms",
                "https://www.linkedin.com/learning/web-scraping-with-python",
                "https://www.linkedin.com/learning/python-for-data-visualization",
                "https://www.linkedin.com/learning/python-data-analysis-2"
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
            var extractor = new Extractor("https://www.linkedin.com/learning/learning-to-be-assertive?autoplay=true&u=104942210", Quality.Low, Constant.ValidFirefoxToken);
            var course = await extractor.GetCourse();
            Assert.AreNotEqual(Constant.InvalidCourse, course);
        }

        [TestMethod]
        [TestCategory("Link Validity")]
        public void HasValidUrl_ValidUrl_ReturnsTrue()
        {
            var extractor = new Extractor("https://www.linkedin.com/learning/learning-to-be-assertive/welcome?u=104942210", Quality.Low, Constant.ValidFirefoxToken);
            Assert.IsTrue(extractor.HasValidUrl());
        }

        [TestMethod]
        [TestCategory("EnterpriseProfileHash Validity")]
        public async Task GetEnterpriseProfileHash_ValidEnterpriseProfileHash_ReturnsSameValue()
        {
            var extractor = new Extractor("https://www.linkedin.com/learning/learning-to-be-assertive?autoplay=true&u=104942210", Quality.Low, Constant.ValidFirefoxToken);
            await extractor.GetCourse();
            Assert.AreEqual(Constant.ValidEnterpriseProfileHash, extractor.EnterpriseProfileHash);
        }

        [TestMethod]
        [TestCategory("EnterpriseProfileHash Validity")]
        public async Task GetEnterpriseProfileHash_InvalidToken_ReturnsNull()
        {
            var extractor = new Extractor("https://www.linkedin.com/learning/learning-to-be-assertive?autoplay=true&u=104942210", Quality.Low, Constant.InvalidFirefoxToken);
            await extractor.GetCourse();
            Assert.IsNull(extractor.EnterpriseProfileHash);
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