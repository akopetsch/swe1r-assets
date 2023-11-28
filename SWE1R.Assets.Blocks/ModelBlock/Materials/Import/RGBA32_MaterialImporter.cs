﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.TextureBlock;
using SWE1R.Assets.Blocks.Textures;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials.Import
{
    public class RGBA32_MaterialImporter : MaterialImporter
    {
        #region Constructor

        public RGBA32_MaterialImporter(ImageRgba32 image, Block<TextureBlockItem> textureBlock) : 
            base(image, TextureFormat.RGBA32, textureBlock)
        { }

        #endregion

        #region Methods

        // mt_w: 16, 32, 64
        // mt_h: 16, 32

        protected override Material CreateMaterial()
        {
            var material = base.CreateMaterial();
            material.Int = 12; // 4, 6, 12, 14
            return material;
        }

        protected override MaterialTexture CreateMaterialTexture()
        {
            var mt = base.CreateMaterialTexture();
            mt.Mask_Unk = 0; // 0
            mt.Format = TextureFormat.RGBA32;
            mt.Flags = 256; // 64, 128, 256 // TODO: 512?
            mt.Mask = 1023; // 511, 1023
            return mt;
        }

        protected override MaterialTextureChild CreateMaterialTextureChild() =>
            new MaterialTextureChild() {
                Byte_2 = 8, // 4, 8, 16
                DimensionsBitmask = 0x00, // 0x00, 0x02, 0x10, 0x11, 0x22
                Byte_4 = 6, // 4, 5, 6
                Byte_5 = 6, // 4, 5
                Byte_d = 252, // 60, 124, 252
                Byte_f = 252, // 60, 124
            };

        protected override MaterialProperties CreateMaterialProperties() =>
            new MaterialProperties() {
                Word_4 = 2, // 2
                Ints_6 = new int[] {
                    0x011f041f, // 0x011f031f, 0x011f041f, 0x1f1f1f01, 0x1f1f1f03, 0x031f011f
                    0x07070701, // 0x01070307, 0x03070107, 0x07070701
                },
                Ints_e = new int[] {
                    0x1f1f1f00, // 0x1f1f1f00
                    0x07070700, // 0x00070407, 0x07070700
                },
                // bitmasks to make it opaque:
                Bitmask1 = unchecked((int)0xC8000000), // 0
                Bitmask2 = 0x0112038, // 0
                // Bitmask2 = 0x00112078
                Byte_22 = 0, // 0, 255
                Byte_23 = 0, // 0, 255
                Byte_24 = 0, // 0, 255
                Byte_25 = 0, // 0, 64, 255
                Byte_2e = 0, // 0, 128
                Byte_2f = 0, // 0, 128
                Byte_30 = 0, // 0, 128
                Byte_31 = 0, // 0, 16
            };

        #endregion
    }
}
