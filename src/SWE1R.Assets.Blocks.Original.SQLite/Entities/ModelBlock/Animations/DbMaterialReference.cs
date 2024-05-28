// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims
{
    [Table($"{nameof(Model)}_{nameof(MaterialReference)}")]
    public class DbMaterialReference : DbBlockItemStructure<MaterialReference>
    {
        public int P_Material { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var dm = (MaterialReference)node.Value;

            P_Material = GetPropertyPointer(node, nameof(dm.Material));
        }

        public override bool Equals(DbBlockItemStructure<MaterialReference> other)
        {
            var _other = (DbMaterialReference)other;

            if (!base.Equals(_other))
                return false;

            if (P_Material != _other.P_Material) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMaterialReference)
                return this.Equals((DbMaterialReference)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), P_Material);
    }
}
