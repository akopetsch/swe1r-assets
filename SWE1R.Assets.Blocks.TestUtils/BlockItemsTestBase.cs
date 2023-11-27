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

        private readonly BlockItemDumper inputItemDumper;
        private readonly BlockItemDumper outputItemDumper;
        protected readonly MetadataProvider metadataProvider;

        #endregion

        #region Properties

        public Block<TItem> Block { get; }

        #endregion

        #region Constructor

        protected BlockItemsTestBase(Block<TItem> block)
        {
            Block = block;

            string dumpPath = Path.Combine("dump", GetType().FullName);
            inputItemDumper = new BlockItemDumper(dumpPath, "in");
            outputItemDumper = new BlockItemDumper(dumpPath, "out");
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
            inputItemDumper.DumpItemPartsBytes(item, i);
            PrintItemName($"name {metadataProvider.GetNameByValue(item)}");

            // load
            item.Load(out context);

            // dump
            inputItemDumper.DumpItemLog(context, i);

            return item;
        }

        protected void SerializeItem(TItem item, int i, out ByteSerializerContext context)
        {
            // save
            item.Save(out context);

            // dump
            outputItemDumper.DumpItemPartsBytes(item, i);
            outputItemDumper.DumpItemLog(context, i);
        }

        protected abstract void PrintItemIndex(int index);
        protected abstract void PrintItemName(string nameString);
        protected abstract void PrintItemDone();

        #endregion
    }
}
