using System;
using System.Globalization;
using HumanTimeParser.Core.Culture;
using HumanTimeParser.Core.TimeConstructs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HumanTimeParser.English.Tests
{
    [TestClass]
    public class TwentyFourHourTests
    {
        private static readonly EnglishTimeParser EnglishTimeParser = new(new TimeParsingCulture(CultureInfo.CurrentCulture, ClockType.TwentyFourHour));
        [TestMethod]
        public void Stress_Test_24_Hour()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("6 s 5 m 7 h 1 d 2 mth 3 y on 3/24/2021 at 14:56"));
        
            var timeOfDaySetup = DateTime.Parse("14:56").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);
        
            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Stress_Test_24_Hour_Alt_1()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("6 seconds 5 m 7 HOURS 1 d 2 mth 3 year on 3/24/2021 at 4:56"));
        
            var timeOfDaySetup = DateTime.Parse("4:56").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);
        
            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Twenty_Four_Hour_Time_Format_Under_12()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("4:56"));
        
            var expected = DateTime.Parse("4:56");
        
            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Twenty_Four_Hour_Time_Format_Over_12()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("14:56"));
        
            var expected = DateTime.Parse("14:56");
        
            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Ignore_Time_Period_Specifier()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("4:56pm"));
        
            var expected = DateTime.Parse("4:56");
        
            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Ignore_Time_Period_Specifier_When_Over_12()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("14:56pm"));
        
            var expected = DateTime.Parse("14:56");
        
            Assert.AreEqual(expected, result.Value);
        }

    }
}