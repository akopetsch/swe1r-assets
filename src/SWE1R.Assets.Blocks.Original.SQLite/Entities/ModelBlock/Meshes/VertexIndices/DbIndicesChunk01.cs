// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.VertexIndices
{
    [Table("Model_IndicesChunk01")]
    public class DbIndicesChunk01 : DbBlockItemStructure<N64GspVertexCommand>
    {
        public byte VerticesCount { get; set; }
        public int MaxIndex { get; set; }
        public int P_StartVertex { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var c = (N64GspVertexCommand)node.Value;

            VerticesCount = c.VerticesCount;
            MaxIndex = c.NextIndicesBase;
            P_StartVertex = GetValuePosition(node.Graph, c.StartVertex.Value);
        }

        public override bool Equals(DbBlockItemStructure<N64GspVertexCommand> other)
        {
            var _other = (DbIndicesChunk01)other;

            if (!base.Equals(_other))
                return false;

            if (VerticesCount != _other.VerticesCount) return false;
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
            HashCode.Combine(base.GetHashCode(), VerticesCount, MaxIndex, P_StartVertex);
    }
}
