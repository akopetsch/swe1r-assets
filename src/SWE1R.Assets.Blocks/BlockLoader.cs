// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.SplineBlock;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.TextureBlock;
using System;
using System.IO;

namespace SWE1R.Assets.Blocks
{
    public static class BlockLoader
    {
        #region Methods (filename)

        public static IBlock Load(BlockItemType blockItemType, string filename, Endianness endianness = BlockConstants.DefaultEndianness)
        {
            switch (blockItemType)
            {
                case BlockItemType.ModelBlockItem: return Load<ModelBlockItem>(filename, endianness);
                case BlockItemType.SplineBlockItem: return Load<SplineBlockItem>(filename, endianness);
                case BlockItemType.SpriteBlockItem: return Load<SpriteBlockItem>(filename, endianness);
                case BlockItemType.TextureBlockItem: return Load<TextureBlockItem>(filename, endianness);
                default: throw new InvalidOperationException();
            }
        }

        public static Block<TItem> Load<TItem>(string filename, Endianness endianness = BlockConstants.DefaultEndianness)
            where TItem : BlockItem, new()
        {
            var block = new Block<TItem>(endianness);
            block.Load(filename);
            return block;
        }

        #endregion

        #region Methods (stream)

        public static IBlock Load(BlockItemType blockItemType, Stream stream, Endianness endianness = BlockConstants.DefaultEndianness)
        {
            switch (blockItemType)
            {
                case BlockItemType.ModelBlockItem: return Load<ModelBlockItem>(stream, endianness);
                case BlockItemType.SplineBlockItem: return Load<SplineBlockItem>(stream, endianness);
                case BlockItemType.SpriteBlockItem: return Load<SpriteBlockItem>(stream, endianness);
                case BlockItemType.TextureBlockItem: return Load<TextureBlockItem>(stream, endianness);
                default: throw new InvalidOperationException();
            }
        }

        public static Block<TItem> Load<TItem>(Stream stream, Endianness endianness = BlockConstants.DefaultEndianness)
            where TItem : BlockItem, new()
        {
            var block = new Block<TItem>(endianness);
            block.Load(stream);
            return block;
        }

        #endregion
    }
}
