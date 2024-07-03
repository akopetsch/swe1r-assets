﻿// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Vectors;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1327">
    ///       github.com - tim-tim707/SW_RACER_RE - types.h - swrModel_NodeMeshGroup</see></item>
    ///   <item>
    ///     <see href="https://github.com/Olganix/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L188">
    ///       github.com - Olganix/Sw_Racer - Swr_Model.h - SWR_AltN_0x3064</see></item>
    /// </list>
    /// </summary>
    [Sizeof(0x3c)]
    public class MeshGroupNode : FlaggedNode
    {
        #region Properties (serialized)

        [Order(0)]
        public Bounds3Single Aabb { get; set; }

        // TODO: rdMatrix44* cached_model_matrix; // points into rdMatrix44_ringBuffer
        // TODO: rdMatrix44* cached_mvp_matrix; // points into rdMatrix44_ringBuffer

        #endregion

        #region Properties (serialization)

        protected override Type ChildType => typeof(Mesh);

        #endregion

        #region Properties (helper)

        public ReadOnlyCollection<Mesh> Meshes => // TODO: use this property
            Children?.Cast<Mesh>().ToList().AsReadOnly();

        #endregion

        #region Constructor

        public MeshGroupNode() : 
            base(NodeFlags.MeshGroupNode)
        { }

        #endregion

        #region Methods (helper)

        public void UpdateBounds() =>
            Aabb = new Bounds3Single(Meshes.Select(m => m.FixedBounds).ToArray());

        #endregion
    }
}
