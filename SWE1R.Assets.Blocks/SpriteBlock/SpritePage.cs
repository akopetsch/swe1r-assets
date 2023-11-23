// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.IO.Extensions;
using SWE1R.Assets.Blocks.Common.Colors;
using SWE1R.Assets.Blocks.Common.Images;

namespace SWE1R.Assets.Blocks.SpriteBlock
{
    public class SpritePage
    {
        #region Properties (serialization)

        [Order(0)] 
        public short Width { get; set; }
        [Order(1)] 
        public short Height { get; set; }
        [Order(2), Reference(ReferenceHandling.Postpone), Length(typeof(LengthHelper))] 
        public byte[] Data { get; set; } // TODO: type

        #endregion

        #region Classes (serialization)

        private class LengthHelper : IBindingHelper
        {
            public int GetValue(PropertyComponent property)
            {
                var recordComponent = (RecordComponent)property.GetAncestorValueComponent<SpritePage>();

                int startPosition = GetDataPointer(recordComponent);

                int endPosition;
                var elementComponent = recordComponent.Get<CollectionElementComponent>();
                if (elementComponent.IsLastElement)
                    // TODO: HACK: stream could be longer than the SpriteData's data bytes
                    endPosition = (int)property.Context.Stream.Length;
                else
                    endPosition = GetDataPointer(elementComponent.NextElement.Get<RecordComponent>());

                return endPosition - startPosition;
            }

            private int GetDataPointer(RecordComponent recordComponent) =>
                recordComponent.Properties[nameof(Data)].Get<ReferenceComponent>().Pointer.Value;
        }

        #endregion

        #region Methods (export)

        public ImageRgba32 ExportImage(SpriteData spriteData)
        {
            int bpp = spriteData.GetBitsPerPixel();

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
                    if (spriteData.Palette == null)
                    {
                        if (bpp == 4)
                        {
                            byte nibble = Data.GetNibble(pixelIndex);
                            byte grayscale = (byte)(nibble * 16);
                            byte alpha = (byte)(nibble * 17);
                            color = new ColorRgba32(grayscale, grayscale, grayscale, alpha);
                        }
                        else if (bpp == 8)
                        {
                            byte grayscale = Data[pixelIndex];
                            color = new ColorRgba32(grayscale, grayscale, grayscale, grayscale);
                        }
                        else if (bpp == 32)
                        {
                            int offset = pixelIndex * 4;
                            byte r = Data[offset];
                            byte g = Data[offset + 1];
                            byte b = Data[offset + 2];
                            byte a = Data[offset + 3];
                            color = new ColorRgba32(r, g, b, a);
                        }
                    }
                    else
                    {
                        if (bpp == 4)
                        {
                            int index = Data.GetNibble(pixelIndex);
                            color = (ColorRgba32)spriteData.Palette.Colors[index];
                        }
                        else if (bpp == 8)
                        {
                            int index = Data[pixelIndex];
                            color = (ColorRgba32)spriteData.Palette.Colors[index];
                        }
                    }

                    image[x, y] = color;
                }
            }
            return image;
        }

        #endregion
    }
}
