// SPDX-License-Identifier: GPL-2.0-only

using CsvHelper;
using CsvHelper.Configuration;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.Resources.Metadata;
using SWE1R.Assets.Blocks.SplineBlock;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.TextureBlock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SWE1R.Assets.Blocks.Metadata
{
    public class MetadataProvider
    {
        #region Fields

        private readonly Dictionary<Type, List<BlockItemValueMetadata>> _metadataByItemType;

        #endregion

        #region Properties

        public List<ReleaseMetadata> Releases { get; }
        public List<BlockMetadata> Blocks { get; }
        public List<BlockItemMetadata> BlockItems { get; }
        public List<RacerMetadata> Racers { get; }
        public List<TrackMetadata> Tracks { get; }

        #endregion

        #region Constructor

        public MetadataProvider()
        {
            Releases = Load<ReleaseMetadata>();
            Racers = Load<RacerMetadata>();
            Tracks = Load<TrackMetadata>();

            Blocks = Load<BlockMetadata>();
            BlockItems = Load<BlockItemMetadata>();

            _metadataByItemType = new Dictionary<Type, List<BlockItemValueMetadata>>();
            _metadataByItemType[typeof(ModelBlockItem)] = Load<BlockItemValueMetadata>(typeof(ModelBlockItem).Name);
            _metadataByItemType[typeof(SplineBlockItem)] = Load<BlockItemValueMetadata>(typeof(SplineBlockItem).Name);
            _metadataByItemType[typeof(SpriteBlockItem)] = Load<BlockItemValueMetadata>(typeof(SpriteBlockItem).Name);
            _metadataByItemType[typeof(TextureBlockItem)] = Load<BlockItemValueMetadata>(typeof(TextureBlockItem).Name);
        }

        #endregion

        #region Methods (BlockMetadata)

        public BlockMetadata GetBlockByHash(IBlock block) =>
            Blocks.Single(x => 
                x.Hash.Equals(block.HashString));

        public BlockMetadata GetBlock(BlockItemMetadata blockItemMetadata) =>
            Blocks.SingleOrDefault(x =>
                x.BlockItemType == blockItemMetadata.BlockItemType &&
                x.Id == blockItemMetadata.BlockId);

        public BlockMetadata GetBlock<TItem>(ReleaseMetadata releaseMetadata) where TItem : BlockItem =>
            GetBlock<TItem>(releaseMetadata.GetBlockId<TItem>());

        public BlockMetadata GetBlock<TItem>(int blockId) where TItem : BlockItem
        {
            BlockItemType blockItemType =
                BlockItemTypeAttributeHelper.GetBlockItemType(typeof(TItem));
            return Blocks.Single(x =>
                x.BlockItemType == blockItemType &&
                x.Id == blockId);
        }

        #endregion

        #region Methods (BlockItemMetadata)

        public IEnumerable<BlockItemMetadata> GetBlockItems<TItem>() where TItem : BlockItem =>
            GetBlockItems(typeof(TItem));

        public IEnumerable<BlockItemMetadata> GetBlockItems(Type blockItemClassType)
        {
            BlockItemType blockItemType =
                BlockItemTypeAttributeHelper.GetBlockItemType(blockItemClassType);
            return BlockItems.Where(x => 
                x.BlockItemType == blockItemType);
        }

        public IEnumerable<BlockItemMetadata> GetBlockItems(BlockMetadata blockMetadata) =>
            BlockItems.Where(x => 
                x.BlockItemType == blockMetadata.BlockItemType && 
                x.BlockId == blockMetadata.Id);

        public BlockItemMetadata GetBlockItem<TItem>(int valueId) where TItem : BlockItem =>
            GetBlockItems<TItem>().FirstOrDefault(x => 
                x.ValueId == valueId);

        public BlockItemMetadata GetBlockItem(BlockItem blockItem, BlockMetadata blockMetadata) =>
            GetBlockItem(
                BlockItemTypeAttributeHelper.GetBlockItemType(blockItem.GetType()),
                blockMetadata.Id,
                blockItem.Index.Value);

        public BlockItemMetadata GetBlockItem(BlockItemType blockItemType, int blockId, int index) =>
            BlockItems.Single(x =>
                x.BlockItemType == blockItemType &&
                x.BlockId == blockId &&
                x.Index == index);

        #endregion

        #region Methods (BlockItemValueMetadata)

        public List<BlockItemValueMetadata> GetBlockItemValues<TItem>() where TItem : BlockItem =>
            GetBlockItemValues(typeof(TItem));

        public List<BlockItemValueMetadata> GetBlockItemValues(Type itemType) =>
            _metadataByItemType[itemType];

        public BlockItemValueMetadata GetBlockItemValueByHash(BlockItem blockItem) =>
            GetBlockItemValueByHash(blockItem.GetType(), blockItem.HashString);

        private BlockItemValueMetadata GetBlockItemValueByHash(Type blockItemType, string hashString) =>
            GetBlockItemValues(blockItemType)
                .SingleOrDefault(x => x.Hash.Equals(hashString));

        public string GetBlockItemValueName<TItem>(int index, ReleaseMetadata releaseMetadata) where TItem : BlockItem
        {
            BlockMetadata blockMetadata = GetBlock<TItem>(releaseMetadata);
            BlockItemMetadata blockItemMetadata = GetBlockItem(blockMetadata.BlockItemType, blockMetadata.Id, index);
            BlockItemValueMetadata blockItemValueMetadata = GetBlockItemValues<TItem>().Single(x => x.Id == blockItemMetadata.ValueId);
            return blockItemValueMetadata.Name;
        }

        #endregion

        #region Methods (load/save)

        private List<TRecord> Load<TRecord>(string filenameWithoutExtension = null)
        {
            filenameWithoutExtension ??= GetFilenameWithoutExtension<TRecord>();
            string filename = GetFilename(filenameWithoutExtension);
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) {
                Mode = CsvMode.Escape
            };
            using (var reader = new StreamReader(new MetadataResourcesHelper().ReadEmbeddedResource(filename)))
            using (var csv = new CsvReader(reader, csvConfig))
                return csv.GetRecords<TRecord>().ToList();
        }

        public void Save<TRecord>(IEnumerable<TRecord> records, string filenameWithoutExtension = null, Action<CsvWriter, TRecord> write = null)
        {
            filenameWithoutExtension ??= GetFilenameWithoutExtension<TRecord>();
            string filename = GetFilename(filenameWithoutExtension);
            write ??= WriteRecord;
            using (var writer = new StreamWriter(filename))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteHeader<TRecord>();
                csv.NextRecord();
                foreach (TRecord record in records)
                    write.Invoke(csv, record);
            }
        }

        public void Save(IEnumerable<BlockItemValueMetadata> records, string filename = null) =>
            Save(records, filename, WriteRecord);

        private void WriteRecord<TRecord>(CsvWriter csv, TRecord record)
        {
            csv.WriteRecord(record);
            csv.NextRecord();
        }

        private void WriteRecord(CsvWriter csv, BlockItemValueMetadata iv)
        {
            csv.WriteField(iv.BlockItemType);
            csv.WriteField($"{iv.Id:00000}");
            csv.WriteField(iv.Hash);
            csv.WriteField($"{iv.Size1:000000}");
            csv.WriteField($"{iv.Size2:000000}");
            csv.WriteField(iv.Name);
            csv.NextRecord();
        }

        private string GetFilenameWithoutExtension<TRecord>() =>
            typeof(TRecord).GetCustomAttribute<TableAttribute>().Name;

        private string GetFilename(string filenameWithoutExtension) =>
            $"{filenameWithoutExtension}.csv";

        #endregion
    }
}
