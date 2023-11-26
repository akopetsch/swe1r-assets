﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Colors;
using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.TextureBlock;
using SWE1R.Assets.Blocks.Textures;
using System;
using System.Diagnostics;
using System.Numerics;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials
{
    /// <summary>
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L241">SWR_MODEL_Section5</see>
    /// </summary>
    [DebuggerDisplay("Id = {" + nameof(TextureIndex) + ",nq}")]
    [Sizeof(0x40)]
    public class MaterialTexture
    {
        #region Constants

        public const int ChildrenCount = 6;

        #endregion

        #region Properties (serialized)

        [Order(0)] public int Mask_Unk { get; set; }
        /// <summary>
        /// Always four times <see cref="Width">Width</see>.
        /// </summary>
        [Order(1)] public short Width4 { get; set; }
        /// <summary>
        /// Always four times <see cref="Height">Height</see>.
        /// </summary>
        [Order(2)] public short Height4 { get; set; }
        /// <summary>
        /// Always zero.
        /// </summary>
        [Order(3)] public short Always0_08 { get; set; } = 0;
        /// <summary>
        /// Always zero.
        /// </summary>
        [Order(4)] public short Always0_0a { get; set; } = 0;
        [Order(5)] public TextureFormat TextureFormat { get; set; }
        [Order(6)] public short Word_0e { get; set; }
        [Order(7)] public short Width { get; set; }
        [Order(8)] public short Height { get; set; }
        /// <summary>
        /// Always 128, 256 or 512 times <see cref="Width">Width</see>.
        /// </summary>
        [Order(9)] public ushort Width_Unk { get; set; }
        /// <summary>
        /// Always 128, 256 or 512 times <see cref="Height">Height</see>.
        /// </summary>
        [Order(10)] public ushort Height_Unk { get; set; }
        [Order(11)] public short Flags { get; set; }
        [Order(12)] public short Mask { get; set; }

        [Length(ChildrenCount)]
        [ElementReference]
        [Order(13)] public MaterialTextureChild[] Children { get; set; }

        [Offset(0x38)]
        [Order(14)] public TextureIndex TextureIndex { get; set; }

        #endregion

        #region Properties (helper)

        public static Vector2 MaxSize { get; } = new Vector2(short.MaxValue, short.MaxValue);
        public static Vector2 MaxSize4 { get; } = new Vector2(short.MaxValue, short.MaxValue);
        public static Vector2 MaxSizeUnk { get; } = new Vector2(ushort.MaxValue, ushort.MaxValue);

        #endregion

        #region Constructor

        public MaterialTexture() =>
            Children = new MaterialTextureChild[ChildrenCount];

        #endregion

        #region Methods (pixels)

        public ImageRgba32 ExportImage(Block<TextureBlockItem> textureBlock)
        {
            var result = new ImageRgba32(Width, Height);

            // get texture
            int textureIndex = TextureIndex.Id;
            if (textureIndex == -1)
                return null;
            TextureBlockItem textureBlockItem = textureBlock[textureIndex];
            textureBlockItem.Load();

            // get pixels
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    result[x, y] = GetPixel(x, y, textureBlockItem);

            return result;
        }

        public ImageRgba32 ExportEffectiveImage(Block<TextureBlockItem> textureBlock, MaterialTextureChild child)
        {
            ImageRgba32 result = ExportImage(textureBlock);
            if (child != null)
                result = result.Mirror(child.HasDoubleWidth, child.HasDoubleHeight);
            return result;
        }

        private ColorRgba32 GetPixel(int x, int y, TextureBlockItem textureBlockItem)
        {
            int i = y * Width + x;
            int pixelData;
            int bpp = TextureFormat.GetBpp();
            if (TextureFormat.HasPalette())
            {
                // get pixel data
                if (bpp == 4)
                    if (i < textureBlockItem.PixelsPart.NibblesCount)
                        pixelData = textureBlockItem.PixelsPart.GetNibble(i);
                    else
                        return ColorRgba32.Pink;
                else if (bpp == 8)
                    pixelData = textureBlockItem.PixelsPart.GetByte(i);
                else
                    throw new InvalidOperationException();

                // get index palette color
                return (ColorRgba32)textureBlockItem.PaletteColors[pixelData];
            }
            else
            {
                // get pixel data
                if (bpp == 4)
                    pixelData = textureBlockItem.PixelsPart.GetNibble(i);
                else if (bpp == 8)
                    pixelData = textureBlockItem.PixelsPart.GetByte(i);
                else if (bpp == 32)
                    pixelData = textureBlockItem.PixelsPart.GetInt32(i);
                else
                    throw new InvalidOperationException();

                // get color
                if (bpp == 4)
                {
                    byte a = (byte)Math.Round(pixelData * 17f); // alpha (as in ARGB)
                    byte v = (byte)Math.Round(pixelData * 16f); // value (as in HSV)
                    return new ColorRgba32(v, v, v, a);
                }
                else if (bpp == 8)
                {
                    byte v = (byte)pixelData; // value (as in HSV)
                    return new ColorRgba32(v, v, v, v);

                }
                else if (bpp == 32)
                {
                    byte[] bytes = BitConverter.GetBytes(pixelData);
                    return new ColorRgba32(bytes);
                }
                else
                    throw new InvalidOperationException();
            }
        }

        #endregion
    }
}
