using System;

namespace HumanTimeParser
{
    public static class StringExtensions
    {

        public static int FirstNonNumberPos(this string str)
        {
            char[] chars = str.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
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
        {
            if (str.Equals("am", StringComparison.OrdinalIgnoreCase) || str.Equals("pm", StringComparison.OrdinalIgnoreCase))
                return true;
            else
                return false;
        }

        public static bool EndsWithAmPmSpecifier(this string str)
        {
            string lowerCase = str.ToLower();
            if (lowerCase.EndsWith("am") || lowerCase.EndsWith("pm"))
                return true;
            else
                return false;
        }
    }
}