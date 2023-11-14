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
        public int BlockType { get; set; }
        [Key] public int Id { get; set; }
        public string Hash { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }

        public BlockMetadata() { }
        public BlockMetadata(IBlock block)
        {
            // Id = ?
            //BlockType = block.Type.Value;
            Hash = block.HashString;
            Size = block.Bytes.Length;
            // Name = ?
        }
    }
}
