// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.Metadata;

namespace SWE1R.Assets.Blocks.TestUtils
{
    public abstract class BlockItemsTestBase<TItem> where TItem : BlockItem, new()
    {
        #region Fields

        private readonly BlockItemDumper _inputItemDumper;
        private readonly BlockItemDumper _outputItemDumper;
        private readonly MetadataProvider _metadataProvider;

        #endregion

        #region Constructor

        protected BlockItemsTestBase()
        {
            string dumpPath = Path.Combine("dump", GetType().FullName);
            _inputItemDumper = new BlockItemDumper(dumpPath, "in");
            _outputItemDumper = new BlockItemDumper(dumpPath, "out");
            _metadataProvider = new MetadataProvider();
        }

        #endregion

        #region Methods

        protected abstract TItem GetItem(int valueId);

        protected void CompareItem(int valueId)
        {
            // print
            PrintItemValueId(valueId);

            // compare
            CompareItemInternal(valueId);

            // print
            PrintItemDone();
        }

        protected abstract void CompareItemInternal(int valueId);

        protected TItem DeserializeItem(int valueId, out ByteSerializerContext context)
        {
            // dump/print
            TItem item = GetItem(valueId);
            _inputItemDumper.DumpItemPartsBytes(item, valueId);
            PrintItemName($"name {_metadataProvider.GetBlockItemValueByHash(item).Name}");

            // load
            item.Load(out context);

            // dump
            _inputItemDumper.DumpItemLog(context, valueId);

            return item;
        }

        protected void SerializeItem(TItem item, int valueId, out ByteSerializerContext context)
        {
            // save
            item.Save(out context);

            // dump
            _outputItemDumper.DumpItemPartsBytes(item, valueId);
            _outputItemDumper.DumpItemLog(context, valueId);
        }

        protected abstract void PrintItemValueId(int valueId);
        protected abstract void PrintItemName(string nameString);
        protected abstract void PrintItemDone();

        #endregion
    }
}
