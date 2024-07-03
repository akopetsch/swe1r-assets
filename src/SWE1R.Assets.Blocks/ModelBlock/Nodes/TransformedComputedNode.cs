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
    ///     <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1311">
    ///       github.com - tim-tim707/SW_RACER_RE - types.h - swrModel_NodeTransformedComputed</see></item>
    ///   <item>
    ///     <see href="https://github.com/Olganix/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L171">
    ///       github.com - Olganix/Sw_Racer - Swr_Model.h - SWR_AltN_0xD066</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class TransformedComputedNode : FlaggedNode
    {
        #region Properties (serialized)

        /// <summary>
        /// If 1, this node's position is always moved with the model.
        /// Used for cubemaps, podd binders and podd dark smoke when overheating.
        /// <para>
        ///   Always 1 if <see cref="OrientationOption">OrientationOption</see> is 0, otherwise 0.
        /// </para>
        /// </summary>
        [Order(0)]
        public short FollowModelPosition { get; set; }
        /// <summary>
        /// Modifies the rotation (and maybe scale) of this node:
        /// <list type="bullet">
        ///   <item>0: disabled</item>
        ///   <item>1: orients node to face to the model (billboard)</item>
        ///   <item>2: TODO (maybe unused)</item>
        ///   <item>3: TODO (maybe unused)</item>
        /// </list>
        /// <para>
        ///   Always 1 if <see cref="FollowModelPosition">FollowModelPosition</see> is 0, otherwise 0.
        /// </para>
        /// </summary>
        [Order(1)]
        public short OrientationOption { get; set; }
        /// <summary>
        /// Always (0, 0, 1).
        /// <para>
        /// X and Y are always 0x00000000 (0.0 as float32) 
        /// and Z is always 0x3F800000 (1.0 as float32), 
        /// thus these values are assumed to be float32 values composing a vector.
        /// </para>
        /// </summary>
        [Order(2)]
        public Vector3Single UpVector { get; set; }

        // TODO: uint32_t unk4;

        #endregion

        #region Constructor

        public TransformedComputedNode() :
            base(NodeFlags.TransformedComputedNode)
        { }

        #endregion
    }
}
