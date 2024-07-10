// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table($"{nameof(Model)}_{nameof(FlaggedNode)}_{nameof(TransformedWithPivotNode)}")]
    public class DbTransformedWithPivotNode : DbAbstractTransformedNode<TransformedWithPivotNode>
    {
        #region Properties

        public float Pivot_X { get; set; }
        public float Pivot_Y { get; set; }
        public float Pivot_Z { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (TransformedWithPivotNode)node.Value;

            Pivot_X = x.Pivot.X;
            Pivot_Y = x.Pivot.Y;
            Pivot_Z = x.Pivot.Z;
        }

        public override bool Equals(DbBlockItemStructure<TransformedWithPivotNode> other)
        {
            var x = (DbTransformedWithPivotNode)other;

            if (!base.Equals(x))
                return false;

            if (Pivot_X != x.Pivot_X) return false;
            if (Pivot_Y != x.Pivot_Y) return false;
            if (Pivot_Z != x.Pivot_Z) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbTransformedWithPivotNode x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                Pivot_X, Pivot_Y, Pivot_Z);
    }
}
