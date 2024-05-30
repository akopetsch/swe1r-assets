﻿// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using SWE1R.Assets.Blocks.Colors;
using SWE1R.Assets.Blocks.Images;
using System;

namespace SWE1R.Assets.Blocks.Textures.Export
{
    public class TextureExporter
    {
        #region Properties (input)

        public byte[] PixelsBytes { get; set; }
        public TextureFormat TextureFormat { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public ColorRgba5551[] Palette { get; set; }

        #endregion

        #region Properties (output)

        public ImageRgba32 Image { get; set; }

        #endregion

        #region Constructor

        public TextureExporter(
            byte[] pixelsBytes, TextureFormat textureFormat, int width, int heigth, ColorRgba5551[] palette)
        {
            PixelsBytes = pixelsBytes;
            TextureFormat = textureFormat;
            Width = width;
            Height = heigth;
            Palette = palette;
        }

        #endregion

        #region Methods

        public void Export()
        {
            Image = new ImageRgba32(Width, Height);
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    Image[x, y] = GetPixel(x, y);
        }

        private ColorRgba32 GetPixel(int x, int y)
        {
            int pixelIndex = GetPixelIndex(x, y);
            if (TextureFormat == TextureFormat.RGBA5551_I4)
            {
                if (pixelIndex < NibbleHelper.GetNibblesCount(PixelsBytes))
                {
                    int paletteIndex = NibbleHelper.GetNibble(PixelsBytes, pixelIndex);
                    return (ColorRgba32)Palette[paletteIndex];
                }
                else
                    return ColorRgba32.Pink;
            }
            else if (TextureFormat == TextureFormat.RGBA5551_I8)
            {
                int paletteIndex = PixelsBytes[pixelIndex];
                return (ColorRgba32)Palette[paletteIndex];
            }
            else if (TextureFormat == TextureFormat.FourBitGrayscaleAndAlpha)
            {
                byte pixelData = NibbleHelper.GetNibble(PixelsBytes, pixelIndex);
                byte v = (byte)Math.Round(pixelData * 17f); // value (as in HSV)
                byte a = (byte)Math.Round(pixelData * 17f); // alpha (as in ARGB)
                return new ColorRgba32(v, v, v, a);
            }
            else if (TextureFormat == TextureFormat.EightBitGrayscale)
            {
                byte v = PixelsBytes[pixelIndex]; // value (as in HSV)
                return new ColorRgba32(v, v, v, v);
            }
            else if (TextureFormat == TextureFormat.RGBA32)
            {
                int offset = pixelIndex * sizeof(int);
                byte r = PixelsBytes[offset + 0];
                byte g = PixelsBytes[offset + 1];
                byte b = PixelsBytes[offset + 2];
                byte a = PixelsBytes[offset + 3];
                return new ColorRgba32(r, g, b, a);
            }
            throw new InvalidOperationException();
        }

        private int GetPixelIndex(int x, int y) =>
            y * GetVirtualWidth() + x;

        protected virtual int GetVirtualWidth() => // TODO: consider different name than 'VirtualWidth'
            Width;
        
        #endregion
    }
}