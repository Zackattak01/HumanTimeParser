using System;
using System.Collections.Generic;
using HumanTimeParser.Core.TimeConstructs;
using HumanTimeParser.Core.Tokenization;
using HumanTimeParser.Core.Tokenization.Tokens;

namespace HumanTimeParser.Core.Parsing.Default
{

    public class DefaultParser : ParserBase
    {
        public ClockType ClockType { get; }
        
        protected static readonly TimeSpan TwelveHourTimeSpan = TimeSpan.FromHours(12);
        
        protected HashSet<RelativeTimeFormat> ParsedRelativeTimeFormats { get; }
        protected List<Func<DateTime, DateTime>> RelativeTimeFunctions { get; }

        protected DateTime StartingDate { get; set; }
        protected DateTime? ParsedDate { get; set; }
        
        protected TimeSpan? ParsedTime { get; set; }
        
        protected DayOfWeek? ParsedDayOfWeek { get; set; }

        protected int FirstParsedTokenPosition { get; set; }
        protected int LastParsedTokenPosition { get; set; }


        public DefaultParser(ITokenizer tokenizer) : base(tokenizer)
        {
            ParsedRelativeTimeFormats = new HashSet<RelativeTimeFormat>();
            RelativeTimeFunctions = new List<Func<DateTime, DateTime>>();

            FirstParsedTokenPosition = -1;
            LastParsedTokenPosition = -1;

            ClockType = ClockType.TwelveHour;
        }
        
        public DefaultParser(ClockType clockType, ITokenizer tokenizer) : base(tokenizer)
        {
            ParsedRelativeTimeFormats = new HashSet<RelativeTimeFormat>();
            RelativeTimeFunctions = new List<Func<DateTime, DateTime>>();

            FirstParsedTokenPosition = -1;
            LastParsedTokenPosition = -1;

            ClockType = clockType;
        }

        public override ITimeParsingResult Parse()
        {
            StartingDate = DateTime.Now;
            IToken token;

            do
            {
                var parsedCurrentToken = false;
                token = Tokenizer.NextToken();
                switch (token)
                {
                    case EOFToken:
                        break;
                    case NumberToken numberToken:
                        parsedCurrentToken = ParseNumberToken(numberToken);
                        break;
                    case QualifiedRelativeTimeToken qualifiedRelativeTimeToken:
                        parsedCurrentToken = ParseFullyQualifiedRelativeTimeToken(qualifiedRelativeTimeToken);
                        break;
                    case TimeOfDayToken timeOfDayToken:
                        parsedCurrentToken = ParseTimeOfDayToken(timeOfDayToken);
                        break;
                    case QualifiedTimeOfDayToken qualifiedTimeOfDayToken:
                        parsedCurrentToken = ParseQualifiedTimeOfDayToken(qualifiedTimeOfDayToken);
                        break;
                    case DateToken dateToken:
                        parsedCurrentToken = ParseDateToken(dateToken);
                        break;
                    case DayOfWeekToken dayOfWeekToken:
                        parsedCurrentToken = ParseDayOfWeekToken(dayOfWeekToken);
                        break;
                    default:
                        HandleUnexpectedToken(token);
                        break;
                }

                if (parsedCurrentToken)
                {
                    LastParsedTokenPosition = token.Position;

                    if (FirstParsedTokenPosition == -1)
                        FirstParsedTokenPosition = token.Position;
                }

            } while (token is not EOFToken);

            if (LastParsedTokenPosition == -1)
                return NoParseableTokensFound();

            return new DefaultSuccessfulTimeParsingResult(ConstructDateTime(), FirstParsedTokenPosition, LastParsedTokenPosition);
        }

        protected virtual bool ParseNumberToken(NumberToken token)
        {
            var nextToken = Tokenizer.PeekNextToken();

            if (nextToken is RelativeTimeFormatToken relativeTimeFormatToken && !ParsedRelativeTimeFormats.Contains(relativeTimeFormatToken.Value))
            {
                RelativeTimeFunctions.Add(ParseRelativeTime(token.Value, relativeTimeFormatToken.Value));
                ParsedRelativeTimeFormats.Add(relativeTimeFormatToken.Value);

                //make sure to advance token
                Tokenizer.SkipToken();
                return true;
            }

            return false;
        }

