using System;
using System.Globalization;
using System.Linq;

namespace HumanTimeParser
{
    internal class Tokenizer
    {
        public const string DefaultSeparator = " ";

        private string[] unparsedTokens;
        private int tokenIndex;

        public Tokenizer(string input, string separator = null)
        {
            unparsedTokens = SplitInput(input, separator ?? DefaultSeparator);
            tokenIndex = -1;
        }

        private string[] SplitInput(string input, string separator)
            => input.Split(separator);

        private string GetNextUnparsedToken()
        {
            tokenIndex++;
            if (tokenIndex >= unparsedTokens.Length)
                return null;

            return unparsedTokens[tokenIndex];
        }

        public Token NextToken()
        {
            string unparsedToken = GetNextUnparsedToken();
            ReadOnlySpan<char> unparsedSpan = unparsedToken.AsSpan();

            if (unparsedToken == null)
                return new Token(TokenType.END, -1, null);

            if (double.TryParse(unparsedSpan, out _))
                return new Token(TokenType.Number, tokenIndex, unparsedToken);

            if (TokenizeTimeAndTwelveHourSpecifer(unparsedSpan) is { } givenTimeToken)
                return givenTimeToken;

            //tokenize given date
            if (DateTime.TryParse(unparsedSpan, out _))
                return new Token(TokenType.Date, tokenIndex, unparsedToken);

            if (TokenizeNumberAndRelativeTimeFormat(unparsedSpan) is { } relativeTimeToken)
                return relativeTimeToken;

            if (TokenizeDayOfWeek(unparsedSpan) is { } dayOfWeekToken)
                return dayOfWeekToken;

            if (unparsedToken.IsAmPmSpecifier())
                return new Token(TokenType.TwelveHourSpecifier, tokenIndex, unparsedToken);

            //if a token cant be parsed recurse until one is found
            return NextToken();
        }

        public Token PeekNextToken()
        {
            var token = NextToken();
            tokenIndex--;
            return token;
        }

        private Token TokenizeNumberAndRelativeTimeFormat(ReadOnlySpan<char> unparsedToken)
        {
            TokenType tokenType = TokenType.None;
            int splitPos = unparsedToken.FirstNonNumberPos();
            if (splitPos == -1)
                return null;

            Span<char> unparsedAbbreviation = new char[unparsedToken.Length - splitPos].AsSpan();
            unparsedToken.Slice(splitPos).ToLower(unparsedAbbreviation, CultureInfo.CurrentCulture);

            foreach (var abbreviation in Constants.RelativeTimeAbbreviations)
            {
                //TODO: see if there is a way to avoid allocating a string here
                if (abbreviation.Value.Contains(unparsedAbbreviation.ToString()))
                {
                    tokenType = abbreviation.Key;
                    break;
                }
            }

            if (tokenType == TokenType.None)
                return null;

            var containsNum = unparsedToken.ContainsNumber();

            if (tokenType == TokenType.Tomorrow)
                tokenType = tokenType | TokenType.Day | TokenType.Date;
            else if (containsNum)
                tokenType = tokenType | TokenType.Number;



            return new Token(tokenType, tokenIndex, containsNum ? unparsedToken.Slice(0, unparsedToken.FirstNonNumberPos()).ToString() : null);
        }

        private Token TokenizeTimeAndTwelveHourSpecifer(ReadOnlySpan<char> unparsedToken)
        {
            ReadOnlySpan<char> parseSpan = unparsedToken;
            TokenType tokenType = TokenType.TimeOfDay;
            if (unparsedToken.EndsWithAmPmSpecifier())
            {
                //subtract 2 because am and pm are only two chars long
                parseSpan = unparsedToken.Slice(0, unparsedToken.Length - 2);
                tokenType = tokenType | TokenType.TwelveHourSpecifier;
            }

            if (TimeSpan.TryParse(parseSpan, out _))
            {
                return new Token(tokenType, tokenIndex, unparsedToken.ToString());
            }
            else
                return null;
        }

        private Token TokenizeDayOfWeek(ReadOnlySpan<char> unparsedToken)
        {
            // var lowerCase = unparsedToken.ToLower();
            Span<char> lowerCaseToken = new char[unparsedToken.Length].AsSpan();
            unparsedToken.ToLower(lowerCaseToken, CultureInfo.CurrentCulture);
            foreach (var abbreviation in Constants.WeekdayAbbreviations)
            {
                //TODO: Find a way to avoid string allocation here
                var token = lowerCaseToken.ToString();
                if (abbreviation.Value.Contains(token))
                {
                    return new Token(TokenType.DayOfWeek, tokenIndex, token);
                }
            }

            return null;
        }
    }
}