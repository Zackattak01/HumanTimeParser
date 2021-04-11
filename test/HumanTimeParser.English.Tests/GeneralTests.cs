using System;
using HumanTimeParser.Core.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HumanTimeParser.English.Tests
{
    [TestClass]
    public class GeneralTests
    {
        private const int FivePmHourCount = 17;
        
        [TestMethod]
        public void StressTest()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("6 s 5 m 7 h 1 d 2 mth 3 y on 3/24/2021 at 4:56am");

            var timeOfDaySetup = DateTime.Parse("4:56 am").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);

            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void StressTest_Alt_1()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("6 seconds 5 m 7 HOURS 1 d 2 mth 3 year on 3/24/2021 at 4:56am");

            var timeOfDaySetup = DateTime.Parse("4:56 am").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);

            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void StressTest_Alt_2()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("6s 5m 7h 1d 2MTH 3y on 3/24/2021 at 4:56am");

            var timeOfDaySetup = DateTime.Parse("4:56 am").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);

            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void RelativeTimeTest()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("30m 33s");

            var expected = DateTime.Now.AddSeconds(33).AddMinutes(30);

            //the following chunk of code checks to see if the two time values are "close enough"
            //execution time effects the final parsed time.  It is usually around a difference of 100 ticks or 10000 nano seconds
            // 1s accuracy.  Seeing as the lib can only parse seconds as its lowest value it should be fine
            var closeEnough = expected.Ticks - result.Value.Ticks < 10000000;


            Assert.IsTrue(closeEnough);
        }

        [TestMethod]
        public void IgnoreEnglish()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("33.5 minutes after 6:50am on 1/7/2021");

            var timeOfDaySetup = DateTime.Parse("6:50 am").TimeOfDay;
            var expected = DateTime.Parse("1/7/2021").Add(timeOfDaySetup).AddMinutes(33.5);

            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void EmptyString()
        {
            var result = EnglishTimeParser.Parse("");


            Assert.IsInstanceOfType(result, typeof(IFailedTimeParsingResult));
        }

        [TestMethod]
        public void TimeTest()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("4:56pm");

            var timeOfDaySetup = DateTime.Parse("4:56 pm").TimeOfDay;
            var expected = DateTime.Now.Date.Add(timeOfDaySetup);

            Assert.AreEqual(expected, result.Value);
        }


        [TestMethod]
        public void Parse_With_Spaced_AM_PM()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("5:30 PM");

            var expected = DateTime.Parse("5:30 PM");

            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void TomorrowTest()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("Tomorrow");


            var expected = DateTime.Now.AddDays(1);

            //the following chunk of code checks to see if the two time values are "close enough"
            //execution time effects the final parsed time.  It is usually around a difference of 100 ticks or 10000 nano seconds
            // 1s accuracy.  Seeing as the lib can only parse seconds as its lowest value it should be fine
            var closeEnough = expected.Ticks - result.Value.Ticks < 10000000;


            Console.WriteLine(result.Value);
            Console.WriteLine(expected);

            Console.WriteLine(expected.Ticks - result.Value.Ticks);

            Assert.IsTrue(closeEnough);
        }
        
        [TestMethod]
        public void Weekday_Test()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("monday at 5:00");

            var today = DateTime.Now;
            Console.WriteLine(today);
            var dayOfWeek = today.DayOfWeek;
            var daysToAdd = DayOfWeek.Monday - dayOfWeek;
            if (daysToAdd < 0)
                daysToAdd += 7;
            var expected = DateTime.Now.AddDays(daysToAdd).Date.Add(today.TimeOfDay.TotalHours > 5 ? new TimeSpan(FivePmHourCount,0,0) : new TimeSpan(5, 0, 0));

            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Weekday_With_Time_Period_Test()
        {
            var result = (ISuccessfulTimeParsingResult<DateTime>)EnglishTimeParser.Parse("monday at 5:00 pm");

            var today = DateTime.Now;
            var dayOfWeek = today.DayOfWeek;
            var daysToAdd = DayOfWeek.Monday - dayOfWeek;
            if (daysToAdd < 0)
                daysToAdd += 7;
            var expected = DateTime.Now.AddDays(daysToAdd).Date.Add(new TimeSpan(FivePmHourCount,0,0));

            Assert.AreEqual(expected, result.Value);
        }
    }
}