// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.VertexIndices
{
    [Table("Model_N64Gsp1TriangleCommand")]
    public class DbN64Gsp1TriangleCommand : DbBlockItemStructure<N64Gsp1TriangleCommand>
    {
        public byte Index0 { get; set; }
        public byte Index1 { get; set; }
        public byte Index2 { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var c = (N64Gsp1TriangleCommand)node.Value;

            Index0 = c.Index0;
            Index1 = c.Index1;
            Index2 = c.Index2;
        }

        public override bool Equals(DbBlockItemStructure<N64Gsp1TriangleCommand> other)
        {
            var _other = (DbN64Gsp1TriangleCommand)other;

            if (!base.Equals(_other))
                return false;

            if (Index0 != _other.Index0) return false;
            if (Index1 != _other.Index1) return false;
            if (Index2 != _other.Index2) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbN64Gsp1TriangleCommand)
                return this.Equals((DbN64Gsp1TriangleCommand)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), Index0, Index1, Index2);
    }
}
