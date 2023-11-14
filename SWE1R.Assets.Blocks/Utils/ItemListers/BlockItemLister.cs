// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Metadata;
using System;

namespace SWE1R.Assets.Blocks.TestApp.ItemListers
{
    public class BlockItemLister<TItem> : IBlockItemLister where TItem : BlockItem, new()
    {
        #region Fields

        protected const string indexColumnName = "Index";
        protected const string metadataIdColumnName = "M-Id";
        protected const string nameColumnName = "Name";

        protected readonly MetadataProvider metadataProvider = new MetadataProvider();

        #endregion

        #region Properties

        public Block<TItem> Block { get; }
        public Action<string> WriteLineAction { get; }
        
        #endregion

        #region Constructor

        public BlockItemLister(Block<TItem> block, Action<string> writeLineAction)
        {
            Block = block;
            WriteLineAction = writeLineAction;
        }

        #endregion

        #region Methods

        public void Run()
        {
            string[] headerRow = GetHeaderRow();
            var mdTable = new MarkdownTableHelper(headerRow);
            WriteLineAction(mdTable.HeaderRow);
            WriteLineAction(mdTable.SeparatorRow);
            for (int i = 0; i < Block.Count; i++)
            {
                TItem item = Block[i];
                string[] recordRow = GetRecordRow(item);
                WriteLineAction(mdTable.GetRowString(recordRow));
            }
        }

        protected virtual string[] GetHeaderRow() =>
            new string[] { indexColumnName, metadataIdColumnName, nameColumnName };

        protected virtual string[] GetRecordRow(TItem item) =>
            new string[] {
                GetIndexString(item),
                GetIdString(item),
                GetNameString(item),
            };

        protected string GetIndexString(TItem item) =>
            item.Index.Value.ToString("d4");

        protected string GetIdString(TItem item) =>
            metadataProvider.GetBlockItemValueByValue(item)?.Id.ToString("d4");

        protected string GetNameString(TItem item)
        {
            string nameByValue = metadataProvider.GetBlockItemValueByValue(item)?.Name;
            string nameByIndex = metadataProvider.GetNameByIndex(item);
            return nameByValue ?? $"? ({nameByIndex ?? "?"})";
        }

        #endregion
    }
}
