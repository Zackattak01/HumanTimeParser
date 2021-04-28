using System;
using System.Globalization;
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

        private readonly ITimeParsingCulture _timeParsingCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnglishTimeTokenizer"/> class.
        /// </summary>
        public EnglishTimeTokenizer(ITimeParsingCulture timeParsingCulture)
        {
            _timeParsingCulture = timeParsingCulture;
        }
        
        protected override IToken TokenizeSection(Section section)
        {
            if (section is null)
                return new EOFToken();

            var span = section.Value.AsSpan();
            
            if (TokenizerUtils.TryParseNumber(span, _timeParsingCulture, out var number))
                return new NumberToken(section.Position, number);

            if (TryTokenizeTimeAndTwelveHourSpecifier(section, out var timeToken))
                return timeToken;

            if (TokenizerUtils.TryParseDate(span, _timeParsingCulture, out var dateTime))
                return new DateToken(section.Position, dateTime);

            if (TryTokenizeNumberAndRelativeTimeFormat(section, out var relativeToken))
                return relativeToken;

            if (TryTokenizeDayOfWeek(section, out var dayOfWeekToken))
                return dayOfWeekToken;

            if (TryTokenizePeriodSpecifier(section, out var periodSpecifierToken))
                return periodSpecifierToken;


            return new UnknownToken(section.Position, section.Value); //no token was found. return an unknown token to let TokenizerBase recurse
        }

        private bool TryTokenizeTimeAndTwelveHourSpecifier(Section section, out IToken result)
        {
            var span = section.Value.AsSpan();
            if (span.TryParseEndingTimePeriodSpecifier(out var timePeriod) && TokenizerUtils.TryParseTimeSpan(span[..^2], _timeParsingCulture, out var parsedQualifiedTimeSpan))
            {
                result = _timeParsingCulture.ClockType switch
                {
                    ClockType.TwelveHour => new QualifiedTimeOfDayToken(section.Position,
                        new QualifiedTimeOfDay(timePeriod, parsedQualifiedTimeSpan)),
                    ClockType.TwentyFourHour => new TimeOfDayToken(section.Position,
                        new TimeOfDay(parsedQualifiedTimeSpan)),
                    _ => null
                };

                return true;
            }
            else if(TokenizerUtils.TryParseTimeSpan(span, _timeParsingCulture, out var parsedTimeSpanSpan))
            {
                result = new TimeOfDayToken(section.Position, new TimeOfDay(parsedTimeSpanSpan));
                return true;
            }

            result = null;
            return false;
        }

        private bool TryTokenizeNumberAndRelativeTimeFormat(Section section, out IToken result)
        {
            var splitPos = section.Value.FirstNonNumberPos();

            if (splitPos == -1)
            {
                result = null;
                return false;
            }

            var unparsedAbbreviation = section.Value[splitPos..];

            if (EnglishTimeKeywordConstants.RelativeTimeKeywordDictionary.TryGetValue(unparsedAbbreviation, out var relativeTimeFormat))
            {
                if (relativeTimeFormat == RelativeTimeFormat.Tomorrow)// special case with the "tomorrow" keyword
                {
                    result = new QualifiedRelativeTimeToken(section.Position, new RelativeTime(1, relativeTimeFormat));
                    return true;
                }
                else if (splitPos == 0) 
                {
                    result = new RelativeTimeFormatToken(section.Position, relativeTimeFormat);
                    return true;
                }
                else
                {
                    var parseSpan = section.Value.AsSpan()[..splitPos]; // resharper suggestion here
                    TokenizerUtils.TryParseNumber(parseSpan, _timeParsingCulture, out var number);
                    
                    result = new QualifiedRelativeTimeToken(section.Position, new RelativeTime(number, relativeTimeFormat));
                    return true;
                }
                    
            }

            result = null;
            return false;
        }

        private bool TryTokenizeDayOfWeek(Section section, out DayOfWeekToken result)
        {
            if (EnglishTimeKeywordConstants.DayOfWeekKeywordDictionary.TryGetValue(section.Value, out var dayOfWeek))
            {
                result = new DayOfWeekToken(section.Position, dayOfWeek);
                return true;
            }

            result = null;
            return false;
        }
        
        private bool TryTokenizePeriodSpecifier(Section section, out PeriodSpecifierToken result)
        {
            if (section.Value.TryParseTimePeriodSpecifier(out var timePeriod))
            {
                result = new PeriodSpecifierToken(section.Position, timePeriod);
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