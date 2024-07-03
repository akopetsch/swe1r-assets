// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types_enums.h#L64">
    ///       github.com - tim-tim707/SW_RACER_RE - types_enums.h - swrModel_NodeType</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public enum NodeFlags : uint
    {
        MeshGroupNode =
            0b0_0011_0000_0110_0100, // 0x3064
        BasicNode =
            0b0_0101_0000_0110_0100, // 0x5064
        SelectorNode =
            0b0_0101_0000_0110_0101, // 0x5065
        LodSelectorNode =
            0b0_0101_0000_0110_0110, // 0x5066
        TransformedNode =
            0b0_1101_0000_0110_0100, // 0xD064
        TransformedWithPivotNode =
            0b0_1101_0000_0110_0101, // 0xD065
        TransformedComputedNode =
            0b0_1101_0000_0110_0110, // 0xD066
    }
}
