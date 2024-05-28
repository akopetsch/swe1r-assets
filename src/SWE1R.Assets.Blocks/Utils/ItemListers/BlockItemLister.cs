// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Metadata;
using System;

namespace SWE1R.Assets.Blocks.TestApp.ItemListers
{
    public class BlockItemLister<TItem> : IBlockItemLister where TItem : BlockItem, new()
    {
        #region Fields

        protected const string _indexColumnName = "Index";
        protected const string _metadataIdColumnName = "M-Id";
        protected const string _nameColumnName = "Name";
        private readonly MetadataProvider _metadataProvider = new MetadataProvider();

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
            new string[] { _indexColumnName, _metadataIdColumnName, _nameColumnName };

        protected virtual string[] GetRecordRow(TItem item) =>
            new string[] {
                GetIndexString(item),
                GetIdString(item),
                GetNameString(item),
            };

        protected string GetIndexString(TItem item) =>
            item.Index.Value.ToString("d4");

        protected string GetIdString(TItem item) =>
            _metadataProvider.GetBlockItemValueByHash(item)?.Id.ToString("d4");

        protected string GetNameString(TItem item) =>
            _metadataProvider.GetBlockItemValueByHash(item)?.Name;

        #endregion
    }
}
