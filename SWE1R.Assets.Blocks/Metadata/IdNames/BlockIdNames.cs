// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.Metadata.IdNames
{
    public static class BlockIdNames
    {
        private static readonly Dictionary<BlockItemType, List<string>> _allByItemType =
            new Dictionary<BlockItemType, List<string>>() {
                { BlockItemType.ModelBlockItem, ModelBlockIdNames.All.ToList() },
                { BlockItemType.SplineBlockItem, SplineBlockIdNames.All.ToList() },
                { BlockItemType.SpriteBlockItem, SpriteBlockIdNames.All.ToList() },
                { BlockItemType.TextureBlockItem, TextureBlockIdNames.All.ToList() },
        };

        public static IEnumerable<string> GetAll<TBlockItem>() where TBlockItem : BlockItem =>
            GetAll(BlockItemTypeAttributeHelper.GetBlockItemClassType(typeof(TBlockItem)));

        public static IEnumerable<string> GetAll(BlockItemType blockItemType) =>
            _allByItemType[blockItemType].OrderBy(x => x);
    }
}
