// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.Images.SystemDrawing;
using SWE1R.Assets.Blocks.TextureBlock;
using SWE1R.Assets.Blocks.Utils;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Resources.ResourceHelpers
{
    public abstract class ModelBlockImportResourceHelper<TResourceHelper>(
        Block<TextureBlockItem> textureBlock)
        where TResourceHelper : ResourceHelperBase, new()
    {
        protected TResourceHelper ResourcesHelper { get; } = new();
        protected Block<TextureBlockItem> TextureBlock { get; } = textureBlock;
        protected LoadImageRgba32FromStreamDelegate ImageLoader { get; } = 
            new SystemDrawingImageRgba32Loader().Load;
    }
}
