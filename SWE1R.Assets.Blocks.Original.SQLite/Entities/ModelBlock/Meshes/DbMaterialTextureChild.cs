// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [Table("Model_MaterialTextureChild")]
    public class DbMaterialTextureChild : DbBlockItemStructure<MaterialTextureChild>
    {
        public byte Byte_1 { get; set; }
        public byte Byte_2 { get; set; }
        public byte Byte_3 { get; set; }
        public byte Byte_4 { get; set; }
        public byte Byte_5 { get; set; }
        public byte Byte_6 { get; set; }
        public byte Byte_7 { get; set; }

        public byte Byte_c { get; set; }
        public byte Byte_d { get; set; }
        public byte Byte_e { get; set; }
        public byte Byte_f { get; set; }
        
        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var c = (MaterialTextureChild)node.Value;

            Byte_1 = c.Byte_1;
            Byte_2 = c.Byte_2;
            Byte_3 = (byte)c.DimensionsBitmask;
            Byte_4 = c.Byte_4;
            Byte_5 = c.Byte_5;
            Byte_6 = c.Byte_6;
            Byte_7 = c.Byte_7;
            Byte_c = c.Byte_c;
            Byte_d = c.Byte_d;
            Byte_e = c.Byte_e;
            Byte_f = c.Byte_f;
        }

        public override bool Equals(DbBlockItemStructure<MaterialTextureChild> other)
        {
            var _other = (DbMaterialTextureChild)other;

            if (!base.Equals(_other))
                return false;

            if (Byte_1 != _other.Byte_1) return false;
            if (Byte_2 != _other.Byte_2) return false;
            if (Byte_3 != _other.Byte_3) return false;
            if (Byte_4 != _other.Byte_4) return false;
            if (Byte_5 != _other.Byte_5) return false;
            if (Byte_6 != _other.Byte_6) return false;
            if (Byte_7 != _other.Byte_7) return false;
            if (Byte_c != _other.Byte_c) return false;
            if (Byte_d != _other.Byte_d) return false;
            if (Byte_e != _other.Byte_e) return false;
            if (Byte_f != _other.Byte_f) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMaterialTextureChild)
                return this.Equals((DbMaterialTextureChild)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), 
                HashCode.Combine(Byte_1, Byte_2, Byte_3, Byte_4, Byte_5, Byte_6, Byte_7), 
                HashCode.Combine(Byte_c, Byte_d, Byte_e, Byte_f));
    }
}
