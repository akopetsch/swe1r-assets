// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.CommandLine.MaterialExamples
{
    public static class Model_170_MaterialExample
    {
        public static MeshGroup3064 CreateMeshGroup() =>
            new MeshGroup3064() {
                // values copied from model 170
                Bitfield1 = -1,
                Bitfield2 = -3,
                Children = new List<INode>(),
            };

        public static Material CreateMaterial() =>
            new Material() {
                // values copied from model 170 (Material @ 0x0108)
                Int = 31,
                Texture = CreateMaterialTexture(),
                Properties = CreateMaterialProperties(),
            };

        private static MaterialTexture CreateMaterialTexture() =>
            new MaterialTexture() {
                // values copied from Model 170, MaterialTexture @ 0x0118
                Width4 = 256,
                Height4 = 128,
                Byte_0c = 4,
                Width = 64,
                Height = 32,
                Width_Unk = 8192,
                Height_Unk = 4096,
                Flags = 512,
                Mask = 511,
                Children = new MaterialTextureChild[] {
                    new MaterialTextureChild() {
                        Byte_2 = 4,
                        Byte_4 = 6,
                        Byte_5 = 5,
                        Byte_d = 252,
                        Byte_f = 124,
                    },
                    null,
                    null,
                    null,
                    null,
                    null,
                },
                IdField = new TextureId() { Id = 35 },
            };

        private static MaterialProperties CreateMaterialProperties() =>
            new MaterialProperties() {
                // values copied from model 170 (Material @ 0x0108)
                AlphaBpp = 1,
                Word_4 = 2,
                Ints_6 = new int[] {
                    0x011F041F, // = 18809887
                    0x07070704, // = 117901060
                },
                Ints_e = new int[] {
                    0x001F031F, // = 2032415,
                    0x07070704, // = 117901060
                },
                Bitmask1 = unchecked((int)0xC8000000), // = -939524096
                Bitmask2 = 0x00112078, // = 1122424
                Byte_22 = byte.MaxValue,
                Byte_23 = byte.MaxValue,
                Byte_24 = byte.MaxValue,
                Byte_25 = byte.MaxValue,
            };
    }
}
