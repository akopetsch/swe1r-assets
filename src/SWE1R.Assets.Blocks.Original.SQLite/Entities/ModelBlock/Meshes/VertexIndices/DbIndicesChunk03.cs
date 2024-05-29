// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.VertexIndices
{
    [Table("Model_IndicesChunk03")]
    public class DbIndicesChunk03 : DbBlockItemStructure<IndicesChunk03>
    {
        public byte Index { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var c = (IndicesChunk03)node.Value;

            Index = c.MaxIndex;
        }

        public override bool Equals(DbBlockItemStructure<IndicesChunk03> other)
        {
            var _other = (DbIndicesChunk03)other;

            if (!base.Equals(_other))
                return false;

            if (Index != _other.Index) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbIndicesChunk03)
                return this.Equals((DbIndicesChunk03)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), Index);
    }
}
