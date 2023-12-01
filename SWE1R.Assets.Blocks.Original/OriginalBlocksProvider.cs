// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.Metadata.IdNames;
using SWE1R.Assets.Blocks.Original.Resources;

namespace SWE1R.Assets.Blocks.Original
{
    public class OriginalBlocksProvider
    {
        private readonly MetadataProvider _metadataProvider = new ();
        private readonly Dictionary<(BlockItemType, string), IBlock> _blocks = new();

        public void Init()
        {
            LoadBlocks();
        }

        public Block<TItem> GetBlock<TItem>(string blockIdName) where TItem : BlockItem, new()
        {
            BlockItemType blockItemType = BlockItemTypeAttributeHelper.GetBlockItemType(typeof(TItem));
            return (Block<TItem>)_blocks[(blockItemType, blockIdName)];
        }

        public TItem GetBlockItem<TItem>(int valueId) where TItem : BlockItem, new()
        {
            BlockItemMetadata blockItemMetadata = _metadataProvider.GetBlockItem<TItem>(valueId);
            BlockMetadata blockMetadata = _metadataProvider.GetBlock(blockItemMetadata);
            Block<TItem> block = GetBlock<TItem>(blockMetadata.Name);
            return block[blockItemMetadata.Index];
        }

        private void LoadBlocks()
        {
            foreach (BlockItemType blockItemType in Enum.GetValues<BlockItemType>())
                foreach (string blockIdName in BlockIdNames.GetAll(blockItemType))
                    _blocks[(blockItemType, blockIdName)] = LoadBlock(blockItemType, blockIdName);
        }

        private IBlock LoadBlock(BlockItemType blockItemType, string blockIdName)
        {
            string resourcePath = GetBlockResourcePath(blockItemType, blockIdName);
            Stream resourceStream = new OriginalBlocksResourceHelper().ReadEmbeddedResource(resourcePath);
            return Block.Load(blockItemType, resourceStream);
        }

        private string GetBlockResourcePath(BlockItemType blockItemType, string blockIdName)
        {
            string blockDefaultFilename = BlockDefaultFilenames.GetDefaultFilename(blockItemType);
            string blockIdFilename = $"{blockIdName}.bin";
            return $"{blockDefaultFilename}.{blockIdFilename}";
        }
    }
}
