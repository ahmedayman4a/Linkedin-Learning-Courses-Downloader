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
            Assert.AreEqual(Constant.ValidFirefoxToken, Extractor.ExtractToken(Browser.Firefox));
        }

        [TestMethod]
        public void ExtractToken_InvalidFirefoxCookieExtraction_ReturnsNonEqualTokenValue()
        {
            Assert.AreNotEqual(Constant.InvalidFirefoxToken, Extractor.ExtractToken(Browser.Firefox));
        }

        [TestMethod]
        public void ExtractToken_ValidChromeCookieExtraction_ReturnsEqualTokenValue()
        {
            Assert.AreEqual(Constant.ValidChromeToken, Extractor.ExtractToken(Browser.Chrome));
        }

        [TestMethod]
        public void ExtractToken_InvalidChromeCookieExtraction_ReturnsNonEqualTokenValue()
        {
            Assert.AreNotEqual(Constant.InvalidChromeToken, Extractor.ExtractToken(Browser.Chrome));
        }
    }
}
