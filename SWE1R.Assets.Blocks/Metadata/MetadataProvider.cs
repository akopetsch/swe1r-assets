// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using CsvHelper;
using CsvHelper.Configuration;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.Resources;
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

        private readonly Dictionary<Type, List<BlockItemMetadataByValue>> _metadataByItemType;

        #endregion

        #region Properties

        public List<BlockMetadata> Blocks { get; }
        public List<BlockItemMetadata> BlockItems { get; }
        public List<RacerMetadata> Racers { get; }
        public List<TrackMetadata> Tracks { get; }

        #endregion

        #region Constructor

        public MetadataProvider()
        {
            Blocks = Load<BlockMetadata>();
            BlockItems = Load<BlockItemMetadata>();
            Racers = Load<RacerMetadata>();
            Tracks = Load<TrackMetadata>();

            _metadataByItemType = new Dictionary<Type, List<BlockItemMetadataByValue>>();
            _metadataByItemType[typeof(ModelBlockItem)] = Load<BlockItemMetadataByValue>(typeof(ModelBlockItem).Name);
            _metadataByItemType[typeof(SplineBlockItem)] = Load<BlockItemMetadataByValue>(typeof(SplineBlockItem).Name);
            _metadataByItemType[typeof(SpriteBlockItem)] = Load<BlockItemMetadataByValue>(typeof(SpriteBlockItem).Name);
            _metadataByItemType[typeof(TextureBlockItem)] = Load<BlockItemMetadataByValue>(typeof(TextureBlockItem).Name);
        }

        #endregion

        #region Methods

        public BlockItemMetadata GetBlockItem(int blockId, int id) =>
            BlockItems.FirstOrDefault(i => i.Block == blockId && i.Index == id);

        public List<BlockItemMetadataByValue> GetBlockItemValues<TItem>() where TItem : BlockItem =>
            _metadataByItemType[typeof(TItem)];

        public BlockItemMetadataByValue GetBlockItemValue<TItem>(int id) where TItem : BlockItem =>
            _metadataByItemType[typeof(TItem)].FirstOrDefault(iv => iv.Id == id);

        public BlockItemMetadataByValue GetBlockItemValueByHash<TItem>(TItem item) where TItem : BlockItem =>
            _metadataByItemType[typeof(TItem)].FirstOrDefault(iv => iv.Hash.Equals(item.HashString));

        #endregion

        #region Methods (name)

        public string GetNameByIndex<TItem>(TItem item) where TItem : BlockItem =>
            GetBlockItemValue<TItem>(item.Index.Value)?.Name;

        public string GetNameByIndex<TItem>(int index) where TItem : BlockItem =>
            GetBlockItemValue<TItem>(index)?.Name;

        public string GetNameByValue<TItem>(TItem item) where TItem : BlockItem =>
            GetBlockItemValueByHash(item)?.Name;

        public string GetName<TItem>(TItem item) where TItem : BlockItem =>
            GetNameByValue(item) ?? GetNameByIndex(item);

        public string GetNameOrUnknown<TItem>(TItem item) where TItem : BlockItem =>
            GetName(item) ?? $"Unknown";

        #endregion

        #region Methods (load/save)

        private List<TRecord> Load<TRecord>(string filenameWithoutExtension = null)
        {
            filenameWithoutExtension ??= GetFilenameWithoutExtension<TRecord>();
            string filename = GetFilename(filenameWithoutExtension);
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) {
                Mode = CsvMode.Escape
            };
            using (var reader = new StreamReader(new BlocksResourceHelper().ReadEmbeddedResource(filename)))
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

        public void Save(IEnumerable<BlockItemMetadataByValue> records, string filename = null) =>
            Save(records, filename, WriteRecord);

        private void WriteRecord<TRecord>(CsvWriter csv, TRecord record)
        {
            csv.WriteRecord(record);
            csv.NextRecord();
        }

        private void WriteRecord(CsvWriter csv, BlockItemMetadataByValue iv)
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
