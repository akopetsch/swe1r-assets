// SPDX-License-Identifier: MIT

using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.Metadata.IdNames
{
    public static class ModelBlockIdNames
    {
        public static string Default { get; } = "_default";
        public static string WinDemo { get; } = "win-demo";
        public static string N64Us { get; } = "n64_us";
        public static string N64EuJp { get; } = "n64_eu-jp";

        public static IEnumerable<string> All
        {
            get
            {
                yield return Default;
                yield return WinDemo;
                yield return N64Us;
                yield return N64EuJp;
            }
        }
    }
}
