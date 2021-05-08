using System;
using System.Globalization;
using HumanTimeParser.Core.Culture;
using HumanTimeParser.Core.TimeConstructs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HumanTimeParser.English.Tests
{
    [TestClass]
    public class CultureTests
    {
        private static readonly EnglishTimeParser EnglishTimeParser = new(new TimeParsingCulture(new CultureInfo("en-gb"), ClockType.TwentyFourHour));

        [TestMethod]
        public void Date_Test()
        {
            // d/m/y
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("1/2/24"));
            
            // m/d/y
            var expected = DateTime.Parse("2/1/24");
            
            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Date_With_Time_Test()
        {
            // d/m/y
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("1/2/24 14:56"));
            
            // m/d/y
            var expected = DateTime.Parse("2/1/24 14:56");
            
            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Date_With_Time__With_Relative_Test()
        {
            // d/m/y
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("1/2/24 14:56 5.5 m"));
            
            // m/d/y
            var expected = DateTime.Parse("2/1/24 14:56").AddMinutes(5.5);
            
            Assert.AreEqual(expected, result.Value);
        }
    }
}