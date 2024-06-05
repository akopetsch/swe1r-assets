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
        #region Properties

        public int Bitmask { get; set; }
        public short Width_Unk_Dividend { get; set; }
        public short Height_Unk_Dividend { get; set; }
        public int P_MaterialTexture { get; set; }
        public int P_Properties { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (MeshMaterial)node.Value;

            Bitmask = x.Bitmask;
            Width_Unk_Dividend = x.Width_Unk_Dividend;
            Height_Unk_Dividend = x.Height_Unk_Dividend;
            P_MaterialTexture = GetPropertyPointer(node, nameof(x.MaterialTexture));
            P_Properties = GetPropertyPointer(node, nameof(x.Material));
        }

        public override bool Equals(DbBlockItemStructure<MeshMaterial> other)
        {
            var x = (DbMeshMaterial)other;

            if (!base.Equals(x))
                return false;

            if (Bitmask != x.Bitmask) return false;
            if (Width_Unk_Dividend != x.Width_Unk_Dividend) return false;
            if (Height_Unk_Dividend != x.Height_Unk_Dividend) return false;
            if (P_MaterialTexture != x.P_MaterialTexture) return false;
            if (P_Properties != x.P_Properties) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMeshMaterial x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(), 
                Bitmask, 
                Width_Unk_Dividend, Height_Unk_Dividend, 
                P_MaterialTexture, P_Properties);
    }
}
