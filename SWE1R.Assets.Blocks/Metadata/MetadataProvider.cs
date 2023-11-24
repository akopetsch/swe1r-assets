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
using System.Collections.ObjectModel;
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

        private const string csvFileEnding = ".csv";
        private Dictionary<Type, IList<BlockItemMetadataByValue>> metadataByItemType;

        #endregion

        #region Properties

        public IList<BlockMetadata> Blocks { get; }
        public IList<BlockItemMetadata> BlockItems { get; }
        public IList<RacerMetadata> Racers { get; }
        public IList<TrackMetadata> Tracks { get; }

        #endregion

        #region Constructor

        public MetadataProvider()
        {
            Blocks = GetRecords<BlockMetadata>();
            BlockItems = GetRecords<BlockItemMetadata>();
            Racers = GetRecords<RacerMetadata>();
            Tracks = GetRecords<TrackMetadata>();

            metadataByItemType = new Dictionary<Type, IList<BlockItemMetadataByValue>>();
            metadataByItemType[typeof(Model)] = GetRecords<BlockItemMetadataByValue>(typeof(Model).Name + csvFileEnding);
            metadataByItemType[typeof(Spline)] = GetRecords<BlockItemMetadataByValue>(typeof(Spline).Name + csvFileEnding);
            metadataByItemType[typeof(Sprite)] = GetRecords<BlockItemMetadataByValue>(typeof(Sprite).Name + csvFileEnding);
            metadataByItemType[typeof(TextureBlockItem)] = GetRecords<BlockItemMetadataByValue>(typeof(TextureBlockItem).Name + csvFileEnding);
        }

        #endregion

        #region Methods

        public BlockMetadata GetBlock(int id) =>
            Blocks.FirstOrDefault(b => b.Id == id);
        public BlockMetadata GetBlock(string hash) =>
            Blocks.FirstOrDefault(b => b.Hash.Equals(hash));

        public BlockItemMetadata GetBlockItem(int blockId, int id) =>
            BlockItems.FirstOrDefault(i => i.Block == blockId && i.Index == id);

        public IList<BlockItemMetadataByValue> GetBlockItemValues<TItem>() where TItem : BlockItem =>
            metadataByItemType[typeof(TItem)];

        public BlockItemMetadataByValue GetBlockItemValue<TItem>(int id) where TItem : BlockItem =>
            metadataByItemType[typeof(TItem)].FirstOrDefault(iv => iv.Id == id);

        public BlockItemMetadataByValue GetBlockItemValueByValue<TItem>(TItem item) where TItem : BlockItem =>
            metadataByItemType[typeof(TItem)].FirstOrDefault(iv => iv.Hash.Equals(item.HashString));

        public string GetNameByIndex<TItem>(TItem item) where TItem : BlockItem =>
            GetBlockItemValue<TItem>(item.Index.Value)?.Name;

        public string GetNameByIndex<TItem>(int index) where TItem : BlockItem =>
            GetBlockItemValue<TItem>(index)?.Name;

        public string GetNameByValue<TItem>(TItem item) where TItem : BlockItem =>
            GetBlockItemValueByValue(item)?.Name;

        public string GetName<TItem>(TItem item) where TItem : BlockItem =>
            GetNameByValue(item) ?? GetNameByIndex(item);

        public string GetNameOrUnknown<TItem>(TItem item) where TItem : BlockItem =>
            GetName(item) ?? $"Unknown";

        private ReadOnlyCollection<TRecord> GetRecords<TRecord>(string filename = null)
        {
            filename ??= GetFilename<TRecord>();
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) {
                Mode = CsvMode.Escape
            };
            using (var reader = new StreamReader(ResourceHelper.ReadEmbeddedResource(filename)))
            using (var csv = new CsvReader(reader, csvConfig))
                return csv.GetRecords<TRecord>().ToList().AsReadOnly();
        }

        private void Save<TRecord>(IEnumerable<TRecord> records, Action<CsvWriter, TRecord> write = null)
        {
            string filename = GetFilename<TRecord>();
            write ??= (c, t) => c.WriteRecord(t);
            using (var writer = new StreamWriter(filename))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteHeader<TRecord>();
                csv.NextRecord();
                foreach (var r in records)
                    write.Invoke(csv, r);
            }
        }

        private void Save(IEnumerable<BlockItemMetadataByValue> records) =>
            Save(records, Write);

        private void Write(CsvWriter csv, BlockItemMetadataByValue iv)
        {
            csv.WriteField(iv.BlockType);
            csv.WriteField($"{iv.Id:0000}");
            csv.WriteField(iv.Hash);
            csv.WriteField($"{iv.Size1:000000}");
            csv.WriteField($"{iv.Size2:000000}");
            csv.WriteField(iv.Name);
            csv.NextRecord();
        }

        private string GetFilename<TRecord>() =>
            typeof(TRecord).GetCustomAttribute<TableAttribute>().Name + csvFileEnding;

        #endregion
    }
}
