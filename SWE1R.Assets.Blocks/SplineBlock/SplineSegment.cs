// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.SplineBlock
{
    public class SplineSegment
    {
        [Order(0)] public SplineSegmentData Data { get; set; }
        [Order(1)] public SplineSegmentHeader Header { get; set; }
    }
}
