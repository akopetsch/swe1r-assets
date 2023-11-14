// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;

namespace SWE1R.Assets.Blocks.ModelBlock.Animations
{
    public class DoubleMaterial
    {
        [Order(0), Reference] public Material Foo1 { get; set; }
        [Order(1), Reference] public Material Foo2 { get; set; } // TODO: confirm property
    }
}
