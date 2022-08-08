using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLCD.CourseExtractor.Tests
{
    [TestClass]
    public class TokenExtractorTest
    {
        [TestMethod]
        public void ExtractToken_ValidFirefoxCookieExtraction_ReturnsEqualTokenValue()
        {
            Assert.AreEqual(VALID_FIREFOX_TOKEN, Extractor.ExtractToken(Browser.Firefox));
        }

        [TestMethod]
        public void ExtractToken_InvalidFirefoxCookieExtraction_ReturnsNonEqualTokenValue()
        {
            Assert.AreNotEqual(INVALID_FIREFOX_TOKEN, Extractor.ExtractToken(Browser.Firefox));
        }

        [TestMethod]
        public void ExtractToken_ValidChromeCookieExtraction_ReturnsEqualTokenValue()
        {
            Assert.AreEqual(VALID_CHROME_TOKEN, Extractor.ExtractToken(Browser.Chrome));
        }

        [TestMethod]
        public void ExtractToken_InvalidChromeCookieExtraction_ReturnsNonEqualTokenValue()
        {
            Assert.AreNotEqual(INVALID_CHROME_TOKEN, Extractor.ExtractToken(Browser.Chrome));
        }
    }
}
