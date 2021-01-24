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

        public int LastTokenPosition { get; private set; }
        public int IndexOffsetFromTrue { get; private set; }

        public TimeToken TimeToken { get; private set; }

        private int index;



        public Tokenizer(string input)
        {
            ProvidedDate = null;
            ProvidedTimeOfDay = null;

            IndexOffsetFromTrue = 0;
            index = -1;

            UnparsedTokens = GetUnparsedTokens(input).ToArray();



        }

        private IEnumerable<string> GetUnparsedTokens(string input)
        {

            string[] baseTokens = input.Split(' ');
            List<string> tokens = new List<string>();

            foreach (var baseToken in baseTokens)
            {
                if (baseToken.IsAmPmSpecifier() && tokens.Count > 1)
                {
                    string lastToken = tokens.Last();

                    tokens.RemoveAt(tokens.Count - 1);

                    //combine the spaced AM/PM specifier to the supposed date in the previous token
                    //this is definetly a blind approach and will certainly lead to a bug or two in the future.
                    tokens.Add(lastToken + baseToken);
                    continue;
                }
                else if (baseToken.IsNumber() || DateTime.TryParse(baseToken, out var dateTime))
                {
                    tokens.Add(baseToken);
                    continue;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(baseToken) || string.IsNullOrEmpty(baseToken))
                        continue;


                    int pos = 0;

                    if (baseToken.ContainsNumber())
                    {
                        pos = baseToken.FirstNonNumberPos();
                        //theoretically impossible
                        if (pos == -1)
                            throw new Exception("baseToken is a number?");


                        if (pos != 0)
                        {
                            tokens.Add(baseToken.Substring(0, pos));
                            IndexOffsetFromTrue++;
                        }

                    }
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

            //System.Console.WriteLine(UnparsedTokens[index]);
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
                    LastTokenPosition = index;
                    CurrentValue = doubleResult;
                    TimeToken = TimeToken.Value;
                    return TimeToken;
                }

                if (DateTime.TryParse(unparsed, out var dtResult))
                {
                    if (DateTime.Now.Date == dtResult.Date)
                    {
                        LastTokenPosition = index;
                        ProvidedTimeOfDay = dtResult;
                        TimeToken = TimeToken.TimeOfDay;
                        return TimeToken;
                    }
                    else
                    {
                        LastTokenPosition = index;
                        ProvidedDate = dtResult;
                        TimeToken = TimeToken.Date;
                        return TimeToken;
                    }

                }

                if (unparsed == Constants.SecondAbbreviation || Constants.SecondAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    LastTokenPosition = index;
                    TimeToken = TimeToken.Second;
                    return TimeToken;
                }

                if (unparsed == Constants.MinuteAbbreviation || Constants.MinuteAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    LastTokenPosition = index;
                    TimeToken = TimeToken.Minute;
                    return TimeToken;
                }

                if (unparsed == Constants.HourAbbreviation || Constants.HourAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    LastTokenPosition = index;
                    TimeToken = TimeToken.Hour;
                    return TimeToken;
                }

                if (unparsed == Constants.DayAbbreviation || Constants.DayAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    LastTokenPosition = index;
                    TimeToken = TimeToken.Day;
                    return TimeToken;
                }

                if (unparsed == Constants.WeekAbbreviation || Constants.WeekAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    LastTokenPosition = index;
                    TimeToken = TimeToken.Week;
                    return TimeToken;
                }

                if (unparsed == Constants.MonthAbbreviation || Constants.MonthAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    LastTokenPosition = index;
                    TimeToken = TimeToken.Month;
                    return TimeToken;
                }

                if (unparsed == Constants.YearAbbreviation || Constants.YearAbbreviations.Any(x => x == unparsed.ToLower()))
                {
                    LastTokenPosition = index;
                    TimeToken = TimeToken.Year;
                    return TimeToken;
                }
            }

        }
    }
}