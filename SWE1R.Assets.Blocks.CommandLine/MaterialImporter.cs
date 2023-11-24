// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Common.Images;
using SWE1R.Assets.Blocks.Common.Vectors;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Numerics;
using Material = SWE1R.Assets.Blocks.ModelBlock.Meshes.Material;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public abstract class MaterialImporter
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
            Texture = CreateTexture();
            Texture.Block = TextureBlock;
            TextureBlock.Add(Texture);

            Material = CreateMaterial();
        }

        #endregion

        #region Methods (Texture)

        protected abstract Texture CreateTexture();

        #endregion

        #region Methods (Material)

        protected virtual Material CreateMaterial() =>
            new Material() {
                Int = 12,
                Texture = CreateMaterialTexture(),
                Properties = CreateMaterialProperties(),
            };

        protected virtual MaterialTexture CreateMaterialTexture()
        {
            var mt = new MaterialTexture();
            mt.Mask_Unk = 1;
            mt.Width4 = (short)(Image.Width * 4);
            mt.Height4 = (short)(Image.Height * 4);
            mt.Byte_0c = 2;
            mt.Byte_0d = 1;
            mt.Width = (short)(Image.Width);
            mt.Height = (short)(Image.Height);
            (mt.Width_Unk, mt.Height_Unk) = CalculateWidthHeightUnk();
            mt.Flags = 512; // TODO: or 256?
            mt.Mask = 1023;
            mt.Children = new MaterialTextureChild[] {
                    CreateMaterialTextureChild(),
                    null,
                    null,
                    null,
                    null,
                    null,
                };
            mt.IdField = new TextureId() { Id = Texture.Index.Value };
            return mt;
        }

        private (ushort width_Unk, ushort height_Unk) CalculateWidthHeightUnk()
        {
            var vector2 = new Vector2(Image.Width, Image.Height) * 512;
            vector2 = vector2.ScaleWithinBounds(ushort.MaxValue, ushort.MaxValue);
            return ((ushort)vector2.X, (ushort)vector2.Y);
        }

        protected virtual MaterialTextureChild CreateMaterialTextureChild() =>
            new MaterialTextureChild() {
                Byte_2 = 8, // or 4
                DimensionsBitmask = 0, // or 0x34
                Byte_4 = 6, // or 5
                Byte_5 = 6, // or 5
                Byte_d = 252, // or 124
                Byte_f = 252, // or 124
            };

        protected virtual MaterialProperties CreateMaterialProperties() =>
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
