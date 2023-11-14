// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Common.Vectors;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    /// <summary>
    /// <see href="https://github.com/Olganix/Sw_Racer/blob/master/include/Swr_Model.h#L188">SWR_AltN_0x3064</see>
    /// </summary>
    [Sizeof(0x3c)]
    public class MeshGroup3064 : FlaggedNode
    {
        [Order(0)] public Bounds3Single Bounds { get; set; }

        protected override Type ChildType => typeof(Mesh);

        public MeshGroup3064() : base() =>
            Flags = NodeFlags.MeshGroup3064;

        public ReadOnlyCollection<Mesh> Meshes => // TODO: use this property
            Children?.Cast<Mesh>().ToList().AsReadOnly();
    }
}
