// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Metadata
{
    [Table("Block")]
    public class BlockMetadata
    {
        [Key, Column(Order = 0)] public BlockItemType BlockItemType { get; set; }
        [Key, Column(Order = 1)] public int Id { get; set; }
        public string Hash { get; set; }
        public int Count { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }

        public BlockMetadata() { }
        public BlockMetadata(IBlock block, int id, string name)
        {
            BlockItemType = block.BlockItemType;
            Id = id;
            Hash = block.HashString;
            Count = block.Count;
            Size = block.Bytes.Length;
            Name = name;
        }
    }
}
