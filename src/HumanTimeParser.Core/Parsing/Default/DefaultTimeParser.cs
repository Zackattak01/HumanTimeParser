using System;
using HumanTimeParser.Core.Culture;
using HumanTimeParser.Core.Extensions;
using HumanTimeParser.Core.Parsing.State;
using HumanTimeParser.Core.Sectioning;
using HumanTimeParser.Core.TimeConstructs;
using HumanTimeParser.Core.Tokenization;
using HumanTimeParser.Core.Tokenization.Tokens;

namespace HumanTimeParser.Core.Parsing.Default
{
    /// <summary>
    /// A default time parser
    /// </summary>
    public class DefaultTimeParser : TimeParserBase
    {
        /// <summary>
        /// Timespan useful for adding twelve hours to other timespans
        /// </summary>
        protected static readonly TimeSpan TwelveHourTimeSpan = TimeSpan.FromHours(12);
        
        /// <summary>
        /// The culture this parser uses
        /// </summary>
        public ITimeParsingCulture Culture { get; }
        
        /// <summary>
        /// The parser's state
        /// </summary>
        protected DefaultTimeParserState State { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTimeParser"/> class.
        /// </summary>
        /// <param name="culture">The culture this parser will use.</param>
        /// <inheritdoc/>
        public DefaultTimeParser(ITimeParsingCulture culture, ITokenizer tokenizer) : base(tokenizer)
        {
            Culture = culture;
        }

        /// <inheritdoc/>
        public override ITimeParsingResult Parse(string input)
        {
            Tokenizer.Sectionizer = new DefaultSectionizer(input);
            State = new DefaultTimeParserState();
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
                    //Prefer 'CurrentToken' instead of the 'token' var because some of the parse funcs advance the token
                    State.LastParsedTokenPosition = Tokenizer.CurrentToken is not EOFToken ? Tokenizer.CurrentToken.Position : token.Position; 

                    if (State.FirstParsedTokenPosition == -1)
                        State.FirstParsedTokenPosition = token.Position;
                }

            } while (token is not EOFToken);

            if (State.LastParsedTokenPosition == -1)
                return NoParseableTokensFound();

            return new DefaultSuccessfulTimeParsingResult(ConstructDateTime(), State.FirstParsedTokenPosition, State.LastParsedTokenPosition);
        }

