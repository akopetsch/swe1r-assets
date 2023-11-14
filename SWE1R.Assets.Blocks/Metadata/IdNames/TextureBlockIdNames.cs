// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
