// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.SplineBlock;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.TextureBlock;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.Metadata.IdNames
{
    public static class BlockIdNames
    {
        private static Dictionary<Type, List<string>> allByItemType =
            new Dictionary<Type, List<string>>() {
                { typeof(Model), ModelBlockIdNames.All.ToList() },
                { typeof(SplineBlockItem), SplineBlockIdNames.All.ToList() },
                { typeof(SpriteBlockItem), SpriteBlockIdNames.All.ToList() },
                { typeof(TextureBlockItem), TextureBlockIdNames.All.ToList() },
        };

        public static IEnumerable<string> GetAll<TBlockItem>() where TBlockItem : BlockItem =>
            allByItemType[typeof(TBlockItem)];
    }
}
