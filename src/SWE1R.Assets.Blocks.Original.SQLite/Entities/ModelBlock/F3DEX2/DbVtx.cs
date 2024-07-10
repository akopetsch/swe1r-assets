// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.F3DEX2;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.F3DEX2
{
    [Table($"{nameof(Model)}_{nameof(Vtx)}")]
    public class DbVtx : DbBlockItemStructure<Vtx>
    {
        #region Properties

        public short Ob_X { get; set; }
        public short Ob_Y { get; set; }
        public short Ob_Z { get; set; }

        public short Tc_X { get; set; }
        public short Tc_Y { get; set; }

        public byte Byte_C { get; set; }
        public byte Byte_D { get; set; }
        public byte Byte_E { get; set; }
        public byte A { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (Vtx)node.Value;

            Ob_X = x.Ob.X;
            Ob_Y = x.Ob.Y;
            Ob_Z = x.Ob.Z;

            Tc_X = x.Tc.X;
            Tc_Y = x.Tc.Y;

            Byte_C = x.Byte_C;
            Byte_D = x.Byte_D;
            Byte_E = x.Byte_E;
            A = x.A;
        }

        public override bool Equals(DbBlockItemStructure<Vtx> other)
        {
            var x = (DbVtx)other;

            if (!base.Equals(x))
                return false;

            if (Ob_X != x.Ob_X) return false;
            if (Ob_Y != x.Ob_Y) return false;
            if (Ob_Z != x.Ob_Z) return false;

            if (Tc_X != x.Tc_X) return false;
            if (Tc_Y != x.Tc_Y) return false;

            if (Byte_C != x.Byte_C) return false;
            if (Byte_D != x.Byte_D) return false;
            if (Byte_E != x.Byte_E) return false;
            if (A != x.A) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbVtx x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                Ob_X, Ob_Y, Ob_Z,
                Tc_X, Tc_Y,
                Byte_C, Byte_D, Byte_E, A);
    }
}
