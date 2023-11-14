// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Common.Vectors;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    /// <summary>
    /// <see href="https://github.com/Olganix/Sw_Racer/blob/master/include/Swr_Model.h#L488">SWR_MODEL_Section8</see>
    /// </summary>
    public class MappingChild
    {
        /// <summary>
        /// Seems to be a position vector.
        /// </summary>
        [Order(0)] public Vector3Single Vector_00 { get; set; }

        /// <summary>
        /// X: -1.0 - 1.0,  two decimal places
        /// Y: -1.0 - 1.0,  two decimal places
        /// Z: -0.3 - 0.11, two decimal places
        /// </summary>
        [Order(1)] public Vector3Single Vector_0c { get; set; }
        [Order(2)] public short Word_18 { get; set; }
        [Order(3)] public byte Byte_1a { get; set; }
        [Order(4)] public byte Byte_1b { get; set; }
        [Order(5)] public short Word_1c { get; set; }

        /// <summary>
        /// 53 different values, 59 times 163
        /// </summary>
        [Order(6)] public byte Byte_1e { get; set; }

        /// <summary>
        /// 25 different values, 59 times 31, 59 times 215
        /// </summary>
        [Order(7)] public byte Byte_1f { get; set; }

        /// <summary>
        /// Nullable.
        /// </summary>
        [Reference(ReferenceHandling.Postpone)]
        [Order(8)] public FlaggedNode FlaggedNode_20 { get; set; }
        [Order(9)] public short Word_24 { get; set; }

        /// <summary>
        /// Padding. (always 0)
        /// </summary>
        [Order(10)] public short Word_26 { get; set; }

        /// <summary>
        /// Null if this is the last one.
        /// </summary>
        [Reference]
        [Order(11)] public MappingChild Next { get; set; }
    }
}
