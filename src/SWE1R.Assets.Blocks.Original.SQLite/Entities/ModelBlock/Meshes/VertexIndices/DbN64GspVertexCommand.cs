// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.VertexIndices
{
    [Table("Model_N64GspVertexCommand")]
    public class DbN64GspVertexCommand : DbBlockItemStructure<N64GspVertexCommand>
    {
        public int P_V { get; set; }
        public int N { get; set; }
        public int V0 { get; set; }
        public byte V0PlusN { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var c = (N64GspVertexCommand)node.Value;

            P_V = GetValuePosition(node.Graph, c.V.Value);
            N = c.N;
            V0 = c.V0;
            V0PlusN = c.V0PlusN;
        }

        public override bool Equals(DbBlockItemStructure<N64GspVertexCommand> other)
        {
            var _other = (DbN64GspVertexCommand)other;

            if (!base.Equals(_other))
                return false;

            if (P_V != _other.P_V) return false;
            if (N != _other.N) return false;
            if (V0 != _other.V0) return false;
            if (V0PlusN != _other.V0PlusN) return false;

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
            HashCode.Combine(base.GetHashCode(),
                P_V, N, V0, V0PlusN);
    }
}
