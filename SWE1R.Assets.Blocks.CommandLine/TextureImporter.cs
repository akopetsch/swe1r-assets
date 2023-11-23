// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.CommandLine.MaterialExamples;
using SWE1R.Assets.Blocks.Common.Colors;
using SWE1R.Assets.Blocks.Common.Images;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public class TextureImporter
    {
        #region Properties

        public ImageRgba32 Image { get; }

        public Material Material { get; private set; }
        public Texture Texture { get; private set; }

        #endregion

        #region Constructor

        public TextureImporter(ImageRgba32 image)
        {
            Image = image;
        }

        #endregion

        #region Methods

        public void Import()
        {
            if (Image.Palette?.Length > 0)
            {
                Texture = GetRgba5551IndexedTexture();
                Material = Model_142_MaterialExample.CreateMaterial();
            }
            else
            {
                Texture = GetRgba32Texture();
                Material = Model_130_MaterialExample.CreateMaterial();
            }
        }

        private Texture GetRgba32Texture()
        {
            var texture = new Texture();

            int w = Image.Width;
            int h = Image.Height;

            // pixels
            byte[] pixels = new byte[w * h * ColorRgba32.StructureSize];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    //int i = x * h + y;
                    int i = y * w + x;
                    byte[] bytes = Image[x, y].Bytes.Reverse().ToArray(); // TODO: use EndianBinaryWrite
                    Array.Copy(bytes, 0, pixels, i * bytes.Length, bytes.Length);
                }
            }
            texture.PixelsPart.Bytes = pixels;

            // palette
            texture.PalettePart.Bytes = new byte[] { };

            return texture;
        }

        private Texture GetRgba5551IndexedTexture()
        {
            var texture = new Texture();

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
            texture.PixelsPart.Bytes = indices;

            // palette
            byte[] palette = Image.Palette
                .Select(c => (ColorRgba5551)c)
                .SelectMany(c => c.Bytes.Reverse()) // TODO: use EndianBinaryWrite
                .ToArray();
            byte[] palette512 = new byte[512];
            Array.Copy(palette, palette512, palette.Length);
            texture.PalettePart.Bytes = palette512;

            return texture;
        }

        #endregion
    }
}
