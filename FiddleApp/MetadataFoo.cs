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
            DoFoo<ModelBlockItem>(idBaseStep: 1000);
            DoFoo<SplineBlockItem>(idBaseStep: 100);
            DoFoo<SpriteBlockItem>(idBaseStep: 1000);
            DoFoo<TextureBlockItem>(idBaseStep: 10000);
        }

        private void DoFoo<TBlockItem>(int idBaseStep) where TBlockItem : BlockItem, new()
        {
            Console.WriteLine(typeof(TBlockItem));
            List<BlockItemMetadataByValue> existingMetadataList = _metadataProvider.GetBlockItemValues<TBlockItem>();
            List<BlockItemMetadataByValue> newMetadataList = new List<BlockItemMetadataByValue>();
            int idBase = 0;
            foreach (string blockIdName in BlockIdNames.GetAll<TBlockItem>().OrderBy(x => x))
            {
                Console.WriteLine($"  {blockIdName}");
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
                            Debug.Assert(newBlockItemMetadata.BlockType.Equals(existingBlockItemMetadata.BlockType));
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
    }
}
