// SPDX-License-Identifier: GPL-2.0-only

using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.Metadata.IdNames
{
    public static class TextureBlockIdNames
    {
        public static string Default { get; } = "_default";
        public static string WinDemo { get; } = "win-demo";
        public static string N64 { get; } = "n64";

        public static IEnumerable<string> All
        {
            get
            {
                yield return Default;
                yield return WinDemo;
                yield return N64;
            }
        }
    }
}
