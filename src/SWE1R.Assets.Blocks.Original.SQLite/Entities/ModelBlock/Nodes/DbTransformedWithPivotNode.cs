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

        public float Transform_0_0 { get; set; }
        public float Transform_0_1 { get; set; }
        public float Transform_0_2 { get; set; }

        public float Transform_1_0 { get; set; }
        public float Transform_1_1 { get; set; }
        public float Transform_1_2 { get; set; }

        public float Transform_2_0 { get; set; }
        public float Transform_2_1 { get; set; }
        public float Transform_2_2 { get; set; }

        public float Transform_3_0 { get; set; }
        public float Transform_3_1 { get; set; }
        public float Transform_3_2 { get; set; }

        public float Pivot_X { get; set; }
        public float Pivot_Y { get; set; }
        public float Pivot_Z { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (TransformedWithPivotNode)node.Value;

            Transform_0_0 = x.Transform[0, 0];
            Transform_0_1 = x.Transform[0, 1];
            Transform_0_2 = x.Transform[0, 2];

            Transform_1_0 = x.Transform[1, 0];
            Transform_1_1 = x.Transform[1, 1];
            Transform_1_2 = x.Transform[1, 2];

            Transform_2_0 = x.Transform[2, 0];
            Transform_2_1 = x.Transform[2, 1];
            Transform_2_2 = x.Transform[2, 2];

            Transform_3_0 = x.Transform[3, 0];
            Transform_3_1 = x.Transform[3, 1];
            Transform_3_2 = x.Transform[3, 2];

            Pivot_X = x.Pivot.X;
            Pivot_Y = x.Pivot.Y;
            Pivot_Z = x.Pivot.Z;
        }

        public override bool Equals(DbBlockItemStructure<TransformedWithPivotNode> other)
        {
            var x = (DbTransformedWithPivotNode)other;

            if (!base.Equals(x))
                return false;

            if (Transform_0_0 != x.Transform_0_0) return false;
            if (Transform_0_1 != x.Transform_0_1) return false;
            if (Transform_0_2 != x.Transform_0_2) return false;

            if (Transform_1_0 != x.Transform_1_0) return false;
            if (Transform_1_1 != x.Transform_1_1) return false;
            if (Transform_1_2 != x.Transform_1_2) return false;

            if (Transform_2_0 != x.Transform_2_0) return false;
            if (Transform_2_1 != x.Transform_2_1) return false;
            if (Transform_2_2 != x.Transform_2_2) return false;

            if (Transform_3_0 != x.Transform_3_0) return false;
            if (Transform_3_1 != x.Transform_3_1) return false;
            if (Transform_3_2 != x.Transform_3_2) return false;

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
                Transform_0_0, Transform_0_1, Transform_0_2,
                Transform_1_0, Transform_1_1, Transform_1_2,
                Transform_2_0, Transform_2_1, Transform_2_2,
                Transform_3_0, Transform_3_1, Transform_3_2,
                Pivot_X, Pivot_Y, Pivot_Z);
    }
}
