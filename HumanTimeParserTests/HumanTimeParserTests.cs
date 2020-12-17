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
    }
}
