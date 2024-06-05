﻿// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims
{
    [Table($"{nameof(Model)}_{nameof(MaterialReference)}")]
    public class DbMaterialReference : DbBlockItemStructure<MaterialReference>
    {
        #region Properties

        public int P_MeshMaterial { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (MaterialReference)node.Value;

            P_MeshMaterial = GetPropertyPointer(node, nameof(x.MeshMaterial));
        }

        public override bool Equals(DbBlockItemStructure<MaterialReference> other)
        {
            var x = (DbMaterialReference)other;

            if (!base.Equals(x))
                return false;

            if (P_MeshMaterial != x.P_MeshMaterial) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMaterialReference x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(), 
                P_MeshMaterial);
    }
}
