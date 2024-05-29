// SPDX-License-Identifier: GPL-2.0-only

using SWE1R.Assets.Blocks.ModelBlock;
using System;

namespace SWE1R.Assets.Blocks.TestApp.ItemListers
{
    public static class BlockItemListerFactory
    {
        public static IBlockItemLister Get<TItem>(Block<TItem> block, Action<string> writeLineAction) 
            where TItem : BlockItem, new()
        {
            if (block is Block<ModelBlockItem> modelBlock)
                return new ModelBlockItemLister(modelBlock, writeLineAction);
            else
                return new BlockItemLister<TItem>(block, writeLineAction);
        }
    }
}
