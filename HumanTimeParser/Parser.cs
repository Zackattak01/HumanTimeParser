using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Name;

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
                        var parseStr = currentToken.Value;
                        string specifier = null;
                        if (currentToken.TokenType.HasFlag(TokenType.TwelveHourSpecifier))
                        {
                            specifier = parseStr.Substring(parseStr.Length - 2);
                            parseStr = parseStr.Substring(0, parseStr.Length - 2);
                        }

                        var ts = TimeSpan.Parse(parseStr);

                        //attempt to fill specifier
                        if (specifier is null)
                        {
                            var token = tokenizer.PeekNextToken();

                            if (token.TokenType.HasFlag(TokenType.TwelveHourSpecifier))
                            {
                                specifier = token.Value;
                                currentToken = tokenizer.NextToken();
                            }
                        }

                        //specifier may still be null. fail quietely
                        if (specifier?.ToLower() == "pm")
                            ts = ts.Add(new TimeSpan(12, 0, 0));
                        else if (ts < startingTime.TimeOfDay && specifier is null)
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