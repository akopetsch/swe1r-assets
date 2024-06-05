// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [Table($"{nameof(Model)}_{nameof(Material)}")]
    public class DbMaterial : DbBlockItemStructure<Material>
    {
        public int AlphaBpp { get; set; }
        public short Word_4 { get; set; }
        public int Ints_6_0 { get; set; }
        public int Ints_6_1 { get; set; }
        public int Ints_e_0 { get; set; }
        public int Ints_e_1 { get; set; }
        public short Unk_16 { get; set; }
        public int Bitmask1 { get; set; }
        public int Bitmask2 { get; set; }
        public short Unk_20 { get; set; }
        public byte Byte_22 { get; set; }
        public byte Byte_23 { get; set; }
        public byte Byte_24 { get; set; }
        public byte Byte_25 { get; set; }
        public short Unk_26 { get; set; }
        public short Unk_28 { get; set; }
        public short Unk_2a { get; set; }
        public short Unk_2c { get; set; }
        public byte Byte_2e { get; set; }
        public byte Byte_2f { get; set; }
        public byte Byte_30 { get; set; }
        public byte Byte_31 { get; set; }
        public short Unk_32 { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (Material)node.Value;

            AlphaBpp = x.AlphaBpp;
            Word_4 = x.Word_4;
            Ints_6_0 = x.Ints_6[0];
            Ints_6_1 = x.Ints_6[1];
            Ints_e_0 = x.Ints_e[0];
            Ints_e_1 = x.Ints_e[1];
            Unk_16 = x.Unk_16;
            Bitmask1 = x.Bitmask1;
            Bitmask2 = x.Bitmask2;
            Unk_20 = x.Unk_20;
            Byte_22 = x.Byte_22;
            Byte_23 = x.Byte_23;
            Byte_24 = x.Byte_24;
            Byte_25 = x.Byte_25;
            Unk_26 = x.Unk_26;
            Unk_28 = x.Unk_28;
            Unk_2a = x.Unk_2a;
            Unk_2c = x.Unk_2c;
            Byte_2e = x.Byte_2e;
            Byte_2f = x.Byte_2f;
            Byte_30 = x.Byte_30;
            Byte_31 = x.Byte_31;
            Unk_32 = x.Unk_32;
        }

        public override bool Equals(DbBlockItemStructure<Material> other)
        {
            var _other = (DbMaterial)other;

            if (!base.Equals(_other))
                return false;

            if (AlphaBpp != _other.AlphaBpp) return false;
            if (Word_4 != _other.Word_4) return false;
            if (Ints_6_0 != _other.Ints_6_0) return false;
            if (Ints_6_1 != _other.Ints_6_1) return false;
            if (Ints_e_0 != _other.Ints_e_0) return false;
            if (Ints_e_1 != _other.Ints_e_1) return false;
            if (Unk_16 != _other.Unk_16) return false;
            if (Bitmask1 != _other.Bitmask1) return false;
            if (Bitmask2 != _other.Bitmask2) return false;
            if (Unk_20 != _other.Unk_20) return false;
            if (Byte_22 != _other.Byte_22) return false;
            if (Byte_23 != _other.Byte_23) return false;
            if (Byte_24 != _other.Byte_24) return false;
            if (Byte_25 != _other.Byte_25) return false;
            if (Unk_26 != _other.Unk_26) return false;
            if (Unk_28 != _other.Unk_28) return false;
            if (Unk_2a != _other.Unk_2a) return false;
            if (Unk_2c != _other.Unk_2c) return false;
            if (Byte_2e != _other.Byte_2e) return false;
            if (Byte_2f != _other.Byte_2f) return false;
            if (Byte_30 != _other.Byte_30) return false;
            if (Byte_31 != _other.Byte_31) return false;
            if (Unk_32 != _other.Unk_32) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMaterial)
                return this.Equals((DbMaterial)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), 
                HashCode.Combine(AlphaBpp, Word_4, Ints_6_0, Ints_6_1, Ints_e_0, Ints_e_1, Unk_16, Ints_6_0),
                HashCode.Combine(Ints_6_1, Ints_e_0, Ints_e_1, Unk_16, Bitmask1, Bitmask2, Unk_20, Byte_22),
                HashCode.Combine(Byte_23, Byte_24, Byte_25, Unk_26, Unk_28, Unk_2a, Unk_2c, Byte_2e),
                HashCode.Combine(Byte_2f, Byte_30, Byte_31, Unk_32));
    }
}
