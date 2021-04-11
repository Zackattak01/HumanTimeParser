using System;
using System.Linq;
using HumanTimeParser.Core.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HumanTimeParser.English.Tests
{
    [TestClass]
    public class LastTokenPositionTests
    {
        [TestMethod]
        public void LastTokenPositionTest()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("in 5 s do things cool stuff");
            Assert.AreEqual(2, result.LastParsedTokenIndex);
        }

        [TestMethod]
        public void LastTokenPositionTest_Alt()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("10s gamer time");

            Assert.AreEqual(0, result.LastParsedTokenIndex);
        }

        [TestMethod]
        public void LastTokenPositionTest_Alt2()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("5:30pm files");

            Assert.AreEqual(0, result.LastParsedTokenIndex);
        }

        [TestMethod]
        public void LastTokenPositionTest_Alt3()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("5:30pm .files");

            Assert.AreEqual(0, result.LastParsedTokenIndex);
        }

        [TestMethod]
        public void Spaced_AM_PM_Last_Token_Pos()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("5:30 PM");

            Assert.AreEqual(1, result.LastParsedTokenIndex);
        }
    }
}