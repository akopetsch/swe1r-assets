// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.Metadata.IdNames;
using SWE1R.Assets.Blocks.Original.Resources;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Original
{
    public class OriginalBlocksProvider
    {
        #region Properties

        private readonly MetadataProvider _metadataProvider = new();
        private readonly ResourceHelper _resourceHelper = new();
        private readonly Dictionary<(BlockItemType, string), IBlock> _blocks = [];

        #endregion

        #region Methods (public)

        public void Load() =>
            LoadBlocks();

        public Block<TItem> GetBlock<TItem>(string blockIdName) where TItem : BlockItem, new()
        {
            BlockItemType blockItemType = BlockItemTypeAttributeHelper.GetBlockItemType(typeof(TItem));
            return (Block<TItem>)_blocks[(blockItemType, blockIdName)];
        }

        public TItem GetFirstBlockItemByValueId<TItem>(int valueId) where TItem : BlockItem, new()
        {
            BlockItemMetadata blockItemMetadata = _metadataProvider.GetBlockItem<TItem>(valueId);
            BlockMetadata blockMetadata = _metadataProvider.GetBlock(blockItemMetadata);
            Block<TItem> block = GetBlock<TItem>(blockMetadata.Name);
            return block[blockItemMetadata.Index];
        }

        #endregion

        #region Methods (private)

        private void LoadBlocks()
        {
            Debug.WriteLine($"{nameof(OriginalBlocksProvider)}.{nameof(LoadBlocks)}()");
            foreach (BlockItemType blockItemType in Enum.GetValues<BlockItemType>())
                foreach (string blockIdName in BlockIdNames.GetAll(blockItemType))
                    _blocks[(blockItemType, blockIdName)] = LoadBlock(blockItemType, blockIdName);
        }

        private IBlock LoadBlock(BlockItemType blockItemType, string blockIdName)
        {
            string resourcePath = GetBlockResourcePath(blockItemType, blockIdName);
            using Stream stream = _resourceHelper.ReadEmbeddedResource(resourcePath);
            
            var endianness = _metadataProvider.GetBlock(blockItemType, blockIdName).Endianness;

            return BlockLoader.Load(blockItemType, stream, endianness);
        }

        private string GetBlockResourcePath(BlockItemType blockItemType, string blockIdName)
        {
            string blockDefaultFilename = BlockDefaultFilenames.GetDefaultFilename(blockItemType);
            string blockIdFilename = $"{blockIdName}.bin";
            return $"{blockDefaultFilename}.{blockIdFilename}";
        }

        #endregion
    }
}
