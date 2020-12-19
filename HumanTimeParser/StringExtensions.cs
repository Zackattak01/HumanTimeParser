using System;

namespace Name
{
    public static class StringExtensions
    {
        public static bool IsNumber(this string str)
        {
            char[] chars = str.ToCharArray();

            foreach (var c in chars)
            {
                if (!char.IsDigit(c) && !char.IsPunctuation(c))
                    return false;


            }

            return true;
        }

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
    }
}