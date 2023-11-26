// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.Textures;
using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.SpriteBlock
{
    /// <summary>
    /// <see href="https://github.com/OpenSWE1R/swe1r-re/blob/master/swep1rcr.exe/Sprites.md">SpriteTexture</see>
    /// </summary>
    public class Sprite : BlockItemValue
    {
        #region Properties (serialized)

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
        public TextureFormat TextureFormat { get; set; }
        /// <summary>
        /// Always 0.
        /// </summary>
        [Order(3)]
        public short Word_6 { get; set; } = 0;
        /// <summary>
        /// Can be null.
        /// </summary>
        [Order(4), Reference(1)]
        public SpritePalette Palette { get; set; }
        /// <summary>
        /// Always a value from 0 to 80.
        /// </summary>
        [Order(5)]
        public short TilesCount { get; set; }
        /// <summary>
        /// Always 32.
        /// </summary>
        [Order(6)]
        public short Word_E { get; private set; } = 32;
        /// <summary>
        /// Never null.
        /// </summary>
        [Order(7), Reference(0), Length(nameof(TilesCount))] 
        public List<SpriteTile> Tiles { get; private set; } // TODO: consider two-dimensional array

        #endregion

        #region Properties (helper)

        public int TilesGridWidth =>
            (int)Math.Ceiling(Width / 64f); // TODO: do not hardcode 64

        public int TilesGridHeight =>
            (int)Math.Ceiling(Height / 32f); // TODO: do not hardcode 32 (is it actually Word_E?)

        #endregion

        #region Methods (serialization)

        public void UpdatePagesCount() => // TODO: implement in BindingComponent
            TilesCount = (short)Tiles.Count;

        #endregion

        #region Methods (export)

        public IEnumerable<SpriteTile> GetTilesRow(int tileY)
        {
            for (int tileX = 0; tileX < TilesGridWidth; tileX++)
                yield return GetTile(tileX, tileY);
        }

        public SpriteTile GetTile(int tileX, int tileY)
        {
            int tileIndex = GetTileIndex(tileX, tileY);
            if (tileIndex < Tiles.Count) // TODO: that check should be unnecessary
                return Tiles[tileIndex];
            else
                return null;
        }

        public int GetTileIndex(int tileX, int tileY) =>
            tileY * TilesGridWidth + tileX;

        public ImageRgba32 ExportImage()
        {
            ImageRgba32 image = new ImageRgba32(Width, Height);
            for (int tileY = 0; tileY < TilesGridHeight; tileY++)
            {
                for (int tileX = 0; tileX < TilesGridWidth; tileX++)
                {
                    int tileIndex = GetTileIndex(tileX, tileY);
                    if (tileIndex < Tiles.Count)
                    {
                        SpriteTile tile = GetTile(tileX, tileY);
                        ImageRgba32 tileImage = tile.ExportImage(this);
                        image.Insert(tileImage.FlipY(), tileX * 64, tileY * 32);
                        // TODO: do not hardcode 64
                        // TODO: do not hardcode 32 (is it actually Word_E?)
                    }
                }
            }
            return image;
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({nameof(Width)}={Width}, {nameof(Height)}={Height})";

        #endregion
    }
}
