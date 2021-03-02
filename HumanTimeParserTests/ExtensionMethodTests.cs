using Microsoft.VisualStudio.TestTools.UnitTesting;
using HumanTimeParser;

namespace HumanTimeParserTests
{
    [TestClass]
    public class ExtensionMethodTests
    {

        [TestMethod]
        public void Contains_AM_PM_Specifier_Test()
        {
            var result = "5:30".EndsWithAmPmSpecifier();
            var result2 = "5:30pm".EndsWithAmPmSpecifier();
            var result3 = "5:30 am".EndsWithAmPmSpecifier();

            var expected = false;
            var expected2 = true;
            var expected3 = true;

            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected2, result2);
            Assert.AreEqual(expected3, result3);
        }
    }
}