// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.VertexIndices
{
    [Table("Model_N64GspVertexCommand")]
    public class DbN64GspVertexCommand : DbBlockItemStructure<N64GspVertexCommand>
    {
        public byte VerticesCount { get; set; }
        public int MaxIndex { get; set; }
        public int P_V { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var c = (N64GspVertexCommand)node.Value;

            VerticesCount = c.VerticesCount;
            MaxIndex = c.V0PlusN;
            P_V = GetValuePosition(node.Graph, c.V.Value);
        }

        public override bool Equals(DbBlockItemStructure<N64GspVertexCommand> other)
        {
            var _other = (DbN64GspVertexCommand)other;

            if (!base.Equals(_other))
                return false;

            if (VerticesCount != _other.VerticesCount) return false;
            if (MaxIndex != _other.MaxIndex) return false;
            if (P_V != _other.P_V) return false;
            
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbN64GspVertexCommand)
                return this.Equals((DbN64GspVertexCommand)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), VerticesCount, MaxIndex, P_V);
    }
}
