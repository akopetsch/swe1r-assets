// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Common.Colors;
using SWE1R.Assets.Blocks.Common.Images;
using SWE1R.Assets.Blocks.TextureBlock;
using System;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    /// <summary>
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L241">SWR_MODEL_Section5</see>
    /// </summary>
    [DebuggerDisplay("Id = {" + nameof(IdField) + ",nq}")]
    [Sizeof(0x40)]
    public class MaterialTexture
    {
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
        [Order(3)] public short Always0_08 { get; set; }
        /// <summary>
        /// Always zero.
        /// </summary>
        [Order(4)] public short Always0_0a { get; set; }
        [Order(5)] public byte Byte_0c { get; set; }
        [Order(6)] public byte Byte_0d { get; set; }
        [Order(7)] public short Word_0e { get; set; }
        [Order(8)] public short Width { get; set; }
        [Order(9)] public short Height { get; set; }
        /// <summary>
        /// Always 128, 256 or 512 times <see cref="Width">Width</see>.
        /// </summary>
        [Order(10)] public ushort Width_Unk { get; set; }
        /// <summary>
        /// Always 128, 256 or 512 times <see cref="Height">Height</see>.
        /// </summary>
        [Order(11)] public ushort Height_Unk { get; set; }
        [Order(12)] public short Flags { get; set; }
        [Order(13)] public short Mask { get; set; }

        [Length(6)]
        [ElementReference]
        [Order(14)] public MaterialTextureChild[] Children { get; set; }

        [Offset(0x38)]
        [Order(15)] public TextureId IdField { get; set; }

        #endregion

        #region Methods (pixels)

        public ImageRgba32 ExportImage(Block<Texture> textureBlock)
        {
            var result = new ImageRgba32(Width, Height);

            // get texture
            int textureIndex = IdField.Id;
            if (textureIndex == -1)
                return null;
            Texture texture = textureBlock[textureIndex];
            texture.Load();

            // get pixels
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    result[x, y] = GetPixel(x, y, texture);

            return result;
        }

        public ImageRgba32 ExportEffectiveImage(Block<Texture> textureBlock, MaterialTextureChild child)
        {
            ImageRgba32 result = ExportImage(textureBlock);
            if (child != null)
                result = result.Mirror(child.HasDoubleWidth, child.HasDoubleHeight);
            return result;
        }

        // https://github.com/Olganix/Sw_Racer/issues/9
        private TextureDataFormat GetTextureDataFormat()
        {
            if (Byte_0c == 0)
            {
                if (Byte_0d == 3)
                    return TextureDataFormat.RGBA32;
            }
            if (Byte_0c == 2)
            {
                if (Byte_0d == 0)
                    return TextureDataFormat.I4_RGBA5551;
                if (Byte_0d == 1)
                    return TextureDataFormat.I8_RGBA5551;
            }
            if (Byte_0c == 4)
            {
                if (Byte_0d == 0)
                    return TextureDataFormat.FourBitGrayscaleAndAlpha;
                if (Byte_0d == 1)
                    return TextureDataFormat.EightBitGrayscale;
            }
            throw new NotImplementedException();
        }

        private ColorRgba32 GetPixel(int x, int y, Texture texture)
        {
            int i = y * Width + x;
            TextureDataFormat textureDataFormat = GetTextureDataFormat();
            int pixelData;
            int bpp = textureDataFormat.GetBpp();
            if (textureDataFormat.IsIndexFormat())
            {
                // get pixel data
                if (bpp == 4)
                {
                    if (i < texture.PixelsPart.NibblesCount)
                        pixelData = texture.PixelsPart.GetNibble(i);
                    else
                        return ColorRgba32.Pink;
                }
                else if (bpp == 8)
                    pixelData = texture.PixelsPart.GetByte(i);
                else
                    throw new InvalidOperationException();

                // get index palette color
                return (ColorRgba32)texture.PaletteColors[pixelData];
            }
            else
            {
                // get pixel data
                if (bpp == 4)
                    pixelData = texture.PixelsPart.GetNibble(i);
                else if (bpp == 8)
                    pixelData = texture.PixelsPart.GetByte(i);
                else if (bpp == 32)
                    pixelData = texture.PixelsPart.GetInt32(i);
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
