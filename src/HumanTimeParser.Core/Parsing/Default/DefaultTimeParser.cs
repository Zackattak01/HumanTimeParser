using System;
using System.Linq;
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
        /// This parser's state
        /// </summary>
        protected DefaultTimeParserState State { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTimeParser"/> class.
        /// </summary>
        /// <param name="culture">The culture this parser will use.</param>
        /// <param name="tokenizer">The <see cref="ITokenizer"/> instance this parser will use</param>
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
                    case UnknownToken:
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
                    case QualifiedDayOfMonthToken qualifiedDayOfMonthToken:
                        parsedCurrentToken = ParseQualifiedDayOfMonthToken(qualifiedDayOfMonthToken);
                        break;
                    case MonthToken monthToken:
                        parsedCurrentToken = ParseMonthToken(monthToken);
                        break;
                    default:
                        parsedCurrentToken = ParseUnexpectedToken(token);
                        break;
                }

                if (parsedCurrentToken)
                {
                    //Prefer 'CurrentToken' instead of the 'token' var because some of the parse funcs advance the token
                    State.LastParsedTokenPosition = Tokenizer.CurrentToken is not EOFToken ? Tokenizer.CurrentToken.Position : token.Position; 

                    if (State.FirstParsedTokenPosition == -1)
                        State.FirstParsedTokenPosition = token.Position - token.Length; // want the first char of the token 
                }

            } while (token is not EOFToken);

            if (State.LastParsedTokenPosition == -1 && !State.ParsedDateState.CanConstructValidDate())
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
                {
                    State.RelativeTimeFunctions.Add(ParseRelativeTime(token.Value, relativeTimeFormatToken.Value));
                    State.ParsedRelativeTimeFormats.Add(relativeTimeFormatToken.Value);

                    //make sure to advance token
                    Tokenizer.SkipToken();
                    return true;
                }
                case PeriodSpecifierToken specifierToken when State.ParsedTime is null && Culture.ClockType != ClockType.TwentyFourHour: 
                {
                    // This functionality is not supported for 24 clocks, since the TimePeriod marker would not exist
                    // At that point the parser would be relying on the fact that a number would always be a time which obviously isn't the case
                    var hours = (int) token.Value;

                    if (specifierToken.Value == TimePeriod.Am && hours == 12)
                        hours -= 12;
                    else if (specifierToken.Value == TimePeriod.Pm && hours != 12)
                        hours += 12;

                    var ts = new TimeSpan(hours, 0, 0);

                    // use TwentyFourHour check because adding twelve hours pushes the ts over a valid twelve hour clock time
                    if (!ts.IsValidTimeOfDay(ClockType.TwentyFourHour))
                        return false;
                    
                    State.ParsedTime = ts;
                    //make sure to advance token
                    Tokenizer.SkipToken();
                    return true;
                }
            }

            // If there is already a parsed date no need to collect day, month, year tokens
            if (State.ParsedDateState.Date is not null)
                return false;
            
            switch (token)
            {
                case DayOfMonthToken dayOfMonthToken when State.ParsedDateState.Day is null: 
                    State.ParsedDateState.Day = (int)dayOfMonthToken.Value;
                    State.ParsedDateState.LastParsedTokenPosition = token.Position;
                    break;
                case YearToken yearToken when State.ParsedDateState.Year is null:
                    State.ParsedDateState.Year = (int)yearToken.Value;
                    State.ParsedDateState.LastParsedTokenPosition = token.Position;
                    break;
            }

            // return false despite possibly adding date info to state because the info is not guaranteed to be used
            // therefore the token position should not be advanced.
            return false;
        }

        /// <summary>
        /// Responsible for parsing a <see cref="QualifiedRelativeTimeToken"/>
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>Whether the parse was successful</returns>
        protected virtual bool ParseFullyQualifiedRelativeTimeToken(QualifiedRelativeTimeToken token)
        {
            var parsedFormatsCount = State.ParsedRelativeTimeFormats.Count;
            foreach (var relativeTime in token.Value)
            {
                if (State.ParsedRelativeTimeFormats.Contains(relativeTime.Format))
                    continue;

                State.RelativeTimeFunctions.Add(ParseRelativeTime(relativeTime.Amount, relativeTime.Format));
                State.ParsedRelativeTimeFormats.Add(relativeTime.Format);
            }
            
            return State.ParsedRelativeTimeFormats.Count != parsedFormatsCount;
        }

        /// <summary>
        /// Responsible for parsing a <see cref="TimeOfDayToken"/>
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>Whether the parse was successful</returns>
        protected virtual bool ParseTimeOfDayToken(TimeOfDayToken token)
        {
            if (!token.Value.IsValid(Culture.ClockType) || State.ParsedTime is not null)
                return false;
            
            // no additional parsing is required for 24 hour clocks
            if(Culture.ClockType == ClockType.TwentyFourHour)
            {
                State.ParsedTime = token.Value.Time;
                return true;
            }
            
            var nextToken = Tokenizer.PeekNextToken();
            
            if (nextToken is PeriodSpecifierToken periodSpecifierToken)
            {
                var time = token.Value.Time;
                
                if (periodSpecifierToken.Value == TimePeriod.Am && time.Hours == 12)
                    time = time.Subtract(TwelveHourTimeSpan);
                else if (periodSpecifierToken.Value == TimePeriod.Pm && time.Hours != 12)
                    time = time.Add(TwelveHourTimeSpan);

                
                State.ParsedTime = time;
                Tokenizer.SkipToken();  // make sure to actually advance the token
                return true;
                
            }
            else 
            {
                if (token.Value.Time < State.StartingDate.TimeOfDay) // implied am/pm parsing
                {
                    var impliedTime = token.Value.Time.Add(TwelveHourTimeSpan);
                    if (impliedTime >= State.StartingDate.TimeOfDay)
                    {
                        State.ParsedTime = impliedTime;
                        return true;
                    }
                    else
                    {
                        State.ParsedTime = token.Value.Time; // implied parsing was not successful... fallback to original value
                        return true;
                    }
                }
                else // no need for implied parsing
                {
                    State.ParsedTime = token.Value.Time;
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

            if (token.Value.Period == TimePeriod.Am && token.Value.Time.Hours == 12)
                State.ParsedTime = State.ParsedTime.Value.Subtract(TwelveHourTimeSpan);
            else if (token.Value.Period == TimePeriod.Pm && token.Value.Time.Hours != 12) 
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
            if (State.ParsedDateState.Date is not null)
                return false;

            State.ParsedDateState.Date = token.Value;

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
        /// Responsible for parsing a <see cref="QualifiedDayOfMonthToken"/>
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>Whether the parse was successful</returns>
        protected virtual bool ParseQualifiedDayOfMonthToken(QualifiedDayOfMonthToken token)
        {
            if (State.ParsedDateState.ParsedQualifiedDay)
                return false;

            State.ParsedDateState.Day = token.Value;
            State.ParsedDateState.ParsedQualifiedDay = true;
            State.ParsedDateState.LastParsedTokenPosition = token.Position;
            
            // return false despite possibly adding date info to state because the info is not guaranteed to be used
            // therefore the token position should not be advanced.
            return false;
        }

        /// <summary>
        /// Responsible for parsing a <see cref="MonthToken"/>
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>Whether the parse was successful</returns>
        private bool ParseMonthToken(MonthToken token)
        {
            if (State.ParsedDateState.Month is null)
            {
                State.ParsedDateState.Month = token.Value;
                State.ParsedDateState.LastParsedTokenPosition = token.Position;
            }
            
            // return false despite possibly adding date info to state because the info is not guaranteed to be used
            // therefore the token position should not be advanced.
            return false;
        }

        /// <summary>
        /// Responsible for handling an unknown or unexpected token; is a nop by default
        /// </summary>
        /// <param name="token">The unknown token</param>
        /// <returns>Whether the parse was successful</returns>
        /// <remarks>
        /// This method is meant to be overriden to support custom tokens.
        /// <see cref="UnknownToken"/>'s will not be passed to this method.
        /// Create a custom implantation of <see cref="ITokenizer"/> to properly tokenize them instead. 
        /// </remarks>
        protected virtual bool ParseUnexpectedToken(IToken token)
        {
            // do nothing by default
            return false;
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
            var dateState = State.ParsedDateState;
            
            DateTime date;
            // always prefer a date in the form mm/dd/yy 
            if (dateState.Date is not null)
                date = dateState.Date.Value;
            else
            {
                if (dateState.Day is not null)
                {
                    if (dateState.Month is not null) 
                    {
                        date = dateState.Year is null
                            ? new DateTime(State.StartingDate.Year, dateState.Month.Value, dateState.Day.Value)
                            : new DateTime(dateState.Year.Value, dateState.Month.Value, dateState.Day.Value);
                        
                        if (State.LastParsedTokenPosition < dateState.LastParsedTokenPosition)
                            State.LastParsedTokenPosition = dateState.LastParsedTokenPosition;
                    }
                    else if (dateState.ParsedQualifiedDay)
                    {
                        date = new DateTime(State.StartingDate.Year, State.StartingDate.Month, dateState.Day.Value);

                        if (State.LastParsedTokenPosition < dateState.LastParsedTokenPosition)
                            State.LastParsedTokenPosition = dateState.LastParsedTokenPosition;
                    }
                    else
                        date = State.StartingDate;
                }
                else
                    date = State.StartingDate;
            }

            if (State.ParsedTime is not null)
                date = date.Date.Add(State.ParsedTime.Value);

            foreach (var func in State.RelativeTimeFunctions)
                date = func(date);
            
            return date;
        }
    }
}