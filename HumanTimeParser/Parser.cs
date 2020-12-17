using System;
using System.Collections.Generic;

namespace HumanTimeParser
{
    internal class Parser
    {
        //we can use a dictionary here because the user can only supply one of each type of token
        //if they supply multiple of each token it would not read like english, thus not Human Readable
        Dictionary<TimeToken, string> tokenValues;

        string input;

        Tokenizer tokenizer;

        public Parser(string input)
        {
            tokenValues = new Dictionary<TimeToken, string>();
            this.input = input;
            tokenizer = new Tokenizer(input);
        }

        public DateTime Parse()
        {
            ReadTokens();

            DateTime startingTime = DateTime.Now;

            if (tokenValues.TryGetValue(TimeToken.Date, out var date))
            {
                startingTime = DateTime.Parse(date);
            }

            if (tokenValues.TryGetValue(TimeToken.TimeOfDay, out var timeOfDay))
            {
                startingTime = startingTime.Add(DateTime.Parse(timeOfDay).TimeOfDay);
            }

            if (tokenValues.TryGetValue(TimeToken.Second, out var seconds))
            {
                startingTime = startingTime.AddSeconds(double.Parse(seconds));
            }

            if (tokenValues.TryGetValue(TimeToken.Minute, out var minutes))
            {
                startingTime = startingTime.AddMinutes(double.Parse(minutes));
            }

            if (tokenValues.TryGetValue(TimeToken.Hour, out var hours))
            {
                startingTime = startingTime.AddHours(double.Parse(hours));
            }

            if (tokenValues.TryGetValue(TimeToken.Day, out var days))
            {
                startingTime = startingTime.AddDays(double.Parse(days));
            }

            if (tokenValues.TryGetValue(TimeToken.Week, out var weeks))
            {
                startingTime = startingTime.AddDays(double.Parse(weeks) * 7);
            }

            if (tokenValues.TryGetValue(TimeToken.Month, out var months))
            {
                startingTime = startingTime.AddMonths(int.Parse(months));
            }

            if (tokenValues.TryGetValue(TimeToken.Year, out var years))
            {
                startingTime = startingTime.AddYears(int.Parse(years));
            }



            return startingTime;
        }

        private void ReadTokens()
        {
            //System.Console.WriteLine(tokenizer.NextToken());
            while (tokenizer.TimeToken != TimeToken.END)
            {
                tokenizer.NextToken();

                while (tokenizer.TimeToken == TimeToken.Value)
                {
                    tokenizer.NextToken();
                }

                bool success;
                if (tokenizer.TimeToken == TimeToken.TimeOfDay)
                {
                    success = tokenValues.TryAdd(tokenizer.TimeToken, tokenizer.ProvidedTimeOfDay.ToString());
                }
                else if (tokenizer.TimeToken == TimeToken.Date)
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