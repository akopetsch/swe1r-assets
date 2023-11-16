// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
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
        private const string _textureBlockFilename = "out_textureblock.bin";
        private const string _modelBlockFilename = "out_modelblock.bin";

        public TextureImporter()
        {
            //Change_142_ModelSkyboxTexture();            
            Change_130_IcicleTexture();
        }

        private void Change_130_IcicleTexture()
        {
            short w = 1024;
            short h = 1024;
            string imageFilename = $"TestTexture_{w}x{h}_I8.png";
            Texture texture = GetRgba32Texture(imageFilename);

            var textureBlock = Block.Load<Texture>(_textureBlockFilename);
            texture.Block = textureBlock;
            textureBlock[923] = texture;
            textureBlock.Add(texture);
            textureBlock.Save(_textureBlockFilename);

            var modelBlock = Block.Load<Model>(_modelBlockFilename);
            Model model = modelBlock[130]; // 130 = BeedosWildRide
            model.Load(out ByteSerializerContext byteSerializerContext);

            var mt = (MaterialTexture)
                byteSerializerContext.Graph.GetValueComponent(typeof(MaterialTexture), 0x1c2e4).Value;

            mt.Width = w;
            mt.Height = h;
            mt.Width4 = (short)(w * 4);
            mt.Height4 = (short)(h * 4);
            mt.Width_Unk = 32768; // 32768 = 128 * 256
            mt.Width_Unk = 32768; // 32768 = 128 * 256
            //mt.IdField.Id = texture.Index.Value;

            Debug.Assert(mt.Flags == 128);
            Debug.Assert(mt.Mask == 1023);
            Debug.Assert(mt.Word_0e == 0);
            Debug.Assert(mt.Mask_Unk == 0);

            model.Save();
            modelBlock.Save(_modelBlockFilename);
        }

        private void Change_142_ModelSkyboxTexture()
        {
            short w = 2048;
            short h = 2048;
            string imageFilename = $"TestTexture_{w}x{h}_I8.png";
            Texture texture = GetRgba5551IndexedTexture(imageFilename);
            AddTexture(texture);

            var modelBlock = Block.Load<Model>(_modelBlockFilename);
            Model model = modelBlock[142]; // 142 = MonGazza_Speedway
            model.Load();

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
                mt.IdField.Id = texture.Index.Value;
                Debug.Assert(mt.Flags == 512);
                Debug.Assert(mt.Mask == 1023);
                Debug.Assert(mt.Word_0e == 0);
                Debug.Assert(mt.Mask_Unk == 1);
            }

            model.Save();
            modelBlock.Save(_modelBlockFilename);
        }

        private void AddTexture(Texture texture)
        {
            var textureBlock = Block.Load<Texture>(_textureBlockFilename);
            texture.Block = textureBlock;
            textureBlock.Add(texture);
            textureBlock.Save(_textureBlockFilename);
        }

        #region Methods

        private Texture GetRgba32Texture(string imageFilename)
        {
            var texture = new Texture();
            using var image = (Bitmap)Image.FromFile(imageFilename);
            
            // pixels
            int w = image.Width;
            int h = image.Height;
            byte[] pixels = new byte[w * h * sizeof(int)];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    //int i = x * h + y;
                    int i = y * w + x;
                    Color color = image.GetPixel(x, y);
                    byte[] bytes = ToColorRgba32(color);
                    Array.Copy(bytes, 0, pixels, i * sizeof(int), bytes.Length);
                }
            }
            texture.PixelsPart.Bytes = pixels;

            // palette
            texture.PalettePart.Bytes = new byte[] { };

            return texture;
        }

        private Texture GetRgba5551IndexedTexture(string imageFilename)
        {
            var texture = new Texture();
            using var image = (Bitmap)Image.FromFile(imageFilename);

            // indices
            byte[] indices = GetIndices(image);
            texture.PixelsPart.Bytes = indices;

            // palette
            byte[] palette = GetPalette(image)
                .Select(x => x.ToRgba5551().SwapBytes())
                .SelectMany(BitConverter.GetBytes)
                .ToArray();
            byte[] palette512 = new byte[512];
            Array.Copy(palette, palette512, palette.Length);
            texture.PalettePart.Bytes = palette512;

            return texture;
        }

        #endregion

        #region Methods (bitmap indices/palette)

        private byte[] GetIndices(Bitmap bitmap)
        {
            int w = bitmap.Width;
            int h = bitmap.Height;
            var indices = new byte[w * h];
            Color[] palette = bitmap.Palette.Entries.ToArray();

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    byte index = (byte)Array.IndexOf(palette, color);
                    Debug.Assert(index >= 0);
                    //int i = x * h + y;
                    int i = y * w + x;
                    indices[i] = index;
                }
            }
            return indices;
        }

        private ColorArgbF[] GetPalette(Bitmap bitmap) =>
            bitmap.Palette.Entries.Select(ToColorArgbF).ToArray();

        #endregion

        #region Methods (bitmap colors)

        private byte[] ToColorRgba32(Color color) =>
            new byte[] { color.R, color.G, color.B, color.A };

        private ColorArgbF ToColorArgbF(Color color)
        {
            var a = (float)color.A / byte.MaxValue;
            var r = (float)color.R / byte.MaxValue;
            var g = (float)color.G / byte.MaxValue;
            var b = (float)color.B / byte.MaxValue;
            return new ColorArgbF(a, r, g, b);
        }

        #endregion
    }
}
