﻿// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.TextureBlock;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials.Import
{
    public class MaterialImporterFactory
    {
        public MaterialImporter Get(ImageRgba32 imageRgba32, Block<TextureBlockItem> textureBlock)
        {
            if (imageRgba32.HasPalette) // TODO: switch(TextureFormat) like in TextureImporterFactory
                return new RGBA5551_I8_MaterialImporter(imageRgba32, textureBlock);
            else
                return new RGBA32_MaterialImporter(imageRgba32, textureBlock);
        }
    }
}