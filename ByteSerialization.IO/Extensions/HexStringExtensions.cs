// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Text;

namespace ByteSerialization.IO.Extensions
{
    public static class HexStringExtensions
    {
        public static string ToHexString(this byte[] bytes)
        {
            var result = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString("x2"));
            return result.ToString();
        }

        public static string ToHexString(this int value) => 
            ((long)value).ToHexString();

        public static string ToHexString(this long value)
        {
            string s = value.ToString("x");
            int n = s.Length;
            return n % 2 == 1 ?
                ("0" + s) : s;
        }
    }
}
