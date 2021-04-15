using System;
using HumanTimeParser.Core.Sectioning;
using HumanTimeParser.Core.TimeConstructs;
using HumanTimeParser.Core.Tokenization;
using HumanTimeParser.Core.Tokenization.Tokens;
using HumanTimeParser.English.Extensions;
using Microsoft.VisualBasic;

namespace HumanTimeParser.English
{
    /// <summary>
    /// The tokenizer used for parsing english.
    /// </summary>
    public sealed class EnglishTimeTokenizer : TokenizerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnglishTimeTokenizer"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EnglishTimeTokenizer(ISectionizer sectionizer) : base(sectionizer) { }
        protected override IToken TokenizeSection(Section section)
        {
            if (section is null)
                return new EOFToken();

            var span = section.Value.AsSpan();

            if (double.TryParse(span, out var number))
                return new NumberToken(section.Position, number);

            if (TryTokenizeTimeAndTwelveHourSpecifier(section, out var timeToken))
                return timeToken;

            if (DateTime.TryParse(span, out var dateTime))
                return new DateToken(section.Position, dateTime);

            if (TryTokenizeNumberAndRelativeTimeFormat(section, out var relativeToken))
                return relativeToken;

            if (TryTokenizeDayOfWeek(section, out var dayOfWeekToken))
                return dayOfWeekToken;

            if (TryTokenizePeriodSpecifier(section, out var periodSpecifierToken))
                return periodSpecifierToken;


            return null; //no token was found. return null to let TokenizerBase recurse
        }

        private static bool TryTokenizeTimeAndTwelveHourSpecifier(Section section, out IToken result)
        {
            var span = section.Value.AsSpan();
            if (span.TryParseEndingTimePeriodSpecifier(out var timePeriod) && TimeSpan.TryParse(span[..^2], out var parsedQualifiedTimeSpan))
            {
                result = new QualifiedTimeOfDayToken(section.Position, new QualifiedTimeOfDay(timePeriod, parsedQualifiedTimeSpan));
                return true;
            }
            else if(TimeSpan.TryParse(span, out var parsedTimeSpanSpan))
            {
                result = new TimeOfDayToken(section.Position, new TimeOfDay(parsedTimeSpanSpan));
                return true;
            }

            result = null;
            return false;
        }

        private static bool TryTokenizeNumberAndRelativeTimeFormat(Section section, out IToken result)
        {
            var splitPos = section.Value.FirstNonNumberPos();

            if (splitPos == -1)
            {
                result = null;
                return false;
            }

            var unparsedAbbreviation = section.Value[splitPos..]; // resharper suggestion here

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
                    var number = double.Parse(parseSpan);

                    result = new QualifiedRelativeTimeToken(section.Position, new RelativeTime(number, relativeTimeFormat));
                    return true;
                }
                    
            }

            result = null;
            return false;
        }

        private static bool TryTokenizeDayOfWeek(Section section, out DayOfWeekToken result)
        {
            if (EnglishTimeKeywordConstants.DayOfWeekKeywordDictionary.TryGetValue(section.Value, out var dayOfWeek))
            {
                result = new DayOfWeekToken(section.Position, dayOfWeek);
                return true;
            }

            result = null;
            return false;
        }
        
        private static bool TryTokenizePeriodSpecifier(Section section, out PeriodSpecifierToken result)
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