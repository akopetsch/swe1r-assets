// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using Swe1rDimensionsBitmask = SWE1R.Assets.Blocks.ModelBlock.Meshes.DimensionsBitmask;
using Swe1rMaterialTextureChild = SWE1R.Assets.Blocks.ModelBlock.Materials.MaterialTextureChild;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class MaterialTextureChildObject
    {
        public byte byte_0;
        public byte byte_1;
        public byte byte_2;
        public Swe1rDimensionsBitmask dimensionsBitmask;
        public byte byte_4;
        public byte byte_5;
        public byte byte_6;
        public byte byte_7;
        public byte byte_c;
        public byte byte_d;
        public byte byte_e;
        public byte byte_f;

        public MaterialTextureChildObject(
            Swe1rMaterialTextureChild materialTextureChild, ModelImporter modelImporter)
        {
            byte_0 = materialTextureChild.Byte_0;
            byte_1 = materialTextureChild.Byte_1;
            byte_2 = materialTextureChild.Byte_2;
            dimensionsBitmask = materialTextureChild.DimensionsBitmask;
            byte_4 = materialTextureChild.Byte_4;
            byte_5 = materialTextureChild.Byte_5;
            byte_6 = materialTextureChild.Byte_6;
            byte_7 = materialTextureChild.Byte_7;
            byte_c = materialTextureChild.Byte_c;
            byte_d = materialTextureChild.Byte_d;
            byte_e = materialTextureChild.Byte_e;
            byte_f = materialTextureChild.Byte_f;
        }

        public Swe1rMaterialTextureChild Export() =>
            new Swe1rMaterialTextureChild() {
                Byte_0 = byte_0,
                Byte_1 = byte_1,
                Byte_2 = byte_2,
                DimensionsBitmask = dimensionsBitmask,
                Byte_4 = byte_4,
                Byte_5 = byte_5,
                Byte_6 = byte_6,
                Byte_7 = byte_7,
                Byte_c = byte_c,
                Byte_d = byte_d,
                Byte_e = byte_e,
                Byte_f = byte_f,
            };
    }
}
