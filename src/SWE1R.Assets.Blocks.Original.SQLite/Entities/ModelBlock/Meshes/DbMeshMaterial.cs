// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [Table($"{nameof(Model)}_{nameof(MeshMaterial)}")]
    public class DbMeshMaterial : DbBlockItemStructure<MeshMaterial>
    {
        public int Bitmask { get; set; }
        public short Width_Unk_Dividend { get; set; }
        public short Height_Unk_Dividend { get; set; }
        public int P_Texture { get; set; }
        public int P_Properties { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (MeshMaterial)node.Value;

            Bitmask = x.Bitmask;
            Width_Unk_Dividend = x.Width_Unk_Dividend;
            Height_Unk_Dividend = x.Height_Unk_Dividend;
            P_Texture = GetPropertyPointer(node, nameof(x.Texture));
            P_Properties = GetPropertyPointer(node, nameof(x.Properties));
        }

        public override bool Equals(DbBlockItemStructure<MeshMaterial> other)
        {
            var _other = (DbMeshMaterial)other;

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
            if (obj is DbMeshMaterial)
                return this.Equals((DbMeshMaterial)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), 
                Bitmask, 
                Width_Unk_Dividend, Height_Unk_Dividend, 
                P_Texture, P_Properties);
    }
}
