// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.VertexIndices
{
    [Table("Model_N64Gsp2TrianglesCommand")]
    public class DbN64Gsp2TrianglesCommand : DbBlockItemStructure<N64Gsp2TrianglesCommand>
    {
        public byte V00 { get; set; }
        public byte V01 { get; set; }
        public byte V02 { get; set; }
        public byte V10 { get; set; }
        public byte V11 { get; set; }
        public byte V12 { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var c = (N64Gsp2TrianglesCommand)node.Value;

            V00 = c.V00;
            V01 = c.V01;
            V02 = c.V02;

            V10 = c.V10;
            V11 = c.V11;
            V12 = c.V12;
        }

        public override bool Equals(DbBlockItemStructure<N64Gsp2TrianglesCommand> other)
        {
            var _other = (DbN64Gsp2TrianglesCommand)other;

            if (!base.Equals(_other))
                return false;

            if (V00 != _other.V00) return false;
            if (V01 != _other.V01) return false;
            if (V02 != _other.V02) return false;

            if (V10 != _other.V10) return false;
            if (V11 != _other.V11) return false;
            if (V12 != _other.V12) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbN64Gsp2TrianglesCommand command)
                return Equals(command);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), 
                V00, V01, V02, 
                V10, V11, V12);
    }
}
