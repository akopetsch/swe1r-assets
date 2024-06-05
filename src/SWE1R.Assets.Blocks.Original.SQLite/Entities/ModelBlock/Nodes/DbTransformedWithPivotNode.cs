// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table($"{nameof(Model)}_{nameof(FlaggedNode)}_{nameof(TransformedWithPivotNode)}")]
    public class DbTransformedWithPivotNode : DbNode<TransformedWithPivotNode>
    {
        #region Properties

        public float Transform_11 { get; set; }
        public float Transform_12 { get; set; }
        public float Transform_13 { get; set; }
        public float Transform_14 { get; set; }
        public float Transform_21 { get; set; }
        public float Transform_22 { get; set; }
        public float Transform_23 { get; set; }
        public float Transform_24 { get; set; }
        public float Transform_31 { get; set; }
        public float Transform_32 { get; set; }
        public float Transform_33 { get; set; }
        public float Transform_34 { get; set; }
        public float Pivot_X { get; set; }
        public float Pivot_Y { get; set; }
        public float Pivot_Z { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (TransformedWithPivotNode)node.Value;

            Transform_11 = x.Transform[0, 0];
            Transform_12 = x.Transform[0, 1];
            Transform_13 = x.Transform[0, 2];
            Transform_14 = x.Transform[1, 3];
            Transform_21 = x.Transform[1, 0];
            Transform_22 = x.Transform[1, 1];
            Transform_23 = x.Transform[1, 2];
            Transform_24 = x.Transform[1, 3];
            Transform_31 = x.Transform[2, 0];
            Transform_32 = x.Transform[2, 1];
            Transform_33 = x.Transform[2, 2];
            Transform_34 = x.Transform[2, 3];
            Pivot_X = x.Pivot.X;
            Pivot_Y = x.Pivot.Y;
            Pivot_Z = x.Pivot.Z;
        }

        public override bool Equals(DbBlockItemStructure<TransformedWithPivotNode> other)
        {
            var x = (DbTransformedWithPivotNode)other;

            if (!base.Equals(x))
                return false;

            if (Transform_11 != x.Transform_11) return false;
            if (Transform_12 != x.Transform_12) return false;
            if (Transform_13 != x.Transform_13) return false;
            if (Transform_14 != x.Transform_14) return false;
            if (Transform_21 != x.Transform_21) return false;
            if (Transform_22 != x.Transform_22) return false;
            if (Transform_23 != x.Transform_23) return false;
            if (Transform_24 != x.Transform_24) return false;
            if (Transform_31 != x.Transform_31) return false;
            if (Transform_32 != x.Transform_32) return false;
            if (Transform_33 != x.Transform_33) return false;
            if (Transform_34 != x.Transform_34) return false;
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
                Transform_11, Transform_12, Transform_13, Transform_14,
                Transform_21, Transform_22, Transform_23, Transform_24,
                Transform_31, Transform_32, Transform_33, Transform_34,
                Pivot_X, Pivot_Y, Pivot_Z);
    }
}
