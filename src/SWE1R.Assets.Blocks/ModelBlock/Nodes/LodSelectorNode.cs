// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/akopetsch/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1290">
    ///       github.com - akopetsch/SW_RACER_RE - types.h - swrModel_NodeLODSelector</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L111">
    ///       github.com - akopetsch/Sw_Racer - Swr_Model.h - SWR_AltN_0x5066</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class LodSelectorNode : FlaggedNode
    {
        [Order(0), Length(8)] public float[] Floats { get; set; }
        /// <summary>
        /// Always <c>{0, 0x80000000, 0}</c>.
        /// <para>
        /// Not sure if these are float32 values composing a vector, because 0x80000000 would be an unusual value (-0).
        /// </para>
        /// </summary>
        [Order(1), Length(3)] public int[] Ints { get; set; }

        public LodSelectorNode() : base() =>
            Flags = NodeFlags.LodSelectorNode;
    }
}
