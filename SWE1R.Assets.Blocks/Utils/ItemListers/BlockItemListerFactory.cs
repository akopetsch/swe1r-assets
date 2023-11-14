// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using System;

namespace SWE1R.Assets.Blocks.TestApp.ItemListers
{
    public static class BlockItemListerFactory
    {
        public static IBlockItemLister Get<TItem>(Block<TItem> block, Action<string> writeLineAction) 
            where TItem : BlockItem, new()
        {
            if (block is Block<Model> modelBlock)
                return new ModelBlockItemLister(modelBlock, writeLineAction);
            else
                return new BlockItemLister<TItem>(block, writeLineAction);
        }
    }
}
