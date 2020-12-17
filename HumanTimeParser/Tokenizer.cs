using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using Name;

namespace HumanTimeParser
{
    internal class Tokenizer
    {
        private readonly string[] UnparsedTokens;

        public DateTime? ProvidedDate { get; private set; }

        public DateTime? ProvidedTimeOfDay { get; private set; }

        public double CurrentValue { get; private set; }

        public TimeToken TimeToken { get; private set; }

        private int index;

        public Tokenizer(string input)
        {
            ProvidedDate = null;
            ProvidedTimeOfDay = null;


            UnparsedTokens = GetUnparsedTokens(input).ToArray();

            index = -1;
        }

        private IEnumerable<string> GetUnparsedTokens(string input)
        {

            string[] baseTokens = input.Split(' ');
            List<string> tokens = new List<string>();

            foreach (var baseToken in baseTokens)
            {
                if (baseToken.IsNumber() || DateTime.TryParse(baseToken, out var dateTime))
                {
                    tokens.Add(baseToken);
                    continue;
                }
                else
                {
                    int pos = baseToken.FirstNonNumberPos();

                    //theoretically impossible
                    if (pos == -1)
                        throw new Exception("baseToken is a number?");

                    tokens.Add(baseToken.Substring(0, pos));
                    tokens.Add(baseToken.Substring(pos));



                }
            }

            return tokens;
        }

        private string NextUnparsedToken()
        {
            index++;

            if (index >= UnparsedTokens.Length)
                return null;

            return UnparsedTokens[index];
        }

        public TimeToken NextToken()
        {

            while (true)
            {
                string unparsed = NextUnparsedToken();

                if (unparsed is null)
                {
                    TimeToken = TimeToken.END;
                    return TimeToken;
                }


                if (double.TryParse(unparsed, out var doubleResult))
                {
                    CurrentValue = doubleResult;
                    TimeToken = TimeToken.Value;
                    return TimeToken;
                }

                if (DateTime.TryParse(unparsed, out var dtResult))
                {
                    if (DateTime.Now.Date == dtResult.Date)
                    {
                        ProvidedTimeOfDay = dtResult;
                        TimeToken = TimeToken.TimeOfDay;
                        return TimeToken;
                    }
                    else
                    {
                        ProvidedDate = dtResult;
                        TimeToken = TimeToken.Date;
                        return TimeToken;
                    }

                }

                if (unparsed == Constants.SecondAbbreviation || Constants.SecondAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    TimeToken = TimeToken.Second;
                    return TimeToken;
                }

                if (unparsed == Constants.MinuteAbbreviation || Constants.MinuteAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    TimeToken = TimeToken.Minute;
                    return TimeToken;
                }

                if (unparsed == Constants.HourAbbreviation || Constants.HourAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    TimeToken = TimeToken.Hour;
                    return TimeToken;
                }

                if (unparsed == Constants.DayAbbreviation || Constants.DayAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    TimeToken = TimeToken.Day;
                    return TimeToken;
                }

                if (unparsed == Constants.WeekAbbreviation || Constants.WeekAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    TimeToken = TimeToken.Week;
                    return TimeToken;
                }

                if (unparsed == Constants.MonthAbbreviation || Constants.MonthAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    TimeToken = TimeToken.Month;
                    return TimeToken;
                }

                if (unparsed == Constants.YearAbbreviation || Constants.YearAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    TimeToken = TimeToken.Year;
                    return TimeToken;
                }
            }

        }
    }
}