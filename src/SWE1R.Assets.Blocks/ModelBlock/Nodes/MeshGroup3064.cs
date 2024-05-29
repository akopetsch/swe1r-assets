// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Vectors;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    /// <summary>
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L188">SWR_AltN_0x3064</see>
    /// </summary>
    [Sizeof(0x3c)]
    public class MeshGroup3064 : FlaggedNode
    {
        #region Properties (serialized)

        [Order(0)]
        public Bounds3Single Bounds { get; set; }

        #endregion

        #region Properties (serialization)

        protected override Type ChildType => typeof(Mesh);

        #endregion

        #region Properties (helper)

        public ReadOnlyCollection<Mesh> Meshes => // TODO: use this property
            Children?.Cast<Mesh>().ToList().AsReadOnly();

        #endregion

        #region Constructor

        public MeshGroup3064() : base() =>
            Flags = NodeFlags.MeshGroup3064;

        #endregion

        #region Methods (helper)

        public void UpdateBounds() =>
            Bounds = new Bounds3Single(Meshes.Select(m => m.FixedBounds).ToArray());

        #endregion
    }
}
