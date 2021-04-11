using System;
using HumanTimeParser.Core.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HumanTimeParser.English.Tests
{
    public static class TestHelper
    {
        public static ISuccessfulTimeParsingResult<DateTime> AssertSuccessfulTimeParsingResult(ITimeParsingResult result)
        {
            Assert.IsInstanceOfType(result, typeof(ISuccessfulTimeParsingResult<DateTime>));
            return result as ISuccessfulTimeParsingResult<DateTime>;
        }
    }
}