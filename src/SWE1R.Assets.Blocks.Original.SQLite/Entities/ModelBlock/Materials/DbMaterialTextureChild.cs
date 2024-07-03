// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Materials
{
    [Table($"{nameof(Model)}_{nameof(MaterialTextureChild)}")]
    public class DbMaterialTextureChild : DbBlockItemStructure<MaterialTextureChild>
    {
        #region Properties

        public byte Byte_1 { get; set; }
        public byte Byte_2 { get; set; }
        public byte DimensionsBitmask { get; set; }
        public byte Byte_4 { get; set; }
        public byte Byte_5 { get; set; }
        public byte Byte_6 { get; set; }
        public byte Byte_7 { get; set; }

        public byte Byte_c { get; set; }
        public byte Byte_d { get; set; }
        public byte Byte_e { get; set; }
        public byte Byte_f { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (MaterialTextureChild)node.Value;

            Byte_1 = x.Byte_1;
            Byte_2 = x.Byte_2;
            DimensionsBitmask = (byte)x.DimensionsBitmask;
            Byte_4 = x.Byte_4;
            Byte_5 = x.Byte_5;
            Byte_6 = x.Byte_6;
            Byte_7 = x.Byte_7;
            Byte_c = x.Byte_c;
            Byte_d = x.Byte_d;
            Byte_e = x.Byte_e;
            Byte_f = x.Byte_f;
        }

        public override bool Equals(DbBlockItemStructure<MaterialTextureChild> other)
        {
            var x = (DbMaterialTextureChild)other;

            if (!base.Equals(x))
                return false;

            if (Byte_1 != x.Byte_1) return false;
            if (Byte_2 != x.Byte_2) return false;
            if (DimensionsBitmask != x.DimensionsBitmask) return false;
            if (Byte_4 != x.Byte_4) return false;
            if (Byte_5 != x.Byte_5) return false;
            if (Byte_6 != x.Byte_6) return false;
            if (Byte_7 != x.Byte_7) return false;
            if (Byte_c != x.Byte_c) return false;
            if (Byte_d != x.Byte_d) return false;
            if (Byte_e != x.Byte_e) return false;
            if (Byte_f != x.Byte_f) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMaterialTextureChild x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                Byte_1, Byte_2, DimensionsBitmask, Byte_4, Byte_5, Byte_6, Byte_7,
                Byte_c, Byte_d, Byte_e, Byte_f);
    }
}
