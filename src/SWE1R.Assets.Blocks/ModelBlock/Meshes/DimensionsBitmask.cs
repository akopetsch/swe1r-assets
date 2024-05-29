// SPDX-License-Identifier: MIT

using System;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    [Flags]
    public enum DimensionsBitmask : byte
    {
        DoubleHeight =     0x01,
        DoubleWidth =      0x10,
        FlippedVertically =   0x02,
        FlippedHorizontally = 0x20,
    }
}
