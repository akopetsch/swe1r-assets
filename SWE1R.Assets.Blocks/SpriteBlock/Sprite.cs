// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Common.Images;
using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.SpriteBlock
{
    /// <summary>
    /// <see href="https://github.com/OpenSWE1R/swe1r-re/blob/master/swep1rcr.exe/Sprites.md">SpriteTexture</see>
    /// </summary>
    public class Sprite : BlockItemValue
    {
        #region Properties (serialization)

        /// <summary>
        /// Always has a value from 1 to 640.
        /// </summary>
        [Order(0)]
        public short Width { get; set; }
        /// <summary>
        /// Always has a value from 1 to 256.
        /// </summary>
        [Order(1)]
        public short Height { get; set; }
        /// <summary>
        /// Always 0, 2 or 4.
        /// </summary>
        [Order(2)]
        public byte Format { get; set; }
        /// <summary>
        /// Always 0, 1, or 3.
        /// </summary>
        [Order(3)]
        public byte PageWidthAlignment { get; set; }
        /// <summary>
        /// Always 0.
        /// </summary>
        [Order(4)]
        public short Word_6 { get; set; } = 0;
        /// <summary>
        /// Can be null.
        /// </summary>
        [Order(5), Reference(1)]
        public SpritePalette Palette { get; set; }
        /// <summary>
        /// Always a value from 0 to 80.
        /// </summary>
        [Order(6)]
        public short PagesCount { get; set; }
        /// <summary>
        /// Always 32.
        /// </summary>
        [Order(7)]
        public short Word_E { get; private set; } = 32;
        /// <summary>
        /// Never null.
        /// </summary>
        [Order(8), Reference(0), Length(nameof(PagesCount))] 
        public List<SpritePage> Pages { get; private set; }

        #endregion

        #region Methods (serialization)

        public void UpdatePagesCount() => // TODO: implement in BindingComponent
            PagesCount = (short)Pages.Count;

        #endregion

        #region Methods (export)

        public ImageRgba32 ExportImage()
        {
            int widthCount = (int)Math.Ceiling(Width / 64f);
            int heightCount = (int)Math.Ceiling(Height / (float)Word_E);
            ImageRgba32 image = new ImageRgba32(Width, Height);
            for (int y = 0; y < heightCount; y++)
            {
                for (int x = 0; x < widthCount; x++)
                {
                    int index = y * widthCount + x;
                    if (index < Pages.Count)
                    {
                        SpritePage page = Pages[index];
                        ImageRgba32 pageImage = page.ExportImage(this);
                        image.Insert(pageImage.FlipY(), x * 64, y * Word_E);
                    }
                }
            }
            return image;
            // TODO: usage of Word_E not confirmed, consider hardcoding 32
        }

        public int GetBitsPerPixel()
        {
            if (Format == 0 && PageWidthAlignment == 3)
                return 32;
            else if (Format == 2 || Format == 4)
            {
                switch (PageWidthAlignment)
                {
                    case 0: return 4;
                    case 1: return 8;
                }
            }
            throw new InvalidOperationException();
        }

        #endregion
    }
}
