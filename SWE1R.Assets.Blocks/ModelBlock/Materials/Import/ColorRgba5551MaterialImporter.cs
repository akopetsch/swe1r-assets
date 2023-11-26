// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Common.Colors;
using SWE1R.Assets.Blocks.Common.Images;
using SWE1R.Assets.Blocks.Common.Textures;
using SWE1R.Assets.Blocks.TextureBlock;
using System;
using System.Diagnostics;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials.Import
{
    public class ColorRgba5551MaterialImporter : MaterialImporter
    {
        #region Constructor

        public ColorRgba5551MaterialImporter(ImageRgba32 image, Block<TextureBlockItem> textureBlock) :
            base(image, textureBlock)
        { }

        #endregion

        #region Methods

        protected override TextureBlockItem CreateTextureBlockItem()
        {
            var textureBlockItem = new TextureBlockItem();

            int w = Image.Width;
            int h = Image.Height;

            // indices
            var indices = new byte[w * h];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    int index = Image.GetPaletteIndex(x, y);
                    Debug.Assert(index >= 0);
                    //int i = x * h + y;
                    int i = y * w + x;
                    indices[i] = (byte)index;
                }
            }
            textureBlockItem.PixelsPart.Bytes = indices;

            // palette
            byte[] palette = Image.Palette
                .Select(c => (ColorRgba5551)c)
                .SelectMany(c => c.Bytes.Reverse()) // TODO: use EndianBinaryWrite
                .ToArray();
            byte[] palette512 = new byte[512];
            Array.Copy(palette, palette512, palette.Length);
            textureBlockItem.PalettePart.Bytes = palette512;

            return textureBlockItem;
        }

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
            mt.TextureFormat = TextureFormat.I8_RGBA5551;
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
