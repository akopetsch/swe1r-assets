// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Common.Vectors;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    public class UnknownD066 : FlaggedNode
    {
        /// <summary>
        /// Always 1 if <see cref="Word2">Word2</see> is 0, otherwise 0.
        /// </summary>
        [Order(0)] public short Word1 { get; set; }
        /// <summary>
        /// Always 1 if <see cref="Word1">Word1</see> is 0, otherwise 0.
        /// </summary>
        [Order(1)] public short Word2 { get; set; }
        /// <summary>
        /// Always (0.0, 0.0, 1.0).
        /// <para>
        /// X and Y are always 0x00000000 (0.0 as float32) and Z is always 0x3F800000 (1.0 as float32), thus these values are assumed to be float32 values composing a vector.
        /// </para>
        /// </summary>
        [Order(2)] public Vector3Single Vector { get; set; }

        public UnknownD066() : base() =>
            Flags = NodeFlags.UnknownD066;
    }
}
