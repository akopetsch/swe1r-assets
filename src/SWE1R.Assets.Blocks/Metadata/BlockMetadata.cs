// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Metadata
{
    [Table("Block")]
    public class BlockMetadata
    {
        /* [Key, Column(Order = 0)] */ public BlockItemType BlockItemType { get; set; }
        /* [Key, Column(Order = 1)] */ public int Id { get; set; }
        public string Sha1Sum { get; set; }
        public Endianness Endianness { get; set; }
        public int Count { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }

        public BlockMetadata() { }
        public BlockMetadata(IBlock block, int id, string name)
        {
            BlockItemType = block.BlockItemType;
            Id = id;
            Sha1Sum = block.HashString;
            Endianness = block.Endianness;
            Count = block.Count;
            Size = block.Bytes.Length;
            Name = name;
        }
    }
}
