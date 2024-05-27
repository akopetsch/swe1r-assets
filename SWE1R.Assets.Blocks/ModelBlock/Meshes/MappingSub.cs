// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    public class MappingSub
    {
        #region Properties (serialized)

        [Order(0)] public int Int_0 { get; set; }
        [Order(1)] public int Int_1 { get; set; }
        [Order(2), Reference] public MappingChild Child { get; set; }

        #endregion
    }
}
