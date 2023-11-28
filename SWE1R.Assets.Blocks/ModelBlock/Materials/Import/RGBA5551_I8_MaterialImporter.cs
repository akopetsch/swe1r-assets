// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.TextureBlock;
using SWE1R.Assets.Blocks.Textures;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials.Import
{
    public class RGBA5551_I8_MaterialImporter : MaterialImporter
    {
        #region Constructor

        public RGBA5551_I8_MaterialImporter(ImageRgba32 image, Block<TextureBlockItem> textureBlock) :
            base(image, TextureFormat.RGBA5551_I8, textureBlock)
        { }

        #endregion

        #region Methods

        // mt_w: 4, 16, 32, 64
        // mt_h: 4, 16, 32, 64

        protected override Material CreateMaterial()
        {
            var material = base.CreateMaterial();
            material.Int = 12;
            return material;
        }

        protected override MaterialTexture CreateMaterialTexture()
        {
            var mt = base.CreateMaterialTexture();
            mt.Mask_Unk = 1; // 1
            mt.Format = TextureFormat.RGBA5551_I8;
            mt.Flags = 512; // 256, 512, 1024, 2048
            mt.Mask = 1023; // 7, 127, 1023
            return mt;
        }

        protected override MaterialTextureChild CreateMaterialTextureChild() =>
            new MaterialTextureChild() {
                Byte_2 = 8, // 1, 2, 4, 8
                DimensionsBitmask = 0x00, // 0x00, 0x22
                Byte_4 = 6, // 2, 4, 5, 6
                Byte_5 = 6, // 2, 4, 5, 6
                Byte_d = 252, // 12, 60, 124, 252
                Byte_f = 252, // 12, 60, 124, 252
            };

        protected override MaterialProperties CreateMaterialProperties() =>
            new MaterialProperties() {
                Word_4 = 1, // 1
                Ints_6 = new int[] {
                    0x11f041f, // 0x011f041f
                    0x7070704, // 0x07070704
                },
                Ints_e = new int[] {
                    0x11f041f, // 0x011f041f
                    0x7070704, // 0x07070704
                },
                // bitmasks to make it opaque:
                Bitmask1 = unchecked((int)0xC8000000), // 0
                Bitmask2 = 0x0112038, // 0
                // Bitmask2 = 0x00112078
            };

        #endregion
    }
}
