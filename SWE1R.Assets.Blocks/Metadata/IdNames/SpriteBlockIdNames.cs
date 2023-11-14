// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.Metadata.IdNames
{
    public static class SpriteBlockIdNames
    {
        public static string Default { get; } = "_default";
        public static string WinDemo { get; } = "win-demo";
        public static string WinDe_Mac_Dc { get; } = "win_de__mac__dco";
        public static string N64Us { get; } = "n64_us";
        public static string N64Jp { get; } = "n64_jp";

        public static IEnumerable<string> All
        {
            get
            {
                yield return Default;
                yield return WinDemo;
                yield return WinDe_Mac_Dc;
                yield return N64Us;
                yield return N64Jp;
            }
        }
    }
}
