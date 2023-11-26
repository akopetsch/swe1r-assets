// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Images;
using System;

namespace SWE1R.Assets.Blocks.Textures.Import
{
    public class TextureImporterFactory
    {
        public TextureImporter Get(ImageRgba32 imageRgba32, TextureFormat textureFormat)
        {
            switch (textureFormat)
            {
                case TextureFormat.RGBA32: return new RGBA32_TextureImporter(imageRgba32);
                case TextureFormat.RGBA5551_I8: return new RGBA5551_I8_TextureImporter(imageRgba32);
                default: throw new NotImplementedException();
            }
        }
    }
}
