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
        [Key, Column(Order = 0)] public int Block { get; set; }
        [Key, Column(Order = 1)] public int Index { get; set; }
        public int ValueId { get; set; }
        public string Name { get; set; }

        public BlockItemMetadata() { }
        public BlockItemMetadata(BlockItem item)
        {
            // Block = ?
            Index = item.Index.Value;
            // ValueId = ?
            // Name = ?
        }
    }
}
