// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Nodes
{
    public class FlaggedNodeComponentFactory
    {
        public static FlaggedNodeComponentFactory Instance { get; } = new FlaggedNodeComponentFactory();
        private FlaggedNodeComponentFactory() { }

        private readonly
            Dictionary<NodeFlags, Type> componentTypeByNodeFlags =
            new Dictionary<NodeFlags, Type>()
        {
            { NodeFlags.Group5064, typeof(Group5064Component) },
            { NodeFlags.Group5065, typeof(Group5065Component) },
            { NodeFlags.Group5066, typeof(Group5066Component) },
            { NodeFlags.MeshGroup3064, typeof(MeshGroup3064Component) },
            { NodeFlags.TransformableD064, typeof(TransformableD064Component) },
            { NodeFlags.TransformableD065, typeof(TransformableD065Component) },
            { NodeFlags.UnknownD066, typeof(UnknownD066Component) },
        };

        public Type GetComponentType(FlaggedNode node) =>
            componentTypeByNodeFlags[node.Flags];
    }
}
