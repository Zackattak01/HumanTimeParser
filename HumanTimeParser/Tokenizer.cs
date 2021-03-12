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

            if (TokenizeTimeAndTwelveHourSpecifer(unparsedSpan, unparsedToken) is { } givenTimeToken)
                return givenTimeToken;

            //tokenize given date
            if (DateTime.TryParse(unparsedSpan, out _))
                return new Token(TokenType.Date, tokenIndex, unparsedToken);

            if (TokenizeNumberAndRelativeTimeFormat(unparsedSpan, unparsedToken) is { } relativeTimeToken)
                return relativeTimeToken;

            if (TokenizeDayOfWeek(unparsedToken) is { } dayOfWeekToken)
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

        //include a str obj to avoid allocing a new one.  We already have one so might as well use it.
        private Token TokenizeNumberAndRelativeTimeFormat(ReadOnlySpan<char> unparsedToken, string comparisonString)
        {
            TokenType tokenType = TokenType.None;
            int splitPos = comparisonString.FirstNonNumberPos();
            if (splitPos == -1)
                return null;

            var unparsedAbbreviation = unparsedToken.Slice(splitPos);

            foreach (var abbreviation in Constants.RelativeTimeAbbreviations)
            {
                if (abbreviation.Value.Contains(comparisonString))
                {
                    tokenType = abbreviation.Key;
                    break;
                }
            }

            if (tokenType == TokenType.None)
                return null;

            var containsNum = comparisonString.ContainsNumber();

            if (tokenType == TokenType.Tomorrow)
                tokenType = tokenType | TokenType.Day | TokenType.Date;
            else if (containsNum)
                tokenType = tokenType | TokenType.Number;



            return new Token(tokenType, tokenIndex, containsNum ? unparsedToken.Slice(0, splitPos).ToString() : null);
        }

        private Token TokenizeTimeAndTwelveHourSpecifer(ReadOnlySpan<char> unparsedToken, string returnString)
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
                return new Token(tokenType, tokenIndex, returnString);
            }
            else
                return null;
        }

        //include a str obj to avoid allocing a new one.  We already have one so might as well use it.
        private Token TokenizeDayOfWeek(string comparisonString)
        {
            foreach (var abbreviation in Constants.WeekdayAbbreviations)
            {
                if (abbreviation.Value.Contains(comparisonString))
                {
                    return new Token(TokenType.DayOfWeek, tokenIndex, comparisonString);
                }
            }

            return null;
        }
    }
}