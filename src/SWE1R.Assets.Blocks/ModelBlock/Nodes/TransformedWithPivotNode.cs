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
    ///     <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1303">
    ///       github.com - tim-tim707/SW_RACER_RE - types.h - swrModel_NodeTransformedWithPivot</see></item>
    ///   <item>
    ///     <see href="https://github.com/Olganix/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L148">
    ///       github.com - Olganix/Sw_Racer - Swr_Model.h - SWR_AltN_0xD065</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class TransformedWithPivotNode : AbstractTransformedNode
    {
        #region Properties (serialized)

        /// <summary>
        /// If <seealso cref="FlaggedNode.Flags3">Flags3</seealso> &amp; 0x10
        /// transforms are modified to use this position as the center position.
        /// </summary>
        [Order(0)]
        public Vector3Single Pivot { get; set; }

        #endregion

        #region Constructor

        public TransformedWithPivotNode() : 
            base(NodeFlags.TransformedWithPivotNode)
        { }

        #endregion
    }
}
