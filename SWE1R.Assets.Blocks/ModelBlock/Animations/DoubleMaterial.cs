// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Animations
{
    public class DoubleMaterial
    {
        #region Properties (serialized)

        [Order(0), Reference] public Material Foo1 { get; set; }
        [Order(1), Reference] public Material Foo2 { get; set; } // TODO: confirm property

        #endregion

        #region Methods

        public IEnumerable<Material> GetMaterials()
        {
            yield return Foo1;
            yield return Foo2;
        }

        #endregion
    }
}
