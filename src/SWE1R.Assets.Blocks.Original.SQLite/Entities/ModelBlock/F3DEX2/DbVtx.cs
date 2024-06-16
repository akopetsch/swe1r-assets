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

        public short Position_X { get; set; }
        public short Position_Y { get; set; }
        public short Position_Z { get; set; }

        public short U { get; set; }
        public short V { get; set; }

        public byte Byte_C { get; set; }
        public byte Byte_D { get; set; }
        public byte Byte_E { get; set; }
        public byte Byte_F { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (Vtx)node.Value;

            Position_X = x.Position.X;
            Position_Y = x.Position.Y;
            Position_Z = x.Position.Z;

            U = x.U;
            V = x.V;

            Byte_C = x.Byte_C;
            Byte_D = x.Byte_D;
            Byte_E = x.Byte_E;
            Byte_F = x.Byte_F;
        }

        public override bool Equals(DbBlockItemStructure<Vtx> other)
        {
            var x = (DbVtx)other;

            if (!base.Equals(x))
                return false;

            if (Position_X != x.Position_X) return false;
            if (Position_Y != x.Position_Y) return false;
            if (Position_Z != x.Position_Z) return false;

            if (U != x.U) return false;
            if (V != x.V) return false;

            if (Byte_C != x.Byte_C) return false;
            if (Byte_D != x.Byte_D) return false;
            if (Byte_E != x.Byte_E) return false;
            if (Byte_F != x.Byte_F) return false;

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
                Position_X, Position_Y, Position_Z,
                U, V,
                Byte_C, Byte_D, Byte_E, Byte_F);
    }
}
