// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.Behaviours
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/tly000/SW_RACER_RE/blob/3d3a45a44ce7043389d2a6686af1b72732fb9d41/src/types.h#L1483">
    ///       github.com - tly000/SW_RACER_RE - types.h - swrModel_TriggerDescription</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1504">
    ///       github.com - akopetsch/SW_RACER_RE - types.h - swrModel_MappingChild</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L488">
    ///       github.com - akopetsch/Sw_Racer - Swr_Model.h - SWR_MODEL_Section8</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class MappingChild
    {
        #region Properties (serialized)

        /// <summary>
        /// Seems to be a position vector.
        /// </summary>
        [Order(0)]
        public Vector3Single Vector_00 { get; set; }
        /// <summary>
        /// X: -1.0 - 1.0,  two decimal places
        /// Y: -1.0 - 1.0,  two decimal places
        /// Z: -0.3 - 0.11, two decimal places
        /// </summary>
        [Order(1)]
        public Vector3Single Vector_0c { get; set; }
        [Order(2)]
        public short Word_18 { get; set; }
        [Order(3)]
        public byte Byte_1a { get; set; }
        [Order(4)]
        public byte Byte_1b { get; set; }
        [Order(5)]
        public short Word_1c { get; set; }
        /// <summary>
        /// 53 different values, 59 times 163
        /// </summary>
        [Order(6)]
        public byte Byte_1e { get; set; }
        /// <summary>
        /// 25 different values, 59 times 31, 59 times 215
        /// </summary>
        [Order(7)]
        public byte Byte_1f { get; set; }
        /// <summary>
        /// Nullable.
        /// </summary>
        [Order(8), Reference(ReferenceHandling.Postpone)]
        public FlaggedNode FlaggedNode_20 { get; set; }
        [Order(9)]
        public short Word_24 { get; set; }
        /// <summary>
        /// Padding. (always 0)
        /// </summary>
        [Order(10)]
        public short Word_26 { get; set; }
        /// <summary>
        /// Null if this is the last one.
        /// </summary>
        [Order(11), Reference]
        public MappingChild Next { get; set; }

        #endregion
    }
}
