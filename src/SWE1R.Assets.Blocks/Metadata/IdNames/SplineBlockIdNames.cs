// SPDX-License-Identifier: MIT

using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.Metadata.IdNames
{
    public static class SplineBlockIdNames
    {
        public static string Default { get; } = "_default";
        public static string N64 { get; } = "n64";

        public static IEnumerable<string> All
        {
            get
            {
                yield return Default;
                yield return N64;
            }
        }
    }
}
