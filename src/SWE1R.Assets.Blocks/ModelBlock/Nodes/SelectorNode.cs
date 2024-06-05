// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/akopetsch/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1280">
    ///       github.com - akopetsch/SW_RACER_RE - types.h - swrModel_NodeSelector</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L104">
    ///       github.com - akopetsch/Sw_Racer - Swr_Model.h - SWR_AltN_0x5065</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class SelectorNode : FlaggedNode
    {
        /// <summary>
        /// Always 0 or -1.
        /// </summary>
        [Order(0)] public int Int { get; set; }

        public SelectorNode() : base() =>
            Flags = NodeFlags.SelectorNode;
    }
}
