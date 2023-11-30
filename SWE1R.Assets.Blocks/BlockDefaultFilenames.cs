// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.SplineBlock;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.TextureBlock;
using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks
{
    public static class BlockDefaultFilenames
    {
        #region Fields

        private static Dictionary<Type, string> _filenameByItemType = new Dictionary<Type, string>()
        {
            { typeof(ModelBlockItem), "out_modelblock.bin" },
            { typeof(SplineBlockItem), "out_splineblock.bin" },
            { typeof(SpriteBlockItem), "out_spriteblock.bin" },
            { typeof(TextureBlockItem), "out_textureblock.bin" },
        };

        #endregion

        #region Properties

        public static string ModelBlock { get; } = GetDefaultFilename<ModelBlockItem>();
        public static string SplineBlock { get; } = GetDefaultFilename<SplineBlockItem>();
        public static string SpriteBlock { get; } = GetDefaultFilename<SpriteBlockItem>();
        public static string TextureBlock { get; } = GetDefaultFilename<TextureBlockItem>();

        #endregion

        #region Methods

        public static string GetDefaultFilename<TBlockItem>() where TBlockItem : BlockItem =>
            _filenameByItemType[typeof(TBlockItem)];

        #endregion
    }
}
