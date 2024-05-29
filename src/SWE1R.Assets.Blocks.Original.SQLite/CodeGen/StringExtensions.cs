// SPDX-License-Identifier: GPL-2.0-only

namespace SWE1R.Assets.Blocks.Original.SQLite.CodeGen
{
    internal static class StringExtensions
    {
        internal static string TrimEnd(this string s, string value)
        {
            if (s.EndsWith(value))
                return s.Substring(0, s.Length - value.Length);
            else
                return s;
        }
    }
}
