// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Metadata
{
    [Table("BlockItem")]
    public class BlockItemMetadata
    {
        [Key, Column(Order = 0)] public BlockItemType BlockItemType { get; set; }
        [Key, Column(Order = 1)] public int BlockId { get; set; }
        [Key, Column(Order = 2)] public int Index { get; set; }
        public int ValueId { get; set; }

        public BlockItemMetadata() { }
        public BlockItemMetadata(BlockItem item, MetadataProvider metadataProvider)
        {
            BlockItemType = BlockItemTypeAttributeHelper.GetBlockItemType(item.GetType());
            BlockId = metadataProvider.GetBlockByHash(item.Block).Id;
            Index = item.Index.Value;
            ValueId = metadataProvider.GetBlockItemValueByHash(item).Id;
        }
    }
}
