﻿// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.Behaviours
{
    public class MappingSub
    {
        #region Properties (serialized)

        [Order(0)]
        public int Int_0 { get; set; }
        [Order(1)]
        public int Int_1 { get; set; }
        [Order(2), Reference]
        public MappingChild Child { get; set; }

        #endregion
    }
}
