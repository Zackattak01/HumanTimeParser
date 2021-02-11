using System;
using System.Collections.Generic;
using Name;

namespace HumanTimeParser
{
    internal class Parser
    {
        //we can use a dictionary here because the user can only supply one of each type of token
        //if they supply multiple of each token it would not read like english, thus not Human Readable
        Dictionary<TokenType, string> tokenValues;

        string input;

        Tokenizer tokenizer;

        public Parser(string input)
        {
            tokenValues = new Dictionary<TokenType, string>();
            this.input = input;
            tokenizer = new Tokenizer(input);
        }

        public DateTime? Parse()
        {
            ReadTokens();

            DateTime startingTime = DateTime.Now;
            DateTime newTime = startingTime;

            if (tokenValues.TryGetValue(TokenType.Date, out var date))
            {
                newTime = DateTime.Parse(date);
            }

            if (tokenValues.TryGetValue(TokenType.TimeOfDay, out var timeOfDay))
            {
                newTime = newTime.Date.Add(DateTime.Parse(timeOfDay).TimeOfDay);

                //try to parse implied am/pm
                // if (newTime < DateTime.Now && !timeOfDay.ContainsAmPmSpecifier())
                // {
                //     var impliedTime = newTime.AddHours(12);
                //     if (impliedTime > DateTime.Now)
                //         newTime = impliedTime;
                // }

            }

            if (tokenValues.TryGetValue(TokenType.Second, out var seconds))
            {
                newTime = newTime.AddSeconds(double.Parse(seconds));
            }

            if (tokenValues.TryGetValue(TokenType.Minute, out var minutes))
            {
                newTime = newTime.AddMinutes(double.Parse(minutes));
            }

            if (tokenValues.TryGetValue(TokenType.Hour, out var hours))
            {
                newTime = newTime.AddHours(double.Parse(hours));
            }

            if (tokenValues.TryGetValue(TokenType.Day, out var days))
            {
                newTime = newTime.AddDays(double.Parse(days));
            }

            if (tokenValues.TryGetValue(TokenType.Week, out var weeks))
            {
                newTime = newTime.AddDays(double.Parse(weeks) * 7);
            }

            if (tokenValues.TryGetValue(TokenType.Month, out var months))
            {
                newTime = newTime.AddMonths(int.Parse(months));
            }

            if (tokenValues.TryGetValue(TokenType.Year, out var years))
            {
                newTime = newTime.AddYears(int.Parse(years));
            }

            if (startingTime.Equals(newTime))
                return null;



            return newTime;
        }

        public int GetLastTokenPosition()
            => (tokenizer.LastTokenPosition - tokenizer.IndexOffsetFromTrue) + 1;



        private void ReadTokens()
        {
            //System.Console.WriteLine(tokenizer.NextToken());
            while (tokenizer.TimeToken != TokenType.END)
            {
                tokenizer.NextToken();

                while (tokenizer.TimeToken == TokenType.Value)
                {
                    tokenizer.NextToken();
                }

                bool success;
                if (tokenizer.TimeToken == TokenType.TimeOfDay)
                {
                    success = tokenValues.TryAdd(tokenizer.TimeToken, tokenizer.ProvidedTimeOfDay.ToString());
                }
                else if (tokenizer.TimeToken == TokenType.Date)
                {
                    success = tokenValues.TryAdd(tokenizer.TimeToken, tokenizer.ProvidedDate.ToString());
                }
                else
                {
                    success = tokenValues.TryAdd(tokenizer.TimeToken, tokenizer.CurrentValue.ToString());
                }


                if (!success)
                {
                    throw new ArgumentException("Time was not human readable!");
                }
            }
        }
    }
}