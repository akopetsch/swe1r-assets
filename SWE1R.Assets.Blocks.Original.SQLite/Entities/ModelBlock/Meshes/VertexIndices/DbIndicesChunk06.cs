// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.VertexIndices
{
    [Table("Model_IndicesChunk06")]
    public class DbIndicesChunk06 : DbBlockItemStructure<IndicesChunk06>
    {
        public byte Index0 { get; set; }
        public byte Index1 { get; set; }
        public byte Index2 { get; set; }
        public byte Index3 { get; set; }
        public byte Index4 { get; set; }
        public byte Index5 { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var c = (IndicesChunk06)node.Value;

            Index0 = c.Index0;
            Index1 = c.Index1;
            Index2 = c.Index2;
            Index3 = c.Index3;
            Index4 = c.Index4;
            Index5 = c.Index5;
        }

        public override bool Equals(DbBlockItemStructure<IndicesChunk06> other)
        {
            var _other = (DbIndicesChunk06)other;

            if (!base.Equals(_other))
                return false;

            if (Index0 != _other.Index0) return false;
            if (Index1 != _other.Index1) return false;
            if (Index2 != _other.Index2) return false;
            if (Index3 != _other.Index3) return false;
            if (Index4 != _other.Index4) return false;
            if (Index5 != _other.Index5) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbIndicesChunk06)
                return this.Equals((DbIndicesChunk06)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), 
                Index0, Index1, Index2, 
                Index3, Index4, Index5);
    }
}
