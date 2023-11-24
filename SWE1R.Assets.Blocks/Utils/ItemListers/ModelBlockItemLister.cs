// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using System;

namespace SWE1R.Assets.Blocks.TestApp.ItemListers
{
    public class ModelBlockItemLister : BlockItemLister<ModelBlockItem>
    {
        public ModelBlockItemLister(Block<ModelBlockItem> block, Action<string> writeLineAction) :
            base(block, writeLineAction)
        { }

        protected override string[] GetHeaderRow() =>
            new string[] { indexColumnName, "C?", metadataIdColumnName, nameColumnName };

        protected override string[] GetRecordRow(ModelBlockItem item) =>
            new string[] {
                GetIndexString(item),
                GetCompressedString(item),
                GetIdString(item),
                GetNameString(item),
            };

        private string GetCompressedString(ModelBlockItem item)
        {
            // compressed?
            string compressed = string.Empty;
            if (item.Part2.IsCompressed())
                compressed = "C!";
            else if (item.Part2.WasCompressed)
                compressed = "C";
            return compressed;
        }
    }
}
