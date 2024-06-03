// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.N64GspCommands
{
    [Table("Model_N64GspCullDisplayListCommand")]
    public class DbN64GspCullDisplayListCommand : DbBlockItemStructure<N64GspCullDisplayListCommand>
    {
        public byte V0 { get; set; }
        public byte VN { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var c = (N64GspCullDisplayListCommand)node.Value;

            V0 = c.V0;
            VN = c.VN;
        }

        public override bool Equals(DbBlockItemStructure<N64GspCullDisplayListCommand> other)
        {
            var _other = (DbN64GspCullDisplayListCommand)other;

            if (!base.Equals(_other))
                return false;

            if (V0 != _other.V0) return false;
            if (VN != _other.VN) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbN64GspCullDisplayListCommand)
                return this.Equals((DbN64GspCullDisplayListCommand)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(),
                V0, VN);
    }
}
