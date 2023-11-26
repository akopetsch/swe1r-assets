// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.IO.Extensions;
using SWE1R.Assets.Blocks.Colors;
using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.Textures;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.SpriteBlock
{
    public class SpriteTile
    {
        #region Properties (serialization)

        /// <summary>
        /// Always a value from 2 to 64.
        /// </summary>
        [Order(0)] 
        public short Width { get; set; }
        /// <summary>
        /// Always a value from 1 to 32.
        /// </summary>
        [Order(1)] 
        public short Height { get; set; }
        /// <summary>
        /// Never null. Always has a length from 16 to 4096.
        /// </summary>
        [Order(2), Reference(ReferenceHandling.Postpone), Length(typeof(LengthHelper))] 
        public byte[] Pixels { get; set; } // TODO: type

        #endregion

        #region Classes (serialization)

        private class LengthHelper : IBindingHelper
        {
            public int GetValue(PropertyComponent property)
            {
                var recordComponent = (RecordComponent)property.GetAncestorValueComponent<SpriteTile>();

                int startPosition = GetPixelsPointer(recordComponent);

                int endPosition;
                var elementComponent = recordComponent.Get<CollectionElementComponent>();
                if (elementComponent.IsLastElement)
                {
                    // TODO: HACK: stream could be longer than the SpriteData's data bytes
                    endPosition = (int)property.Context.Stream.Length;
                }
                else
                {
                    endPosition = GetPixelsPointer(elementComponent.NextElement.Get<RecordComponent>());
                }

                int length = endPosition - startPosition;
                Debug.Assert(endPosition % 4 == 0);
                return length;
            }

            private int GetPixelsPointer(RecordComponent recordComponent) =>
                recordComponent.Properties[nameof(Pixels)].Get<ReferenceComponent>().Pointer.Value;
        }

        #endregion

        #region Methods (export)

        public ImageRgba32 ExportImage(Sprite sprite)
        {
            int bpp = sprite.TextureFormat.GetBpp();

            float bytesPerPixel = (float)bpp / 8;
            int bytesPerLine = (int)(Width * bytesPerPixel);
            int virtualBytesPerLine = bytesPerLine & 0xfff8; // round down by 8 (padding)
            if (bytesPerLine % 8 > 0)
                virtualBytesPerLine += 8;
            int virtualWidth = (int)(virtualBytesPerLine / bytesPerPixel);

            var image = new ImageRgba32(Width, Height);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    // get color
                    ColorRgba32 color = ColorRgba32.Pink; // test color
                    int pixelIndex = y * virtualWidth + x;
                    if (sprite.Palette == null)
                    {
                        if (bpp == 4)
                        {
                            byte nibble = Pixels.GetNibble(pixelIndex);
                            byte grayscale = (byte)(nibble * 16);
                            byte alpha = (byte)(nibble * 17);
                            color = new ColorRgba32(grayscale, grayscale, grayscale, alpha);
                        }
                        else if (bpp == 8)
                        {
                            byte grayscale = Pixels[pixelIndex];
                            color = new ColorRgba32(grayscale, grayscale, grayscale, grayscale);
                        }
                        else if (bpp == 32)
                        {
                            int offset = pixelIndex * 4;
                            byte r = Pixels[offset];
                            byte g = Pixels[offset + 1];
                            byte b = Pixels[offset + 2];
                            byte a = Pixels[offset + 3];
                            color = new ColorRgba32(r, g, b, a);
                        }
                    }
                    else
                    {
                        if (bpp == 4)
                        {
                            int index = Pixels.GetNibble(pixelIndex);
                            color = (ColorRgba32)sprite.Palette.Colors[index];
                        }
                        else if (bpp == 8)
                        {
                            int index = Pixels[pixelIndex];
                            color = (ColorRgba32)sprite.Palette.Colors[index];
                        }
                    }

                    image[x, y] = color;
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
