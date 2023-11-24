// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Meshes;

namespace SWE1R.Assets.Blocks.CommandLine.MaterialExamples
{
    public class Model_130_MaterialExample
    {
        public static Material CreateMaterial() =>
            new Material() {
                // values copied from model 130 (Material @ 0x05a574) (from icicles)
                Int = 6,
                Texture = CreateMaterialTexture(),
                Properties = CreateMaterialProperties(),
            };

        private static MaterialTexture CreateMaterialTexture() =>
            new MaterialTexture() {
                // values copied from Model 130, MaterialTexture @ 0x01c2e4
                Mask_Unk = 0,
                Width4 = 128,
                Height4 = 128,
                Byte_0c = 0,
                Byte_0d = 3,
                Width = 32,
                Height = 32,
                Width_Unk = 16384,
                Height_Unk = 16384,
                Flags = 128,
                Mask = 1023,
                Children = new MaterialTextureChild[] {
                    new MaterialTextureChild() {
                        Byte_2 = 8,
                        DimensionsBitmask = DimensionsBitmask.FlippedVertically | DimensionsBitmask.FlippedHorizontally,
                        Byte_4 = 5,
                        Byte_5 = 5,
                        Byte_d = 124,
                        Byte_f = 124,
                    },
                    null,
                    null,
                    null,
                    null,
                    null,
                },
                TextureIndex = 923,
            };

        private static MaterialProperties CreateMaterialProperties() =>
            new MaterialProperties() {
                // values copied from model 130 (Material @ 0x05a574) (from icicles)
                AlphaBpp = 0,
                Word_4 = 2,
                Ints_6 = new int[] {
                    0x011F041F, // = 18809887
                    0x07070701, // = 117901057
                },
                Ints_e = new int[] {
                    0x1F1F1F00, // = 522133248,
                    0x07070700, // = 117901056
                },
                Bitmask1 = unchecked((int)0xC8000000), // = 201859080
                Bitmask2 = 0x001049D8, // = 1067480
            };
    }
}
