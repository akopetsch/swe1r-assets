// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Common.Vectors;

namespace SWE1R.Assets.Blocks.SplineBlock
{
    public class SplineSegmentData 
    {
        /* 00 */ [Order(0)] public short Word_00 { get; set; }
        /* 02 */ [Order(1)] public short Word_02 { get; set; }
        /* 04 */ [Order(2)] public short Word_04 { get; set; }
        /* 06 */ [Order(3)] public short Word_06 { get; set; }
        /* 08 */ [Order(4)] public short PreviousNodeId { get; set; }
        /* 0a */ [Order(5)] public short Unk_0a { get; set; }
        /* 0c */ [Order(6)] public short Unk_0c { get; set; }
        /* 0e */ [Order(7)] public short Unk_0e { get; set; }
        /* 10 */ [Order(8)] public Vector3Single KnotVector { get; set; }
        /* .. */
        /* 24 */ [Order(9), Offset(0x24)] public float Float_24 { get; set; }
        /* 28 */ [Order(10)] public Vector3Single ControlPoint1 { get; set; } // before KnotVector
        /* 34 */ [Order(11)] public Vector3Single ControlPoint2 { get; set; } // after KnotVector
        /* 40 */ [Order(12)] public int Unk_40 { get; set; }
    }
}
