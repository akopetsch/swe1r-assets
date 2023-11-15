// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO.Extensions;
using SWE1R.Assets.Blocks.Common.Colors;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Diagnostics;
using System.Drawing;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public class TextureImporter
    {
        public TextureImporter()
        {
            var modelBlock = Block.Load<Model>("out_modelblock.bin");
            Model model = modelBlock[142]; // 142 = MonGazza_Speedway
            model.Load();

            int textureIndex = 1648;
            short w = 2048;
            short h = 2048;

            var skybox = (TransformableD065)model.Header.Nodes[2].FlaggedNode;
            var materials = skybox.GetDescendants().OfType<MeshGroup3064>()
                .SelectMany(mg => mg.Meshes)
                .Select(m => m.Material).ToList();
            foreach (Material material in materials)
            {
                Debug.Assert(material.Width_Unk_Dividend == 0);
                Debug.Assert(material.Height_Unk_Dividend == 0);

                MaterialTexture mt = material.Texture;
                Debug.Assert(mt != null);

                mt.Width = w;
                mt.Height = h;
                mt.Width4 = (short)(w * 4);
                mt.Height4 = (short)(h * 4);
                mt.Width_Unk = 32768; // 32768 = 128 * 256
                mt.Width_Unk = 32768; // 32768 = 128 * 256
                mt.IdField.Id = textureIndex;
            }

            model.Save();
            modelBlock.Save("out_modelblock.bin");

            string pngFilename = $"TestTexture_{w}x{h}_I8.png";
            using var png = (Bitmap)Image.FromFile(pngFilename);

            var texture = new Texture();

            byte[] indices = GetIndices(png);
            byte[] palette = GetPalette(png)
                .Select(ToColorArgbF)
                .Select(x => x.ToRgba5551().SwapBytes())
                .SelectMany(GetBytes)
                .ToArray();
            byte[] palette512 = new byte[512];
            Array.Copy(palette, palette512, palette.Length);

            texture.PixelsPart.Bytes = indices;
            texture.PalettePart.Bytes = palette512;

            var textureBlock = Block.Load<Texture>("out_textureblock.bin");

            texture.Block = textureBlock;

            // textureIndex will be the last
            if (textureBlock.Count >= textureIndex)
                textureBlock[textureIndex] = texture;
            else
                textureBlock.Add(texture);

            textureBlock.Save("out_textureblock.bin");
        }

        private byte[] GetBytes(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Debug.Assert(bytes.Length == 2);
            return bytes; // Reverse().ToArray()
        }

        private ColorArgbF ToColorArgbF(Color color)
        {
            var a = (float)color.A / byte.MaxValue;
            var r = (float)color.R / byte.MaxValue;
            var g = (float)color.G / byte.MaxValue;
            var b = (float)color.B / byte.MaxValue;
            return new ColorArgbF(a, r, g, b);
        }

        private byte[] GetIndices(Bitmap bitmap)
        {
            var indices = new byte[bitmap.Width * bitmap.Height];
            Color[] palette = bitmap.Palette.Entries.ToArray();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    byte index = (byte)Array.IndexOf(palette, color);
                    Debug.Assert(index >= 0);
                    //int i = y * bitmap.Width + x;
                    int i = x * bitmap.Height + y;
                    indices[i] = index;
                }
            }
            return indices;
        }

        private Color[] GetPalette(Bitmap bitmap) =>
            bitmap.Palette.Entries.ToArray();
    }
}
