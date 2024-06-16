// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using SWE1R.Assets.Blocks.Images;
using System;

namespace SWE1R.Assets.Blocks.Textures.Import
{
    public class TextureImporterFactory
    {
        public TextureImporter Get(ImageRgba32 imageRgba32, TextureFormat textureFormat, Endianness endianness)
        {
            switch (textureFormat)
            {
                case TextureFormat.RGBA32: return new RGBA32_TextureImporter(imageRgba32, endianness);
                case TextureFormat.RGBA5551_I8: return new RGBA5551_I8_TextureImporter(imageRgba32, null, endianness);
                default: throw new NotImplementedException();
            }
        }
    }
}
