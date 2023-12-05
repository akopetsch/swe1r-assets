// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace FiddleApp
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string s, string value)
        {
            if (s.EndsWith(value))
                return s.Substring(0, s.Length - value.Length);
            else
                return s;
        }
    }
}
