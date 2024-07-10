// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table($"{nameof(Model)}_{nameof(FlaggedNode)}_{nameof(TransformedNode)}")]
    public class DbTransformedNode : DbAbstractTransformedNode<TransformedNode>
    {
        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (TransformedNode)node.Value;
        }

        public override bool Equals(DbBlockItemStructure<TransformedNode> other)
        {
            var x = (DbTransformedNode)other;

            if (!base.Equals(x))
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbTransformedNode x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode());
    }
}
