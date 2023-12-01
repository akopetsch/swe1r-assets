// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks;
using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.Metadata.IdNames;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.Original;
using SWE1R.Assets.Blocks.SplineBlock;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Diagnostics;

namespace FiddleApp
{
    public class MetadataCsvBuilder
    {
        private MetadataProvider _metadataProvider = new();
        private OriginalBlockProvider _originalBlockProvider = new OriginalBlockProvider();

        public void Run()
        {
            SaveBlockCsv();
            SaveBlockItemValueCsv();
            SaveBlockItemCsv();
        }

        #region Methods (BlockMetadata)

        private void SaveBlockCsv()
        {
            Console.WriteLine($"{nameof(SaveBlockCsv)}");
            List<BlockMetadata> metadataList = _metadataProvider.Blocks;
            metadataList.Clear();
            metadataList.AddRange(GetBlockMetadata<ModelBlockItem>());
            metadataList.AddRange(GetBlockMetadata<SplineBlockItem>());
            metadataList.AddRange(GetBlockMetadata<SpriteBlockItem>());
            metadataList.AddRange(GetBlockMetadata<TextureBlockItem>());
            _metadataProvider.Save(metadataList);
            Console.WriteLine();
        }

        private List<BlockMetadata> GetBlockMetadata<TBlockItem>() where TBlockItem : BlockItem, new()
        {
            ConsoleWriteBlockItemTypeName<TBlockItem>();
            var metadataList = new List<BlockMetadata>();
            int i = 0;
            foreach (string blockIdName in BlockIdNames.GetAll<TBlockItem>())
            {
                ConsoleWriteBlockIdName(blockIdName);
                Block<TBlockItem> block = _originalBlockProvider.LoadBlock<TBlockItem>(blockIdName);
                var metadata = new BlockMetadata(block, i++, blockIdName);
                metadataList.Add(metadata);
            }
            return metadataList;
        }

        #endregion

        #region Methods (BlockItemMetadataByValue)

        private void SaveBlockItemValueCsv()
        {
            Console.WriteLine($"{nameof(SaveBlockItemMetadataByValueCsv)}");
            SaveBlockItemMetadataByValueCsv<ModelBlockItem>(idBaseStep: 1000);
            SaveBlockItemMetadataByValueCsv<SplineBlockItem>(idBaseStep: 100);
            SaveBlockItemMetadataByValueCsv<SpriteBlockItem>(idBaseStep: 1000);
            SaveBlockItemMetadataByValueCsv<TextureBlockItem>(idBaseStep: 10000);
            Console.WriteLine();
        }

        private void SaveBlockItemMetadataByValueCsv<TBlockItem>(int idBaseStep) where TBlockItem : BlockItem, new()
        {
            ConsoleWriteBlockItemTypeName<TBlockItem>();
            List<BlockItemValueMetadata> existingMetadataList = _metadataProvider.GetBlockItemValues<TBlockItem>();
            List<BlockItemValueMetadata> newMetadataList = new List<BlockItemValueMetadata>();
            int idBase = 0;
            foreach (string blockIdName in BlockIdNames.GetAll<TBlockItem>())
            {
                ConsoleWriteBlockIdName(blockIdName);
                Block<TBlockItem> block = _originalBlockProvider.LoadBlock<TBlockItem>(blockIdName);
                Debug.Assert(idBaseStep > block.Count);
                foreach (TBlockItem blockItem in block)
                {
                    var newBlockItemMetadata = new BlockItemValueMetadata(blockItem);
                    newBlockItemMetadata.Id += idBase;
                    if (!newMetadataList.Any(x => x.Hash.Equals(newBlockItemMetadata.Hash)))
                    {
                        BlockItemValueMetadata existingBlockItemMetadata =
                            existingMetadataList.SingleOrDefault(x => x.Hash.Equals(newBlockItemMetadata.Hash));
                        if (existingBlockItemMetadata != null)
                        {
                            Debug.Assert(newBlockItemMetadata.BlockItemType.Equals(existingBlockItemMetadata.BlockItemType));
                            //Debug.Assert(newBlockItemMetadata.Id.Equals(existingBlockItemMetadata.Id));
                            Debug.Assert(newBlockItemMetadata.Size1.Equals(existingBlockItemMetadata.Size1));
                            Debug.Assert(newBlockItemMetadata.Size2.Equals(existingBlockItemMetadata.Size2));
                            newBlockItemMetadata.Name = existingBlockItemMetadata.Name;
                        }
                        newMetadataList.Add(newBlockItemMetadata);
                    }
                }
                idBase += idBaseStep;
            }
            existingMetadataList.Clear();
            existingMetadataList.AddRange(newMetadataList);
            _metadataProvider.Save(existingMetadataList, typeof(TBlockItem).Name);
        }

        #endregion

        #region Methods (BlockItemMetadata)

        private void SaveBlockItemCsv()
        {
            Console.WriteLine(nameof(SaveBlockItemCsv));
            var metadataList = _metadataProvider.BlockItems;
            metadataList.Clear();
            metadataList.AddRange(GetBlockItemMetadata<ModelBlockItem>());
            metadataList.AddRange(GetBlockItemMetadata<SplineBlockItem>());
            metadataList.AddRange(GetBlockItemMetadata<SpriteBlockItem>());
            metadataList.AddRange(GetBlockItemMetadata<TextureBlockItem>());
            _metadataProvider.Save(metadataList);
            Console.WriteLine();
        }

        private List<BlockItemMetadata> GetBlockItemMetadata<TBlockItem>() where TBlockItem : BlockItem, new()
        {
            ConsoleWriteBlockItemTypeName<TBlockItem>();
            var metadataList = new List<BlockItemMetadata>();
            foreach (string blockIdName in BlockIdNames.GetAll<TBlockItem>())
            {
                ConsoleWriteBlockIdName(blockIdName);
                Block<TBlockItem> block = _originalBlockProvider.LoadBlock<TBlockItem>(blockIdName);
                string blockHashString = block.HashString;
                for (int i = 0; i < block.Count; i++)
                {
                    TBlockItem item = block[i];
                    var metadata = new BlockItemMetadata(item, _metadataProvider);
                    metadataList.Add(metadata);
                }
            }
            return metadataList;
        }

        #endregion

        #region Methods (console helper)

        private void ConsoleWriteBlockItemTypeName<TBlockItem>() =>
            ConsoleWriteIndented(typeof(TBlockItem).Name, 2);

        private void ConsoleWriteBlockIdName(string blockIdName) =>
            ConsoleWriteIndented(blockIdName, 4);

        private void ConsoleWriteIndented(string s, int indentLevel) =>
            Console.WriteLine($"{new string(' ', indentLevel)}{s}");

        #endregion
    }
}
