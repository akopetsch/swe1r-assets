// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Materials;

namespace SWE1R.Assets.Blocks.ModelBlock.Animations
{
    [Sizeof(8)]
    public class MaterialReference
    {
        #region Properties (serialized)

        /// <summary>
        /// Never null.
        /// </summary>
        [Order(0), Reference]
        public Material Material { get; set; }

        #endregion
    }
}
