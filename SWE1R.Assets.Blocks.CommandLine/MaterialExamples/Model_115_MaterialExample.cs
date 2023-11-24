// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.CommandLine.MaterialExamples
{
    public static class Model_115_MaterialExample
    {
        public static MeshGroup3064 CreateMeshGroup() =>
            new MeshGroup3064() {
                // values copied from model 115
                Bitfield1 = 767,
                Bitfield2 = 761,
                Children = new List<INode>(),
            };

        public static Material CreateMaterial() =>
            new Material() {
                // values copied from model 115 (Material @ 0x013358)
                Int = 14,
                Texture = CreateMaterialTexture(),
                Properties = CreateMaterialProperties(),
            };

        private static MaterialTexture CreateMaterialTexture() =>
            new MaterialTexture() {
                // values copied from Model 115, MaterialTexture @ 0x013368
                Mask_Unk = 1,
                Width4 = 256,
                Height4 = 256,
                Byte_0c = 2,
                Width = 64,
                Height = 64,
                Width_Unk = 32768,
                Height_Unk = 32768,
                Flags = 512,
                Mask = 1023,
                Children = new MaterialTextureChild[] {
                    new MaterialTextureChild() {
                        Byte_2 = 4,
                        Byte_4 = 6,
                        Byte_5 = 6,
                        Byte_d = 252,
                        Byte_f = 252,
                    },
                    null,
                    null,
                    null,
                    null,
                    null,
                },
                TextureIndex = 51,
            };

        private static MaterialProperties CreateMaterialProperties() =>
            new MaterialProperties() {
                // values copied from model 115 (Material @ 0x013358)
                Word_4 = 2,
                Ints_6 = new int[] {
                    0x011F041F, // = 18809887
                    0x07070704, // = 117901060
                },
                Ints_e = new int[] {
                    0x1f1f1f00, // = 522133248,
                    0x07070700, // = 117901060
                },
                Bitmask1 = unchecked((int)0xC8000000), // = -939524096
                Bitmask2 = 0x0112038, // = 1122360
            };
    }
}
