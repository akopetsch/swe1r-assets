// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.N64GspCommands
{
    [Table($"{nameof(Model)}_{nameof(N64GspCullDisplayListCommand)}")]
    public class DbN64GspCullDisplayListCommand : DbBlockItemStructure<N64GspCullDisplayListCommand>
    {
        #region Properties

        public byte V0 { get; set; }
        public byte VN { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (N64GspCullDisplayListCommand)node.Value;

            V0 = x.V0;
            VN = x.VN;
        }

        public override bool Equals(DbBlockItemStructure<N64GspCullDisplayListCommand> other)
        {
            var x = (DbN64GspCullDisplayListCommand)other;

            if (!base.Equals(x))
                return false;

            if (V0 != x.V0) return false;
            if (VN != x.VN) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbN64GspCullDisplayListCommand x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                V0, VN);
    }
}
