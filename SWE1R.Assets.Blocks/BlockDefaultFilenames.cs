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
    public class BlockDefaultFilenames
    {
        #region Fields

        private Dictionary<Type, string> filenameByItemType = new Dictionary<Type, string>()
        {
            { typeof(Model), "out_modelblock.bin" },
            { typeof(Spline), "out_splineblock.bin" },
            { typeof(Sprite), "out_spriteblock.bin" },
            { typeof(Texture), "out_textureblock.bin" },
        };

        #endregion

        #region Methods

        public string GetDefaultFilename<TBlockItem>() where TBlockItem : BlockItem =>
            filenameByItemType[typeof(TBlockItem)];

        public string GetDefaultFilename(Type itemType) =>
            filenameByItemType[itemType];

        #endregion
    }
}
