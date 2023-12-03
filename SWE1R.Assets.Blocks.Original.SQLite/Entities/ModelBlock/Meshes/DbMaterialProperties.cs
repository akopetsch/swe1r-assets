// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [Table("Model_MaterialProperties")]
    public class DbMaterialProperties : DbBlockItemStructure<MaterialProperties>
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

            var mp = (MaterialProperties)node.Value;

            AlphaBpp = mp.AlphaBpp;
            Word_4 = mp.Word_4;
            Ints_6_0 = mp.Ints_6[0];
            Ints_6_1 = mp.Ints_6[1];
            Ints_e_0 = mp.Ints_e[0];
            Ints_e_1 = mp.Ints_e[1];
            Unk_16 = mp.Unk_16;
            Bitmask1 = mp.Bitmask1;
            Bitmask2 = mp.Bitmask2;
            Unk_20 = mp.Unk_20;
            Byte_22 = mp.Byte_22;
            Byte_23 = mp.Byte_23;
            Byte_24 = mp.Byte_24;
            Byte_25 = mp.Byte_25;
            Unk_26 = mp.Unk_26;
            Unk_28 = mp.Unk_28;
            Unk_2a = mp.Unk_2a;
            Unk_2c = mp.Unk_2c;
            Byte_2e = mp.Byte_2e;
            Byte_2f = mp.Byte_2f;
            Byte_30 = mp.Byte_30;
            Byte_31 = mp.Byte_31;
            Unk_32 = mp.Unk_32;
        }

        public override bool Equals(DbBlockItemStructure<MaterialProperties> other)
        {
            var _other = (DbMaterialProperties)other;

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
            if (obj is DbMaterialProperties)
                return this.Equals((DbMaterialProperties)obj);
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
