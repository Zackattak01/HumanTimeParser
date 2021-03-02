using Microsoft.VisualStudio.TestTools.UnitTesting;
using HumanTimeParser;
using System;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace HumanTimeParserTests
{
    [TestClass]
    public class LastTokenPositionTests
    {
        [TestMethod]
        public void LastTokenPositionTest()
        {
            var result = HumanReadableTimeParser.ParseTime("in 5 s do things cool stuff");

            Assert.AreEqual(3, result.LastTokenPosition);
        }

        [TestMethod]
        public void LastTokenPositionTest_Alt()
        {
            var result = HumanReadableTimeParser.ParseTime("10s gamer time");

            var splitReason = "10s gamer time".Split(' ').Skip((int)result.LastTokenPosition);
            var reminderValue = string.Join(' ', splitReason);

            System.Console.WriteLine(reminderValue);

            Assert.AreEqual(1, result.LastTokenPosition);
        }

        [TestMethod]
        public void LastTokenPositionTest_Alt2()
        {
            var result = HumanReadableTimeParser.ParseTime("5:30pm files");

            var splitReason = "5:30pm files".Split(' ').Skip((int)result.LastTokenPosition);
            var reminderValue = string.Join(' ', splitReason);

            System.Console.WriteLine(reminderValue);

            Assert.AreEqual(1, result.LastTokenPosition);
        }

        [TestMethod]
        public void LastTokenPositionTest_Alt3()
        {
            var result = HumanReadableTimeParser.ParseTime("5:30pm .files");

            var splitReason = "5:30pm .files".Split(' ').Skip((int)result.LastTokenPosition);
            var reminderValue = string.Join(' ', splitReason);

            System.Console.WriteLine(reminderValue);

            Assert.AreEqual(1, result.LastTokenPosition);
        }

        [TestMethod]
        public void Spaced_AM_PM_Last_Token_Pos()
        {
            var result = HumanReadableTimeParser.ParseTime("5:30 PM");

            var expected = DateTime.Parse("5:30 PM");

            Assert.AreEqual(2, result.LastTokenPosition);
        }
    }
}