// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Meshes;

namespace SWE1R.Assets.Blocks.CommandLine.MaterialExamples
{
    public static class Model_142_MaterialExample
    {
        public static Material CreateMaterial() =>
            new Material() {
                // values copied from model 142 (Material @ 0x016b98) (from skybox)
                Int = 12,
                Texture = CreateMaterialTexture(),
                Properties = CreateMaterialProperties(),
            };

        private static MaterialTexture CreateMaterialTexture() =>
            new MaterialTexture() {
                // values copied from Model 142, MaterialTexture @ 0x016ba8
                Mask_Unk = 1,
                Width4 = 128,
                Height4 = 256,
                Byte_0c = 2,
                Byte_0d = 1,
                Width = 32,
                Height = 64,
                Width_Unk = 16384,
                Height_Unk = 32768,
                Flags = 512,
                Mask = 1023,
                Children = new MaterialTextureChild[] {
                    new MaterialTextureChild() {
                        Byte_2 = 4,
                        DimensionsBitmask = DimensionsBitmask.FlippedVertically | DimensionsBitmask.FlippedHorizontally,
                        Byte_4 = 5,
                        Byte_5 = 6,
                        Byte_d = 124,
                        Byte_f = 252,
                    },
                    null,
                    null,
                    null,
                    null,
                    null,
                },
                IdField = new TextureId() { Id = 1364 },
            };

        private static MaterialProperties CreateMaterialProperties() =>
            new MaterialProperties() {
                // values copied from model 142 (Material @ 0x016b98)
                AlphaBpp = 0,
                Word_4 = 1,
                Ints_6 = new int[] {
                    0x011F041F, // = 18809887
                    0x07070704, // = 117901060
                },
                Ints_e = new int[] {
                    0x011f041f, // = 18809887,
                    0x07070704, // = 117901060
                },
                Bitmask1 = 0x0c082008, // = 201859080
                Bitmask2 = 0x03022008, // = 50470920
            };
    }
}
