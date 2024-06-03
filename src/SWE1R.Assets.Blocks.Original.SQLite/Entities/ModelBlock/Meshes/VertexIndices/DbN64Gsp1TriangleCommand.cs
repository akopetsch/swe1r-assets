// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.VertexIndices
{
    [Table("Model_N64Gsp1TriangleCommand")]
    public class DbN64Gsp1TriangleCommand : DbBlockItemStructure<N64Gsp1TriangleCommand>
    {
        public byte V0 { get; set; }
        public byte V1 { get; set; }
        public byte V2 { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var c = (N64Gsp1TriangleCommand)node.Value;

            V0 = c.V0;
            V1 = c.V1;
            V2 = c.V2;
        }

        public override bool Equals(DbBlockItemStructure<N64Gsp1TriangleCommand> other)
        {
            var _other = (DbN64Gsp1TriangleCommand)other;

            if (!base.Equals(_other))
                return false;

            if (V0 != _other.V0) return false;
            if (V1 != _other.V1) return false;
            if (V2 != _other.V2) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbN64Gsp1TriangleCommand command)
                return Equals(command);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), V0, V1, V2);
    }
}
