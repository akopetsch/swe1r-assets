// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [Table("Model_Material")]
    public class DbMaterial : DbBlockItemStructure<Material>
    {
        public int Bitmask { get; set; }
        public short Width_Unk_Dividend { get; set; }
        public short Height_Unk_Dividend { get; set; }
        public int P_Texture { get; set; }
        public int P_Properties { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var m = (Material)node.Value;

            Bitmask = m.Bitmask;
            Width_Unk_Dividend = m.Width_Unk_Dividend;
            Height_Unk_Dividend = m.Height_Unk_Dividend;
            P_Texture = GetPropertyPointer(node, nameof(m.Texture));
            P_Properties = GetPropertyPointer(node, nameof(m.Properties));
        }

        public override bool Equals(DbBlockItemStructure<Material> other)
        {
            var _other = (DbMaterial)other;

            if (!base.Equals(_other))
                return false;

            if (Bitmask != _other.Bitmask) return false;
            if (Width_Unk_Dividend != _other.Width_Unk_Dividend) return false;
            if (Height_Unk_Dividend != _other.Height_Unk_Dividend) return false;
            if (P_Texture != _other.P_Texture) return false;
            if (P_Properties != _other.P_Properties) return false;

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
            HashCode.Combine(base.GetHashCode(), Bitmask, 
                Width_Unk_Dividend, Height_Unk_Dividend, 
                P_Texture, P_Properties);
    }
}
