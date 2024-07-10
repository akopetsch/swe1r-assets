// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1297">
    ///       github.com - tim-tim707/SW_RACER_RE - types.h - swrModel_NodeTransformed</see></item>
    ///   <item>
    ///     <see href="https://github.com/Olganix/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L129">
    ///       github.com - Olganix/Sw_Racer - Swr_Model.h - SWR_AltN_0xD064</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class TransformedNode : AbstractTransformedNode
    {
        #region Constructor

        public TransformedNode() : 
            base(NodeFlags.TransformedNode)
        { }

        #endregion
    }
}
