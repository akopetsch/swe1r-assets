// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/akopetsch/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1311">
    ///       github.com - akopetsch/SW_RACER_RE - types.h - swrModel_NodeTransformedComputed</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L171">
    ///       github.com - akopetsch/Sw_Racer - Swr_Model.h - SWR_AltN_0xD066</see></item>
    /// </list>
    /// </summary>
    public class TransformedComputedNode : FlaggedNode
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
        /// X and Y are always 0x00000000 (0.0 as float32) 
        /// and Z is always 0x3F800000 (1.0 as float32), 
        /// thus these values are assumed to be float32 values composing a vector.
        /// </para>
        /// </summary>
        [Order(2)] public Vector3Single Vector { get; set; }

        public TransformedComputedNode() : base() =>
            Flags = NodeFlags.TransformedComputedNode;
    }
}
