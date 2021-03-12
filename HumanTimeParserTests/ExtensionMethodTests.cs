using Microsoft.VisualStudio.TestTools.UnitTesting;
using HumanTimeParser;
using System;

namespace HumanTimeParserTests
{
    [TestClass]
    public class ExtensionMethodTests
    {

        [TestMethod]
        public void Contains_AM_PM_Specifier_Test()
        {
            var result = "5:30".AsSpan().EndsWithAmPmSpecifier();
            var result2 = "5:30pm".AsSpan().EndsWithAmPmSpecifier();
            var result3 = "5:30 am".AsSpan().EndsWithAmPmSpecifier();

            var expected = false;
            var expected2 = true;
            var expected3 = true;

            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected2, result2);
            Assert.AreEqual(expected3, result3);
        }

        [TestMethod]
        public void First_Non_Number_Pos_Test()
        {
            var result = "5:f30".AsSpan().FirstNonNumberPos();
            var result2 = "a5:30pm".AsSpan().FirstNonNumberPos();
            var result3 = "5:30 a".AsSpan().FirstNonNumberPos();

            var expected = 2;
            var expected2 = 0;
            var expected3 = 4;

            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected2, result2);
            Assert.AreEqual(expected3, result3);
        }

        [TestMethod]
        public void Contains_Number_Test()
        {
            var result = "gdfs2dasa".AsSpan().ContainsNumber();
            var result2 = "".AsSpan().ContainsNumber();
            var result3 = "wiodsfk".AsSpan().ContainsNumber();

            var expected = true;
            var expected2 = false;
            var expected3 = false;

            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected2, result2);
            Assert.AreEqual(expected3, result3);
        }
    }
}