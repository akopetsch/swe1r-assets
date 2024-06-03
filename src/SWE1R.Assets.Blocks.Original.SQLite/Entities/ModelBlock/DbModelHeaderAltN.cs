// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    [Table("Model_HeaderAltN")]
    public class DbModelHeaderAltN : DbBlockItemStructure<FlaggedNodeOrLodSelectorNodeChildReference>
    {
        public int Value { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var flaggedNodeOrInteger = (FlaggedNodeOrLodSelectorNodeChildReference)node.Value;
            Value = flaggedNodeOrInteger.LodSelectorNodeChildReference?.Pointer ??
                 GetValuePosition(node.Graph, flaggedNodeOrInteger.FlaggedNode);
        }

        public override bool Equals(DbBlockItemStructure<FlaggedNodeOrLodSelectorNodeChildReference> other)
        {
            var _other = (DbModelHeaderAltN)other;

            if (!base.Equals(_other))
                return false;

            if (Value != _other.Value) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbModelHeaderAltN)
                return this.Equals((DbModelHeaderAltN)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), Value);
    }
}
