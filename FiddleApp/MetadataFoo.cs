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
    public class MetadataFoo
    {
        private MetadataProvider _metadataProvider = new MetadataProvider();
        private OriginalBlockProvider _originalBlockProvider = new OriginalBlockProvider();

        public void Run()
        {
            SaveBlockCsv();
            SaveBlockItemMetadataByValueCsv();
        }

        private void SaveBlockCsv()
        {
            Console.WriteLine($"{nameof(SaveBlockCsv)}");
            var metadataList = new List<BlockMetadata>();
            metadataList.AddRange(GetBlockMetadata<ModelBlockItem>());
            metadataList.AddRange(GetBlockMetadata<SplineBlockItem>());
            metadataList.AddRange(GetBlockMetadata<SpriteBlockItem>());
            metadataList.AddRange(GetBlockMetadata<TextureBlockItem>());
            for (int i = 0; i < metadataList.Count; i++)
                metadataList[i].Id = i;
            _metadataProvider.Save(metadataList);
            Console.WriteLine();
        }

        private List<BlockMetadata> GetBlockMetadata<TBlockItem>() where TBlockItem : BlockItem, new()
        {
            Console.WriteLine(typeof(TBlockItem).Name);
            var metadataList = new List<BlockMetadata>();
            foreach (string blockIdName in BlockIdNames.GetAll<TBlockItem>())
            {
                ConsoleWriteBlockIdName(blockIdName);
                Block<TBlockItem> block = _originalBlockProvider.LoadBlock<TBlockItem>(blockIdName);
                var metadata = new BlockMetadata(block);
                metadataList.Add(metadata);
            }
            return metadataList;
        }
        private void SaveBlockItemMetadataByValueCsv()
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
            Console.WriteLine(typeof(TBlockItem).Name);
            List<BlockItemMetadataByValue> existingMetadataList = _metadataProvider.GetBlockItemValues<TBlockItem>();
            List<BlockItemMetadataByValue> newMetadataList = new List<BlockItemMetadataByValue>();
            int idBase = 0;
            foreach (string blockIdName in BlockIdNames.GetAll<TBlockItem>())
            {
                ConsoleWriteBlockIdName(blockIdName);
                Block<TBlockItem> block = _originalBlockProvider.LoadBlock<TBlockItem>(blockIdName);
                Debug.Assert(idBaseStep > block.Count);
                foreach (TBlockItem blockItem in block)
                {
                    var newBlockItemMetadata = new BlockItemMetadataByValue(blockItem);
                    newBlockItemMetadata.Id += idBase;
                    if (!newMetadataList.Any(x => x.Hash.Equals(newBlockItemMetadata.Hash)))
                    {
                        BlockItemMetadataByValue existingBlockItemMetadata =
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
            _metadataProvider.Save(newMetadataList, typeof(TBlockItem).Name);
        }

        private void ConsoleWriteBlockIdName(string blockIdName) =>
            Console.WriteLine($"  {blockIdName}");
    }
}
