using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLCD.CourseExtractor.Tests
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        [TestCategory("Link Validity")]
        public void HasValidUrl_ValidCourseUrl_ReturnsTrue()
        {
            Assert.IsTrue(Validator.IsUrlValid("https://www.linkedin.com/learning/learning-to-be-assertive"));
        }

        [TestMethod]
        [TestCategory("Link Validity")]
        public void HasValidUrl_ValidLearningPathUrl_ReturnsTrue()
        {
            Assert.IsTrue(Validator.IsUrlValid("https://www.linkedin.com/learning/paths/become-a-c-sharp-developer"));
        }

        [TestMethod]
        [TestCategory("Link Validity")]
        public void HasValidUrl_InvalidUrl_ReturnsFalse()
        {
            Assert.IsFalse(Validator.IsUrlValid("https://www.linkedin.com/"));
        }

        [TestMethod]
        [TestCategory("EnterpriseProfileHash Validity")]
        public async void GetEnterpriseProfileHash_ValidEnterpriseProfileHash_ReturnsSameValueAsync()
        {
            var session = new ExtractionSession(Constant.ValidFirefoxToken);
            await session.Initialize();
            List<string> headerValues = session.Client.DefaultRequestHeaders.GetValues("x-li-identity").ToList();
            Assert.AreEqual(Constant.ValidEnterpriseProfileHash, headerValues[0]);
        }

        [TestMethod]
        [TestCategory("EnterpriseProfileHash Validity")]
        public void GetEnterpriseProfileHash_InvalidToken_ReturnsFalse()
        {
            using var session = new ExtractionSession(Constant.InvalidFirefoxToken);
            bool isHeaderPresent = session.Client.DefaultRequestHeaders.TryGetValues("x-li-identity", out var values);
            Assert.IsFalse(isHeaderPresent);
        }
    }
}
