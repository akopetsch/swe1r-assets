// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.Textures;
using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.SpriteBlock.Import
{
    public class SpriteImporter
    {
        #region Properties (input)

        public ImageRgba32 Image { get; }

        #endregion

        #region Properties (output)

        public Sprite Sprite { get; private set; }

        #endregion

        #region Constructor

        public SpriteImporter(ImageRgba32 image)
        {
            Image = image;
        }

        #endregion

        #region Methods

        public void Import()
        {
            Sprite = new Sprite() {
                Width = Convert.ToInt16(Image.Width),
                Height = Convert.ToInt16(Image.Height),
                TextureFormat = TextureFormat.RGBA5551_I8,
            };
            Sprite.UpdatePagesCount();
        }

        private List<SpriteTile> ImportPages()
        {
            var pages = new List<SpriteTile>();

            return pages;
        }

        #endregion
    }
}
