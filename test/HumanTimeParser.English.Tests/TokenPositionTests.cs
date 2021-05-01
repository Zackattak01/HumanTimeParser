using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HumanTimeParser.English.Tests
{
    [TestClass]
    public class LastTokenPositionTests
    {
        private static readonly EnglishTimeParser EnglishTimeParser = new();

        
        [TestMethod]
        public void LastTokenPositionTest()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("in 5 s do things cool stuff"));
            Assert.AreEqual(3, result.FirstParsedTokenIndex);
            Assert.AreEqual(6, result.LastParsedTokenIndex);
        }

        [TestMethod]
        public void LastTokenPositionTest_Alt()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("10s gamer time"));

            Assert.AreEqual(0, result.FirstParsedTokenIndex);
            Assert.AreEqual(3, result.LastParsedTokenIndex);
        }

        [TestMethod]
        public void LastTokenPositionTest_Alt2()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("5:30pm files"));

            Assert.AreEqual(0, result.FirstParsedTokenIndex);
            Assert.AreEqual(6, result.LastParsedTokenIndex);
        }

        [TestMethod]
        public void LastTokenPositionTest_Alt3()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("5:30pm .files"));

            Assert.AreEqual(0, result.FirstParsedTokenIndex);
            Assert.AreEqual(6, result.LastParsedTokenIndex);
        }

        [TestMethod]
        public void Spaced_AM_PM_Last_Token_Pos()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("5:30 PM"));

            Assert.AreEqual(0, result.FirstParsedTokenIndex);
            Assert.AreEqual(7, result.LastParsedTokenIndex);
        }
        
        [TestMethod]
        public void Number_At_End()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("saturday at 1:30 finish 2.0"));
        
            Assert.AreEqual(16, result.LastParsedTokenIndex);
        }
        
        [TestMethod]
        public void Number_As_Peek_Token()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("saturday at 1:30 2.0"));
        
            Assert.AreEqual(16, result.LastParsedTokenIndex);
        }
    }
}