﻿// SPDX-License-Identifier: MIT

using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Metadata
{
    [Table("BlockItemValue")]
    public class BlockItemValueMetadata
    {
        /* [Key, Column(Order = 0)] */ public BlockItemType BlockItemType { get; set; }
        /* [Key, Column(Order = 1)] */ public int Id { get; set; }
        public string Hash { get; set; }
        public int? Size1 { get; set; }
        public int? Size2 { get; set; }
        public string Name { get; set; }

        public BlockItemValueMetadata() { }
        public BlockItemValueMetadata(BlockItem item)
        {
            BlockItemType = BlockItemTypeAttributeHelper.GetBlockItemType(item.GetType());
            Id = item.Index.Value;
            Hash = item.HashString;
            Size1 = item.Parts.Length >= 1 ? (int?)item.Parts[0].Length : null;
            Size2 = item.Parts.Length >= 2 ? (int?)item.Parts[1].Length : null;
        }
    }
}
