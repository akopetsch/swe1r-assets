// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Common.Colors;
using System;
using System.Numerics;

namespace SWE1R.Assets.Blocks.Common.Images
{
    public class ImageRgba32
    {
        #region Properties

        public int Width { get; }
        public int Height { get; }

        public ColorRgba32[][] Pixels { get; }

        public ColorRgba32[] Palette { get; set; }

        public Vector2 Size => new Vector2(Width, Height);

        public bool HasPalette => Palette?.Length > 0;

        #endregion

        #region Constructor

        public ImageRgba32(int width, int height)
        {
            Width = width;
            Height = height;

            Pixels = new ColorRgba32[Width][];
            for (int x = 0; x < Pixels.Length; x++)
                Pixels[x] = new ColorRgba32[Height];
        }

        #endregion

        #region Indexers

        public ColorRgba32 this[int x, int y]
        {
            get => Pixels[x][y];
            set => Pixels[x][y] = value;
        }

        #endregion

        #region Methods (palette)

        public int GetPaletteIndex(int x, int y) =>
            Array.IndexOf(Palette, this[x, y]);

        #endregion

        #region Methods (mirror, flip, append, insert)

        public ImageRgba32 Mirror(bool right, bool bottom)
        {
            if (right && bottom)
            {
                ImageRgba32 upperHalf = AppendRight(FlipX());
                ImageRgba32 mirrored = upperHalf.AppendBottom(upperHalf.FlipY());
                return mirrored;
            }
            else if (right)
                return AppendRight(FlipX());
            else if (bottom)
                return AppendBottom(FlipY());
            else
                return this;
        }

        public ImageRgba32 FlipX()
        {
            int w = Width;
            int h = Height;
            var flipped = new ImageRgba32(w, h);
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    flipped[x, y] = this[w - 1 - x, y];
            return flipped;
        }

        public ImageRgba32 FlipY()
        {
            int w = Width;
            int h = Height;
            var flipped = new ImageRgba32(w, h);
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    flipped[x, y] = this[x, h - 1 - y];
            return flipped;
        }

        public ImageRgba32 AppendRight(ImageRgba32 other)
        {
            int w0 = Width;
            int h0 = Height;
            int w1 = other.Width;
            var appended = new ImageRgba32(w0 + w1, h0);
            appended.Insert(this, 0, 0);
            appended.Insert(other, w0, 0);
            return appended;
        }

        public ImageRgba32 AppendBottom(ImageRgba32 other)
        {
            int w0 = Width;
            int h0 = Height;
            int h1 = other.Height;
            var appended = new ImageRgba32(w0, h0 + h1);
            appended.Insert(this, 0, 0);
            appended.Insert(other, 0, h1);
            return appended;
        }

        public void Insert(ImageRgba32 other, int x, int y)
        {
            int w1 = other.Width;
            int h1 = other.Height;
            for (int y1 = 0; y1 < h1; y1++)
            {
                int y0 = y + y1;
                if (y0 >= Height)
                    break;
                for (int x1 = 0; x1 < w1; x1++)
                {
                    int x0 = x + x1;
                    if (x0 >= Width)
                        break;
                    this[x0, y0] = other[x1, y1];
                }
            }
        }

        #endregion

        #region Methods (alpha)

        public void SetAlpha(byte alpha)
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    Pixels[x][y].A = alpha;
        }

        #endregion
    }
}
