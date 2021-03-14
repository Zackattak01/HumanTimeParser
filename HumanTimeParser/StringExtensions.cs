using System;

namespace HumanTimeParser
{
    public static class StringExtensions
    {

        public static int FirstNonNumberPos(this string str)
        {
            var chars = str.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                if (!char.IsDigit(chars[i]) && !char.IsPunctuation(chars[i]))
                    return i;
            }

            return -1;
        }

        public static bool ContainsNumber(this string str)
        {
            foreach (var c in str)
            {
                if (char.IsDigit(c))
                    return true;
            }
            return false;
        }

        public static bool IsAmPmSpecifier(this string str)
            => str.Equals(Constants.Am, StringComparison.OrdinalIgnoreCase) || str.Equals(Constants.Pm, StringComparison.OrdinalIgnoreCase);
        }
}