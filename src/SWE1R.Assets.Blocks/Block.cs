// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.SplineBlock;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.TextureBlock;
using System;
using System.IO;

namespace SWE1R.Assets.Blocks
{
    public static class Block
    {
        public static Block<TItem> Load<TItem>(string filename) where TItem : BlockItem, new()
        {
            var block = new Block<TItem>();
            block.Load(filename);
            return block;
        }

        public static IBlock Load(BlockItemType blockItemType, string filename)
        {
            switch (blockItemType)
            {
                case BlockItemType.ModelBlockItem: return Load<ModelBlockItem>(filename);
                case BlockItemType.SplineBlockItem: return Load<SplineBlockItem>(filename);
                case BlockItemType.SpriteBlockItem: return Load<SpriteBlockItem>(filename);
                case BlockItemType.TextureBlockItem: return Load<TextureBlockItem>(filename);
                default: throw new InvalidOperationException();
            }
        }

        public static Block<TItem> Load<TItem>(Stream stream) where TItem : BlockItem, new()
        {
            var block = new Block<TItem>();
            block.Load(stream);
            return block;
        }

        public static IBlock Load(BlockItemType blockItemType,Stream stream)
        {
            switch (blockItemType)
            {
                case BlockItemType.ModelBlockItem: return Load<ModelBlockItem>(stream);
                case BlockItemType.SplineBlockItem: return Load<SplineBlockItem>(stream);
                case BlockItemType.SpriteBlockItem: return Load<SpriteBlockItem>(stream);
                case BlockItemType.TextureBlockItem: return Load<TextureBlockItem>(stream);
                default: throw new InvalidOperationException();
            }
        }
    }
}
