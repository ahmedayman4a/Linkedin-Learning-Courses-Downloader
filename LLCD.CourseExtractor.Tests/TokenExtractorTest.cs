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
            Assert.AreEqual(Constant.ValidFirefoxToken, TokenExtractor.ExtractToken(Browser.Firefox));
        }

        [TestMethod]
        public void ExtractToken_InvalidFirefoxCookieExtraction_ReturnsNonEqualTokenValue()
        {
            Assert.AreNotEqual(Constant.InvalidFirefoxToken, TokenExtractor.ExtractToken(Browser.Firefox));
        }

        [TestMethod]
        public void ExtractToken_ValidChromeCookieExtraction_ReturnsEqualTokenValue()
        {
            Assert.AreEqual(Constant.ValidChromeToken, TokenExtractor.ExtractToken(Browser.Chrome));
        }

        [TestMethod]
        public void ExtractToken_InvalidChromeCookieExtraction_ReturnsNonEqualTokenValue()
        {
            Assert.AreNotEqual(Constant.InvalidChromeToken, TokenExtractor.ExtractToken(Browser.Chrome));
        }
    }
}
