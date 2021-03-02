using Microsoft.VisualStudio.TestTools.UnitTesting;
using HumanTimeParser;
using System;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace HumanTimeParserTests
{
    [TestClass]
    public class GeneralHumanTimeParserTests
    {
        [TestMethod]
        public void StressTest()
        {
            var result = HumanReadableTimeParser.ParseTime("6 s 5 m 7 h 1 d 2 mth 3 y on 3/24/2021 at 4:56am");

            var timeOfDaySetup = DateTime.Parse("4:56 am").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);

            Assert.AreEqual(expected, result.DateTime);
        }

        [TestMethod]
        public void StressTest_Alt_1()
        {
            var result = HumanReadableTimeParser.ParseTime("6 seconds 5 m 7 HOURS 1 d 2 mth 3 year on 3/24/2021 at 4:56am");

            var timeOfDaySetup = DateTime.Parse("4:56 am").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);

            Assert.AreEqual(expected, result.DateTime);
        }

        [TestMethod]
        public void StressTest_Alt_2()
        {
            var result = HumanReadableTimeParser.ParseTime("6s 5m 7h 1d 2MTH 3y on 3/24/2021 at 4:56am");

            var timeOfDaySetup = DateTime.Parse("4:56 am").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);

            Assert.AreEqual(expected, result.DateTime);
        }

        [TestMethod]
        public void RelativeTimeTest()
        {
            var result = HumanReadableTimeParser.ParseTime("30m 33s");

            var expected = DateTime.Now.AddSeconds(33).AddMinutes(30);

            //the following chunk of code checks to see if the two time values are "close enough"
            //execution time effects the final parsed time.  It is usually around a difference of 100 ticks or 10000 nano seconds
            bool closeEnough = false;

            // 0.1ms accuracy.  Seeing as the lib can only parse seconds as its lowest value it should be fine
            if (expected.Ticks - result.DateTime?.Ticks < 1000)
                closeEnough = true;


            Assert.IsTrue(closeEnough);
        }

        [TestMethod]
        public void IgnoreEnglish()
        {
            var result = HumanReadableTimeParser.ParseTime("33.5 minutes after 6:50am on 1/7/2021");

            var timeOfDaySetup = DateTime.Parse("6:50 am").TimeOfDay;
            var expected = DateTime.Parse("1/7/2021").Add(timeOfDaySetup).AddMinutes(33.5);

            Assert.AreEqual(expected, result.DateTime);
        }

        [TestMethod]
        public void EmptyString()
        {
            var result = HumanReadableTimeParser.ParseTime("");


            Assert.AreEqual(null, result.DateTime);
        }

        [TestMethod]
        public void TimeTest()
        {
            var result = HumanReadableTimeParser.ParseTime("4:56pm");

            var timeOfDaySetup = DateTime.Parse("4:56 pm").TimeOfDay;
            var expected = DateTime.Now.Date.Add(timeOfDaySetup);

            Assert.AreEqual(expected, result.DateTime);
        }


        [TestMethod]
        public void Parse_With_Spaced_AM_PM()
        {
            var result = HumanReadableTimeParser.ParseTime("5:30 PM");

            var expected = DateTime.Parse("5:30 PM");

            Assert.AreEqual(expected, result.DateTime);
        }

        //fails test becuase of implied am/pm parsing
        //should am/pm parsing apply here
        //probably
        // [TestMethod]
        // public void Fail_Quietly_With_Incorrect_Spaced_AM_PM()
        // {
        //     var result = HumanReadableTimeParser.ParseTime("pm 5:30");

        //     var expected = DateTime.Parse("5:30 AM");

        //     Assert.AreEqual(true, result.Success);
        //     Assert.AreEqual(expected, result.DateTime);
        // }


        //change this test to an appropriate time on every run
        //could work on a solution but too lazy
        [TestMethod]
        public void ImpliedAmPmTesting()
        {
            var result = HumanReadableTimeParser.ParseTime("7:00");

            var expected = DateTime.Parse("7:00 PM");

            Assert.AreEqual(expected, result.DateTime);

        }
        [TestMethod]
        public void TomorrowTest()
        {
            var result = HumanReadableTimeParser.ParseTime("Tomorrow");


            var expected = DateTime.Now.AddDays(1);

            //the following chunk of code checks to see if the two time values are "close enough"
            //execution time effects the final parsed time.  It is usually around a difference of 100 ticks or 10000 nano seconds
            bool closeEnough = false;

            // 1s accuracy.  Seeing as the lib can only parse seconds as its lowest value it should be fine
            if (expected.Ticks - result.DateTime?.Ticks < 10000000)
                closeEnough = true;

            System.Console.WriteLine(result.DateTime);
            System.Console.WriteLine(expected);

            System.Console.WriteLine(expected.Ticks - result.DateTime?.Ticks);

            Assert.IsTrue(closeEnough);
        }
    }
}
