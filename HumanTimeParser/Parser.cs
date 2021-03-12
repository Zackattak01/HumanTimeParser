using System;
using System.Collections.Generic;
using System.Linq;

namespace HumanTimeParser
{
    internal class Parser
    {
        public int LastTokenPosition { get; private set; }
        private Tokenizer tokenizer;
        public Parser(string input, string separator = null)
        {
            tokenizer = new Tokenizer(input, separator);
        }

        public DateTime? Parse()
        {
            var startingTime = DateTime.Now;
            var parsedDate = startingTime.Date;
            var parsedTime = startingTime.TimeOfDay;
            var lastParsedTokenPos = -1;

            var parsedTypes = new HashSet<TokenType>();
            var relativeTimeFunctions = new List<Func<DateTime, DateTime>>();

            Token currentToken;
            do
            {
                currentToken = tokenizer.NextToken();

                //potential greedy parse
                if (currentToken.TokenType.HasFlag(TokenType.Number))
                {

                    var num = double.Parse(currentToken.Value);

                    if (currentToken.TokenType == TokenType.Number)
                        currentToken = tokenizer.NextToken();

                    var function = ParseRelativeTime(currentToken.TokenType, num);

                    //removes TokenType.Number from the enum
                    var parsedType = currentToken.TokenType & ~TokenType.Number;

                    if (function is { } && !parsedTypes.Contains(parsedType))
                    {
                        relativeTimeFunctions.Add(function);
                        parsedTypes.Add(parsedType);
                        lastParsedTokenPos = currentToken.TokenPosition;
                    }

                }
                else if (currentToken.TokenType.HasFlag(TokenType.Tomorrow))
                {
                    if (!parsedTypes.Contains(TokenType.Tomorrow) && !parsedTypes.Contains(TokenType.Date) && !parsedTypes.Contains(TokenType.Date))
                    {
                        var function = ParseRelativeTime(currentToken.TokenType, 1);
                        relativeTimeFunctions.Add(function);

                        parsedTypes.Add(TokenType.Tomorrow);
                        parsedTypes.Add(TokenType.Date);
                        parsedTypes.Add(TokenType.Day);
                        lastParsedTokenPos = currentToken.TokenPosition;
                    }

                }
                else if (currentToken.TokenType.HasFlag(TokenType.Date))
                {
                    if (!parsedTypes.Contains(TokenType.Date))
                    {
                        parsedDate = DateTime.Parse(currentToken.Value);

                        parsedTypes.Add(TokenType.Date);
                        lastParsedTokenPos = currentToken.TokenPosition;
                    }
                }
                //potential greedy parse
                else if (currentToken.TokenType.HasFlag(TokenType.TimeOfDay))
                {
                    if (!parsedTypes.Contains(TokenType.TimeOfDay))
                    {
                        var parseSpan = currentToken.Value.AsSpan();
                        ReadOnlySpan<char> specifier = ReadOnlySpan<char>.Empty;
                        if (currentToken.TokenType.HasFlag(TokenType.TwelveHourSpecifier))
                        {
                            specifier = parseSpan.Slice(parseSpan.Length - 2);
                            parseSpan = parseSpan.Slice(0, parseSpan.Length - 2);
                        }

                        var ts = TimeSpan.Parse(parseSpan);

                        //attempt to fill specifier
                        if (specifier == ReadOnlySpan<char>.Empty)
                        {
                            var token = tokenizer.PeekNextToken();

                            if (token.TokenType.HasFlag(TokenType.TwelveHourSpecifier))
                            {
                                specifier = token.Value;
                                currentToken = tokenizer.NextToken();
                            }
                        }

                        //specifier may still be null. fail quietely
                        if (specifier.Equals("pm", StringComparison.OrdinalIgnoreCase))
                            ts = ts.Add(new TimeSpan(12, 0, 0));
                        else if (ts < startingTime.TimeOfDay && specifier == ReadOnlySpan<char>.Empty)
                        {
                            var newTs = ts.Add(new TimeSpan(12, 0, 0));

                            if (newTs >= startingTime.TimeOfDay)
                                ts = newTs;
                        }

                        parsedTime = ts;

                        lastParsedTokenPos = currentToken.TokenPosition;
                        parsedTypes.Add(TokenType.TimeOfDay);
                    }
                }
                else if (currentToken.TokenType.HasFlag(TokenType.DayOfWeek))
                {
                    if (!parsedTypes.Contains(TokenType.DayOfWeek) && !parsedTypes.Contains(TokenType.Date) && !parsedTypes.Contains(TokenType.Day))
                    {
                        var func = ParseDayOfWeek(currentToken.Value);
                        relativeTimeFunctions.Add(func);

                        lastParsedTokenPos = currentToken.TokenPosition;
                        parsedTypes.Add(TokenType.DayOfWeek);
                        parsedTypes.Add(TokenType.Date);
                        parsedTypes.Add(TokenType.Day);
                    }
                }
            } while (currentToken.TokenType != TokenType.END);

            LastTokenPosition = lastParsedTokenPos + 1;

            //parsedDate = parsedDate.Add(parsedTime);

            //found no parseable tokens
            if (parsedTypes.Count == 0)
                return null;
            else
                return ConstructDateTime(parsedDate, parsedTime, relativeTimeFunctions);
        }

        //returns a function that will carry out the correct task
        //it returns a function because all relative times need to be added AFTER the date has been confirmed
        private Func<DateTime, DateTime> ParseRelativeTime(TokenType type, double number)
        {
            if (type.HasFlag(TokenType.Second))
                return (x) => x.AddSeconds(number);
            else if (type.HasFlag(TokenType.Minute))
                return (x) => x.AddMinutes(number);
            else if (type.HasFlag(TokenType.Hour))
                return (x) => x.AddHours(number);
            else if (type.HasFlag(TokenType.Day))
                return (x) => x.AddDays(number);
            else if (type.HasFlag(TokenType.Week))
                return (x) => x.AddDays(number * 7);
            else if (type.HasFlag(TokenType.Month))
                return (x) => x.AddMonths((int)number);
            else if (type.HasFlag(TokenType.Year))
                return (x) => x.AddYears((int)number);
            else
                return null;
        }

        private Func<DateTime, DateTime> ParseDayOfWeek(string unparsedDay)
        {
            //var lowerCase = unparsedDay.ToLower();
            DayOfWeek today = DateTime.Now.DayOfWeek;
            foreach (var abbreviation in Constants.WeekdayAbbreviations)
            {
                if (abbreviation.Value.Contains(unparsedDay))
                {
                    DayOfWeek specifiedDay = abbreviation.Key;

                    int difference = specifiedDay - today;

                    //assume "next monday" is implied
                    if (difference <= 0)
                        difference += 7;

                    return (x) => x.AddDays(difference);
                }
            }

            throw new ArgumentException("Did not match any known day", nameof(unparsedDay));
        }

        private DateTime ConstructDateTime(DateTime startingDate,
                                           TimeSpan timeOfDay,
                                           IEnumerable<Func<DateTime, DateTime>> relativeTimeFuncs)
        {
            //apply starting time
            var result = startingDate.Add(timeOfDay);

            foreach (var func in relativeTimeFuncs)
            {
                result = func(result);
            }

            return result;
        }
    }
}