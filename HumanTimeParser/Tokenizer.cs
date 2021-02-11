using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using Name;

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

            if (unparsedToken == null)
                return new Token(TokenType.END, -1, null);

            //tokenize given date
            if (DateTime.TryParse(unparsedToken, out _))
                return new Token(TokenType.Date, tokenIndex, unparsedToken);


            if (TokenizeNumberAndRelativeTimeFormat(unparsedToken) is { } relativeTimeToken)
                return relativeTimeToken;

            if (TokenizeTimeAndTwelveHourSpecifer(unparsedToken) is { } givenTimeToken)
                return givenTimeToken;

            if (unparsedToken.IsAmPmSpecifier())
                return new Token(TokenType.TwelveHourSpecifier, tokenIndex, unparsedToken);

            //if a token cant be parsed recurse until one is found
            return NextToken();
        }

        private Token TokenizeNumberAndRelativeTimeFormat(string unparsedToken)
        {
            TokenType tokenType = TokenType.None;

            foreach (var abbreviation in Constants.Abbreviations)
            {
                if (abbreviation.Value.Any(x => unparsedToken.EndsWith(x)))
                {
                    tokenType = abbreviation.Key;
                    break;
                }
            }

            if (tokenType == TokenType.None)
                return null;

            var containsNum = unparsedToken.ContainsNumber();

            if (tokenType == TokenType.Tomorrow)
                tokenType = tokenType | TokenType.Day;
            else if (containsNum)
                tokenType = tokenType | TokenType.Number;



            return new Token(tokenType, tokenIndex, containsNum ? unparsedToken.Substring(0, unparsedToken.FirstNonNumberPos()) : null);
        }

        private Token TokenizeTimeAndTwelveHourSpecifer(string unparsedToken)
        {
            string parseStr = unparsedToken;
            TokenType tokenType = TokenType.TimeOfDay;
            if (unparsedToken.EndsWithAmPmSpecifier())
            {
                //subtract 2 because am and pm are only two chars long
                parseStr = unparsedToken.Substring(0, unparsedToken.Length - 2);
                tokenType = tokenType | TokenType.TwelveHourSpecifier;
            }

            if (TimeSpan.TryParse(parseStr, out _))
            {
                return new Token(tokenType, tokenIndex, unparsedToken);
            }
            else
                return null;
        }
    }
}