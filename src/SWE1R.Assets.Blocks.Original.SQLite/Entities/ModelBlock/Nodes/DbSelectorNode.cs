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
        #region Properties

        public int SelectionValue { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (SelectorNode)node.Value;

            SelectionValue = x.SelectionValue;
        }

        public override bool Equals(DbBlockItemStructure<SelectorNode> other)
        {
            var x = (DbSelectorNode)other;

            if (!base.Equals(x))
                return false;

            if (SelectionValue != x.SelectionValue) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbSelectorNode x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(), 
                SelectionValue);
    }
}
