// SPDX-License-Identifier: MIT

using System.Collections.Generic;

namespace SWE1R.Assets.Blocks
{
    public static class BlockDefaultFilenames
    {
        #region Fields

        private static Dictionary<BlockItemType, string> _filenameByItemType = new Dictionary<BlockItemType, string>()
        {
            { BlockItemType.ModelBlockItem, "out_modelblock.bin" },
            { BlockItemType.SplineBlockItem, "out_splineblock.bin" },
            { BlockItemType.SpriteBlockItem, "out_spriteblock.bin" },
            { BlockItemType.TextureBlockItem, "out_textureblock.bin" },
        };

        #endregion

        #region Properties

        public static string ModelBlock { get; } = GetDefaultFilename(BlockItemType.ModelBlockItem);
        public static string SplineBlock { get; } = GetDefaultFilename(BlockItemType.SplineBlockItem);
        public static string SpriteBlock { get; } = GetDefaultFilename(BlockItemType.SpriteBlockItem);
        public static string TextureBlock { get; } = GetDefaultFilename(BlockItemType.TextureBlockItem);

        #endregion

        #region Methods

        public static string GetDefaultFilename(BlockItemType blockItemType) =>
            _filenameByItemType[blockItemType];

        #endregion
    }
}
