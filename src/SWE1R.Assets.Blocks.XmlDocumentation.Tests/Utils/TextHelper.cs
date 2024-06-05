// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.XmlDocumentation.Tests.Utils
{
    internal static class TextHelper
    {
        internal static readonly string[] LineBreaks = ["\r\n", "\r", "\n"];

        internal static string GetLineByNumber(string text, int lineNumber)
        {
            string[] lines = text.Split(LineBreaks, StringSplitOptions.None);
            if (lineNumber > 0 && lineNumber <= lines.Length)
                return lines[lineNumber - 1];
            else
                throw new ArgumentOutOfRangeException(nameof(lineNumber));
        }
    }
}
