// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    [Table("Model_HeaderAltN")]
    public class DbModelHeaderAltN : DbBlockItemStructure<FlaggedNodeOrGroup5066ChildReference>
    {
        public int Value { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var flaggedNodeOrInteger = (FlaggedNodeOrGroup5066ChildReference)node.Value;
            Value = flaggedNodeOrInteger.Group5066ChildReference?.Pointer ??
                 GetValuePosition(node.Graph, flaggedNodeOrInteger.FlaggedNode);
        }

        public override bool Equals(DbBlockItemStructure<FlaggedNodeOrGroup5066ChildReference> other)
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
