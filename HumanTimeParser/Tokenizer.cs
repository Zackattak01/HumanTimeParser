using System;
using System.Globalization;
using System.Linq;

namespace HumanTimeParser
{
    internal class Tokenizer
    {
        private const string DefaultSeparator = " ";

        private readonly string[] _unparsedTokens;
        private int _tokenIndex;

        public Tokenizer(string input, string separator = null)
        {
            _unparsedTokens = SplitInput(input, separator ?? DefaultSeparator);
            _tokenIndex = -1;
        }

        private static string[] SplitInput(string input, string separator)
            => input.Split(separator);

        private string GetNextUnparsedToken()
        {
            _tokenIndex++;
            if (_tokenIndex >= _unparsedTokens.Length)
                return null;

            return _unparsedTokens[_tokenIndex];
        }

        public Token NextToken()
        {
            var unparsedToken = GetNextUnparsedToken();
            var unparsedSpan = unparsedToken.AsSpan();

            if (unparsedToken == null)
                return new Token(TokenType.End, -1, null);

            if (double.TryParse(unparsedSpan, out _))
                return new Token(TokenType.Number, _tokenIndex, unparsedToken);

            if (TokenizeTimeAndTwelveHourSpecifier(unparsedSpan, unparsedToken) is { } givenTimeToken)
                return givenTimeToken;

            //tokenize given date
            if (DateTime.TryParse(unparsedSpan, out _))
                return new Token(TokenType.Date, _tokenIndex, unparsedToken);

            if (TokenizeNumberAndRelativeTimeFormat(unparsedSpan, unparsedToken) is { } relativeTimeToken)
                return relativeTimeToken;

            if (TokenizeDayOfWeek(unparsedToken) is { } dayOfWeekToken)
                return dayOfWeekToken;

            if (unparsedToken.IsAmPmSpecifier())
                return new Token(TokenType.TwelveHourSpecifier, _tokenIndex, unparsedToken);

            //if a token cant be parsed recurse until one is found
            return NextToken();
        }

        public Token PeekNextToken()
        {
            var token = NextToken();
            _tokenIndex--;
            return token;
        }

        //include a str obj to avoid allocating a new one.  We already have one so might as well use it.
        private Token TokenizeNumberAndRelativeTimeFormat(ReadOnlySpan<char> unparsedToken, string comparisonString)
        {
            var tokenType = TokenType.None;
            var splitPos = comparisonString.FirstNonNumberPos();
            if (splitPos == -1)
                return null;

            var unparsedAbbreviation = unparsedToken.Slice(splitPos);

            foreach (var abbreviation in Constants.RelativeTimeAbbreviations)
            {
                if (abbreviation.Value.Contains(unparsedAbbreviation.ToString()))
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
                tokenType |= TokenType.Number;



            return new Token(tokenType, _tokenIndex, containsNum ? unparsedToken.Slice(0, splitPos).ToString() : null);
        }

        private Token TokenizeTimeAndTwelveHourSpecifier(ReadOnlySpan<char> unparsedToken, string returnString)
        {
            var parseSpan = unparsedToken;
            var tokenType = TokenType.TimeOfDay;
            if (unparsedToken.EndsWithAmPmSpecifier())
            {
                //subtract 2 because am and pm are only two chars long
                parseSpan = unparsedToken.Slice(0, unparsedToken.Length - 2);
                tokenType |= TokenType.TwelveHourSpecifier;
            }

            if (TimeSpan.TryParse(parseSpan, out _))
            {
                return new Token(tokenType, _tokenIndex, returnString);
            }
            else
                return null;
        }

        //include a str obj to avoid allocating a new one.  We already have one so might as well use it.
        private Token TokenizeDayOfWeek(string comparisonString)
        {
            foreach (var abbreviation in Constants.WeekdayAbbreviations)
            {
                if (abbreviation.Value.Contains(comparisonString))
                {
                    return new Token(TokenType.DayOfWeek, _tokenIndex, comparisonString);
                }
            }

            return null;
        }
    }
}