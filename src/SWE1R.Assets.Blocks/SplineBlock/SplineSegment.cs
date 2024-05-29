// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.SplineBlock
{
    public class SplineSegment
    {
        [Order(0)] public SplineSegmentData Data { get; set; }
        [Order(1)] public SplineSegmentHeader Header { get; set; }
    }
}
