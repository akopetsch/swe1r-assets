// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.CommandLine.MaterialExamples;
using SWE1R.Assets.Blocks.Common.Colors;
using SWE1R.Assets.Blocks.Common.Images;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.TextureBlock;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public class ColorRgba32MaterialImporter : MaterialImporter
    {
        #region Constructor

        public ColorRgba32MaterialImporter(ImageRgba32 image, Block<Texture> textureBlock) : 
            base(image, textureBlock)
        { }

        #endregion

        #region Methods

        protected override Texture CreateTexture()
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

        protected override Material CreateMaterial()
        {
            var material = Model_130_MaterialExample.CreateMaterial();
            MaterialTexture mt = Material.Texture;
            mt.Width = (short)Image.Width;
            mt.Height = (short)Image.Height;
            mt.Width4 = (short)(Image.Width * 4);
            mt.Height4 = (short)(Image.Height * 4);
            mt.Width_Unk = 32768; // 32768 = 64 * 512
            mt.Height_Unk = 32768; // 32768 = 64 * 512
                                   // TODO: !!! Width_Unk / Height_Unk
            mt.IdField.Id = Texture.Index.Value;
            return material;
        }

        #endregion
    }
}
