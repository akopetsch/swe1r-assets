// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table($"{nameof(Model)}_{nameof(FlaggedNode)}_{nameof(SelectorNode)}")]
    public class DbSelectorNode : DbNode<SelectorNode>
    {
        public int SelectionValue { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var n = (SelectorNode)node.Value;

            SelectionValue = n.SelectionValue;
        }

        public override bool Equals(DbBlockItemStructure<SelectorNode> other)
        {
            var _other = (DbSelectorNode)other;

            if (!base.Equals(_other))
                return false;

            if (SelectionValue != _other.SelectionValue) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbSelectorNode)
                return this.Equals((DbSelectorNode)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), SelectionValue);
    }
}