        /// <summary>
        /// Responsible for parsing a <see cref="NumberToken"/>
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>Whether the parse was successful</returns>
        protected virtual bool ParseNumberToken(NumberToken token)
        {
            var nextToken = Tokenizer.PeekNextToken();

            switch (nextToken)
            {
                case RelativeTimeFormatToken relativeTimeFormatToken when !State.ParsedRelativeTimeFormats.Contains(relativeTimeFormatToken.Value):
                    State.RelativeTimeFunctions.Add(ParseRelativeTime(token.Value, relativeTimeFormatToken.Value));
                    State.ParsedRelativeTimeFormats.Add(relativeTimeFormatToken.Value);

                    //make sure to advance token
                    Tokenizer.SkipToken();
                    return true;
                case PeriodSpecifierToken specifierToken when (State.ParsedDate is null && Culture.ClockType != ClockType.TwentyFourHour): 
                {
                    // This functionality is not supported for 24 clocks, since the TimePeriod marker would not exist
                    // At that point the parser would be relying on the fact that a number would always be a time which obviously isn't the case
                    var hours = (int) token.Value;

                    if (specifierToken.Value == TimePeriod.Pm)
                        hours += 12;

                    var ts = new TimeSpan(hours, 0, 0);

                    // by this point the time may have been converted to a 24 hour clock type
                    if (!ts.IsValidTimeOfDay(ClockType.TwentyFourHour))
                        return false;
                    
                    State.ParsedTime = ts;
                    //make sure to advance token
                    Tokenizer.SkipToken();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Responsible for parsing a <see cref="QualifiedRelativeTimeToken"/>
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>Whether the parse was successful</returns>
        protected virtual bool ParseFullyQualifiedRelativeTimeToken(QualifiedRelativeTimeToken token)
        {
            if (State.ParsedRelativeTimeFormats.Contains(token.Value.Format))
                return false;

            State.RelativeTimeFunctions.Add(ParseRelativeTime(token.Value.Amount, token.Value.Format));
            State.ParsedRelativeTimeFormats.Add(token.Value.Format);
            
            return true;
        }

        /// <summary>
        /// Responsible for parsing a <see cref="TimeOfDayToken"/>
        /// </summary>
        /// <param name="timeOfDayToken">The token</param>
        /// <returns>Whether the parse was successful</returns>
        protected virtual bool ParseTimeOfDayToken(TimeOfDayToken timeOfDayToken)
        {
            if (!timeOfDayToken.Value.IsValid(Culture.ClockType) || State.ParsedTime is not null)
                return false;
            
            // no additional parsing is required for 24 hour clocks
            if(Culture.ClockType == ClockType.TwentyFourHour)
            {
                State.ParsedTime = timeOfDayToken.Value.Time;
                return true;
            }
            
            var nextToken = Tokenizer.PeekNextToken();
            
            if (nextToken is PeriodSpecifierToken periodSpecifierToken)
            {
                var time = timeOfDayToken.Value.Time;
                if (periodSpecifierToken.Value == TimePeriod.Pm)
                    time = time.Add(TwelveHourTimeSpan);

                
                State.ParsedTime = time;
                Tokenizer.SkipToken();  // make sure to actually advance the token
                return true;
                
            }
            else 
            {
                if (timeOfDayToken.Value.Time < State.StartingDate.TimeOfDay) // implied am/pm parsing
                {
                    var impliedTime = timeOfDayToken.Value.Time.Add(TwelveHourTimeSpan);
                    if (impliedTime >= State.StartingDate.TimeOfDay)
                    {
                        State.ParsedTime = impliedTime;
                        return true;
                    }
                    else
                    {
                        State.ParsedTime = timeOfDayToken.Value.Time; // implied parsing was not successful... fallback to original value
                        return true;
                    }
                }
                else // no need for implied parsing
                {
                    State.ParsedTime = timeOfDayToken.Value.Time;
                    return true;
                }
                    
            }
        }
        
        /// <summary>
        /// Responsible for parsing a <see cref="QualifiedTimeOfDayToken"/>
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>Whether the parse was successful</returns>
        protected virtual bool ParseQualifiedTimeOfDayToken(QualifiedTimeOfDayToken token)
        {
            if (State.ParsedTime is not null || !token.Value.IsValid())
                return false;
            
            State.ParsedTime = token.Value.Time; 
            if (token.Value.Period == TimePeriod.Pm) 
                State.ParsedTime = State.ParsedTime.Value.Add(TwelveHourTimeSpan);

            
            return true;
        }

        /// <summary>
        /// Responsible for parsing a <see cref="DateToken"/>
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>Whether the parse was successful</returns>
        protected virtual bool ParseDateToken(DateToken token)
        {
            if (State.ParsedDate is not null)
                return false;

            State.ParsedDate = token.Value;

            return true;
        }

        /// <summary>
        /// Responsible for parsing a <see cref="DayOfWeekToken"/>
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>Whether the parse was successful</returns>
        protected virtual bool ParseDayOfWeekToken(DayOfWeekToken token)
        {
            if (State.ParsedDayOfWeek is not null)
                return false;

            State.ParsedDayOfWeek = token.Value;

            DateTime CalculateDay(DateTime dt)
            {
                var difference = token.Value - dt.DayOfWeek;

                //assume "next monday" is implied
                if (difference <= 0)
                    difference += 7;

                return dt.AddDays(difference);
            }
            
            State.RelativeTimeFunctions.Add(CalculateDay);
            
            return true;
        }

        /// <summary>
        /// Responsible for handling an unknown or unexpected token; is a nop by default
        /// </summary>
        /// <param name="token">The unknown token</param>
        protected virtual void HandleUnexpectedToken(IToken token)
        {
            // do nothing by default
        }

        /// <summary>
        /// Function for creating the failed result when no tokens could be parsed
        /// </summary>
        /// <returns>A failed result indicating no tokens could be parsed</returns>
        protected virtual IFailedTimeParsingResult NoParseableTokensFound()
            => new DefaultFailedTimeParsingResult("No parseable time formats were found.");

        /// <summary>
        /// Responsible for converting a relative time format and value into a delegate
        /// </summary>
        /// <param name="number">The amount of units</param>
        /// <param name="format">The time unit</param>
        /// <returns>A delegate that will be used to construct the final result</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

        /// <summary>
        /// Constructs the final <see cref="DateTime"/>
        /// </summary>
        /// <returns>The final <see cref="DateTime"/></returns>
        protected virtual DateTime ConstructDateTime()
        {
            var date = State.ParsedDate ?? State.StartingDate;

            if (State.ParsedTime is not null)
                date = date.Date.Add(State.ParsedTime.Value);

            foreach (var func in State.RelativeTimeFunctions)
            {
                date = func(date);
            }

            return date;
        }
    }
}