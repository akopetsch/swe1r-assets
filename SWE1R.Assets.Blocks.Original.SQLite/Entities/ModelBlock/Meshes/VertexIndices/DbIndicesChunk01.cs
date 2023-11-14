// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.VertexIndices
{
    [Table("Model_IndicesChunk01")]
    public class DbIndicesChunk01 : DbModelStructure<IndicesChunk01>
    {
        public short Length { get; set; }
        public int MaxIndex { get; set; }
        public int P_StartVertex { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var c = (IndicesChunk01)node.Value;

            Length = c.Length;
            MaxIndex = c.MaxIndex;
            P_StartVertex = GetValuePosition(node.Graph, c.StartVertex.Value);
        }

        public override bool Equals(DbModelStructure<IndicesChunk01> other)
        {
            var _other = (DbIndicesChunk01)other;

            if (!base.Equals(_other))
                return false;

            if (Length != _other.Length) return false;
            if (MaxIndex != _other.MaxIndex) return false;
            if (P_StartVertex != _other.P_StartVertex) return false;
            
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbIndicesChunk01)
                return this.Equals((DbIndicesChunk01)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), Length, MaxIndex, P_StartVertex);
    }
}
