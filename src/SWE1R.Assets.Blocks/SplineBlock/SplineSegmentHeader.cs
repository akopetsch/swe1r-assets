// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.SplineBlock
{
    public class SplineSegmentHeader 
    {
        /* 00 */ [Order(0)] public short Word_0 { get; set; }
        /* 02 */ [Order(1)] public short Word_2 { get; set; }
        /* 04 */ [Order(2)] public int ElementsCount { get; set; }
        /* 08 */ [Order(3)] public int UnknownCount { get; set; }
        /* 0c */ [Order(4)] public short Word_c { get; set; }
        /* 0e */ [Order(5)] public short Word_e { get; set; }
    }
}
