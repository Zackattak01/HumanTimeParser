using System;
using HumanTimeParser.Core.Culture;
using HumanTimeParser.Core.Sectioning;
using HumanTimeParser.Core.TimeConstructs;
using HumanTimeParser.Core.Tokenization;
using HumanTimeParser.Core.Tokenization.Tokens;
using HumanTimeParser.English.Extensions;

namespace HumanTimeParser.English
{
    /// <summary>
    /// The tokenizer used for parsing english.
    /// </summary>
    public sealed class EnglishTimeTokenizer : TokenizerBase
    {
        private const int MinNumberOfDaysInMonth = 1;
        private const int MaxNumberOfDaysInMonth = 31;

        private const int MinYear = 1;
        private const int MaxYear = 9999;
        
        private readonly ITimeParsingCulture _timeParsingCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnglishTimeTokenizer"/> class.
        /// </summary>
        public EnglishTimeTokenizer(ITimeParsingCulture timeParsingCulture)
        {
            _timeParsingCulture = timeParsingCulture;
        }

        /// <inheritdoc/>
        protected override IToken TokenizeSection(Section section)
        {
            if (section is null)
                return EOFToken;

            var span = section.Value.AsSpan();

            if (TokenizerUtils.TryParseNumber(span, _timeParsingCulture, out var number))
            {
                var numberInt = (int)number;
                // check if number is a whole number
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (number == numberInt)
                {
                    switch (numberInt)
                    {
                        case >= MinNumberOfDaysInMonth and <= MaxNumberOfDaysInMonth:
                            return new DayOfMonthToken(section.Position, section.Length, numberInt);
                        case >= MinYear and <= MaxYear:
                            return new YearToken(section.Position, section.Length, numberInt);
                    }
                }
                return new NumberToken(section.Position, section.Length, number);
            }

            if (TryTokenizeTimeAndTwelveHourSpecifier(section, out var timeToken))
                return timeToken;

            if (TokenizerUtils.TryParseDate(span, _timeParsingCulture, out var dateTime))
                return new DateToken(section.Position, section.Length, dateTime);

            if (TryTokenizeNumberAndRelativeTimeFormat(section, out var relativeToken))
                return relativeToken;
            
            if (TryTokenizeDayOfWeek(section, out var dayOfWeekToken))
                return dayOfWeekToken;
            
            if (TryTokenizeQualifiedDayOfMonth(section, out var dayOfMonthToken))
                return dayOfMonthToken;

            if (TryTokenizeMonth(section, out var monthToken))
                return monthToken;

            if (TryTokenizePeriodSpecifier(section, out var periodSpecifierToken))
                return periodSpecifierToken;


            return new UnknownToken(section.Position, section.Length, section.Value); //no token was found. return an unknown token to let TokenizerBase recurse
        }

        private bool TryTokenizeTimeAndTwelveHourSpecifier(Section section, out IToken result)
        {
            var span = section.Value.AsSpan();
            if (span.TryParseEndingTimePeriodSpecifier(out var timePeriod))
            {
                var truncatedSpan = span[..^2];
                
                if (TokenizerUtils.TryParseNumber(truncatedSpan, _timeParsingCulture, out var parsedNumber))
                {
                    result = new QualifiedTimeOfDayToken(section.Position, section.Length,
                        new QualifiedTimeOfDay(timePeriod, new TimeSpan((int) parsedNumber, 0, 0)));
                    return true;
                }
                else if (TokenizerUtils.TryParseTimeSpan(truncatedSpan, _timeParsingCulture, out var parsedQualifiedTimeSpan))
                {
                    result = _timeParsingCulture.ClockType switch
                    {
                        ClockType.TwelveHour => new QualifiedTimeOfDayToken(section.Position, section.Length,
                            new QualifiedTimeOfDay(timePeriod, parsedQualifiedTimeSpan)),
                        ClockType.TwentyFourHour => new TimeOfDayToken(section.Position, section.Length,
                            new TimeOfDay(parsedQualifiedTimeSpan)),
                        _ => null
                    };
                    return true;
                }


            }
            else if(TokenizerUtils.TryParseTimeSpan(span, _timeParsingCulture, out var parsedTimeSpanSpan))
            {
                result = new TimeOfDayToken(section.Position, section.Length, new TimeOfDay(parsedTimeSpanSpan));
                return true;
            }

            result = null;
            return false;
        }

        private bool TryTokenizeNumberAndRelativeTimeFormat(Section section, out IToken result)
        {
            result = null;
            var splitPos = section.Value.FirstNonNumberPos();

            if (splitPos == -1)
                return false;

            var unparsedAbbreviation = section.Value[splitPos..];

            if (EnglishTimeKeywordConstants.RelativeTimeKeywordDictionary.TryGetValue(unparsedAbbreviation, out var relativeTimeFormat))
            {
                if (relativeTimeFormat == RelativeTimeFormat.Tomorrow) // special case with the "tomorrow" keyword
                {
                    result = new QualifiedRelativeTimeToken(section.Position, section.Length, new RelativeTime(1, relativeTimeFormat));
                    return true;
                }
                else if (splitPos == 0) 
                {
                    result = new RelativeTimeFormatToken(section.Position, section.Length, relativeTimeFormat);
                    return true;
                }
                else
                {
                    var numberSpan = section.Value.AsSpan()[..splitPos];
                    TokenizerUtils.TryParseNumber(numberSpan, _timeParsingCulture, out var number);
                    
                    result = new QualifiedRelativeTimeToken(section.Position, section.Length, new RelativeTime(number, relativeTimeFormat));
                    return true;
                }
                    
            }
            
            return false;
        }

        private bool TryTokenizeQualifiedDayOfMonth(Section section, out QualifiedDayOfMonthToken result)
        {
            result = null;
            var splitPos = section.Value.FirstNonNumberPos();

            if (splitPos == -1)
                return false;

            var unparsedText = section.Value[splitPos..];

            if (EnglishTimeKeywordConstants.OrdinalNumberIndicators.Contains(unparsedText))
            {
                var numberSpan = section.Value.AsSpan()[..splitPos];
                TokenizerUtils.TryParseNumber(numberSpan, _timeParsingCulture, out var number);

                result = new QualifiedDayOfMonthToken(section.Position, section.Length, (int)number);
                return true;
            }

            return false;
        }

        private bool TryTokenizeMonth(Section section, out MonthToken result)
        {
            if (EnglishTimeKeywordConstants.MonthKeywordDictionary.TryGetValue(section.Value, out var month))
            {
                result = new MonthToken(section.Position, section.Length, month);
                return true;
            }

            result = null;
            return false;
        }

        private bool TryTokenizeDayOfWeek(Section section, out DayOfWeekToken result)
        {
            if (EnglishTimeKeywordConstants.DayOfWeekKeywordDictionary.TryGetValue(section.Value, out var dayOfWeek))
            {
                result = new DayOfWeekToken(section.Position, section.Length, dayOfWeek);
                return true;
            }

            result = null;
            return false;
        }

        private bool TryTokenizePeriodSpecifier(Section section, out PeriodSpecifierToken result)
        {
            if (section.Value.TryParseTimePeriodSpecifier(out var timePeriod))
            {
                result = new PeriodSpecifierToken(section.Position, section.Length, timePeriod);
                return true;
            }
            else
            {
                result = null;
                return false;
            }

        }
        
        
    }
}