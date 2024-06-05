// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/akopetsch/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1297">
    ///       github.com - akopetsch/SW_RACER_RE - types.h - swrModel_NodeTransformed</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L129">
    ///       github.com - akopetsch/Sw_Racer - Swr_Model.h - SWR_AltN_0xD064</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class TransformedNode : FlaggedNode
    {
        #region Properties (serialized)

        [Order(0)]
        public Matrix3x4Single Matrix { get; set; }

        #endregion

        #region Constructor

        public TransformedNode() : base() =>
            Flags = NodeFlags.TransformedNode;

        #endregion
    }
}
