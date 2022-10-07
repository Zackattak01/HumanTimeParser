using System;
using HumanTimeParser.Core.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HumanTimeParser.English.Tests
{
    [TestClass]
    public class GeneralTests
    {
        
        private const int FivePmHourCount = 17;

        private static readonly EnglishTimeParser EnglishTimeParser = new();

        [TestMethod]
        public void Stress_Test()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("6 s 5 m 7 h 1 d 2 mth 3 y on 3/24/2021 at 4:56am"));

            var timeOfDaySetup = DateTime.Parse("4:56 am").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);

            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void Stress_Test_Alt_1()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("6 seconds 5 m 7 HOURS 1 d 2 mth 3 year on 3/24/2021 at 4:56am"));

            var timeOfDaySetup = DateTime.Parse("4:56 am").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);

            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void Stress_Test_Alt_2()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("6s 5m 7h 1d 2MTH 3y on 3/24/2021 at 4:56am"));

            var timeOfDaySetup = DateTime.Parse("4:56 am").TimeOfDay;
            var expected = DateTime.Parse("3/24/2021").Add(timeOfDaySetup).AddSeconds(6).AddMinutes(5).AddHours(7).AddDays(1).AddMonths(2).AddYears(3);

            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void Relative_Time()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("30m 33s"));

            var expected = DateTime.Now.AddSeconds(33).AddMinutes(30);


            TestHelper.AssertCloseEnough(expected, result.Value);
        }

        [TestMethod]
        public void Relative_Time_No_Whitespace()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("30m33s"));

            var expected = DateTime.Now.AddSeconds(33).AddMinutes(30);


            TestHelper.AssertCloseEnough(expected, result.Value);
        }

        [TestMethod]
        public void Ignore_English()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("33.5 minutes after 6:50am on 1/7/2021"));

            var timeOfDaySetup = DateTime.Parse("6:50 am").TimeOfDay;
            var expected = DateTime.Parse("1/7/2021").Add(timeOfDaySetup).AddMinutes(33.5);

            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void Empty_String()
        {
            var result = EnglishTimeParser.Parse("");


            Assert.IsInstanceOfType(result, typeof(IFailedTimeParsingResult));
        }

        [TestMethod]
        public void Time()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("4:56pm"));

            var timeOfDaySetup = DateTime.Parse("4:56 pm").TimeOfDay;
            var expected = DateTime.Now.Date.Add(timeOfDaySetup);

            Assert.AreEqual(expected, result.Value);
        }


        [TestMethod]
        public void Parse_With_Spaced_AM_PM()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("5:30 PM"));

            var expected = DateTime.Parse("5:30 PM");

            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Tomorrow()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("Tomorrow"));


            var expected = DateTime.Now.AddDays(1);

            TestHelper.AssertCloseEnough(expected, result.Value);
        }
        
        [TestMethod]
        public void Weekday()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("monday at 5:00"));

            var today = DateTime.Now;
            Console.WriteLine(today);
            var dayOfWeek = today.DayOfWeek;
            var daysToAdd = DayOfWeek.Monday - dayOfWeek;
            if (daysToAdd <= 0)
                daysToAdd += 7;

            TimeSpan spanToAdd;
            if (today.TimeOfDay.TotalHours > 5)
            {
                var pmSpan = new TimeSpan(FivePmHourCount, 0, 0);
                spanToAdd = pmSpan > today.TimeOfDay ? pmSpan : new TimeSpan(5, 0, 0);
            }
            else
            {
                spanToAdd = new TimeSpan(5, 0, 0);
            }
            
            var expected = DateTime.Now.AddDays(daysToAdd).Date.Add(spanToAdd);

            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Weekday_With_Time_Period_Test()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("monday at 5:00 pm"));

            var today = DateTime.Now;
            var dayOfWeek = today.DayOfWeek;
            var daysToAdd = DayOfWeek.Monday - dayOfWeek;
            if (daysToAdd <= 0)
                daysToAdd += 7;
            var expected = DateTime.Now.AddDays(daysToAdd).Date.Add(new TimeSpan(FivePmHourCount,0,0));

            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Twelve_Oclock()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("12:34"));
            
            
            var expected = DateTime.Parse("12:34");
            if (expected < DateTime.Now)
                expected = expected.Add(new TimeSpan(12, 0, 0));

            Assert.AreEqual(expected, result.Value);
        }
        
        
        [TestMethod]
        public void Number_As_Time_Period_Specifier()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("6pm"));

            var expected = DateTime.Parse("6pm");

            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Number_As_Time_Period_Specifier_Spaced()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("6 pm"));

            var expected = DateTime.Parse("6pm");

            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Number_As_Time_Overflow()
        {
            Assert.IsInstanceOfType(EnglishTimeParser.Parse("600pm"), typeof(IFailedTimeParsingResult));        
        }
        
        [TestMethod]
        public void Number_As_Time_Overflow_Spaced()
        {
            Assert.IsInstanceOfType(EnglishTimeParser.Parse("600 pm"), typeof(IFailedTimeParsingResult));        
        }

        [TestMethod]
        public void Tomorrow_Exclusivity()
        {
            TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("Tmr 6 pm"));
        }
        
        [TestMethod]
        public void Tomorrow_Exclusivity_2()
        {
            TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("Tmr 6 pm"));
        }

        [TestMethod]
        public void Tomorrow_12pm()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("tmr 12:30pm"));

            var expected = DateTime.Today.AddDays(1).AddHours(12.5);

            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void Long_Form_Date()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("May testing 22 stuff 2022"));

            var expected = new DateTime(2022, 5, 22);
            
            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Long_Form_Date_Ordinal()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("May testing 22nd stuff 2022"));

            var expected = new DateTime(2022, 5, 22);
            
            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Long_Form_Date_No_Year()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("May testing 22 stuff"));

            var expected = new DateTime(DateTime.Today.Year, 5, 22);
            
            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Long_Form_Date_No_Year_Ordinal()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("May testing 22nd stuff"));

            var expected = new DateTime(DateTime.Today.Year, 5, 22);
            
            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Implied_Month_And_Year_Ordinal_Day()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("22nd do stuff"));

            var expected = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 22);
            
            Assert.AreEqual(expected, result.Value);
        }
        
        [TestMethod]
        public void Ignore_Day()
            => TestHelper.AssertFailedTimeParsingResult(EnglishTimeParser.Parse("22 do stuff"));

        [TestMethod]
        public void Ignore_Month()
            => TestHelper.AssertFailedTimeParsingResult(EnglishTimeParser.Parse("Do things in may"));
        
        [TestMethod]
        public void Ignore_Year()
            => TestHelper.AssertFailedTimeParsingResult(EnglishTimeParser.Parse("Do things in 2026"));
        
        [TestMethod]
        public void Ignore_Month_And_Year()
            => TestHelper.AssertFailedTimeParsingResult(EnglishTimeParser.Parse("Do things in may 2023"));
        
        [TestMethod]
        public void Prefer_Short_Form_Date()
        {
            var result = TestHelper.AssertSuccessfulTimeParsingResult(EnglishTimeParser.Parse("may 22nd 1/20/25 "));

            var expected = new DateTime(2025, 1, 20);
            
            Assert.AreEqual(expected, result.Value);
        }
    }
}