        protected virtual bool ParseFullyQualifiedRelativeTimeToken(QualifiedRelativeTimeToken token)
        {
            if (ParsedRelativeTimeFormats.Contains(token.Value.Format))
                return false;

            RelativeTimeFunctions.Add(ParseRelativeTime(token.Value.Amount, token.Value.Format));
            ParsedRelativeTimeFormats.Add(token.Value.Format);
            
            return true;
        }

        protected virtual bool ParseTimeOfDayToken(TimeOfDayToken timeOfDayToken)
        {
            if (!timeOfDayToken.Value.IsValid(ClockType) || ParsedTime is not null)
                return false;
            
            var nextToken = Tokenizer.PeekNextToken();

            if (nextToken is PeriodSpecifierToken periodSpecifierToken && ClockType != ClockType.TwentyFourHour)
            {
                var time = timeOfDayToken.Value.Time;
                if (periodSpecifierToken.Value == TimePeriod.Pm)
                    time = time.Add(TwelveHourTimeSpan);

                
                ParsedTime = time;
                Tokenizer.SkipToken();  // make sure to actually advance the token
                return true;
                
            }
            else 
            {
                if (timeOfDayToken.Value.Time < StartingDate.TimeOfDay) // implied am/pm parsing
                {
                    var impliedTime = timeOfDayToken.Value.Time.Add(TwelveHourTimeSpan);
                    if (impliedTime >= StartingDate.TimeOfDay)
                    {
                        ParsedTime = impliedTime;
                        return true;
                    }
                    else
                    {
                        ParsedTime = timeOfDayToken.Value.Time; // implied parsing was not successful... fallback to original value
                        return true;
                    }
                }
                else // no need for implied parsing
                {
                    ParsedTime = timeOfDayToken.Value.Time;
                    return true;
                }
                    
            }
        }
        
        protected virtual bool ParseQualifiedTimeOfDayToken(QualifiedTimeOfDayToken token)
        {
            if (ParsedTime is not null)
                return false;
            
            ParsedTime = token.Value.Time; 
            if (token.Value.Period == TimePeriod.Pm) 
                ParsedTime = ParsedTime.Value.Add(TwelveHourTimeSpan);

            
            return true;
        }

        protected virtual bool ParseDateToken(DateToken token)
        {
            if (ParsedDate is not null)
                return false;

            ParsedDate = token.Value;

            return true;
        }

        protected virtual bool ParseDayOfWeekToken(DayOfWeekToken token)
        {
            if (ParsedDayOfWeek is not null)
                return false;

            ParsedDayOfWeek = token.Value;

            DateTime CalculateDay(DateTime dt)
            {
                var difference = token.Value - dt.DayOfWeek;

                //assume "next monday" is implied
                if (difference <= 0)
                    difference += 7;

                return dt.AddDays(difference);
            }
            
            RelativeTimeFunctions.Add(CalculateDay);
            
            return true;
        }

        protected virtual void HandleUnexpectedToken(IToken token)
        {
            // do nothing by default
        }

        protected virtual IFailedTimeParsingResult NoParseableTokensFound()
            => new DefaultFailedTimeParsingResult("No parseable time formats were found.");

        protected virtual Func<DateTime, DateTime> ParseRelativeTime(double number, RelativeTimeFormat format)
        {
            const int daysPerWeek = 7;
            return format switch
            {
                RelativeTimeFormat.Tomorrow => time => time.AddDays(1),
                RelativeTimeFormat.Seconds => time => time.AddSeconds(number),
                RelativeTimeFormat.Minutes => time => time.AddMinutes(number),
                RelativeTimeFormat.Hours => time => time.AddHours(number),
                RelativeTimeFormat.Days => time => time.AddDays(number),
                RelativeTimeFormat.Weeks => time => time.AddDays(number * daysPerWeek),
                RelativeTimeFormat.Months => time => time.AddMonths((int) number), //LOSSY CONVERSION
                RelativeTimeFormat.Years => time => time.AddYears((int) number), //LOSSY CONVERSION
                _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
            };
        }

        protected virtual DateTime ConstructDateTime()
        {
            var date = ParsedDate ?? StartingDate;

            if (ParsedTime is not null)
                date = date.Date.Add(ParsedTime.Value);

            foreach (var func in RelativeTimeFunctions)
            {
                date = func(date);
            }

            return date;
        }
    }
}