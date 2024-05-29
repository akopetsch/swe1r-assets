// SPDX-License-Identifier: GPL-2.0-only

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.SplineBlock;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.TextureBlock;
using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks
{
    public static class BlockItemTypeExtensions
    {
        private static readonly Dictionary<BlockItemType, Type> _blockItemClassTypeLookup =
            new Dictionary<BlockItemType, Type>(){
                { BlockItemType.ModelBlockItem, typeof(ModelBlockItem) },
                { BlockItemType.SplineBlockItem, typeof(SplineBlockItem) },
                { BlockItemType.SpriteBlockItem, typeof(SpriteBlockItem) },
                { BlockItemType.TextureBlockItem, typeof(TextureBlockItem) },
            };

        public static Type GetBlockItemClassType(this BlockItemType blockItemType) =>
            _blockItemClassTypeLookup[blockItemType];
    }
}
