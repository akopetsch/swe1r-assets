// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.F3DEX2;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.F3DEX2
{
    [Table($"{nameof(Model)}_{nameof(GSpCullDisplayListCommand)}")]
    public class DbGSpCullDisplayListCommand : DbBlockItemStructure<GSpCullDisplayListCommand>
    {
        #region Properties

        public byte V0 { get; set; }
        public byte VN { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (GSpCullDisplayListCommand)node.Value;

            V0 = x.V0;
            VN = x.VN;
        }

        public override bool Equals(DbBlockItemStructure<GSpCullDisplayListCommand> other)
        {
            var x = (DbGSpCullDisplayListCommand)other;

            if (!base.Equals(x))
                return false;

            if (V0 != x.V0) return false;
            if (VN != x.VN) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbGSpCullDisplayListCommand x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                V0, VN);
    }
}
