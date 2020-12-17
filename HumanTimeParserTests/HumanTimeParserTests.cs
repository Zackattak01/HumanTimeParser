using Microsoft.VisualStudio.TestTools.UnitTesting;
using HumanTimeParser;
using System;

namespace HumanTimeParserTests
{
    [TestClass]
    public class HumanTimeParserTests
    {
        [TestMethod]
        public void StressTest()
        {
            var time = HumanReadableTimeParser.ParseTime("6 s 5 m 7 h 1 d 2 M 3 y on 3/24/2021 at 4:56am");

            var timeOfDaySetup = DateTime.Parse("4:56 am").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);

            Assert.AreEqual(expected, time);
        }

        [TestMethod]
        public void StressTest_Alt_1()
        {
            var time = HumanReadableTimeParser.ParseTime("6 seconds 5 m 7 HOURS 1 d 2 M 3 year on 3/24/2021 at 4:56am");

            var timeOfDaySetup = DateTime.Parse("4:56 am").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);

            Assert.AreEqual(expected, time);
        }

        [TestMethod]
        public void StressTest_Alt_2()
        {
            var time = HumanReadableTimeParser.ParseTime("6s 5m 7h 1d 2M 3y on 3/24/2021 at 4:56am");

            var timeOfDaySetup = DateTime.Parse("4:56 am").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);

            Assert.AreEqual(expected, time);
        }

        [TestMethod]
        public void RelativeTimeTest()
        {
            var time = HumanReadableTimeParser.ParseTime("30m 33s");

            var timeOfDaySetup = DateTime.Parse("4:56 am").TimeOfDay;
            var expected = DateTime.Now.AddSeconds(33).AddMinutes(30);

            //the following chunk of code checks to see if the two time values are "close enough"
            //execution time effects the final parsed time.  It is usually around a difference of 100 ticks or 3000 nano seconds
            bool closeEnough = false;

            // 0.1ms accuracy.  Seeing as the lib can only parse seconds as its lowest value it should be fine
            if (expected.Ticks - time.Ticks < 1000)
                closeEnough = true;


            Assert.IsTrue(closeEnough);
        }
    }
}
