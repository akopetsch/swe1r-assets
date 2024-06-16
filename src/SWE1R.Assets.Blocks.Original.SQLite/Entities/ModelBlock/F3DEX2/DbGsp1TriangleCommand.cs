// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.F3DEX2;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.F3DEX2
{
    [Table($"{nameof(Model)}_{nameof(Gsp1TriangleCommand)}")]
    public class DbGsp1TriangleCommand : DbBlockItemStructure<Gsp1TriangleCommand>
    {
        #region Properties

        public byte V0 { get; set; }
        public byte V1 { get; set; }
        public byte V2 { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (Gsp1TriangleCommand)node.Value;

            V0 = x.V0;
            V1 = x.V1;
            V2 = x.V2;
        }

        public override bool Equals(DbBlockItemStructure<Gsp1TriangleCommand> other)
        {
            var x = (DbGsp1TriangleCommand)other;

            if (!base.Equals(x))
                return false;

            if (V0 != x.V0) return false;
            if (V1 != x.V1) return false;
            if (V2 != x.V2) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbGsp1TriangleCommand x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                V0, V1, V2);
    }
}
