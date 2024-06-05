// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    [Table($"{nameof(Model)}_{nameof(FlaggedNodeOrLodSelectorNodeChildReference)}")]
    public class DbFlaggedNodeOrLodSelectorNodeChildReference : DbBlockItemStructure<FlaggedNodeOrLodSelectorNodeChildReference>
    {
        #region Properties

        public int I_Value { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (FlaggedNodeOrLodSelectorNodeChildReference)node.Value;

            I_Value = x.LodSelectorNodeChildReference?.Pointer ??
                 GetValuePosition(node.Graph, x.FlaggedNode);
        }

        public override bool Equals(DbBlockItemStructure<FlaggedNodeOrLodSelectorNodeChildReference> other)
        {
            var x = (DbFlaggedNodeOrLodSelectorNodeChildReference)other;

            if (!base.Equals(x))
                return false;

            if (I_Value != x.I_Value) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbFlaggedNodeOrLodSelectorNodeChildReference x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(), 
                I_Value);
    }
}
