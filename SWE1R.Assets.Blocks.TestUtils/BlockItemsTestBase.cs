// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.Metadata;

namespace SWE1R.Assets.Blocks.TestUtils
{
    public abstract class BlockItemsTestBase<TItem> where TItem : BlockItem, new()
    {
        #region Fields

        private const string dumpInputSuffix = "in";
        private const string dumpOutputSuffix = "out";
        private const string dumpFolderName = "dump";

        private readonly BlockItemDumper itemDumper;
        protected readonly MetadataProvider metadataProvider;

        #endregion

        #region Properties

        public Block<TItem> Block { get; }

        #endregion

        #region Constructor

        protected BlockItemsTestBase(Block<TItem> block)
        {
            Block = block;

            itemDumper = new BlockItemDumper(Path.Combine(dumpFolderName, typeof(TItem).Name));
            metadataProvider = new MetadataProvider();
        }

        #endregion

        #region Methods

        public void CompareItems(params int[] indices)
        {
            if (indices.Length == 0)
                indices = Enumerable.Range(0, Block.Count).ToArray();

            foreach (int index in indices)
                CompareItem(index);
        }

        protected void CompareItem(int index)
        {
            // print
            PrintItemIndex(index);

            // compare
            CompareItemInternal(index);

            // print
            PrintItemDone();
        }

        protected abstract void CompareItemInternal(int index);

        protected TItem DeserializeItem(int i, out ByteSerializerContext context)
        {
            // dump/print
            TItem item = Block[i];
            itemDumper.DumpItemPartsBytes(item, i, dumpInputSuffix);
            PrintItemName($"name {metadataProvider.GetNameByValue(item)}");

            // load
            item.Load(out context);

            // dump
            itemDumper.DumpItemLog(context, i, dumpInputSuffix);

            return item;
        }

        protected void SerializeItem(TItem item, int i, out ByteSerializerContext context)
        {
            // save
            item.Save(out context);

            // dump
            itemDumper.DumpItemPartsBytes(item, i, dumpOutputSuffix);
            itemDumper.DumpItemLog(context, i, dumpOutputSuffix);
        }

        protected abstract void PrintItemIndex(int index);
        protected abstract void PrintItemName(string nameString);
        protected abstract void PrintItemDone();

        #endregion
    }
}
