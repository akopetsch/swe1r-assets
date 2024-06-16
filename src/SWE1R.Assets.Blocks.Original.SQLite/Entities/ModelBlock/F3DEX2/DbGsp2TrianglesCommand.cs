// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.F3DEX2;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.F3DEX2
{
    [Table($"{nameof(Model)}_{nameof(Gsp2TrianglesCommand)}")]
    public class DbGsp2TrianglesCommand : DbBlockItemStructure<Gsp2TrianglesCommand>
    {
        #region Properties

        public byte V00 { get; set; }
        public byte V01 { get; set; }
        public byte V02 { get; set; }
        public byte V10 { get; set; }
        public byte V11 { get; set; }
        public byte V12 { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (Gsp2TrianglesCommand)node.Value;

            V00 = x.V00;
            V01 = x.V01;
            V02 = x.V02;

            V10 = x.V10;
            V11 = x.V11;
            V12 = x.V12;
        }

        public override bool Equals(DbBlockItemStructure<Gsp2TrianglesCommand> other)
        {
            var x = (DbGsp2TrianglesCommand)other;

            if (!base.Equals(x))
                return false;

            if (V00 != x.V00) return false;
            if (V01 != x.V01) return false;
            if (V02 != x.V02) return false;

            if (V10 != x.V10) return false;
            if (V11 != x.V11) return false;
            if (V12 != x.V12) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbGsp2TrianglesCommand x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                V00, V01, V02,
                V10, V11, V12);
    }
}
