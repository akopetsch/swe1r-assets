// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.CommandLine.MaterialExamples;
using SWE1R.Assets.Blocks.Common.Colors;
using SWE1R.Assets.Blocks.Common.Images;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Diagnostics;
using Material = SWE1R.Assets.Blocks.ModelBlock.Meshes.Material;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public class MaterialImporter
    {
        #region Properties

        public ImageRgba32 Image { get; }
        public Block<Texture> TextureBlock { get; }

        public Material Material { get; private set; }
        public Texture Texture { get; private set; }

        #endregion

        #region Constructor

        public MaterialImporter(ImageRgba32 image, Block<Texture> textureBlock)
        {
            Image = image;
            TextureBlock = textureBlock;
        }

        #endregion

        #region Methods (import)

        public void Import()
        {
            if (Image.Palette?.Length > 0)
            {
                Texture = GetRgba5551IndexedTexture();
                Texture.Block = TextureBlock;
                TextureBlock.Add(Texture);

                Material = CreateRgba5551Material();
            }
            else
            {
                Texture = GetRgba32Texture();
                Texture.Block = TextureBlock;
                TextureBlock.Add(Texture);

                Material = Model_130_MaterialExample.CreateMaterial();
                MaterialTexture mt = Material.Texture;
                mt.Width = (short)Image.Width;
                mt.Height = (short)Image.Height;
                mt.Width4 = (short)(Image.Width * 4);
                mt.Height4 = (short)(Image.Height * 4);
                mt.Width_Unk = 32768; // 32768 = 64 * 512
                mt.Height_Unk = 32768; // 32768 = 64 * 512
                // TODO: !!! Width_Unk / Height_Unk
                mt.IdField.Id = Texture.Index.Value;
            }
        }

        #endregion

        #region Methods (Texture)

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

        #region Methods (Material)

        private Material CreateRgba5551Material() =>
            new Material() {
                Int = 12,
                Texture = CreateMaterialTexture(),
                Properties = CreateMaterialProperties(),
            };

        private MaterialTexture CreateMaterialTexture() =>
            new MaterialTexture() {
                Mask_Unk = 1,
                Width4 = (short)(Image.Width * 4),
                Height4 = (short)(Image.Height * 4),
                Byte_0c = 2,
                Byte_0d = 1,
                Width = (short)(Image.Width),
                Height = (short)(Image.Height),
                Width_Unk = 32768, // 32768 = 64 * 512
                Height_Unk = 32768, // 32768 = 64 * 512
                // TODO: !!! Width_Unk / Height_Unk
                Flags = 512, // TODO: or 256?
                Mask = 1023,
                Children = new MaterialTextureChild[] {
                    CreateMaterialTextureChild(),
                    null,
                    null,
                    null,
                    null,
                    null,
                },
                IdField = new TextureId() { Id = Texture.Index.Value }
            };

        private MaterialTextureChild CreateMaterialTextureChild() =>
            new MaterialTextureChild() {
                Byte_2 = 8, // or 4
                DimensionsBitmask = 0, // or 0x34
                Byte_4 = 6, // or 5
                Byte_5 = 6, // or 5
                Byte_d = 252, // or 124
                Byte_f = 252, // or 124
            };

        private MaterialProperties CreateMaterialProperties() =>
            new MaterialProperties() {
                Word_4 = 1,
                Ints_6 = new int[] {
                    0x11f041f,
                    0x7070704
                },
                Ints_e = new int[] {
                    0x11f041f,
                    0x7070704
                },
                // bitmasks to make it opaque:
                Bitmask1 = unchecked((int)0xC8000000),
                Bitmask2 = 0x0112038, // or e.g. 0x00112078
            };

        #endregion
    }
}
